using Simmental.Game.Comms;
using Simmental.Game.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters.Tasks
{
    [Serializable]
    public class RageAttack : ITask
    {

        int _turnCount = 0;
        Pathfinder _pathfinder;
        IComms _comms;
        ICharacter _character;
        IGame _game;
        int _screamedOnTurn;
        public RageAttack() { }

        public RageAttack(TaskParts tp) { }

        public void Start(IGame game, ICharacter character) 
        {
            _game = game;
            _character = character;
            _comms = game.GetComms(character.Race.ToString());
            _comms.StartListening(character, Listen);
        }
        public void Stop(IGame game, ICharacter character) 
        {
            _comms.StopListening(character, Listen);
        }

        public bool ExecuteTask(IGame game, ICharacter character)
        {
            if (game.Wayfinder.CanSee(character.Position, game.Player.Position, 20))
            {
                // Yell forhelp!
                _comms.Yell(new SawTarget(CommsMediumEnum.Verbal, 20, game.Player.Position, game.Player), character);
            }

            if (game.Player.Position.AdjacentTo(character.Position))
            {
                character.Attack(game, game.Player, character.PrimaryWeapon);
                return true;
            }

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
        private void Listen(ICommsMessage message) 
        { 
            if (message is SawTarget sawTarget)
            {
                _comms.Yell(message, _character);
                if (_screamedOnTurn != _game.TurnNo)
                {
                    _screamedOnTurn = _game.TurnNo;
                    _game.LogMessage($"{_character.Name} yells {RandomScream()}");
                }
                _pathfinder = new Pathfinder(_game.Wayfinder, _character.Position, _game.Player.Position);
            }        
        }

        private string RandomScream()
        {
            Random rnd = new Random();
            int num = rnd.Next(5);
            switch (num)
            {
                case 0: return "Aaaaa!!!!";
                case 1: return "Here's over there!!!";   // Fill these out 
                case 2: return "Get him!";
                case 3: return "Rip him apart!";
                case 4: return "Get the jerk!";
                case 5: return "To aaa or not to aaa that is the Aaaaaaa!";
            }
            return "Kill him!";
        }

        public string GetSignature()
        {
            var tp = new TaskParts(typeof(RageAttack));
            return tp.ToString();
        }

        public static string GetSignatureFormat()
        {
            return "";
        }
    }
}
