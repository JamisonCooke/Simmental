using Simmental.Interfaces;
using System;
using System.Collections.Generic;

namespace Simmental.Interfaces
{
    public interface IGame
    {
        List<ICharacter> NPC { get; }
        List<IExecuteTurn> RequiresATurn { get; }
        ICharacter Player { get; }
        IWayfinder Wayfinder { get; }

        void InitalizeRandom();
        void NPCTurn();
        IDesigner Designer { get; }
        void LogMessage(string message, bool displayNow = false);
        Action UpdateMessages { get; set; }
        List<IMessage> GetMessages(int startTurnNo, int turnNumber, int maxTurns);
        void CompleteTurn();
        int TurnNo { get;  }
        static Random Random { get; }
        IEnumerable<(ILightSource LightSource, Position Position)> GetLightSources();
        ICommandManager CommandManager { get; }
    }

}