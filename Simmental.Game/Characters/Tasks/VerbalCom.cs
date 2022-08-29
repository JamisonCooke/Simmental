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

        private object _communications;

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
