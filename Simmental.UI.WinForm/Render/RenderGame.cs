using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simmental.Interfaces;
using Simmental.UI.WinForm.Embedded;

namespace Simmental.UI.WinForm.Render
{
    public class RenderGame
    {
        private static Object _lockObject = new Object();
        private static System.Threading.Timer _animationTimer;
        private static Image _screenImage;

        public void Render(IGame game, Graphics pictureBoxGraphics, PictureBox pictureBox)
        {
            if (_animationTimer == null)
                ConfigureAnimationTimer(game, pictureBox);

            lock (_lockObject)
            {
                // See if we need to reinitialize screen image
                if (_screenImage == null || _screenImage.Width != pictureBox.Width || _screenImage.Height != pictureBox.Height)
                {
                    if (_screenImage != null) _screenImage.Dispose();
                    _screenImage = new Bitmap(pictureBox.Width, pictureBox.Height);
                }

                Graphics graphicsScreenImage = Graphics.FromImage(_screenImage);

                var renderWayfinder = new RenderWayfinder(game.Wayfinder, graphicsScreenImage, game.Player.Position);

                // Render the guys
                RenderCharacter renderCharacter = new RenderCharacter();
                renderCharacter.Render(game.Wayfinder, game.Player, graphicsScreenImage);

                foreach (ICharacter npc in game.NPC)
                {
                    if (game.Wayfinder.IsVisible(game.Player.Position, npc.Position))
                        renderCharacter.Render(game.Wayfinder, npc, graphicsScreenImage);
                }

                if (game.Designer.HighlightRange)
                    HighlightSelectedRanged(game, graphicsScreenImage);

                pictureBoxGraphics.DrawImage(_screenImage, 0, 0);

            }
        }

        private void ConfigureAnimationTimer(IGame game, PictureBox pictureBox)
        {
            var gameGraphicsTuple = new Tuple<IGame, PictureBox>(game, pictureBox);
            _animationTimer = new System.Threading.Timer(AnimationTimerCallback, gameGraphicsTuple, 1, 100);
        }

        


        private void AnimationTimerCallback(object state)
        {
            (IGame game, PictureBox pictureBox) = (Tuple<IGame, PictureBox>)state;
            var expired = game.Player.Animations.ExpireAnimations();
            foreach(IAnimation expiredAnimation in expired)
            {
                var dispose = new RenderAnimation(expiredAnimation);
                dispose.AnimationCancel(game, pictureBox);
            }
            IAnimation animation = game.Player.Animations.Current;
            lock (_lockObject)
            {
                try
                {

                    if (animation.StartPosition is not null)
                    {
                        // Moving
                        RenderAnimation renderAnimation = new(animation);
                        renderAnimation.AnimationMove(game, pictureBox);
                        
                    }
                    else
                    {
                        // Still
                        RenderAnimation renderAnimation = new(animation);
                        renderAnimation.AnimationStill(game, pictureBox);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }


       

        public void RenderTile(IGame game, Graphics graphics, int i, int j)
        {
            var renderWayfinder = new RenderWayfinder(game.Wayfinder, graphics, game.Player.Position, i, j);
        
        }

        /// <summary>
        /// Draw a light transparent box over the designed range
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphics"></param>
        private static void HighlightSelectedRanged(IGame game, Graphics graphics)
        {
            RenderHelper renderHelper = new RenderHelper();
            
            // Draw box around the seleted squares (if any)
            Rectangle r1 = renderHelper.GetTileRect(game.Wayfinder, game.Designer.TopLeft.i, game.Designer.TopLeft.j);
            Rectangle r2 = renderHelper.GetTileRect(game.Wayfinder, game.Designer.BottomRight.i, game.Designer.BottomRight.j);

            // Make second rectangle encompass both rectangles
            int x, width;
            if (r1.X < r2.X)
            {
                x = r1.X;
                width = r2.X + r2.Width - r1.X;
            }
            else
            {
                x = r2.X;
                width = r1.X + r1.Width - r2.X;
            }

            int y, height;
            if (r1.Y < r2.Y)
            {
                y = r1.Y;
                height = r2.Y + r2.Height - r1.Y;
            }
            else
            {
                y = r2.Y;
                height = r1.Y + r1.Height - r2.Y;
            }

            var rect = new Rectangle(x, y, width, height);


            using (var brush = new SolidBrush(Color.FromArgb(20, 70, 70, 70)))
            {
                graphics.FillRectangle(brush, rect);
                graphics.DrawRectangle(Pens.Red, rect);
            }
        }

        /// <summary>
        /// This method copies the old visible part of the screen over from scrolling, then calls DrawTile to 
        /// Render the newly exposed part of the screen
        /// </summary>
        /// <param name="game"></param>
        /// <param name="oldCameraI">Prior I value- WayFinder.CameraI has the new value</param>
        /// <param name="oldCameraJ">Prior J value- WayFinder.CameraJ has the new value</param>
        /// <param name="gamePictureBox"></param>
        public void RenderScroll(IGame game, int oldCameraI, int oldCameraJ, PictureBox gamePictureBox)
        {

            

            Graphics screenGraphics = gamePictureBox.CreateGraphics();

            Bitmap offscreenBitmap = new Bitmap(gamePictureBox.Width, gamePictureBox.Height);
            Graphics offscreenGraphics = Graphics.FromImage(offscreenBitmap);

            // Copy the reusuable part of the game screen to the offscreen value
            int tw = game.Wayfinder.TilePixelWidth;
            int th = game.Wayfinder.TilePixelHeight;

            // Copy the image from the screenGraphics to the offscreenGraphics
            offscreenGraphics.DrawImage(_screenImage, (oldCameraI - game.Wayfinder.CameraI) * tw, (oldCameraJ - game.Wayfinder.CameraJ) * th);

            // var renderWayfinder = new RenderWayfinder(game.Wayfinder, wfGraphics, p, p.i - 1, p.i + 2, p.j - 1, p.j + 2);
            if (oldCameraI < game.Wayfinder.CameraI)
            {
                int leftTile = oldCameraI + game.Wayfinder.CameraWidth -1;
                int rightTile = game.Wayfinder.CameraI + game.Wayfinder.CameraWidth +1;
                new RenderWayfinder(game.Wayfinder, offscreenGraphics, game.Player.Position, leftTile, rightTile + 1, game.Wayfinder.CameraJ, game.Wayfinder.CameraJ + game.Wayfinder.CameraHeight);
            }

            if (oldCameraI > game.Wayfinder.CameraI)
            {
                int leftTile = game.Wayfinder.CameraI;
                int rightTile = oldCameraI;
                new RenderWayfinder(game.Wayfinder, offscreenGraphics, game.Player.Position, leftTile, rightTile + 1, game.Wayfinder.CameraJ, game.Wayfinder.CameraJ + game.Wayfinder.CameraHeight);
            }

            if (oldCameraJ < game.Wayfinder.CameraJ)
            {
                int topTile = oldCameraJ + game.Wayfinder.CameraHeight - 1;
                int bottomTile = game.Wayfinder.CameraJ + game.Wayfinder.CameraHeight + 1;
                new RenderWayfinder(game.Wayfinder, offscreenGraphics, game.Player.Position, game.Wayfinder.CameraI, game.Wayfinder.CameraI + game.Wayfinder.CameraWidth, topTile, bottomTile + 1);
            }

            if (oldCameraJ > game.Wayfinder.CameraJ)
            {
                int topTile = game.Wayfinder.CameraJ;
                int bottomTile = oldCameraJ;
                new RenderWayfinder(game.Wayfinder, offscreenGraphics, game.Player.Position, game.Wayfinder.CameraI, game.Wayfinder.CameraI + game.Wayfinder.CameraWidth, topTile, bottomTile + 1);
            }

            // Copy the offscreen bitmap to the screen
            _screenImage.Dispose();
            _screenImage = offscreenBitmap;

            screenGraphics.DrawImage(offscreenBitmap, 0, 0);

            offscreenGraphics.Dispose();
            screenGraphics.Dispose();

        }
    }
}
