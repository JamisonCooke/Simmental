﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.Interfaces;
using Simmental.UI.WinForm.Embedded;

namespace Simmental.UI.WinForm.Render
{
    public class RenderCharacter
    {

        public void Render(IWayfinder wayfinder, ICharacter character, Graphics graphics)
        {
            if (character.Animations?.Current != null) return;

            var renderHelper = new RenderHelper();
            Rectangle rectangle = renderHelper.GetTileRect(wayfinder, character.Position.i, character.Position.j);
            Brush color = Brushes.Black;

            switch(character.Race)
            {
                case RaceEnum.Human:
                    TileManager.Tiles(GraphicNameEnum.gregRun).BitBltTile(graphics, rectangle, 0, character.IsLookingLeft);
                    return;

                case RaceEnum.Orc:
                    color = Brushes.Gray;
                    break;
            }


            graphics.FillRectangle(color, rectangle);

        }

    }
}
