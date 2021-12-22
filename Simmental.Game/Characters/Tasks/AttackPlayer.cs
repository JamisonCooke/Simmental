using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters.Tasks
{
    [Serializable]
    internal class AttackPlayer : ITask
    {
        int _turnCount = 0;
        Position _lastSeen = null;
        public bool ExecuteTask(IGame game, ICharacter character)
        {
            if (game.Player.Position.AdjacentTo(character.Position))
            {
                character.Attack(game, game.Player, character.PrimaryWeapon);
            }

            if (game.Wayfinder.IsVisible(game.Player.Position, character.Position, 10))
                _lastSeen = new Position(game.Player.Position);

            if (_lastSeen == null)
                return false;
            _turnCount++;
            if (_turnCount % 3 != 0)
                return false;

            int i = character.Position.i;
            int j = character.Position.j;

            if (_lastSeen.i < i)
                i--;
            else if (_lastSeen.i > i)
                i++;
            if (_lastSeen.j < j)
                j--;
            else if (_lastSeen.j > j)
                j++;

            Position newPosition = new Position(i, j);

            // Only move forward to the new position, if we can walk on the destination square && we are not on the player's square
            if (game.Wayfinder[newPosition].HasAttribute(TileAttributeEnum.CanWalkOn) && !newPosition.EqualTo(game.Player.Position))
                game.Wayfinder.Move(character, newPosition);

            if (_lastSeen.i == character.Position.i && _lastSeen.j == character.Position.j)
                _lastSeen = null;
        

            return false;
        }
    }
}
