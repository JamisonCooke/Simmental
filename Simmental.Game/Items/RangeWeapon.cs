using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.Interfaces;

namespace Simmental.Game.Items
{
    [Serializable]
    public class RangedWeapon : ItemBase, IRangedWeapon, ISignature
    {
        public int DamageBonus { get; set; }
        public ElementEnum Element { get; set; }

        /// <summary>
        /// When a ProjectileLauncher, ie., a crossbow, shoots a bolt, the RangedWeaponType would be set to "bolt"
        /// and when newing up a RangedWeapon, it must have a matching RangedWeaponType of "bolt" in order to be fired.
        /// </summary>
        public string RangedWeaponType { get; set; }

        public override string GetSignature()
        {
            // ToDo: Add custom formatting per ISignature Type
            // Arrow [12] (rw)
            // string formatString = "{Name} [{Count}] (rw), {Count.ToString(" +#; -#;#")} ";

            var sp = new SignatureParts(typeof(RangedWeapon), Name, Description, Count.ToString(), DamageBonus.ToString(), Element.ToString(), RangedWeaponType);
            // var x = Enum.Parse(typeof(ElementEnum), "Fire");
            return sp.ToString();
        }

        public RangedWeapon() { }
        public RangedWeapon(string name, string description, int count, int damageBonus, ElementEnum element, string rangedWeaponType)
            : base(name, description, count)
        {
            // Arrow (rw), Grisly Arrow, 12, 5, F, bolt
            
            DamageBonus = damageBonus;
            Element = element;
            RangedWeaponType = rangedWeaponType;
        }
        public RangedWeapon(SignatureParts sp)
            : this(sp[0], sp[1], int.Parse(sp[2]), int.Parse(sp[3]), sp.ToElement(4), sp[5]) 
        { }

        public static string GetSignatureFormat()
        {
            return "Name,Description,Count:Int32,DamageBonus:Int32,Element:ElementEnum,RangedWeaponType";
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
