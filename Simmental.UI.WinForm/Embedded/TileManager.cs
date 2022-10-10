using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI.WinForm.Embedded
{
    public static class TileManager
    {
        static GraphicTile[] _graphicTiles;
        static TileManager()
        {
            GraphicNameEnum maxValue = Enum.GetValues(typeof(GraphicNameEnum)).Cast<GraphicNameEnum>().Last();

            _graphicTiles = new GraphicTile[(int)maxValue + 1];

            // Load up every enum
            foreach(GraphicNameEnum graphicName in (GraphicNameEnum[]) Enum.GetValues(typeof(GraphicNameEnum)))
            {
                if (graphicName == GraphicNameEnum.Undefined) continue;

                _graphicTiles[(int)graphicName] = new GraphicTile(graphicName);
            }

            //_graphicTiles[(int)GraphicNameEnum.txGround] = new GraphicTile(GraphicNameEnum.txGround, 32, 32);

            // When you add another resource file, add it the array this way:
            //_graphicTiles[(int)GraphicNameEnum.txGround] = new GraphicTile(GraphicNameEnum.txGround, 32, 32);

        }

        public static GraphicTile Tiles(GraphicNameEnum graphicName) => _graphicTiles[(int)graphicName];

    }

}
