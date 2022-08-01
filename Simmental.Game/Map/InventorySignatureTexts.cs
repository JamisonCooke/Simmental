using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Map
{
    public class InventorySignatureTexts
    {
        
        public static void Execute(IGame game, Position startPosition, string inventorySignatureTexts)
        {
            string oldInventorySignatureText = game.Wayfinder[startPosition].Inventory.GetInventorySignatures();

            var doCommand = new UpdateInventory(startPosition, inventorySignatureTexts);
            var undoCommand = new UpdateInventory(startPosition, oldInventorySignatureText);

            game.CommandManager.ExecuteCommand(doCommand, undoCommand);
        }

    }
}
