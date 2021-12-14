using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Helper
{
    internal class ItemWrapper
    {
        public IItem Item { get; }
        private string _displayText;
        public IInventory Inventory { get; }
        public ItemWrapper(IItem item, IInventory inventory, string displayText)
        {
            Item = item;
            _displayText = displayText;
            Inventory = inventory;
            
        }
        public override string ToString()
        {
            return _displayText;
        }

    }
}
