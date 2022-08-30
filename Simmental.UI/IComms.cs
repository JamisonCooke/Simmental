using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    public interface IComms
    {

        public void StartListening(ICharacter npc, Action<ICommsMessage> listener);
        public void StopListening(ICharacter npc, Action<ICommsMessage> listener);
        public void Yell(ICommsMessage message, ICharacter speaker);
    }
}
