using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Items
{
    [Serializable]
    public class ProjectileLauncher: ItemBase, IWeapon, ISignature
    {
        public ProjectileLauncher(string name, string description,string rangedWeaponType, IDamageRoll damageRoll)
           : base(name, description)
        {
            DamageRoll = damageRoll;
            RangedWeaponType = rangedWeaponType;
        }

        public ProjectileLauncher(SignatureParts sp)
            :this (sp[0], sp[1], sp[2], sp.ToDamageRoll(3))
        {

        }
        public static string GetSignatureFormat()
        {
            return "Name,Description,RangedWeaponType,DamageRoll:DamageRoll";
        }

        public IDamageRoll DamageRoll { get; private set; }

        /// <summary>
        /// When a ProjectileLauncher, ie., a crossbow, shoots a bolt, the RangedWeaponType would be set to "bolt"
        /// and when newing up a RangedWeapon, it must have a matching RangedWeaponType of "bolt" in order to be fired.
        /// </summary>
        public string RangedWeaponType { get; set; }

        public override string GetFullName()
        {
            return base.GetFullName() + " " + DamageRoll.GetRollDescription();
        }

        public override string GetSignature()
        {
            var sp = new SignatureParts(typeof(ProjectileLauncher), Name, Description, RangedWeaponType, DamageRoll.ToString());
            return sp.ToSignature();
        }
    }
}
