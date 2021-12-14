using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Items
{
    [Serializable]
    public abstract class ItemBase : IItem
    {
        private string _name = "";
        private string _description = "";

        public string Name => _name;

        public string Description => _description;
        
        public int Count { get; set; }

        public ItemBase() { }

        public ItemBase(string name, string description)
        {
            _name = name;
            _description = description;
            Count = 1;
        }

        public ItemBase(string name, string description, int count)
        {
            _name = name;
            _description = description;
            Count = count;
        }

        public virtual string GetFullName()
        {
            if (Count == 1)
                return _name;
            else
                return $"{_name} x{Count}";
        }
    }
}
