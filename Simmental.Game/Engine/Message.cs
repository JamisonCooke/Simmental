using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Engine
{
    [Serializable]
    public class Message : IMessage
    {
        public string MessageText { get; private set; }
        public int TurnNo { get; private set; }
        public Message() { }
        public Message(string messageText, int turnNo)
        {
            this.MessageText = messageText;
            this.TurnNo = turnNo;
        }
    }
}
