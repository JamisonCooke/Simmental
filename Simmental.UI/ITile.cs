﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    public interface ITile
    {
        string Name { get; set; }
        TileEnum TileType { get; set; }
        IInventory Inventory { get; }
        TileAttributeEnum TileAttribute { get; set; }
        int LightLevel { get; set; }
        int DefaultLightLevel { get; set; }

        bool HasAttribute(TileAttributeEnum tileAttributeEnum);
        bool Seen { get; set; }
        List<ICharacter> NPCs { get; }
        
    }
}
