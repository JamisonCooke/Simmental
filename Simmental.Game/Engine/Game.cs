using Simmental.Game.Characters;
using Simmental.Game.Items;
using Simmental.Game.Map;
using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Simmental.Game.Characters.Tasks;

namespace Simmental.Game.Engine
{
    [Serializable]
    public class Game : IGame
    {
        public IWayfinder Wayfinder { get; private set; }

        public ICharacter Player { get; private set; }

        public List<ICharacter> NPC { get; private set; }
        public List<IExecuteTurn> RequiresATurn { get; private set; } = new List<IExecuteTurn>();
        
        public IDesigner Designer { get; private set; } = new Design.Designer();

        public int TurnNo => _turnNo;

        private static Random _random = new Random();
        public static Random Random => _random;

        public void InitalizeRandom()
        {
            Wayfinder = new Wayfinder();
            Wayfinder.Initialize(100, 150);

            var sword = new MeleeWeapon("Long Sword", "A Really REALLY long sword", new DamageRoll(2, 8, ElementEnum.Fire));
            
            Wayfinder[5, 5].Inventory.Add(sword);
            var helper = new Simmental.Game.Characters.CharacterHelper();
            Player = helper.GenerateRandom(RaceEnum.Human);
            Player.Name = "Cowman";
            
            var dagger = new MeleeWeapon("Dagger", "Simple but effective", new DamageRoll(3, 6, ElementEnum.Normal));
            Player.Inventory.Add(dagger);
            Player.PrimaryWeapon = dagger;

            var crossbow = new ProjectileLauncher("Crossbow", "Strong Crossbow, fires bolts", "bolt", new DamageRoll(1, 12, ElementEnum.Normal));
            var basicbolt = new RangedWeapon("Basic Bolt", "Can Be fired from crossbows",20, 4, ElementEnum.Fire, "bolt");
            var arrows = new RangedWeapon("Arrows", "Can Be fired from a bows", 20, 4, ElementEnum.Normal, "arrow");

            Player.Inventory.Add(crossbow);
            Player.Inventory.Add(basicbolt);
            Player.Inventory.Add(arrows);
            Player.SecondaryWeapon = crossbow;
            

            NPC = new List<ICharacter>();

            var orc1 = helper.GenerateRandom(RaceEnum.Orc);
            var axe = new MeleeWeapon("Ugly Axe", "A really ugly axe.", new DamageRoll(1, 12, ElementEnum.Normal));
            orc1.Inventory.Add(axe);
            orc1.PrimaryWeapon = axe;
            orc1.Name = "Angry Orc";
            orc1.HP = 25;
            this.Wayfinder.Move(orc1, new Position(10, 10));
            orc1.Tasks.Add(new AttackPlayer());
            NPC.Add(orc1);

            //// Create a new orc portal            
            var orcPortal = new MonsterPortal(this, RaceEnum.Orc, 10, 4, 10, new Position(6, 6));
            this.RequiresATurn.Add(orcPortal);

            this.Designer.TopLeft = new Position(2, 2);
            this.Designer.BottomRight = new Position(5, 3);

        }

        public void NPCTurn()
        {
            // Give all the NPCs a chance to do their thing
            foreach (var npc in this.NPC)
            {
                npc.ExecuteTurn(this);
            }

            // and any special items
            var turnList = new List<IExecuteTurn>(this.RequiresATurn);
            foreach(IExecuteTurn executeTurn in turnList)
            {
                executeTurn.ExecuteTurn(this);
            }

        }

        public static Game LoadFrom(string filename, bool ignoreErrors = false)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();

                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                var game = formatter.Deserialize(stream) as Game;
                stream.Close();

                return game;
            }
            catch(Exception ex)
            {
                if (ignoreErrors)
                    return null;
                else
                    throw new Exception($"Error loading from file '{filename}'.", ex);
            }
        }



        public void SaveTo(string filename, bool ignoreErrors = false)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, this);
                stream.Close();
            }
            catch(Exception ex)
            {
                if (ignoreErrors)
                    return;
                else
                    throw new Exception($"Error saving file '{filename}'.", ex);
            }
        }

        public void SaveToJson(string filename)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };


            string json = JsonSerializer.Serialize(this, options);
            System.IO.File.WriteAllText(filename, json);
        }

        public static Game LoadFromJson(string filename)
        {
            var options = new JsonSerializerOptions
            {
                Converters = {
                    new TypeMappingConverter<IWayfinder, Wayfinder>(),
                    new TypeMappingConverter<ICharacter, BaseCharacter>(),
                    new TypeMappingConverter<ITile, Tile>()
                }
            };


            string json = System.IO.File.ReadAllText(filename);
            var game = JsonSerializer.Deserialize<Game>(json, options);

            return game;
        }

        private List<IMessage> _messages = new List<IMessage>();
        private int _turnNo = 0;

        public void LogMessage(string message)
        {
            var m = new Message(message, TurnNo);
            _messages.Add(m);
        }

        public List<IMessage> GetMessages(int startTurnNo, int endTurnNo, int maxTurns = int.MaxValue)
        {
            if (_messages == null)
                _messages = new List<IMessage>();

            var result = new List<IMessage>();
            var oldTurnNo = -1;
            int turnCount = 0;

            for (int i = _messages.Count - 1; i >= 0; i--)
            {
                var m = _messages[i];

                if (m.TurnNo <= endTurnNo && m.TurnNo >= startTurnNo)
                {                    
                    if (m.TurnNo != oldTurnNo)
                    {
                        oldTurnNo = m.TurnNo;

                        turnCount++;
                        if (turnCount > maxTurns)
                            break;
                    }
                    result.Add(m);
                }
            }

            result.Reverse();
            return result;
        }

        public void CompleteTurn()
        {
            _turnNo++;
        }
    }
}
