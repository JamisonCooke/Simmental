namespace Simmental.Interfaces
{
    public interface ICharacter
    {

        string Name { get; set; }
        string Description { get; set; }
        int HP { get; set; }
        int Level { get; set; }

        int GetMaxHP();

        int Charisma { get; set; }
        int Constitution { get; set; }
        int Dexterity { get; set; }
        int Intelligence { get; set; }
        
        int Strength { get; set; }
        int Wisdom { get; set; }
        int AC { get; set; }
        Position Position { get; }
        void SetPositionInternal(Position position);
        RaceEnum Race { get;}
        void ExecuteTurn(IGame game);
        IInventory Inventory { get; }
        IWeapon PrimaryWeapon { get; set;  } 
        IWeapon SecondaryWeapon { get; set; }

        int Attack(IGame game, ICharacter victim, IItem item);

        ElementEnum ElementallyResistant { get; }
        ElementEnum ElementallyImmune { get; }
        ElementEnum ElementallyVulnerable { get; }

    }
}