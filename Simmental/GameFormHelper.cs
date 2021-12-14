﻿using Simmental.UI;
using Simmental.UI.WinForm.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simmental
{
    class GameFormHelper
    {

        /// <summary>
        /// Referemce to the current game in play
        /// </summary>
        public Simmental.Game.Engine.Game Game { get; set; }

        public PictureBox GamePictureBox { get; set; }

        public Action<string> SetMessages { get; set; }
        public Action<bool> UpdateInfoPanel { get; set; }

        public Simmental.UI.IRenderHelper RenderHelper { get; set; }

        public void RefreshScreen(Graphics graphics, int width, int height)
        {
            Game.Wayfinder.CameraWidth = width / Game.Wayfinder.TilePixelWidth + 1;
            Game.Wayfinder.CameraHeight = height / Game.Wayfinder.TilePixelHeight + 1;
            RenderGame renderer = new RenderGame();
            renderer.Render(Game, graphics);
        }

        public void PrimaryClick(int x, int y)
        {

            if (RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out int i, out int j))
            {
                var victim = Game.NPC.FirstOrDefault(m => m.Position.i == i && m.Position.j == j);
                if (victim == null) return;

                if (Game.Player.SecondaryWeapon == null)
                {
                    Game.LogMessage("You must be holding a secondary weapon (i.e., crossbow) to range attack.");
                    return;
                }

                var rangedWeapon = Game.Player.Inventory.RangedWeapons.FirstOrDefault(w => w.RangedWeaponType == Game.Player.SecondaryWeapon.RangedWeaponType);
                if (rangedWeapon == null)
                {
                    Game.LogMessage($"You do not have the right ammo to fire your weapon. You will need {Game.Player.SecondaryWeapon.RangedWeaponType}");
                    return;
                }

                Game.Player.Attack(Game, victim, rangedWeapon);
                CompleteTurn(true);

            }
        }



        public void DesignerMouseDown(int x, int y)
        {

            int i, j;
            if (RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out i, out j))
            {
                this.Game.Designer.TopLeft.i = i;
                this.Game.Designer.TopLeft.j = j;
                this.Game.Designer.BottomRight.i = i;
                this.Game.Designer.BottomRight.j = j;
                GamePictureBox.Refresh();
            }
        }
        public void DesignerMouseMove(int x, int y)
        {
            DesignerMouseUp(x, y);

        }
        public void DesignerMouseUp(int x, int y)
        {
            int i, j;
            if (RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out i, out j))
            {
                this.Game.Designer.BottomRight.i = i;
                this.Game.Designer.BottomRight.j = j;
                GamePictureBox.Refresh();
            }
        }

        public void HandleKeyStroke(Keys keyCode)
        {
            ICharacter npc = null;

            // Someone pressed a key on the form!
            switch (keyCode)
            {
                case Keys.W:
                    if (CanWalkOn(Game.Player.Position.i, Game.Player.Position.j - 1, out npc))
                        Game.Player.Position.j--;
                    break;

                case Keys.S:
                    if (CanWalkOn(Game.Player.Position.i, Game.Player.Position.j + 1, out npc))
                        Game.Player.Position.j++;
                    break;

                case Keys.A:
                    if (CanWalkOn(Game.Player.Position.i - 1, Game.Player.Position.j, out npc))
                        Game.Player.Position.i--;
                    break;

                case Keys.D:
                    if (CanWalkOn(Game.Player.Position.i + 1, Game.Player.Position.j, out npc))
                        Game.Player.Position.i++;
                    break;
                case Keys.Space:
                    break; //Give the monsters a free turn

                default:    // Runs when no above cases are hit
                    return;
            }
            if (npc != null)
            {
                //Attack the npc!
                Game.Player.Attack(Game, npc, Game.Player.PrimaryWeapon);
                //MOVE ME
            }

            CompleteTurn(false);

        }

        private void CompleteTurn(bool updateInventory)
        {
            Game.NPCTurn();
            this.SetMessages(CompileMessages());
            Game.CompleteTurn();
            UpdateInfoPanel(updateInventory);

            KeepCameraOnPlayer();
            GamePictureBox.Refresh();
        }

        public string CompileMessages()
        {
            StringBuilder sb = new StringBuilder();

            var turnMessages = Game.GetMessages(0, Game.TurnNo, 20);
            turnMessages.Reverse();

            int oldTurnNo = -1;
            List<IMessage> group = new List<IMessage>();

            foreach(var m in turnMessages)
            {

                if (oldTurnNo != m.TurnNo)
                {
                    // Print out the messages from the last section
                    AddGrouptoSB(sb, group);

                    oldTurnNo = m.TurnNo;
                    group.Clear();
                }
                group.Add(m);
            }

            AddGrouptoSB(sb, group);

            // right here

            return sb.ToString();
        }

        private void AddGrouptoSB(StringBuilder sb, List<IMessage> group)
        {
            if (group.Count == 0)       // No messases? No need.
                return;

            // Re-reverse the group
            group.Reverse();
            sb.AppendLine($"Turn {group[0].TurnNo}:");

            foreach (var gm in group)
                sb.AppendLine(gm.MessageText);

            sb.AppendLine();
        }

        public void KeepCameraOnPlayer()
        {
            Game.Wayfinder.CameraI = Game.Player.Position.i - Game.Wayfinder.CameraWidth / 2;
            Game.Wayfinder.CameraJ = Game.Player.Position.j - Game.Wayfinder.CameraHeight / 2;
            if (Game.Wayfinder.CameraI < 0)
                Game.Wayfinder.CameraI = 0;
            if (Game.Wayfinder.CameraJ < 0)
                Game.Wayfinder.CameraJ = 0;
            
            if (Game.Wayfinder.CameraI > Game.Wayfinder.Width - Game.Wayfinder.CameraWidth + 1)
                Game.Wayfinder.CameraI = Game.Wayfinder.Width - Game.Wayfinder.CameraWidth + 1;
            if (Game.Wayfinder.CameraJ > Game.Wayfinder.Height - Game.Wayfinder.CameraHeight + 1)
                Game.Wayfinder.CameraJ = Game.Wayfinder.Height - Game.Wayfinder.CameraHeight + 1;
            
        }

        /// <summary>
        /// Returns true if the user can move the Player object to the passed (i,j) coordinate
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool CanWalkOn(int i, int j, out ICharacter npc)
        {
            npc = null;

            if (i < 0 || i >= Game.Wayfinder.Width)
                return false;
            if (j < 0 || j >= Game.Wayfinder.Height)
                return false;

            // Check the NPC's if they are on the (i,j) coordinate
            foreach(var monster in Game.NPC)
            {
                if (monster.Position.i == i && monster.Position.j == j)
                {
                    npc = monster;
                    return false;
                }
            }


            var tile = Game.Wayfinder[i, j];

            return (tile.TileAttribute & TileAttributeEnum.CanWalkOn) == TileAttributeEnum.CanWalkOn;
        }

        public void SetCamera(int i, int j)
        {
            Game.Wayfinder.CameraI = i;
            Game.Wayfinder.CameraJ = j;
            GamePictureBox.Refresh();

        }

        /// <summary>
        /// Update every selected tile and change the tileType to the passed tileEnum
        /// </summary>
        /// <param name="tileEnum"></param>
        public void DesignerUpdateTiles(TileEnum tileEnum)
        {
            foreach (var tile in Game.Designer.SelectedTiles(Game.Wayfinder))
            {
                    tile.TileType = tileEnum;
            }
            GamePictureBox.Refresh();

        }

        public void DesignerUpdateAttribute(TileAttributeEnum tileAttributes)
        {
            foreach (var tile in Game.Designer.SelectedTiles(Game.Wayfinder))
            {
                tile.TileAttribute = tileAttributes;
            }
            GamePictureBox.Refresh();
        }



        /// <summary>
        /// Set the dropdowns to match the current selection
        /// </summary>
        public ITile FindCommonTileProperties()
        {
            ITile result = new Game.Map.Tile();
            result.TileType = TileEnum.None;
            bool firstTime = true;
            ITile firstTile = null;
            int count = 0;

            foreach (var tile in Game.Designer.SelectedTiles(Game.Wayfinder))
            {
                count++;
                if (firstTime)
                {
                    firstTile = tile;
                    firstTime = false;
                    result.TileType = firstTile.TileType;
                    result.TileAttribute = firstTile.TileAttribute;
                    continue;
                }
                if (tile.TileType != firstTile.TileType)
                {
                    result.TileType = TileEnum.None;
                }
                result.TileAttribute &= tile.TileAttribute;
            }

            if (count == 1)
                return firstTile;

            return result;
        }

        public void ZoomUI(int value)
        {
            Game.Wayfinder.TilePixelHeight = value;
            Game.Wayfinder.TilePixelWidth = value;
            KeepCameraOnPlayer();
            GamePictureBox.Refresh();

        }
    }
}
