using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters.Tasks
{
    [Serializable]
    internal class VerbalCom : ITask
    {

        private ITask _subtask;

        private IComms _comms;
        public void Start(IGame game, ICharacter character)
        {
            _comms = game.GetComms(character.Race.ToString());
            _comms.StartListening(character, Listen);
        }

        public void Stop(IGame game, ICharacter character) 
        {
            _comms.StopListening(character, Listen);
        }

        public bool ExecuteTask(IGame game, ICharacter character)
        {
            // 
            //if (CanSee(game.Player))
            //    _communications.Yell(CanSeePlayerAt game.Player.Position);

            


            return _subtask.ExecuteTask(game, character);
        }

        ICommsMessage _lastMessage;

        private void Listen(ICommsMessage message)
        {
            if (message == _lastMessage)
                return;

            _lastMessage = message;

            //if (message is CommMessageEncircle encircle)
            //{
            //    // 
            //    if (encirle.SweepDirection == N)
            //    {

            //    }
            //}

            //switch (message)
            //{
            //    case "CanSeePlayerAt": _subtask = new AttackPlayer(position);

            //    case: "CanSeePlayerAt":
            //        _communications.Yell(message);
            //}
            
        }

        public string GetSignature()
        {
            throw new NotImplementedException();
        }
    }
}
