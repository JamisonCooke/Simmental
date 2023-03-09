using Simmental.Interfaces;
using Simmental.UI.WinForm.Embedded;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simmental.UI.WinForm.Render;

public class RenderAnimation
{

    private IAnimation Animation { get; }

    public RenderAnimation(IAnimation animation)
    {
        Animation = animation;
    }

    // Rerenders the spot where the character was but without the character anymore
    public void AnimationCancel(IGame game, PictureBox pictureBox)
    {
        if (Animation.CacheObject == null) return;
        var wayfinderBuffer = Animation.CacheObject as Bitmap;
        var renderHelper = new RenderHelper();
        Position p = new Position(Animation.StartPosition);
        Rectangle offsetRectangle = renderHelper.GetTileRect(game.Wayfinder, p.i - 1, p.j - 1);       // Top left when we're rendering
        
        Graphics graphics = pictureBox.CreateGraphics();
        graphics.DrawImage(wayfinderBuffer, offsetRectangle.X, offsetRectangle.Y);
        graphics.Dispose();
        Animation.CacheObject.Dispose();
        Animation.CacheObject = null;
    }


    public void AnimationMove(IGame game, PictureBox pictureBox)
    {
        DateTime now = DateTime.Now;
        var renderHelper = new RenderHelper();
        var wayfinder = game.Wayfinder;
        if (!pictureBox.Visible || game.Player.Animations is null) return;

        int tw = game.Wayfinder.TilePixelWidth;
        int th = game.Wayfinder.TilePixelHeight;

        Graphics graphics = pictureBox.CreateGraphics();

        // Draw the base square/tile to clear whatever was there in the past.
        Position p = new Position(Animation.StartPosition);

        // We need to render the entire screen minus the player to the extenal cache graphic
        Bitmap wayfinderBuffer;
        Rectangle offsetRectangle = renderHelper.GetTileRect(game.Wayfinder, p.i - 1, p.j - 1);       // Top left when we're rendering

        if (Animation.CacheObject == null)
        {
            wayfinderBuffer = new Bitmap(wayfinder.TilePixelWidth * 3, wayfinder.TilePixelHeight * 3);
            var wfGraphics = Graphics.FromImage(wayfinderBuffer);
            Animation.CacheObject = wayfinderBuffer;        // Remember it for nex ttime
            (wayfinder.XOffset, wayfinder.YOffset) = (-offsetRectangle.Left, -offsetRectangle.Top);
            var renderWayfinder = new RenderWayfinder(game.Wayfinder, wfGraphics, p, p.i - 1, p.i + 2, p.j - 1, p.j + 2);
            (wayfinder.XOffset, wayfinder.YOffset) = (0, 0);
        }
        wayfinderBuffer = Animation.CacheObject as Bitmap;

        Bitmap offscreenBitmap = new Bitmap(tw * 3, th * 3);
        Graphics offscreenGraphics = Graphics.FromImage(offscreenBitmap);
        offscreenGraphics.DrawImage(wayfinderBuffer, 0, 0);

        // Add player to wayfinderGraphics
        int slideNo = Animation.GetSlideNo(now);
        double percentDone = Animation.PercentComplete(now);

        Rectangle startRectangle = renderHelper.GetTileRect(game.Wayfinder, Animation.StartPosition.i, Animation.StartPosition.j);
        Rectangle endRectangle = renderHelper.GetTileRect(game.Wayfinder, Animation.EndPosition.i, Animation.EndPosition.j);
        int dx = (int)((endRectangle.X - startRectangle.X) * percentDone);
        int dy = (int)((endRectangle.Y - startRectangle.Y) * percentDone);

        TileManager.Tiles(Animation.GraphicName).BitBltTile(offscreenGraphics, new Rectangle(tw + dx, th + dy, tw, th), slideNo, game.Player.IsLookingLeft);
        graphics.DrawImage(offscreenBitmap, offsetRectangle.X, offsetRectangle.Y);

        offscreenGraphics.Dispose();
        graphics.Dispose();
    }
    public void AnimationStill(IGame game, PictureBox pictureBox)
    {
        DateTime now = DateTime.Now;
        var renderHelper = new RenderHelper();
        if (!pictureBox.Visible || game.Player.Animations is null) return;

        int tw = game.Wayfinder.TilePixelWidth;
        int th = game.Wayfinder.TilePixelHeight;

        Graphics graphics = pictureBox.CreateGraphics();

        // Draw the base square/tile to clear whatever was there in the past.
        Position p = new Position(game.Player.Position);
        if (Animation.StartPosition is not null)
            p = new Position(Animation.StartPosition);

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

        int slideNo = Animation.GetSlideNo(now);
        double percentDone = Animation.PercentComplete(now);


        int dx = 0;
        int dy = 0;

        Rectangle rectangle = renderHelper.GetTileRect(game.Wayfinder, p.i - 1, p.j - 1);

        if (Animation.StartPosition is not null && Animation.EndPosition is not null)
        {
            Rectangle startRectangle = renderHelper.GetTileRect(game.Wayfinder, Animation.StartPosition.i, Animation.StartPosition.j);
            Rectangle endRectangle = renderHelper.GetTileRect(game.Wayfinder, Animation.EndPosition.i, Animation.EndPosition.j);
            dx = (int)((endRectangle.X - startRectangle.X) * percentDone);
            dy = (int)((endRectangle.Y - startRectangle.Y) * percentDone);
            // if it has a start position, then use the cameraI/J from the animation
            rectangle = renderHelper.GetTileRect(game.Wayfinder, p.i - 1, p.j - 1, Animation.CameraI, Animation.CameraJ);
        }
        TileManager.Tiles(Animation.GraphicName).BitBltTile(offscreenGraphics, new Rectangle(tw + dx, th + dy, tw, th), slideNo, game.Player.IsLookingLeft);
        graphics.DrawImage(offscreenBitmap, rectangle.X, rectangle.Y);

        offscreenGraphics.Dispose();
        graphics.Dispose();
    }

}
