using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces.WinForm.Render
{
    public class RenderCharacter
    {

        public void Render(IWayfinder wayfinder, ICharacter character, Graphics graphics)
        {
            var renderHelper = new RenderHelper();
            Rectangle rectangle = renderHelper.GetTileRect(wayfinder, character.Position.i, character.Position.j);
            Brush color = Brushes.Black;

            switch(character.Race)
            {
                case RaceEnum.Human:
                    color = Brushes.Blue;
                    break;

                case RaceEnum.Orc:
                    color = Brushes.Gray;
                    break;
            }


            graphics.FillRectangle(color, rectangle);

        }

    }
}
