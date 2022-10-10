using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    public interface IComms
    {

        public void StartListening(ICharacter npc, IListen listener);
        public void StopListening(ICharacter npc, IListen listener);
        public void Yell(ICommsMessage message, ICharacter speaker);
    }
}
