using Simmental.Game.Characters.Tasks;
using Simmental.Game.Items;
using Simmental.Game.Map;
using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters
{
    /// <summary>
    /// Represents the base / common aspects for both the main character and the RPC's
    /// </summary>
    [Serializable]
    public abstract class BaseCharacter : ICharacter, IExecuteTurn
    {
        /// <summary>
        /// Name of the character
        /// </summary>
        public string Name { get; set; }
        public int HP { get; set; }
        public int Level { get; set; } = 1;

        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Charisma { get; set; }
        public int Wisdom { get; set; }
        public int AC { get; set; }
        public Position Position { get; private set; } = new Position();
        public IWeapon PrimaryWeapon { get; set; }
        public IWeapon SecondaryWeapon { get; set; }
        //public IItem Armor { get; set; }

        public virtual RaceEnum Race
        {
            get { return RaceEnum.Undefined; }
        }
        
        public virtual void ExecuteTurn(IGame game)
        {
            foreach (var task in Tasks)
            {
                if (task.ExecuteTask(game, this))
                    break;
            }
        }

        
        public IInventory Inventory { get; } = new Inventory();

        public ElementEnum ElementallyResistant => _elementallyResistant;

        public ElementEnum ElementallyImmune => _elementallyImmune;

        public ElementEnum ElementallyVulnerable => _elementallyVulnerable;

        protected ElementEnum _elementallyResistant = ElementEnum.Normal;
        protected ElementEnum _elementallyImmune = ElementEnum.Normal;
        protected ElementEnum _elementallyVulnerable = ElementEnum.Normal;

        public List<ITask> Tasks = new List<ITask>();

        public int GetMaxHP()
        {
            return 50;
        }

        void ICharacter.SetPositionInternal(Position position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Attacks the main player
        /// </summary>
        /// <param name="game"></param>
        public int Attack(IGame game, ICharacter victim, IItem item)
        {
            bool criticalHit = false;

            IWeapon weapon = item as IWeapon;
            IRangedWeapon rangedWeapon = item as IRangedWeapon;

            if (rangedWeapon != null)
            {
                // ToDo: Check that the secondary weapon is companitble w/ rangedWeapon
                weapon = this.SecondaryWeapon;
            }

            if (weapon == null)
            {
                weapon = new MeleeWeapon("Fists", "Bare knuckles, baby!", new DamageRoll(1, 2, ElementEnum.Normal));
            }

            if (rangedWeapon != null)
            {
                rangedWeapon.Count--;
                if (rangedWeapon.Count == 0)
                {
                    if (this == game.Player)
                        game.LogMessage($"You just ran out of {rangedWeapon.Name}");

                    this.Inventory.Remove(rangedWeapon);
                }
            }

            // Accuracy roll
            int d20 = Simmental.Game.Engine.Game.Random.Next(1, 20);
            if (d20 == 1)
            {
                game.LogMessage($"The {this.Name} misses {victim.Name} so pathetically, it falls on it's face.");
                return 0;
            }
            else if (d20 == 20)
            {
                criticalHit = true;
            }
            else if (d20 + weapon.DamageRoll.DamageBonus < victim.AC)
            {
                game.LogMessage($"The attack against {victim.Name} misses");
                return 0;
            }

            // Damage roll
            int damage = weapon.DamageRoll.RollForDamage(this, victim);
            if (rangedWeapon != null) damage += rangedWeapon.DamageBonus;
            if (criticalHit) damage *= 2;
            victim.HP -= damage;
            if (victim.HP <= 0)
                Killed(game, victim);

            string hitAdjective = criticalHit ? "critical " : "";
            game.LogMessage($"The {this.Name} used their {weapon.Name} to {hitAdjective}hit {victim.Name} and caused {damage} damage.");


            return damage;
        }
        private void Killed(IGame game, ICharacter deceased)
        {
            Corpse corpse = new Corpse($"Dead {deceased.Name}", "Stinky Corpse", deceased);
            var tile = game.Wayfinder[deceased.Position];
            tile.Inventory.Add(corpse);
            if (tile.NPCs.Contains(deceased))
                tile.NPCs.Remove(deceased);

            game.NPC.Remove(deceased);
            
        }

    }
}
