using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.Game.Items
{
    [Serializable]
    public class MeleeWeapon : ItemBase, IWeapon, ISignature
    {
        public MeleeWeapon(string name, string description, IDamageRoll damageRoll)
            : base(name, description)
        {
            DamageRoll = damageRoll;
        }

        public MeleeWeapon(SignatureParts sp)
            : this(sp[0], sp[1], sp.ToDamageRoll(2))
        { }

        public string GetSignature()
        {
            var sp = new SignatureParts(typeof(MeleeWeapon), Name, Description, DamageRoll.ToString());
            return sp.ToString();
        }

        public IDamageRoll DamageRoll { get; private set; }

        public string RangedWeaponType { get; set; }

        public override string GetFullName()
        {
            return base.GetFullName() + " " + DamageRoll.GetRollDescription();
        }
    }
}
