using Simmental.UI;
using Simmental.Game.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simmental.UI.WinForm.Render;
using Simmental.Game.Map;
using System.IO;
using Simmental.Helper;

namespace Simmental
{
    public partial class SimmentalForm : Form
    {

        // Helper references to cut down on typing
        IGame Game => _gameFormHelper.Game;
        ICharacter Player => _gameFormHelper.Game.Player;
        IWayfinder Wayfinder => _gameFormHelper.Game.Wayfinder;


        public SimmentalForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private GameFormHelper _gameFormHelper;

        private string AutoSaveFilename => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Simmental", "AutoSave.sav");

        private void SimmentalForm_Load(object sender, EventArgs e)
        {
            /*
            var helper = new Simmental.Game.Characters.CharacdesignerterHelper();
            var c = helper.GenerateRandom();

            nameTextBox.Text = c.Name;
            strengthTextBox.Text = c.Strength.ToString();
            intelligenceTextBox.Text = c.Intelligence.ToString();
            dexterityTextBox.Text = c.Dexterity.ToString();
            constitutionTextBox.Text = c.Constitution.ToString();
            wisdomTextBox.Text = c.Wisdom.ToString();
            charismaTextBox.Text = c.Charisma.ToString();
            */
            AlignDesignerPanel(false);

            InitializeGame();

        }

        private void InitializeGame(bool hardreset = false)
        {
            _gameFormHelper = new GameFormHelper();

            var lastGame = Simmental.Game.Engine.Game.LoadFrom(AutoSaveFilename, true);
            if (lastGame == null || hardreset)
            {
                // No saved game? New up a random game
                _gameFormHelper.Game = new Simmental.Game.Engine.Game();
                _gameFormHelper.Game.InitalizeRandom();
            }
            else 
            {
                _gameFormHelper.Game = lastGame;
            }
            _gameFormHelper.GamePictureBox = this.mapPictureBox;
            _gameFormHelper.RenderHelper = new Simmental.UI.WinForm.Render.RenderHelper();
            _gameFormHelper.SetMessages = UpdateMessageText;
            _gameFormHelper.UpdateInfoPanel = UpdateInfoPanel;
            _gameFormHelper.Game.UpdateMessages = _gameFormHelper.UpdateMessages;

            UpdateMessageText(_gameFormHelper.CompileMessages());
            UpdateInfoPanel(true);
        }

        private void UpdateInfoPanel(bool updateInventory)
        {
            string stats = $"HP {_gameFormHelper.Game.Player.HP} ({_gameFormHelper.Game.Player.GetMaxHP()})";

            if (_gameFormHelper.Game.Player.HP <= 0)
                _gameFormHelper.Game.Player.HP = 50;        // Revived

            playerStatLine.Text = stats;

            tileInventory.Items.Clear();
            DisplayInventoryRecursive(tileInventory, Wayfinder[Player.Position].Inventory);
            if (updateInventory)
                UpdateCharacterInventory();

        }

        private void DisplayInventoryRecursive(ListBox listBox, IInventory inventory, string prefix = "")
        {
            foreach (IItem item in inventory.Items)
            {
                listBox.Items.Add(new Helper.ItemWrapper(item, inventory, prefix + item.GetFullName()));
                IInventory inventory2 = item as IInventory;
                if (inventory2 != null)
                {
                    DisplayInventoryRecursive(listBox, inventory2, prefix + "  ");
                }
            }
        }

        private void UpdateMessageText(string messageText)
        {
            messagesTextBox.Text = messageText;
        }


        private void mapPictureBox_Click(object sender, EventArgs e)
        {

        }

        private Font _defaultText = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);

        private void mapPictureBox_Paint(object sender, PaintEventArgs e)
        {
            _gameFormHelper.RefreshScreen(e.Graphics, mapPictureBox.Width, mapPictureBox.Height);

        }

        private void SimmentalForm_KeyDown(object sender, KeyEventArgs e)
        {

            _gameFormHelper.HandleKeyStroke(e.KeyCode);

        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Simmental Save Game (*.sav)|*.sav|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                _gameFormHelper.Game = Simmental.Game.Engine.Game.LoadFrom(filename);
                mapPictureBox.Refresh();
            }


            //string filename = @"C:\Users\cowma\OneDrive\Documents\Simmental\SaveGame.Json";
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Simmental Save Game (*.sav)|*.sav|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            //string filename = ""; // @"C:\Users\cowma\OneDrive\Documents\Simmental\SaveGame.Json";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                var game = _gameFormHelper.Game as Simmental.Game.Engine.Game;
                game.SaveTo(filename); 
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            _gameFormHelper.SetCamera(hScrollBar1.Value, vScrollBar1.Value);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            _gameFormHelper.SetCamera(hScrollBar1.Value, vScrollBar1.Value);
        }

        private void mapPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (designerPanel.Visible)
                _gameFormHelper.DesignerMouseDown(e.X, e.Y);
            else if (e.Button == MouseButtons.Left)
                _gameFormHelper.PrimaryClick(e.X, e.Y);
            else if (e.Button == MouseButtons.Right)
                RightMouseClickMonster(e.X, e.Y);


        }

        private void RightMouseClickMonster(int x, int y)
        {
            if (_gameFormHelper.RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out int i, out int j))
            {
                inventoryContextMenu.Items.Clear();

                var victim = Game.Wayfinder[i, j].NPCs.FirstOrDefault();
                if (victim == null) return;

                //inventoryContextMenu.Items.Add("Player", null, (s, e) => MoveInventory(clickedOnItem, Player.Inventory));
                
                    
                    
                inventoryContextMenu.Show(Cursor.Position);
            }
        }

        private void mapPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //int i, j;
            //_gameFormHelper.RenderHelper.GetTileIndex(_gameFormHelper.Game.Wayfinder, e.X, e.Y, out i, out j);
            //var to = new Position(i, j);
            
            //if (e.Button == MouseButtons.Right)
            //{
            //    int stopMe = 12;
            //}
            
            //bool isVisible = _gameFormHelper.Game.Wayfinder.IsVisible(_gameFormHelper.Game.Player.Position, to, 10);
            //this.Text = $"({i},{j}) {isVisible}";
            if (e.Button == MouseButtons.Left && designerPanel.Visible)
                _gameFormHelper.DesignerMouseMove(e.X, e.Y);
        }

        private bool _updatingUI = false;

        private void mapPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _updatingUI = true;

            if (designerPanel.Visible)
                _gameFormHelper.DesignerMouseUp(e.X, e.Y);

            ITile tile = _gameFormHelper.FindCommonTileProperties();

            if (tile.TileType == TileEnum.None)
                tileTypeComboBox.Text = "";
            else
                tileTypeComboBox.Text = tile.TileType.ToString();

            var att = tile.TileAttribute;
            WalkCheckBox.Checked = (att & TileAttributeEnum.CanWalkOn) == TileAttributeEnum.CanWalkOn;
            FlyCheckBox.Checked = (att & TileAttributeEnum.CanFlyOver) == TileAttributeEnum.CanFlyOver;
            KillCheckBox.Checked = (att & TileAttributeEnum.WillKillYou) == TileAttributeEnum.WillKillYou;
            OpaqueCheckBox.Checked = (att & TileAttributeEnum.Opaque) == TileAttributeEnum.Opaque;
            

            _updatingUI = false;

        }

        private void tileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updatingUI) return;

            // grass, stone, water, wall, wood
            if (Enum.TryParse<TileEnum>(tileTypeComboBox.Text, out TileEnum tileEnum))
            {
                _gameFormHelper.DesignerUpdateTiles(tileEnum);
            }
        }

        private void descriptionPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }   

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            // This textbox is hidden behind the picture box. We use it to capture keystrokes
            // Just clean up the text so it doesn't hit the max input for a textbox.
            textBox1.Text = "";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            _gameFormHelper.HandleKeyStroke(e.KeyCode);
            SyncScrollbars();
        }

        private void SyncScrollbars()
        {
            if (_gameFormHelper.Game.Wayfinder.CameraI != hScrollBar1.Value && _gameFormHelper.Game.Wayfinder.CameraI >= 0)
                hScrollBar1.Value = _gameFormHelper.Game.Wayfinder.CameraI;
            if (_gameFormHelper.Game.Wayfinder.CameraJ != vScrollBar1.Value && _gameFormHelper.Game.Wayfinder.CameraJ >= 0)
                vScrollBar1.Value = _gameFormHelper.Game.Wayfinder.CameraJ;

        }

        /// <summary>
        /// Called by all three attribute checkboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTileAttributes(object sender, EventArgs e)
        {
            if (_updatingUI) return;

            TileAttributeEnum att = TileAttributeEnum.None;

            if (WalkCheckBox.Checked)
            {
                att |= TileAttributeEnum.CanWalkOn;
            }
            if (FlyCheckBox.Checked)
            {
                att |= TileAttributeEnum.CanFlyOver;
            }
            if (KillCheckBox.Checked)
            {
                att |= TileAttributeEnum.WillKillYou;
            }
            if (OpaqueCheckBox.Checked)
            {
                att |= TileAttributeEnum.Opaque;
            }

            _gameFormHelper.DesignerUpdateAttribute(att);
        }

        private void SimmentalForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var game = _gameFormHelper.Game as Simmental.Game.Engine.Game;
            game.SaveTo(AutoSaveFilename, true);

        }

        private void zoomTrackBar_Scroll(object sender, EventArgs e)
        {
            _gameFormHelper.ZoomUI(zoomTrackBar.Value);
        }

        private void designerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAvailable = !designerToolStripMenuItem.Checked;
            AlignDesignerPanel(isAvailable);
        }

        private void AlignDesignerPanel(bool isAvailable)
        {
            int panelWidth = isAvailable ? designerPanel.Width : 0;

            designerToolStripMenuItem.Checked = isAvailable;
            designerPanel.Visible = isAvailable;

            vScrollBar1.Left = this.ClientSize.Width - vScrollBar1.Width - panelWidth;
            mapPanel.Width = this.ClientSize.Width - vScrollBar1.Width - mapPanel.Left - panelWidth;
            mapPictureBox.Width = mapPanel.Width;
            hScrollBar1.Width = this.ClientSize.Width - vScrollBar1.Width - hScrollBar1.Left - panelWidth;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void mapResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeGame(true);
        }

        private void characterInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            characterInfoPanel.Visible = !characterInfoPanel.Visible;
            UpdateCharacterInventory();

        }

        private void UpdateCharacterInventory()
        {
            if (characterInfoPanel.Visible)
            {
                PrimaryWeaponLabel.Text = $"Primary: {Player?.PrimaryWeapon?.GetFullName()}";
                SecondaryWeaponLabel.Text = $"Secondary: {Player?.SecondaryWeapon?.GetFullName()}";

                playerInventoryListBox.Items.Clear();
                DisplayInventoryRecursive(playerInventoryListBox, Player.Inventory);
            }
        }

        private void tileInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            InventoryClick(tileInventory);
        }

        private void InventoryClick(ListBox listBox)
        {
            if (listBox.SelectedIndex < 0) return;

            var clickedOnItem = listBox.Items[listBox.SelectedIndex] as ItemWrapper;

            // show the context menu
            inventoryContextMenu.Items.Clear();

            var moveLabel = new ToolStripLabel("Move To");
            moveLabel.Font = new Font(moveLabel.Font, FontStyle.Bold);
            
            inventoryContextMenu.Items.Add(moveLabel);
            inventoryContextMenu.Items.Add("Player", null, (s, e) => MoveInventory(clickedOnItem, Player.Inventory));
            AddInventoryToContextMenu(inventoryContextMenu, clickedOnItem, Player.Inventory, "  ");

            inventoryContextMenu.Items.Add("Floor", null, (s, e) => MoveInventory(clickedOnItem, Wayfinder[Player.Position].Inventory));
            AddInventoryToContextMenu(inventoryContextMenu, clickedOnItem, Wayfinder[Player.Position].Inventory, "  ");

            if (clickedOnItem.Item is IWeapon)
            {
                var weaponLabel = new ToolStripLabel("Hold Weapon");
                weaponLabel.Font = new Font(moveLabel.Font, FontStyle.Bold);
                inventoryContextMenu.Items.Add(weaponLabel);
                inventoryContextMenu.Items.Add("Use as Primary", null, (s, e) => WieldWeapon(clickedOnItem));
                inventoryContextMenu.Items.Add("Use as Secondary", null, (s, e) => WieldWeaponSecondary(clickedOnItem));
            }

            inventoryContextMenu.Show(Cursor.Position);
        }

        /// <summary>
        /// When updating WieldWeapon, make sure to update counterpart
        /// </summary>
        /// <param name="clickedOnItem"></param>
        private void WieldWeapon(ItemWrapper clickedOnItem)
        {
            Player.PrimaryWeapon = clickedOnItem.Item as IWeapon;
            MoveInventory(clickedOnItem, Player.Inventory);
        }

        private void WieldWeaponSecondary(ItemWrapper clickedOnItem)
        {
            Player.SecondaryWeapon = clickedOnItem.Item as IWeapon;
            MoveInventory(clickedOnItem, Player.Inventory);
        }

        private void AddInventoryToContextMenu(ContextMenuStrip inventoryContextMenu, ItemWrapper clickedOnItem, IInventory inventory, string prefix = "")
        {
            foreach (IItem item in inventory.Items)
            {
                IInventory inventory2 = item as IInventory;
                if (inventory2 != null)
                {
                    inventoryContextMenu.Items.Add(prefix + item.GetFullName(), null, (s, e) => MoveInventory(clickedOnItem, inventory2));
                    AddInventoryToContextMenu(inventoryContextMenu, clickedOnItem, inventory2, prefix + "  ");
                }
            }
        }

        private void MoveInventory(ItemWrapper itemWrapper, IInventory destinationInventory, bool withUIrefresh = true)
        {
            destinationInventory.Add(itemWrapper.Item);
            itemWrapper.Inventory.Remove(itemWrapper.Item);


            if (itemWrapper.Item == Player.PrimaryWeapon && destinationInventory != Player.Inventory)
            {
                Player.PrimaryWeapon = null;
                Game.LogMessage("WARNING: You have no primary weapon.");
            }

            if (itemWrapper.Item == Player.SecondaryWeapon && destinationInventory != Player.Inventory)
            {
                Player.SecondaryWeapon = null;
                Game.LogMessage("Careful, you have no secondary weapon.");
            }

            if (withUIrefresh)
            {
                // update what is in your personal inventory
                UpdateCharacterInventory();

                // Update what is on the floor
                tileInventory.Items.Clear();
                DisplayInventoryRecursive(tileInventory, Wayfinder[Player.Position].Inventory);
            }
        }


        private void PlayerInventoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tileInventory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tileInventory.SelectedIndex < 0)
                return;

            var seletedItem = tileInventory.SelectedItem as ItemWrapper;
            MoveInventory(seletedItem, Player.Inventory);
        }

        private void tileInventory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Right clicked the tile inventory
                tileInventory.SelectedIndex = tileInventory.IndexFromPoint(e.X, e.Y);
                InventoryClick(tileInventory);
            }
        }

        private void playerInventoryListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (playerInventoryListBox.SelectedIndex < 0)
                return;
            
            var selectedItem = playerInventoryListBox.SelectedItem as ItemWrapper;
            MoveInventory(selectedItem, Wayfinder[Player.Position].Inventory);
        }

        private void playerInventoryListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Right clicked the player inventory
                playerInventoryListBox.SelectedIndex = playerInventoryListBox.IndexFromPoint(e.X, e.Y);
                InventoryClick(playerInventoryListBox);
            }

        }

        private void tileInventory_MouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}
