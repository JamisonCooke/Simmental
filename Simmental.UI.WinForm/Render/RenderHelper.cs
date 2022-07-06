using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI.WinForm.Render
{
    public class RenderHelper : IRenderHelper
    {
        public Rectangle GetTileRect(IWayfinder wayfinder, int i, int j)
        {
            int tileWidth = wayfinder.TilePixelWidth;
            int tileHeight = wayfinder.TilePixelHeight;

                Rectangle rectangle = new Rectangle((i - wayfinder.CameraI) * tileWidth, (j - wayfinder.CameraJ) * tileHeight, tileWidth, tileHeight);
            return rectangle;
        }
        public bool GetTileIndex(IWayfinder wayfinder, int x, int y, out int i, out int j)
        {
            i = (x / wayfinder.TilePixelWidth) + wayfinder.CameraI;
            j = (y / wayfinder.TilePixelHeight) + wayfinder.CameraJ;


            return (i >= 0) && (j >= 0) && (i < wayfinder.Width) && (j < wayfinder.Height);
        }
    }
}
