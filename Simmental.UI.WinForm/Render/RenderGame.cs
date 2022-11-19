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

        public void Render(IGame game, Graphics graphics, PictureBox pictureBox)
        {
            if (_animationTimer == null)
                ConfigureAnimationTimer(game, pictureBox);

            lock (_lockObject)
            {
                var renderWayfinder = new RenderWayfinder(game.Wayfinder, graphics, game.Player.Position);

                // Render the guys
                RenderCharacter renderCharacter = new RenderCharacter();
                renderCharacter.Render(game.Wayfinder, game.Player, graphics);

                foreach (ICharacter npc in game.NPC)
                {
                    if (game.Wayfinder.IsVisible(game.Player.Position, npc.Position))
                        renderCharacter.Render(game.Wayfinder, npc, graphics);
                }

                if (game.Designer.HighlightRange)
                    HighlightSelectedRanged(game, graphics);
            }
        }

        private void ConfigureAnimationTimer(IGame game, PictureBox pictureBox)
        {
            var gameGraphicsTuple = new Tuple<IGame, PictureBox>(game, pictureBox);
            _animationTimer = new System.Threading.Timer(AnimationTimerCallback, gameGraphicsTuple, 1, 100);
        }

        private void AnimationTimerCallback(object state) 
        {
            try
            {

                lock (_lockObject)
                {
                    DateTime now = DateTime.Now;
                    var renderHelper = new RenderHelper();
                    (IGame game, PictureBox pictureBox) = (Tuple<IGame, PictureBox>)state;
                    if (!pictureBox.Visible || game.Player.Animations is null) return;

                    int tw = game.Wayfinder.TilePixelWidth;
                    int th = game.Wayfinder.TilePixelHeight;

                    Graphics graphics = pictureBox.CreateGraphics();

                    IAnimation animation = game.Player.Animations.Current;

                    // Draw the base square/tile to clear whatever was there in the past.
                    Position p = new Position(game.Player.Position);
                    if (animation.StartPosition is not null)
                        p = new Position(animation.StartPosition);

                    Rectangle offscreenRectangle = new Rectangle(0, 0, tw * 3, th * 3);
                    Bitmap offscreenBitmap = new Bitmap(offscreenRectangle.Width, offscreenRectangle.Height);
                    Graphics offscreenGraphics = Graphics.FromImage(offscreenBitmap);
                    var renderTile = new RenderTile(offscreenGraphics);

                    renderTile.Render(game, game.Wayfinder[p.i - 1, p.j - 1], new Rectangle(tw * 0, 0, tw, th), true, true);
                    renderTile.Render(game, game.Wayfinder[p.i + 0, p.j - 1], new Rectangle(tw * 1, 0, tw, th), true, true);
                    renderTile.Render(game, game.Wayfinder[p.i + 1, p.j - 1], new Rectangle(tw * 2, 0, tw, th), true, true);

                    renderTile.Render(game, game.Wayfinder[p.i - 1, p.j], new Rectangle(tw * 0, th, tw, th), true, true);
                    renderTile.Render(game, game.Wayfinder[p.i + 0, p.j], new Rectangle(tw * 1, th, tw, th), true, false);
                    renderTile.Render(game, game.Wayfinder[p.i + 1, p.j], new Rectangle(tw * 2, th, tw, th), true, true);

                    renderTile.Render(game, game.Wayfinder[p.i - 1, p.j + 1], new Rectangle(tw * 0, th * 2, tw, th), true, true);
                    renderTile.Render(game, game.Wayfinder[p.i + 0, p.j + 1], new Rectangle(tw * 1, th * 2, tw, th), true, true);
                    renderTile.Render(game, game.Wayfinder[p.i + 1, p.j + 1], new Rectangle(tw * 2, th * 2, tw, th), true, true);

                    int slideNo = animation.GetSlideNo(now);
                    double percentDone = animation.PercentComplete(now);


                    int dx = 0;
                    int dy = 0;

                    Rectangle rectangle = renderHelper.GetTileRect(game.Wayfinder, p.i - 1, p.j - 1);

                    if (animation.StartPosition is not null && animation.EndPosition is not null)
                    {
                        Rectangle startRectangle = renderHelper.GetTileRect(game.Wayfinder, animation.StartPosition.i, animation.StartPosition.j);
                        Rectangle endRectangle = renderHelper.GetTileRect(game.Wayfinder, animation.EndPosition.i, animation.EndPosition.j);
                        dx = (int)((endRectangle.X - startRectangle.X) * percentDone);
                        dy = (int)((endRectangle.Y - startRectangle.Y) * percentDone);
                        // if it has a start position, then use the cameraI/J from the animation
                        rectangle = renderHelper.GetTileRect(game.Wayfinder, p.i - 1, p.j - 1, animation.CameraI, animation.CameraJ);
                    }
                    TileManager.Tiles(animation.GraphicName).BitBltTile(offscreenGraphics, new Rectangle(tw + dx, th + dy, tw, th), slideNo, 1);
                    graphics.DrawImage(offscreenBitmap, rectangle.X, rectangle.Y);

                    offscreenGraphics.Dispose();
                    graphics.Dispose();
                }
            }
            catch(Exception ex)
            {
                // boom
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
    }
}
