using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.Game.Items
{
    [Serializable]
    public class Container : ItemBase, IInventory, ISignature
    {
        // ToDo: In SignatureFactory, support items inside of a container
        // Backpack (c), Ugly leather backpack
        //   Short sword (mw), Rusty sword, 1d4
        //   Arrow (pw), 
        //   Wallet (c)
        //     100 Gold note ($)
        //     Drivers License (id)
        //   Bolt (pw)

        public Container(string name, string description)
            : base(name, description)
        {
        }

        public Container(SignatureParts sp)
            :this(sp[0], sp[1])
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

        public string GetSignature()
        {
            var sp = new SignatureParts(this.GetType(), Name, Description);
            return sp.ToSignature();
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
