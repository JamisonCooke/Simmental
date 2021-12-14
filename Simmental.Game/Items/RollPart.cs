using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.Game.Items
{
    [Serializable]    
    internal class RollPart
    {

        public int NumberOfRolls { get; private set; }
        public int DiceMax { get; private set; }
        public ElementEnum Element { get; private set; }

        public RollPart(int numberOfRolls, int diceMax, ElementEnum element)
        {
            NumberOfRolls = numberOfRolls;
            DiceMax = diceMax;
            Element = element;
        }

        public override string ToString()
        {
            return $"{NumberOfRolls}d{DiceMax}{ElementLetter(Element)}";
        }

        public static string ElementLetter(ElementEnum element)
        {
            switch(element)
            {
                case ElementEnum.Fire: return "F";
                case ElementEnum.Ice: return "I";
                case ElementEnum.Lightning: return "L";
                case ElementEnum.Poison: return "P";
                default: return "";
            }
        }

        public int RollForDamage(ICharacter attacker, ICharacter victim)
        {
            int damage = 0;

            // roll dice / etc
            for(int i = 0; i < NumberOfRolls; i++)
            {
                damage += Simmental.Game.Engine.Game.Random.Next(1, DiceMax); 
            }

            double magnifier = 1;

            // Figure out elemental support for weapons
            if ((victim.ElementallyVulnerable & Element) != 0)
                magnifier = 2.0;
            if ((victim.ElementallyResistant & Element) != 0)
                magnifier = 0.5;
            if ((victim.ElementallyImmune & Element) != 0)
                magnifier = 0.05;

            return (int)(damage * magnifier);
        }

    }
}
