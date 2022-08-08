using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    public interface IWeapon : IItem
    {

        // [ DamageRoll ] [ Pronoun(s) ] [ DamageType--extracted from DamageRoll ] [ WeaponType ] 
        // 1d8 + 1d6f +2  Adamantine Poisoned Fire Spear 

        // 1d6+2 Elven Dagger
        // 1d6f Elven Flaming Dagger
        // 2d10 + 2d8 Elvin Dagger 
        // 1d20 difficulty 17 -- 
        // Staff of teleportation 

        // Fill me in some attributes, please
        //ElementEnum DamageType { get; }

        IDamageRoll DamageRoll { get; }
        //int Range { get; }

        public string RangedWeaponType { get; set; }

    }
}
