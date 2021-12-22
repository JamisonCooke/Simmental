using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.Game.Items;
using Simmental.UI;

namespace Simmental.Game.Map
{
    [Serializable]
    public class Tile : ITile
    {
        public string Name { get; set; }
        public TileEnum TileType { get; set; }
        public IInventory Inventory { get; } = new Inventory();
        public TileAttributeEnum TileAttribute { get; set; }
        public bool Seen { get; set; }

        public List<ICharacter> NPCs { get; } = new List<ICharacter>();

        /// <summary>
        /// Returns true if the passed bit(s) are all set om TileAttribute
        /// </summary>
        /// <param name="tileAttributeEnum"></param>
        /// <returns></returns>
        public bool HasAttribute(TileAttributeEnum tileAttributeEnum)
        {
            return (this.TileAttribute & tileAttributeEnum) == tileAttributeEnum;
        }
    }
}
