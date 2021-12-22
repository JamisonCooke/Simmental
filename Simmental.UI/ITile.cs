using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI
{
    public interface ITile
    {
        string Name { get; set; }
        TileEnum TileType { get; set; }
        IInventory Inventory { get; }
        TileAttributeEnum TileAttribute { get; set; }
        bool HasAttribute(TileAttributeEnum tileAttributeEnum);
        bool Seen { get; set; }
        List<ICharacter> NPCs { get; }

    }
}
