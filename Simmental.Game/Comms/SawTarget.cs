using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Comms
{
    public class SawTarget : CommsMessageBase
    {
        public Position Position { get; }
        public ICharacter Target { get; }

        public SawTarget(CommsMediumEnum medium, int volume, Position position, ICharacter target)
            : base(medium, volume)
        {
            Position = position;
            Target = target;
        }
    }
}
