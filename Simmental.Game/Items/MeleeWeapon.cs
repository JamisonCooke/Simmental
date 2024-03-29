﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.Interfaces;

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

        public static string GetSignatureFormat()
        {
            return "Name,Description,DamageRoll:DamageRoll";

            // Flaming Sword (mw), A fantastic sword, 2d6

            // Attack Player (ap)
            // Wonder (w), 

        }

        public override string GetSignature()
        {
            // Valid signature for a MeleeWeapon:
            // [Name] (mw), [Description], [DamageRoll]

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
