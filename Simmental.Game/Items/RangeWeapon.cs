using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.Game.Items
{
    [Serializable]
    public class RangedWeapon : ItemBase, IRangedWeapon
    {
        public int DamageBonus { get; set; }
        public ElementEnum Element { get; set; }

        /// <summary>
        /// When a ProjectileLauncher, ie., a crossbow, shoots a bolt, the RangedWeaponType would be set to "bolt"
        /// and when newing up a RangedWeapon, it must have a matching RangedWeaponType of "bolt" in order to be fired.
        /// </summary>
        public string RangedWeaponType { get; set; }


        public RangedWeapon() { }
        public RangedWeapon(string name, string description, int count, int damageBonus, ElementEnum element, string rangedWeaponType)
            : base(name, description, count)
        {
            DamageBonus = damageBonus;
            Element = element;
            RangedWeaponType = rangedWeaponType;
        }

        public override string GetFullName()
        {
            string elementText = " (" + Enum.GetName(typeof(ElementEnum), Element) + ")";
            if (Element == ElementEnum.Normal)
                elementText = "";

            string damageBonusText = DamageBonus.ToString(" +#; -#;#");

            return base.GetFullName() + damageBonusText + elementText;

        }

    }
}
