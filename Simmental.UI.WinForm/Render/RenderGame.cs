using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI.WinForm.Render
{
    public class RenderGame
    {
        public void Render(IGame game, Graphics graphics)
        {
            var renderWayfinder = new RenderWayfinder(game.Wayfinder, graphics, game.Player.Position);

            // Render the guys
            RenderCharacter renderCharacter = new RenderCharacter();
            renderCharacter.Render(game.Wayfinder, game.Player, graphics);

            foreach (ICharacter npc in game.NPC)
            {
                if (game.Wayfinder.IsVisible(game.Player.Position, npc.Position, 10))
                    renderCharacter.Render(game.Wayfinder, npc, graphics);
            }

            HighlightSelectedRanged(game, graphics);
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
                graphics.FillRectangle(brush, rect);
        }
    }
}
