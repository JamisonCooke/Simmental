using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.Interfaces;

namespace Simmental.UI.WinForm.Render
{
    public class RenderWayfinder 
    {

        public RenderWayfinder(IWayfinder wayfinder, Graphics g, Position playerPosition)
        {
            Render(wayfinder, g, playerPosition);
        }
        public RenderWayfinder(IWayfinder wayfinder, Graphics g, Position playerPosition, int leftTile, int rightTile, int topTile, int bottomTile)
        {
            Render(wayfinder, g, playerPosition, leftTile, rightTile, topTile, bottomTile);
        }

        public RenderWayfinder(IWayfinder wayfinder, Graphics g, Position playerPosition, int i, int j)
        {
            RenderTile(wayfinder, g, playerPosition, i, j);  
        }

        private static void Render(IWayfinder wayfinder, Graphics g, Position playerPosition)
        {
            var renderTile = new RenderTile(g);
            var renderHelper = new RenderHelper();

            int leftTile = wayfinder.CameraI;
            if (wayfinder.XOffset != 0) 
                leftTile -= ((wayfinder.XOffset + wayfinder.TilePixelWidth - 1) / wayfinder.TilePixelWidth);

            int rightTile = wayfinder.CameraWidth + leftTile;
            if ((wayfinder.XOffset % wayfinder.TilePixelWidth) != 0)
                rightTile++;
            int topTile = wayfinder.CameraJ;
            if (wayfinder.YOffset != 0)
                topTile -= ((wayfinder.YOffset + wayfinder.TilePixelHeight - 1) / wayfinder.TilePixelHeight);
            int bottomTile = wayfinder.CameraHeight + wayfinder.CameraJ;
            if ((wayfinder.YOffset % wayfinder.TilePixelHeight) != 0)
                bottomTile++;

            Render(wayfinder, g, playerPosition, leftTile, rightTile, topTile, bottomTile);
        }

        private static void Render(IWayfinder wayfinder, Graphics g, Position playerPosition, int leftTile, int rightTile, int topTile, int bottomTile)
        {
            var renderTile = new RenderTile(g);
            var renderHelper = new RenderHelper();

            for (int i = leftTile; i < rightTile; i++)
            {
                for (int j = topTile; j < bottomTile; j++)
                {
                    RenderSingleTile(wayfinder, playerPosition, renderTile, renderHelper, i, j);
                }

            }
        }


        public static void RenderTile(IWayfinder wayfinder, Graphics g, Position playerPosition, int i, int j)
        {
            var renderTile = new RenderTile(g);
            var renderHelper = new RenderHelper();

            RenderSingleTile(wayfinder, playerPosition, renderTile, renderHelper, i, j);
        }

        public static void RenderSingleTile(IWayfinder wayfinder, Position playerPosition, RenderTile renderTile, RenderHelper renderHelper, int i, int j)
        {
            ITile tile = wayfinder[i, j];
            Rectangle rectangle = renderHelper.GetTileRect(wayfinder, i, j);
            bool isVisible = wayfinder.IsVisible(new Position(i, j), playerPosition);
            if (tile != null)
            {
                if (!tile.Seen && isVisible)
                    tile.Seen = true;       // First time we've seen this tile

                // If we can't see this tile, don't draw anything!
                if (!tile.Seen)
                    tile = null;
            }

            renderTile.Render(tile, rectangle, isVisible);
        }
    }
}
