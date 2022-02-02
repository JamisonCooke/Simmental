using Simmental.Game.Map;
using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters.Tasks
{
    [Serializable]
    internal class Wander : ITask
    {

        private Pathfinder _pathfinder;
        public bool ExecuteTask(IGame game, ICharacter character)
        {
            if (_pathfinder == null || _pathfinder.Complete || !_pathfinder.CurrentPosition.Equals(character.Position))
            {
                // Find a new destination
                var to = new Position(Game.Engine.Game.Random.Next(game.Wayfinder.Width), Game.Engine.Game.Random.Next(game.Wayfinder.Height));
                _pathfinder = new Pathfinder(game.Wayfinder, character.Position, to);
            }

            _pathfinder.Move();

            if (Math.Abs(character.Position.i - _pathfinder.CurrentPosition.i) > 1)
            {
                int i = 0;
            }
            if (Math.Abs(character.Position.j - _pathfinder.CurrentPosition.j) > 1)
            {
                int i = 0;
            }

            game.Wayfinder.Move(character, _pathfinder.CurrentPosition);

            return !_pathfinder.Complete;
        }
    }
}
