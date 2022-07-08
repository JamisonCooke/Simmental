using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.Game.Items
{
    /// <summary>
    /// Represents the damage a weapon can do. 
    /// **1d8 + 1d6f** Adamantine Poisoned Fire Spear 
    /// </summary>
    [Serializable]
    public class DamageRoll : IDamageRoll
    {

        public DamageRoll() { }

        public DamageRoll(int numberOfRolls, int diceMax, ElementEnum element)
        {
            AddRoll(numberOfRolls, diceMax, element);
        }

        public DamageRoll(string rollDescription)
        {
            FromRollDescription(rollDescription);
        }

        public int DamageBonus { get; set; }

        public ElementEnum DamageType
        {
            get
            {
                ElementEnum result = ElementEnum.Normal;

                foreach (var rollPart in _damageRolls)
                    result |= rollPart.Element;

                return result;
            }
        }

        private List<RollPart> _damageRolls = new List<RollPart>();

        public void AddRoll(int numberOfRolls, int diceMax, ElementEnum element)
        {
            // var dm = new DamageRoll();
            // dm.AddRoll(1, 8, ElementEnum.None);
            // dm.AddRoll(1, 6, ElementEnum.Fire);
            // dm.DamageBonus = 1;
            var rollPart = new RollPart(numberOfRolls, diceMax, element);
            _damageRolls.Add(rollPart);

        }

        /// <summary>
        /// Returns the text like 1d8+1d6f
        /// </summary>
        /// <returns></returns>
        public string GetRollDescription()
        {

            return string.Join(" + ", _damageRolls) + DamageBonus.ToString(" +#; -#;#");

        }

        public void FromRollDescription(string rollDescription)
        {
            // 2d6
            // 2d6 + 1d20
            // 2d6 + 1d20 +5    "2d6", "1d20 +5"  <-- from split
            // 2d6 + 1d20 -1

            var betterDamageRolls = new List<RollPart>();
            foreach (var text in rollDescription.Split(" + "))
            {
                // Handle a DamageBonus tacked on the end
                var damageBonusPosition = text.IndexOf(" +");
                if (damageBonusPosition < 0)
                    damageBonusPosition = text.IndexOf(" -");
                if (damageBonusPosition >= 0)
                {
                    DamageBonus = int.Parse(text.Substring(damageBonusPosition));
                    var rollPartText = text.Substring(0, damageBonusPosition);
                    betterDamageRolls.Add(new RollPart(rollPartText));
                }
                else
                {
                    
                    betterDamageRolls.Add(new RollPart(text));
                }
           }
            _damageRolls = betterDamageRolls;
        }


        public int RollForDamage(ICharacter attacker, ICharacter victim)
        {
            int totalDamage = 0;

            foreach(var roll in _damageRolls)
            {
                totalDamage += roll.RollForDamage(attacker, victim);
            }

            totalDamage += DamageBonus;
            if (totalDamage < 0)        // No healing the monster!
                totalDamage = 0;

            return totalDamage;
        }
        public override string ToString()
        {
            return GetRollDescription();
        }
    }
}
