using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.PropertyGridInternal;
using Simmental.Interfaces;
using Simmental.UI.WinForm.Embedded;

namespace Simmental.UI.WinForm.Render
{
    public class RenderTile
    {
        private Graphics _graphics;
        public RenderTile(Graphics g)
        {
            _graphics = g;

        }

        public void Render(IGame game, ITile tile, Rectangle rectangle, bool isVisible, bool includeNPCs)
        {
            if (tile == null) return;
            Render(tile, rectangle, isVisible);
            if (!includeNPCs)
                return;

            RenderCharacter renderCharacter = new RenderCharacter();
            renderCharacter.Render(game.Wayfinder, game.Player, _graphics);

            foreach (ICharacter npc in tile.NPCs)
            {
                if (game.Wayfinder.IsVisible(game.Player.Position, npc.Position))
                    renderCharacter.Render(game.Wayfinder, npc, _graphics);
            }

        }


        public void Render(ITile tile, Rectangle rectangle, bool isVisible)
        {
            if (tile == null)
            {
                // Draw a dark gray fill
                _graphics.FillRectangle(new SolidBrush(Color.DarkGray), rectangle);
                return;
            }

            if (tile.TileType == TileEnum.Grass)
            {
                DrawResourceTile(GraphicNameEnum.txGround, rectangle, 0 + (tile.GetHashCode() % 32));
            }
            else if (tile.TileType == TileEnum.Stone)
            {
                // DrawResourceTile(GraphicNameEnum.txGround, rectangle, 32 + (tile.GetHashCode() % 30));
                DrawResourceTile(GraphicNameEnum.txStone, rectangle, 27);
            }
            else if (tile.TileType == TileEnum.Wall)
            {
                DrawResourceTile(GraphicNameEnum.txWall, rectangle, 161);
            }
            else if (tile.TileType == TileEnum.Wood)
            {
                DrawResourceTile(GraphicNameEnum.txWall, rectangle, 145);
            }
            else
            {
                DrawSolidColorTile(tile, rectangle);
            }

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
                int alpha = (int)((brightLevel - darkLevel) * (tile.LightLevel / 100.0) + darkLevel);
                using (var brush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 0)))
                    _graphics.FillRectangle(brush, rectangle);

            }

        }

        private void DrawResourceTile(GraphicNameEnum graphicName, Rectangle rectangle, int tileNo)
        {
            TileManager.Tiles(graphicName).BitBltTile(_graphics, rectangle, tileNo);

        }

        private void DrawSolidColorTile(ITile tile, Rectangle rectangle)
        {
            // We can draw our tile based on what it looks like into the rectanle
            Pen pen = new Pen(GetColorForTile(tile.TileType));

            //_graphics.DrawRectangle(pen, rectangle);
            _graphics.FillRectangle(new SolidBrush(GetColorForTile(tile.TileType)), rectangle);
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
