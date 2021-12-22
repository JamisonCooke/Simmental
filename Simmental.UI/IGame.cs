using Simmental.UI;
using System;
using System.Collections.Generic;

namespace Simmental.UI
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
        void LogMessage(string message);
        List<IMessage> GetMessages(int startTurnNo, int turnNumber, int maxTurns);
        void CompleteTurn();
        int TurnNo { get;  }
        static Random Random { get; }

    }

}