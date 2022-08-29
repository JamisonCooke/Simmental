using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Comms
{
    /// <summary>
    /// A call for monsters to line up and space out in the lineup diretion, then walk the sweep direction
    /// </summary>
    public class Sweep : CommsMessageBase
    {
        public CompassEnum LineUpDirection { get; }
        public CompassEnum SweepDirection { get; }

        public Sweep(CommsMediumEnum medium, int volume, CompassEnum lineUpDirection, CompassEnum sweepDirection)
            :base(medium, volume)
        {
            LineUpDirection = lineUpDirection;
            SweepDirection = sweepDirection;
        }
    }
}
