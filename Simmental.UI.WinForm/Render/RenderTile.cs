using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.UI.WinForm.Render
{
    public class RenderTile
    {
        private Graphics _graphics;
        public RenderTile(Graphics g)
        {
            _graphics = g;

        }
        public void Render(ITile tile, Rectangle rectangle, bool isVisible)
        {

            if (tile == null)
            {
                // Draw a dark gray fill
                _graphics.FillRectangle(new SolidBrush(Color.DarkGray), rectangle);
                return;
            }

            // We can draw our tile based on what it looks like into the rectanle
            Pen pen = new Pen(GetColorForTile(tile.TileType));

            _graphics.DrawRectangle(pen, rectangle);
            _graphics.FillRectangle(new SolidBrush(GetColorForTile(tile.TileType)), rectangle);
            if (isVisible && tile.LightLevel > 0)
            {
                foreach (var item in tile.Inventory.Items)
                {
                    _graphics.DrawString(item.Name.Substring(0, 1), SystemFonts.DefaultFont, Brushes.Blue, rectangle.X, rectangle.Y);
                }
            }

            if (isVisible)
            {
                const int brightLevel = 40;     // when tile.LightLevel = 100
                const int darkLevel = 5;        // when tile.LightLevel = 0
                int alpha = (int)((brightLevel - darkLevel) * (tile.LightLevel/100.0) + darkLevel);
                using (var brush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 0)))
                    _graphics.FillRectangle(brush, rectangle);

            }

        }

        private Color GetColorForTile(TileEnum tileType)
        {
            switch (tileType)
            {
                case TileEnum.Grass: return Color.Green;
                case TileEnum.Stone: return Color.Gray;
                case TileEnum.Wall: return Color.DarkRed;
                case TileEnum.Water: return Color.Aquamarine;
                case TileEnum.Wood: return Color.Brown;
            }
            return Color.Black;
        }

    }
}
