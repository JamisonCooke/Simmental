using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Comms
{
    public class CommsMessageBase : ICommsMessage
    {
        public ISet<ICharacter> HeardAlready { get; } = new HashSet<ICharacter>();

        public CommsMediumEnum Medium { get; }

        public int Volume { get; }

        public CommsMessageBase(CommsMediumEnum medium, int volume)
        {
            Medium = medium;
            Volume = volume;
        }
    }
}
