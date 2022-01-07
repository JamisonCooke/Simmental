using Simmental.Game.Characters;
using Simmental.Game.Characters.Tasks;
using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Items
{
    [Serializable]
    public class MonsterPortal : ItemBase, IExecuteTurn
    {
        public MonsterPortal() { }

        private RaceEnum _race;
        private int _max;
        private int _minTurnSpawn;
        private int _maxTurnSpawn;
        private int _nextCreateTurn;
        private Position _position;
        private int _monsterCounter;

        /// <summary>
        /// The monster portal will create monsters randomly over time
        /// </summary>
        /// <param name="game">The game where the monsters are being added</param>
        /// <param name="race">This is the type of monster to add</param>
        /// <param name="max">The total number of monsters the portal will spawn</param>
        /// <param name="minTurnSpawn">The portal will wait between minTurnSpawn and maxTurnSpawn to create a new monster. MinVal</param>
        /// <param name="maxTurnSpawn">Max val</param>
        public MonsterPortal(IGame game, RaceEnum race, int max, int minTurnSpawn, int maxTurnSpawn, Position position)
        {
            game.RequiresATurn.Add(this);

            _race = race;                       
            _max = max;
            _minTurnSpawn = minTurnSpawn;
            _maxTurnSpawn = maxTurnSpawn;
            _position = position;
            _monsterCounter = 0;
            _nextCreateTurn = Simmental.Game.Engine.Game.Random.Next(_minTurnSpawn, _maxTurnSpawn) + game.TurnNo;
        }

        public void DestroyPortal(IGame game)
        {
            game.RequiresATurn.Remove(this);
        }


        public void ExecuteTurn(IGame game)
        {
            if (game.TurnNo < _nextCreateTurn && _monsterCounter >= _max)
                return;

            _nextCreateTurn = Simmental.Game.Engine.Game.Random.Next(_minTurnSpawn, _maxTurnSpawn) + game.TurnNo;
            _monsterCounter++;
            if (_monsterCounter >= _max)
                DestroyPortal(game);

            CharacterHelper characterHelper = new CharacterHelper();
            var monster = characterHelper.FactoryCreate(_race);

            var axe = new MeleeWeapon("Ugly Axe", "A really ugly axe.", new DamageRoll(1, 12, ElementEnum.Normal));
            monster.Inventory.Add(axe);
            monster.PrimaryWeapon = axe;
            monster.Name = "Angry Monster";
            monster.HP = 25;
            game.Wayfinder.Move(monster, _position);
            monster.Tasks.Add(new AttackPlayer());
            monster.Tasks.Add(new Wander());
            monster.Inventory.Add(new LightSource("Torch", "a Stick with fabric and tar fixed at the end", 70, 7));
            game.NPC.Add(monster);

        }
    }
}
