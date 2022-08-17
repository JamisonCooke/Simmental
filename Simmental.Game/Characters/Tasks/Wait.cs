using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters.Tasks
{
    [Serializable]
    public class Wait : ITask, ISignature
    {
        private int _waitTurns;
        public Wait(int waitTurns) 
        {
            _waitTurns = waitTurns;
        }
        public Wait(TaskParts tp)
        : this(tp.ToInt(0))
        { }

        bool _firstTime = true;
        int _wakeUpTurn;
        public bool ExecuteTask(IGame game, ICharacter character)
        {
            if (_firstTime)
            {
                _wakeUpTurn = game.TurnNo + _waitTurns;
                _firstTime = false;
            }
            if (_wakeUpTurn >= game.TurnNo)
            {
                _firstTime = true;
                return true; 
            }
            else
            {
                return false;
            }
        }

        public string GetSignature()
        {
            var tp = new TaskParts(typeof(Wait), _waitTurns.ToString());
            return tp.ToString();
        }
        public static string GetSignatureFormat()
        {
            return "WaitTurns:Int32";
        }
    }
}
