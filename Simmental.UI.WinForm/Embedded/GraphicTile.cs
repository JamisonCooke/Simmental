using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI.WinForm.Embedded
{
    public class GraphicTile
    {
        public GraphicNameEnum GraphicName { get;}
        public int TileWidth { get; }
        public int TileHeight { get; }
        public Bitmap Image { get; }
        public int TilesPerRow { get; }

        public GraphicTile(GraphicNameEnum graphicName, int tileWidth = 32, int tileHeight = 32)
        {
            GraphicName = graphicName;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Image = LoadGraphic();
            TilesPerRow = Image.Width / tileWidth;
        }

        public void BitBltTile(Graphics graphics, Rectangle destRect, int tileNo, double zoom = 1.0)
        {
            // TileNo in a 4 x 4 grapic would be laid out:
            //  0 | 1 | 2 | 3  4, 5, 6, 7, 8, 9, 10, 
            //  --+---+---+---
            //  4 | 5 | 6 | 7
            //  --+---+---+---
            //  8 | 9 | 10| 11
            //  --+---+---+---
            //  12| 13| 14| 15
            //  --+---+---+---
            int x = (tileNo % TilesPerRow) * TileWidth;
            int y = (tileNo / TilesPerRow) * TileWidth;
            Rectangle srcRect = new Rectangle(x, y, TileWidth, TileHeight);
            Rectangle targetRect = destRect;
            if (zoom != 1.0)
            {
                int zoomWidth = (int)(destRect.Width * zoom);
                int zoomHeight = (int)(destRect.Height * zoom);
                targetRect = new Rectangle(destRect.X - (zoomWidth - destRect.Width) / 2, destRect.Y - (zoomHeight - destRect.Height) / 2, zoomWidth, zoomHeight);
            }

            graphics.DrawImage(Image, targetRect, srcRect, GraphicsUnit.Pixel);
        }

        private Bitmap LoadGraphic()
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            string resourceName = $"Simmental.UI.WinForm.Resources.{GraphicName}.png";
            Stream myStream = myAssembly.GetManifestResourceStream(resourceName);
            Bitmap bmp = new Bitmap(myStream);

            return bmp;

        }
    }
}
