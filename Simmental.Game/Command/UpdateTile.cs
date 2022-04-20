using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Command
{
    public class UpdateTile : CommandBase
    {
        private Position _position;
        private TileEnum _tileType;
        private TileAttributeEnum _tileAttribute;

        public UpdateTile(Position position, TileEnum tileType, TileAttributeEnum tileAttribute)
        {
            (_position, _tileType, _tileAttribute) = (position, tileType, tileAttribute);
        }

        public override void Execute(IGame game)
        {
            ITile tile = game.Wayfinder[_position];
            if (tile != null)
            {
                tile.TileType = _tileType;
                tile.TileAttribute = _tileAttribute;
            }
        }

    }
}
