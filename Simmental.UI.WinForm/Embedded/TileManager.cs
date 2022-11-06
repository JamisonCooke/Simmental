using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

            // Load up odd sizes first
            _graphicTiles[(int)GraphicNameEnum.gregRun] = new GraphicTile(GraphicNameEnum.gregRun, 80, 80);
            _graphicTiles[(int)GraphicNameEnum.gregAttack] = new GraphicTile(GraphicNameEnum.gregAttack, 96, 80);
            _graphicTiles[(int)GraphicNameEnum.gregDead] = new GraphicTile(GraphicNameEnum.gregDead, 64, 80);
            _graphicTiles[(int)GraphicNameEnum.gregIdle] = new GraphicTile(GraphicNameEnum.gregIdle, 64, 80);
            _graphicTiles[(int)GraphicNameEnum.gregJump] = new GraphicTile(GraphicNameEnum.gregJump, 64, 80);

            // Load up every enum not loaded yet
            foreach (GraphicNameEnum graphicName in (GraphicNameEnum[]) Enum.GetValues(typeof(GraphicNameEnum)))
            {
                if (graphicName == GraphicNameEnum.Undefined) continue;
                int index = (int)graphicName;
                if (_graphicTiles[index] != null) continue;

                _graphicTiles[index] = new GraphicTile(graphicName);
            }

        }

        public static GraphicTile Tiles(GraphicNameEnum graphicName) => _graphicTiles[(int)graphicName];

    }

}
