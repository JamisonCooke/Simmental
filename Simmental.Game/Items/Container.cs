using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.Game.Items
{
    [Serializable]
    public class Container : ItemBase, IInventory
    {

        public Container(string name, string description)
            : base(name, description)
        {
        }

        private List<IItem> _items = new List<IItem>();

        public IEnumerable<IItem> Items => _items;

        public void Add(IItem item)
        {
            _items.Add(item);
        }

        public void Remove(IItem item)
        {
            _items.Remove(item);
        }
        public IEnumerable<IRangedWeapon> RangedWeapons
        {
            get
            {
                foreach (IItem item in _items)
                {
                    if (item is IRangedWeapon weapon)
                    {
                        yield return weapon;
                    }
                }
            }
        }

        public IEnumerable<IWeapon> Weapons
        {
            get
            {
                foreach (IItem item in _items)
                {
                    if (item is IWeapon weapon)
                    {
                        yield return weapon;
                    }
                }
            }
        }
    }

}
