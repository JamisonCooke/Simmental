using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.Game.Items
{
    [Serializable]
    public class MeleeWeapon : ItemBase, IWeapon
    {
        public MeleeWeapon(string name, string description, IDamageRoll damageRoll)
            : base(name, description)
        {
            DamageRoll = damageRoll;
        }

        public IDamageRoll DamageRoll { get; private set; }

        public string RangedWeaponType { get; set; }

        public override string GetFullName()
        {
            return base.GetFullName() + " " + DamageRoll.GetRollDescription();
        }
    }
}
