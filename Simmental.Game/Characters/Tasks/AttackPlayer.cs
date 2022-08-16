using Simmental.Game.Map;
using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters.Tasks
{
    [Serializable]
    internal class AttackPlayer : ITask, ISignature
    {
        int _turnCount = 0;
        Pathfinder _pathfinder;

        public AttackPlayer() { }

        public AttackPlayer(TaskParts tp) { }

        public bool ExecuteTask(IGame game, ICharacter character)
        {
            if (game.Player.Position.AdjacentTo(character.Position))
            {
                character.Attack(game, game.Player, character.PrimaryWeapon);
                return true;
            }

            if (game.Wayfinder.CanSee(character.Position, game.Player.Position, 20))
                _pathfinder = new Pathfinder(game.Wayfinder, character.Position, game.Player.Position);

            if (_pathfinder == null)
                return false;

            _turnCount++;
            if (_turnCount % 3 != 0)
                return true;

            _pathfinder.Move();
            game.Wayfinder.Move(character, _pathfinder.CurrentPosition);
            if (_pathfinder.Complete)
                _pathfinder = null;

            return _pathfinder != null;
        }

        public string GetSignature()
        {
            var tp = new TaskParts(typeof(AttackPlayer));
            return tp.ToString();
        }

        public static string GetSignatureFormat()
        {
            return "TaskName";
        }
    }
}
