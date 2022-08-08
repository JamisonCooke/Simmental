using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Items
{
    [Serializable]
    public class Corpse : Container
    {
        ICharacter _deadCharacter;

        public Corpse(string name, string description, ICharacter deadCharacter) 
            : base(name, description)
        {

            _deadCharacter = deadCharacter;

            foreach(IItem item in _deadCharacter.Inventory.Items)
                this.Add(item);

            foreach (IItem item in this.Items)
                _deadCharacter.Inventory.Remove(item);

        }

        public Corpse(SignatureParts sp)
            : base(sp) 
        { }

    }
}
