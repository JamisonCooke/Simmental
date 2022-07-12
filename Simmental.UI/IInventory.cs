using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI
{
    public interface IInventory
    {
        void Add(IItem item);
        void Remove(IItem item);
        IEnumerable<IItem> Items { get; }
        IEnumerable<IRangedWeapon> RangedWeapons { get; }
        IEnumerable<IWeapon> Weapons { get; }
        string GetInventorySignatures();
        string SetInventorySignatures(string text);
    }
}
