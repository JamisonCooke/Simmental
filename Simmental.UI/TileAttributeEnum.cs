using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    [Flags]
    public enum TileAttributeEnum
    {
        None = 0,
        CanWalkOn = 1,
        CanFlyOver = 2,
        WillKillYou = 4,
        Opaque = 8,
    }
}
