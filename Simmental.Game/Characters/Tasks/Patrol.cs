using Simmental.Game.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters.Tasks
{
    [Serializable]
    public class Patrol : ITask
    {
        private Pathfinder _pathfinder;
        private Route _route;
        private int _routeIndex;
        public Patrol(Route route) 
        {
            _route = route;
        }
        public Patrol(TaskParts tp)
            : this(tp.ToRoute(0))
        { }
        
        public bool ExecuteTask(IGame game, ICharacter character)
        {
            if (_pathfinder == null || _pathfinder.Complete || !_pathfinder.CurrentPosition.Equals(character.Position))
            {
                // Find a new destination
                var to = _route.Positions[_routeIndex];
                _routeIndex = (_routeIndex + 1) % _route.Positions.Count;   // Increment, but loop to 0 if it hits Positions.Count
                _pathfinder = new Pathfinder(game.Wayfinder, character.Position, to);
            }

            _pathfinder.Move();
            game.Wayfinder.Move(character, _pathfinder.CurrentPosition);

            return true;
        }

        public string GetSignature()
        {
            var tp = new TaskParts(typeof(Patrol), _route.ToString());
            return tp.ToString();
        }
        public static string GetSignatureFormat()
        {
            return "Route:Route";
        }
    }
}
