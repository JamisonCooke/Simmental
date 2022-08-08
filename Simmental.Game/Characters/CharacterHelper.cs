using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters
{
    public class CharacterHelper
    {

        public BaseCharacter GenerateRandom(RaceEnum race)
        {            
            var c = FactoryCreate(race);
            var random = new Random();

            // Name
            c.Name = "Random Name";

            // Stats
            c.Strength = random.Next(3, 18);
            c.Dexterity = random.Next(3, 18);
            c.Charisma = random.Next(3, 18);
            c.Wisdom = random.Next(3, 18);
            c.Intelligence = random.Next(3, 18);
            c.Constitution = random.Next(3, 18);
            c.AC = 10;

            return c;

        }

        public BaseCharacter FactoryCreate(RaceEnum race)
        {
            switch (race) 
            {
                case RaceEnum.Human:
                    return new Human();
                case RaceEnum.Orc:
                    return new Orc();
            }
            return null;
        }


    }
}
