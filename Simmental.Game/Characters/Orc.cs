using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters
{
    [Serializable]
    public class Orc : BaseCharacter
    {
        public override RaceEnum Race
        {
            get { return RaceEnum.Orc; }
        }

        int _turnCount = 0;
        Position _lastSeen = null;

        public override void ExecuteTurn(IGame game)
        {
            if (game.Player.Position.AdjacentTo(this.Position))
            {
                Attack(game, game.Player, this.PrimaryWeapon);
            }

            if (game.Wayfinder.IsVisible(game.Player.Position, this.Position, 10))
                _lastSeen = new Position(game.Player.Position);

            if (_lastSeen == null)
                return;
            _turnCount++;
            if (_turnCount % 3 != 0)
                return;
            Position newPosition = new Position(this.Position);

            if (_lastSeen.i < newPosition.i)
                newPosition.i--;
            else if (_lastSeen.i > newPosition.i)
                newPosition.i++;
            if (_lastSeen.j < newPosition.j)
                newPosition.j--;
            else if (_lastSeen.j > newPosition.j)
                newPosition.j++;

            // Only move forward to the new position, if we can walk on the destination square && we are not on the player's square
            if (game.Wayfinder[newPosition].HasAttribute(TileAttributeEnum.CanWalkOn) && !newPosition.EqualTo(game.Player.Position))
                this.Position = newPosition;

            if (_lastSeen.i == this.Position.i && _lastSeen.j == this.Position.j)
                _lastSeen = null;
        }


    }
}
