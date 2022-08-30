using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Engine
{
    /// <summary>
    /// Allows Npcs to register themselves, then send messages to eachother 
    /// </summary>
    [Serializable]
    public class Comms : IComms
    {
        #region NpcInfo wrapper class (Npc, listen)

        [Serializable]
        private class NpcInfo
        {
            public ICharacter Npc { get; }
            public Action<ICommsMessage> Listener { get; }

            public NpcInfo(ICharacter npc, Action<ICommsMessage> listener)
            {
                Npc = npc;
                Listener = listener;
            }
        }

        #endregion

        private List<NpcInfo> _npcs = new();

        public void StartListening(ICharacter npc, Action<ICommsMessage> listener)
        {
            _npcs.Add(new NpcInfo(npc, listener));
        }

        public void StopListening(ICharacter npc, Action<ICommsMessage> listener)
        {
            // Better way w/ lambdas
            _npcs.RemoveAll((info) => info.Npc == npc && info.Listener == listener);
        }

        public void Yell(ICommsMessage message, ICharacter speaker)
        {
            // ToDo: Have channel specific yells funnel into an eavesdropping channel

            var yellTo = CanHear(speaker, message);
            foreach(var npcInfo in yellTo)
            {
                message.HeardAlready.Add(npcInfo.Npc);
                npcInfo.Listener(message);
            }
        }

        private IEnumerable<NpcInfo> CanHear(ICharacter speaker, ICommsMessage message)
        {
            List<NpcInfo> result = new();

            foreach(var npcInfo in _npcs)
            {
                if (!message.HeardAlready.Contains(npcInfo.Npc) && CloseEnoughToHear(speaker, message, npcInfo.Npc))
                {
                    result.Add(npcInfo);
                }
            }

            return result;

        }

        private bool CloseEnoughToHear(ICharacter speaker, ICommsMessage message, ICharacter listener)
        {
            // a^2 + b^2 = c^2
            // (x diff)^2 + (y diff)^2 <? message.volume^2
            double distance = Math.Pow(2, speaker.Position.i - listener.Position.i) + Math.Pow(2, speaker.Position.j - listener.Position.j);

            return distance <= Math.Pow(2, message.Volume);            
        }


    }
}
