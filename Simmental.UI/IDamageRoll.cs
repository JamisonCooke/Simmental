using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    public interface IDamageRoll
    {

        string GetRollDescription();

        int RollForDamage(ICharacter attacker, ICharacter victim);

        ElementEnum DamageType { get; }
        int DamageBonus { get; }
    }
}
