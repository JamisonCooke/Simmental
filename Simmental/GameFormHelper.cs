using Simmental.Game.Animate;
using Simmental.Game.Command;
using Simmental.Game.Map;
using Simmental.Interfaces;
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

        public Simmental.Interfaces.IRenderHelper RenderHelper { get; set; }

        public void RefreshScreen(Graphics graphics, PictureBox pictureBox, int width, int height)
        {
            Game.Wayfinder.CameraWidth = width / Game.Wayfinder.TilePixelWidth + 1;
            Game.Wayfinder.CameraHeight = height / Game.Wayfinder.TilePixelHeight + 1;
            RenderGame renderer = new RenderGame();
            //graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            renderer.Render(Game, graphics, pictureBox);
        }

        public void RefreshTile(Graphics graphics, int i, int j)
        {
            RenderGame renderer = new RenderGame();
            renderer.RenderTile(Game, graphics, i, j);
        }

        public void PrimaryClick(int x, int y)
        {

            if (RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out int i, out int j))
            {
                var victim = Game.Wayfinder[i, j].NPCs.FirstOrDefault();
                if (victim == null) return;

                if (Game.Player.SecondaryWeapon == null)
                {
                    Game.LogMessage("You must be holding a secondary weapon (i.e., crossbow) to range attack.");
                    return;
                }

                var rangedWeapon = Game.Player.Inventory.RangedWeapons.FirstOrDefault(w => w.RangedWeaponType == Game.Player.SecondaryWeapon.RangedWeaponType);
                if (rangedWeapon == null)
                {
                    Game.LogMessage($"You do not have the right ammo to fire your weapon. You will need {Game.Player.SecondaryWeapon.RangedWeaponType}", true);
                    return;
                }

                Game.Player.Attack(Game, victim, rangedWeapon);
                CompleteTurn(true);

            }
        }



        public void RangeMouseDown(int x, int y)
        {

            int i, j;
            if (RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out i, out j))
            {
                this.Game.Designer.TopLeft = new Position(i, j);
                this.Game.Designer.BottomRight = new Position(i, j);
                GamePictureBox.Refresh();
            }
        }
        public void RangeMouseMove(int x, int y)
        {
            RangeMouseUp(x, y);

        }
        public void RangeMouseUp(int x, int y)
        {
            int i, j;
            if (RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out i, out j))
            {
                this.Game.Designer.BottomRight = new Position(i, j);
                GamePictureBox.Refresh();
            }
        }

        public void HandleKeyStroke(Keys keyCode)
        {
            ICharacter npc = null;

            // Someone pressed a key on the form!
            int i = Game.Player.Position.i;
            int j = Game.Player.Position.j;
            switch (keyCode)
            {
                case Keys.W:
                    if (CanWalkOn(i, j - 1, out npc))
                        j--;
                    break;

                case Keys.S:
                    if (CanWalkOn(i, j + 1, out npc))
                        j++;
                    break;

                case Keys.A:
                    if (CanWalkOn(i - 1, j, out npc))
                        i--;
                    Game.Player.IsLookingLeft = true;
                    break;

                case Keys.D:
                    if (CanWalkOn(i + 1, j, out npc))
                        i++;
                    Game.Player.IsLookingLeft = false;
                    break;

                case Keys.J:
                    Game.Wayfinder.XOffset--; GamePictureBox.Refresh();
                    break;
                case Keys.K:
                    Game.Wayfinder.XOffset++; GamePictureBox.Refresh();
                    break;
                case Keys.I:
                    Game.Wayfinder.YOffset--; GamePictureBox.Refresh();
                    break;
                case Keys.M:
                    Game.Wayfinder.YOffset++; GamePictureBox.Refresh();
                    break;

                case Keys.Space:
                    break; //Give the monsters a free turn

                default:    // Runs when no above cases are hit
                    return;
            }

            if (Game.Player.Position.i != i || Game.Player.Position.j != j)
            {
                if (KeepCameraOnPlayer(new Position(i, j)))
                {
                    using (Graphics g = GamePictureBox.CreateGraphics())
                    {
                        RefreshScreen(g, GamePictureBox, GamePictureBox.Width, GamePictureBox.Height);
                    }                        
                }

                Game.Player.Animations.Add(new Animation(Game, GraphicNameEnum.gregRun, DateTime.Now, new TimeSpan(0, 0, 0, 0, 350), new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, new TimeSpan(0, 0, 0, 0, 50), new Position(Game.Player.Position), new Position(i, j)));
                Game.Wayfinder.Move(Game.Player, new Position(i, j));

            }

            if (npc != null)
            {
                //Attack the npc!
                Game.Player.Animations.Add(new Animation(Game, GraphicNameEnum.gregAttack, DateTime.Now, new TimeSpan(0, 0, 0, 0, 350), new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, new TimeSpan(0, 0, 0, 0, 50), new Position(Game.Player.Position), new Position(i, j)));
                Game.Player.Attack(Game, npc, Game.Player.PrimaryWeapon);
                //MOVE ME
            }

            CompleteTurn(false);

        }

        private void CompleteTurn(bool updateInventory)
        {
            Game.NPCTurn();
            UpdateMessages();
            Game.CompleteTurn();
            UpdateInfoPanel(updateInventory);

            if (KeepCameraOnPlayer(Game.Player.Position))
                GamePictureBox.Refresh();
        }

        public void UpdateMessages()
        {
            this.SetMessages(CompileMessages());
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

        public bool KeepCameraOnPlayer(Position p)
        {
            int startI = Game.Wayfinder.CameraI;
            int startJ = Game.Wayfinder.CameraJ;

            Game.Wayfinder.CameraI = p.i - Game.Wayfinder.CameraWidth / 2;
            Game.Wayfinder.CameraJ = p.j - Game.Wayfinder.CameraHeight / 2;
            if (Game.Wayfinder.CameraI < 0)
                Game.Wayfinder.CameraI = 0;
            if (Game.Wayfinder.CameraJ < 0)
                Game.Wayfinder.CameraJ = 0;
            
            if (Game.Wayfinder.CameraI > Game.Wayfinder.Width - Game.Wayfinder.CameraWidth + 1)
                Game.Wayfinder.CameraI = Game.Wayfinder.Width - Game.Wayfinder.CameraWidth + 1;
            if (Game.Wayfinder.CameraJ > Game.Wayfinder.Height - Game.Wayfinder.CameraHeight + 1)
                Game.Wayfinder.CameraJ = Game.Wayfinder.Height - Game.Wayfinder.CameraHeight + 1;

            return (startI != Game.Wayfinder.CameraI || startJ != Game.Wayfinder.CameraJ);
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
            var tile = Game.Wayfinder[i, j];

            foreach (var monster in tile.NPCs)
            {   
                npc = monster;
                return false;
            }

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
        public void DesignerUpdateTiles(TileEnum tileType)
        {
            var doList = new List<ICommandBase>();
            var undoList = new List<ICommandBase>();

            foreach (var position in Game.Designer.SelectedPositions(Game.Wayfinder))
            {
                var tile = Game.Wayfinder[position];
                if (tileType == tile.TileType)
                    continue;

                doList.Add(new UpdateTile(position, tileType, tile.TileAttribute));
                undoList.Add(new UpdateTile(position, tile.TileType, tile.TileAttribute));
            }

            if (doList.Count > 0)
                Game.CommandManager.ExecuteCommand(doList, undoList);


            GamePictureBox.Refresh();
        }

        public void DesignerUpdateAttribute(TileAttributeEnum tileAttribute)
        {
            var doList = new List<ICommandBase>();
            var undoList = new List<ICommandBase>();

            foreach (var position in Game.Designer.SelectedPositions(Game.Wayfinder))
            {
                var tile = Game.Wayfinder[position];
                if (tileAttribute == tile.TileAttribute)
                    continue;

                doList.Add(new UpdateTile(position, tile.TileType, tileAttribute));
                undoList.Add(new UpdateTile(position, tile.TileType, tile.TileAttribute));
            }

            if (doList.Count > 0)
                Game.CommandManager.ExecuteCommand(doList, undoList);


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

        public void ApplyTileToRange(ITile copyTile)
        {
            if (copyTile == null)
                return;

            var doList = new List<ICommandBase>();
            var undoList = new List<ICommandBase>();

            foreach (var position in Game.Designer.SelectedPositions(Game.Wayfinder))
            {
                var tile = Game.Wayfinder[position];
                if (copyTile.TileType == tile.TileType && copyTile.TileAttribute == tile.TileAttribute)
                    continue;

                doList.Add(new UpdateTile(position, copyTile.TileType, copyTile.TileAttribute));
                undoList.Add(new UpdateTile(position, tile.TileType, tile.TileAttribute));
            }

            if (doList.Count > 0)
                Game.CommandManager.ExecuteCommand(doList, undoList);
        }

        public void ZoomUI(int value)
        {
            Game.Wayfinder.TilePixelHeight = value;
            Game.Wayfinder.TilePixelWidth = value;
            KeepCameraOnPlayer(Game.Player.Position);
            GamePictureBox.Refresh();

        }
    }
}
