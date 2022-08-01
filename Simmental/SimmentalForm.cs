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
using Simmental.Game.Command;
using Simmental.Game.Signatures;

namespace Simmental
{
    public partial class SimmentalForm : Form
    {

        // Helper references to cut down on typing
        IGame Game => _gameFormHelper.Game;
        ICharacter Player => _gameFormHelper.Game.Player;
        IWayfinder Wayfinder => _gameFormHelper.Game.Wayfinder;

        private enum DesignerMode
        {
            Pencil,
            Range,
            EyeDropper,
            Bucket
        }

        private DesignerMode _designerMode = DesignerMode.Range;
        private DesignerMode _priorDesignerMode = DesignerMode.Range;


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
            InitializeGame();

            AlignDesignerPanel(false);
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
            if (designerToolStripMenuItem.Checked)
            {
                if (_designerMode == DesignerMode.Range)
                    _gameFormHelper.RangeMouseDown(e.X, e.Y);
                else if (_designerMode == DesignerMode.EyeDropper)
                    UseEyedropper(e.X, e.Y);
                else if (_designerMode == DesignerMode.Pencil)
                    StartDesignerPen(e.X, e.Y);
                else if (_designerMode == DesignerMode.Bucket)
                    ApplyBucket(e.X, e.Y);
                return;
            }

            if (e.Button == MouseButtons.Left)
                _gameFormHelper.PrimaryClick(e.X, e.Y);
            if (e.Button == MouseButtons.Right)
                RightMouseClickMonster(e.X, e.Y);
            


        }

        private void ApplyBucket(int x, int y)
        {
            if (_gameFormHelper.RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out int i, out int j))
            {
                if (Enum.TryParse<TileEnum>(tileTypeComboBox.Text, out TileEnum tileEnum))
                {
                    FloodFill.Execute(Game, new Position(i, j), GetDesignerControlAtts(), tileEnum);
                    mapPictureBox.Refresh();
                }
            }

        }

        private void RightMouseClickMonster(int x, int y)
        {
            if (_gameFormHelper.RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out int i, out int j))
            {
                inventoryContextMenu.Items.Clear();

                var victim = Game.Wayfinder[i, j].NPCs.FirstOrDefault();
                if (victim == null) return;     
                inventoryContextMenu.Show(Cursor.Position);
            }
        }

        private ITile UseEyedropper(int x, int y)
        {
            if (_gameFormHelper.RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out int i, out int j))
            {
                ITile tile = Game.Wayfinder[i, j];
                SetDesignerTileProperties(tile);
                return tile;
            }
            return null;
        }
        private void mapPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _designerMode == DesignerMode.Range)
                _gameFormHelper.RangeMouseMove(e.X, e.Y);
            if (e.Button == MouseButtons.Left && _designerMode == DesignerMode.EyeDropper)
                UseEyedropper(e.X, e.Y);
            if (e.Button == MouseButtons.Left && _designerMode == DesignerMode.Pencil)
                ApplyDesignerPen(e.X, e.Y);
        }

        private bool _updatingUI = false;

        private void mapPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_designerMode == DesignerMode.Range)
            {
                _gameFormHelper.RangeMouseUp(e.X, e.Y);
                UpdateDesignerControls();
            }
            else if (_designerMode == DesignerMode.EyeDropper)
            {
                if (_priorDesignerMode == DesignerMode.Range)
                {
                    ITile tile = UseEyedropper(e.X, e.Y);
                    _gameFormHelper.ApplyTileToRange(tile);
                    mapPictureBox.Refresh();
                }

                // Switch back to prior mode!
                _designerMode = _priorDesignerMode;
                ApplyDesignerModeToToolbar();
            }  
            else if (e.Button == MouseButtons.Left && _designerMode == DesignerMode.Pencil) 
            {
                EndDesignerPen();
            }
        }

        private void UpdateDesignerControls()
        {
            // No need to do this if the designer is not open
            if (!designerToolStripMenuItem.Checked)
                return;

            ITile tile = _gameFormHelper.FindCommonTileProperties();
            SetDesignerTileProperties(tile);

            // 
            var singleTile = _gameFormHelper.Game.Designer.SelectedTile(Wayfinder);
            if (singleTile != null)
            {
                tileInventoryTextBox.Text = singleTile.Inventory.GetInventorySignatures();
                
                tileInventoryTextBox.Enabled = true;
                tileInventoryApplyButton.Enabled = true;
            }
            else
            {
                tileInventoryTextBox.Enabled = false;
                tileInventoryApplyButton.Enabled = false;
                tileInventoryTextBox.Text = "";
                tileInventoryTextBox.Text = "";
            }
            inventoryErrorLabel.Text = "";
        }

        private void ApplyDesignerModeToToolbar()
        {
            // Make sure the checked toolbar icons match _designerMode
            // TODO: Need to fix name cosistency 
            designerEyeDropper.Checked = (_designerMode == DesignerMode.EyeDropper);
            designerRangeSelector.Checked = (_designerMode == DesignerMode.Range);
            designerFloodFill.Checked = (_designerMode == DesignerMode.Bucket);
            designerPen.Checked = (_designerMode == DesignerMode.Pencil);

        }
        List<ICommandBase> _doList;
        List<ICommandBase> _undoList;

        private void StartDesignerPen(int x, int y)
        {
            _doList = new();
            _undoList = new();
            ApplyDesignerPen(x, y);
        }
        private void ApplyDesignerPen(int x, int y)
        {
            var penAtt = GetDesignerControlAtts();
            var penTileType = GetDesignerTileType();

            // Applies the settings in the checkboxes / dropdowns to the tiles TileAttributes and TileStyle 
            int i, j;   // Convert (x,y) to (i,j)
            if (!_gameFormHelper.RenderHelper.GetTileIndex(Game.Wayfinder, x, y, out i, out j))
                return;

            ITile tile = Wayfinder[i, j];
                
            // See if it's already set
            if (tile.TileType == penTileType && tile.TileAttribute == penAtt)
                return;

            _doList.Add(new UpdateTile(new Position(i, j), penTileType, penAtt));
            _undoList.Add(new UpdateTile(new Position(i, j), tile.TileType, tile.TileAttribute));

            // Update the tile based on what was picked in the designer popout
            tile.TileAttribute = penAtt;
            tile.TileType = penTileType;

            // Repaint the one cell
            _gameFormHelper.RefreshTile(mapPictureBox.CreateGraphics(), i, j);
        }

        private void EndDesignerPen()
        {
            Game.CommandManager.ExecuteCommand(_doList, _undoList);
        }

        private void SetDesignerTileProperties(ITile tile)
        {
            _updatingUI = true;

            if (tile.TileType == TileEnum.None)
                tileTypeComboBox.Text = "";
            else
                tileTypeComboBox.Text = tile.TileType.ToString();

            WalkCheckBox.Checked = tile.HasAttribute(TileAttributeEnum.CanWalkOn);
            FlyCheckBox.Checked = tile.HasAttribute(TileAttributeEnum.CanFlyOver);
            KillCheckBox.Checked = tile.HasAttribute(TileAttributeEnum.WillKillYou);
            OpaqueCheckBox.Checked = tile.HasAttribute(TileAttributeEnum.Opaque);

            _updatingUI = false;
        }

        private void tileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updatingUI) return;

            if (_designerMode == DesignerMode.Range)
            {
                // grass, stone, water, wall, wood
                if (Enum.TryParse<TileEnum>(tileTypeComboBox.Text, out TileEnum tileEnum))
                {
                    _gameFormHelper.DesignerUpdateTiles(tileEnum);
                }
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

            if (_designerMode == DesignerMode.Range)
            {
                TileAttributeEnum att = GetDesignerControlAtts();
                _gameFormHelper.DesignerUpdateAttribute(att);
            }
        }

        private TileAttributeEnum GetDesignerControlAtts()
        {
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
            return att;
        }

        private TileEnum GetDesignerTileType()
        {
            TileEnum tileEnum = TileEnum.None;

            // Parse the text in the combobox that says "Water" or "Grass" or whatever...
            Enum.TryParse<TileEnum>(tileTypeComboBox.Text, out tileEnum);
            
            return tileEnum;
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
            UpdateDesignerControls();
        }

        private void AlignDesignerPanel(bool isAvailable)
        {
            bool refresh = false;

            int panelWidth = isAvailable ? designerPanel.Width : 0;

            designerToolStripMenuItem.Checked = isAvailable;
            designerPanel.Visible = isAvailable;

            bool newHighlightRange = isAvailable && ((_designerMode == DesignerMode.Range) || (_priorDesignerMode == DesignerMode.Range));
            if (newHighlightRange != Game.Designer.HighlightRange)
            {
                refresh = true;
                Game.Designer.HighlightRange = newHighlightRange;
            }

            vScrollBar1.Left = this.ClientSize.Width - vScrollBar1.Width - panelWidth;
            mapPanel.Width = this.ClientSize.Width - vScrollBar1.Width - mapPanel.Left - panelWidth;
            mapPictureBox.Width = mapPanel.Width;
            hScrollBar1.Width = this.ClientSize.Width - vScrollBar1.Width - hScrollBar1.Left - panelWidth;

            if (refresh)
                mapPictureBox.Refresh();
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

            foreach((string menuText, Action menuAction) in clickedOnItem.Item.GetMenuItems(Game))
            {
                inventoryContextMenu.Items.Add(menuText, null, (s, e) => ActionRefresh(clickedOnItem.Item.GetFullName() + ": " + menuText, menuAction));

            }

            inventoryContextMenu.Show(Cursor.Position);
        }

        private void ActionRefresh(string text, Action action)
        {
            Game.LogMessage(text);
            action();

            Game.CompleteTurn();
            mapPictureBox.Refresh();
            UpdateInfoPanel(true);
            UpdateMessageText(_gameFormHelper.CompileMessages());
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

        private void UnselectedAllDesignerModes(bool highlightRange = false)
        {
            if (Game.Designer.HighlightRange != highlightRange)
            {
                Game.Designer.HighlightRange = highlightRange;
                mapPictureBox.Refresh();
            }

            foreach (var item in tileDesignerToolstrip.Items)
            {
                if (item is ToolStripButton button)
                    button.Checked = false;
            }
        }

        private void designerEyeDropper_Click(object sender, EventArgs e)
        {
            var showSelectedRange = _priorDesignerMode == DesignerMode.Range;
            UnselectedAllDesignerModes(showSelectedRange);

            designerEyeDropper.Checked = true;
            _designerMode = DesignerMode.EyeDropper;
        }

        private void designerRangeSelector_Click(object sender, EventArgs e)
        {
            UnselectedAllDesignerModes(true);
            designerRangeSelector.Checked = true;

            _designerMode = DesignerMode.Range;
            _priorDesignerMode = _designerMode;
        }

        private void designerPen_Click(object sender, EventArgs e)
        {
            UnselectedAllDesignerModes();
            designerPen.Checked = true;

            _designerMode = DesignerMode.Pencil;
            _priorDesignerMode = _designerMode;
        }

        private void designerFloodFill_Click(object sender, EventArgs e)
        {
            UnselectedAllDesignerModes();
            designerFloodFill.Checked = true;

            _designerMode = DesignerMode.Bucket;
            _priorDesignerMode = _designerMode;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gameFormHelper.Game.CommandManager.Undo();
            mapPictureBox.Refresh();
            UpdateDesignerControls();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gameFormHelper.Game.CommandManager.Redo();
            mapPictureBox.Refresh();
            UpdateDesignerControls();
        }

        private void tileInventoryApplyButton_Click(object sender, EventArgs e)
        {
            // Take the text from the textbox, create the inventory items with it, then add to the floor
            ITile tile = _gameFormHelper.FindCommonTileProperties();

            //string errorMessage = tile.SetInventorySignatures("Box (c), Golden Box\n  Long Sword (mw), Rusty sword, 2d10+2");

            // Box (c) --A red ruby box
            string parseSignature = "{name} (mw) {damageRoll}";
            string regExParseFormat = "(name:.*)/(mw/)(damageRoll:.*)";

            string errorMessage = tile.Inventory.SetInventorySignatures(tileInventoryTextBox.Text);


            inventoryErrorLabel.Text = errorMessage;
            mapPictureBox.Refresh();

        }

        private void UpdateSignatureFormatHelper()
        {
            int cursorAt = tileInventoryTextBox.SelectionStart;

            // We want to find the text of text where the cursor is currently placed
            var lines = tileInventoryTextBox.Text.Split(Environment.NewLine);

            // if cursorAt 0-41, Signature1, cursorAt 42-87, Signature2, cursorAt 88-139, Signature3
            // 42: Signature1 (ab) blah
            // 46: Signature2 (ab) blah blah
            // 52: Signature3 (ab) blah blah blah
            int lineLength = 0;
            foreach (var line in lines)
            {
                lineLength += line.Length + Environment.NewLine.Length;
                if (cursorAt < lineLength)
                {
                    // 012345678 1 2345678 
                    // foo (ab) bar

                    int lp = line.IndexOf('(');
                    int rp = line.IndexOf(')');
                    if (lp < rp && lp != -1)
                    {
                        string stamp = line.Substring(lp + 1, rp - lp - 1);
                        var sf = new SignatureFactory();
                        helperBar.Text = sf.GetPrettySignatureFormat(stamp);
                    }
                    else
                    {
                        helperBar.Text = "";
                    }
                    break;
                }
            }
        }

        private void tileInventoryTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateSignatureFormatHelper();
        }

        private void tileInventoryTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateSignatureFormatHelper();
        }
    }
}
