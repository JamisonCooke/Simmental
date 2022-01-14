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
            if (_pathfinder == null || _pathfinder.Complete)
            {
                // Find a new destination
                var to = new Position(Game.Engine.Game.Random.Next(game.Wayfinder.Width), Game.Engine.Game.Random.Next(game.Wayfinder.Height));
                _pathfinder = new Pathfinder(game.Wayfinder, character.Position, to);
            }

            _pathfinder.Move();
            game.Wayfinder.Move(character, _pathfinder.CurrentPosition);

            return !_pathfinder.Complete;
        }
    }
}
