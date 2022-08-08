using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    [Flags]
    public enum ElementEnum
    {
        Normal = 0,
        Fire = 1,
        Ice = 2,
        Lightning = 4,
        Poison = 8,
    }
}
