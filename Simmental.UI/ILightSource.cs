﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    public interface ILightSource
    {
        int Brightness { get; set; }
        int Distance { get; set; }
        bool IsLit { get; set; }

    }
}
