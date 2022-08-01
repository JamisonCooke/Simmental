using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Command
{
    public class UpdateInventory : CommandBase
    {
        private Position _position;
        private string _signature;

        public UpdateInventory(Position position, string signature)
        {
            (_position, _signature) = (position, signature);
        }

        public override void Execute(IGame game)
        {
            ITile tile = game.Wayfinder[_position];
            if (tile != null)
            {
                tile.Inventory.SetInventorySignatures(_signature);
            }
        }
    }
}
