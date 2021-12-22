using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI
{
    public interface ILightSource
    {
        int Brightness { get; set; }
        int Distance { get; set; }
    }
}
