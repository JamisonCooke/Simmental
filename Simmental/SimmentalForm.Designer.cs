﻿
namespace Simmental
{
    partial class SimmentalForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimmentalForm));
            this.designerPanel = new System.Windows.Forms.Panel();
            this.DeleteNpcButton = new System.Windows.Forms.LinkLabel();
            this.AddNpcButton = new System.Windows.Forms.LinkLabel();
            this.npcListBox = new System.Windows.Forms.ListBox();
            this.helperBar = new System.Windows.Forms.Label();
            this.inventoryErrorLabel = new System.Windows.Forms.Label();
            this.tileInventoryApplyButton = new System.Windows.Forms.Button();
            this.tileInventoryTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tileDesignerToolstrip = new System.Windows.Forms.ToolStrip();
            this.designerEyeDropper = new System.Windows.Forms.ToolStripButton();
            this.designerRangeSelector = new System.Windows.Forms.ToolStripButton();
            this.designerPen = new System.Windows.Forms.ToolStripButton();
            this.designerFloodFill = new System.Windows.Forms.ToolStripButton();
            this.OpaqueCheckBox = new System.Windows.Forms.CheckBox();
            this.KillCheckBox = new System.Windows.Forms.CheckBox();
            this.FlyCheckBox = new System.Windows.Forms.CheckBox();
            this.WalkCheckBox = new System.Windows.Forms.CheckBox();
            this.tileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mapPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.designerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.zoomTrackBar = new System.Windows.Forms.TrackBar();
            this.characterPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.playerStatLine = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tileInventory = new System.Windows.Forms.ListBox();
            this.messagesTextBox = new System.Windows.Forms.TextBox();
            this.characterInfoPanel = new System.Windows.Forms.Panel();
            this.SecondaryWeaponLabel = new System.Windows.Forms.Label();
            this.PrimaryWeaponLabel = new System.Windows.Forms.Label();
            this.playerInventoryListBox = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.inventoryContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.designerPanel.SuspendLayout();
            this.tileDesignerToolstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
            this.characterPanel.SuspendLayout();
            this.characterInfoPanel.SuspendLayout();
            this.inventoryContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // designerPanel
            // 
            this.designerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.designerPanel.BackColor = System.Drawing.Color.White;
            this.designerPanel.Controls.Add(this.DeleteNpcButton);
            this.designerPanel.Controls.Add(this.AddNpcButton);
            this.designerPanel.Controls.Add(this.npcListBox);
            this.designerPanel.Controls.Add(this.helperBar);
            this.designerPanel.Controls.Add(this.inventoryErrorLabel);
            this.designerPanel.Controls.Add(this.tileInventoryApplyButton);
            this.designerPanel.Controls.Add(this.tileInventoryTextBox);
            this.designerPanel.Controls.Add(this.label7);
            this.designerPanel.Controls.Add(this.tileDesignerToolstrip);
            this.designerPanel.Controls.Add(this.OpaqueCheckBox);
            this.designerPanel.Controls.Add(this.KillCheckBox);
            this.designerPanel.Controls.Add(this.FlyCheckBox);
            this.designerPanel.Controls.Add(this.WalkCheckBox);
            this.designerPanel.Controls.Add(this.tileTypeComboBox);
            this.designerPanel.Controls.Add(this.label1);
            this.designerPanel.Location = new System.Drawing.Point(579, 27);
            this.designerPanel.Name = "designerPanel";
            this.designerPanel.Size = new System.Drawing.Size(230, 493);
            this.designerPanel.TabIndex = 1;
            this.designerPanel.Visible = false;
            this.designerPanel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.descriptionPanel_PreviewKeyDown);
            // 
            // DeleteNpcButton
            // 
            this.DeleteNpcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteNpcButton.AutoSize = true;
            this.DeleteNpcButton.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeleteNpcButton.LinkColor = System.Drawing.Color.Black;
            this.DeleteNpcButton.Location = new System.Drawing.Point(163, 343);
            this.DeleteNpcButton.Name = "DeleteNpcButton";
            this.DeleteNpcButton.Size = new System.Drawing.Size(54, 12);
            this.DeleteNpcButton.TabIndex = 14;
            this.DeleteNpcButton.TabStop = true;
            this.DeleteNpcButton.Text = "Delete NPC";
            this.DeleteNpcButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeleteNpcButton_LinkClicked);
            // 
            // AddNpcButton
            // 
            this.AddNpcButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNpcButton.AutoSize = true;
            this.AddNpcButton.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AddNpcButton.LinkColor = System.Drawing.Color.Black;
            this.AddNpcButton.Location = new System.Drawing.Point(112, 343);
            this.AddNpcButton.Name = "AddNpcButton";
            this.AddNpcButton.Size = new System.Drawing.Size(45, 12);
            this.AddNpcButton.TabIndex = 14;
            this.AddNpcButton.TabStop = true;
            this.AddNpcButton.Text = "Add NPC";
            this.AddNpcButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddNpcButton_LinkClicked);
            // 
            // npcListBox
            // 
            this.npcListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.npcListBox.FormattingEnabled = true;
            this.npcListBox.ItemHeight = 15;
            this.npcListBox.Location = new System.Drawing.Point(12, 291);
            this.npcListBox.Name = "npcListBox";
            this.npcListBox.Size = new System.Drawing.Size(205, 49);
            this.npcListBox.TabIndex = 11;
            this.npcListBox.TabStop = false;
            this.npcListBox.DoubleClick += new System.EventHandler(this.npcListBox_DoubleClick);
            // 
            // helperBar
            // 
            this.helperBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helperBar.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.helperBar.Location = new System.Drawing.Point(12, 45);
            this.helperBar.Name = "helperBar";
            this.helperBar.Size = new System.Drawing.Size(215, 24);
            this.helperBar.TabIndex = 10;
            // 
            // inventoryErrorLabel
            // 
            this.inventoryErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.inventoryErrorLabel.Location = new System.Drawing.Point(12, 246);
            this.inventoryErrorLabel.Name = "inventoryErrorLabel";
            this.inventoryErrorLabel.Size = new System.Drawing.Size(205, 37);
            this.inventoryErrorLabel.TabIndex = 9;
            // 
            // tileInventoryApplyButton
            // 
            this.tileInventoryApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tileInventoryApplyButton.Location = new System.Drawing.Point(142, 220);
            this.tileInventoryApplyButton.Name = "tileInventoryApplyButton";
            this.tileInventoryApplyButton.Size = new System.Drawing.Size(75, 23);
            this.tileInventoryApplyButton.TabIndex = 8;
            this.tileInventoryApplyButton.TabStop = false;
            this.tileInventoryApplyButton.Text = "Apply";
            this.tileInventoryApplyButton.UseVisualStyleBackColor = true;
            this.tileInventoryApplyButton.Click += new System.EventHandler(this.tileInventoryApplyButton_Click);
            // 
            // tileInventoryTextBox
            // 
            this.tileInventoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tileInventoryTextBox.Location = new System.Drawing.Point(12, 71);
            this.tileInventoryTextBox.Multiline = true;
            this.tileInventoryTextBox.Name = "tileInventoryTextBox";
            this.tileInventoryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tileInventoryTextBox.Size = new System.Drawing.Size(205, 143);
            this.tileInventoryTextBox.TabIndex = 7;
            this.tileInventoryTextBox.TabStop = false;
            this.tileInventoryTextBox.TextChanged += new System.EventHandler(this.tileInventoryTextBox_TextChanged);
            this.tileInventoryTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tileInventoryTextBox_TextChanged);
            this.tileInventoryTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tileInventoryTextBox_MouseUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Items on ground";
            // 
            // tileDesignerToolstrip
            // 
            this.tileDesignerToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.designerEyeDropper,
            this.designerRangeSelector,
            this.designerPen,
            this.designerFloodFill});
            this.tileDesignerToolstrip.Location = new System.Drawing.Point(0, 0);
            this.tileDesignerToolstrip.Name = "tileDesignerToolstrip";
            this.tileDesignerToolstrip.Size = new System.Drawing.Size(230, 25);
            this.tileDesignerToolstrip.TabIndex = 5;
            this.tileDesignerToolstrip.Text = "toolStrip1";
            // 
            // designerEyeDropper
            // 
            this.designerEyeDropper.CheckOnClick = true;
            this.designerEyeDropper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.designerEyeDropper.Image = ((System.Drawing.Image)(resources.GetObject("designerEyeDropper.Image")));
            this.designerEyeDropper.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.designerEyeDropper.Name = "designerEyeDropper";
            this.designerEyeDropper.Size = new System.Drawing.Size(23, 22);
            this.designerEyeDropper.Text = "toolStripButton1";
            this.designerEyeDropper.ToolTipText = "Style Selector";
            this.designerEyeDropper.Click += new System.EventHandler(this.designerEyeDropper_Click);
            // 
            // designerRangeSelector
            // 
            this.designerRangeSelector.Checked = true;
            this.designerRangeSelector.CheckOnClick = true;
            this.designerRangeSelector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.designerRangeSelector.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.designerRangeSelector.Image = ((System.Drawing.Image)(resources.GetObject("designerRangeSelector.Image")));
            this.designerRangeSelector.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.designerRangeSelector.Name = "designerRangeSelector";
            this.designerRangeSelector.Size = new System.Drawing.Size(23, 22);
            this.designerRangeSelector.Text = "toolStripButton2";
            this.designerRangeSelector.ToolTipText = "Range Selector";
            this.designerRangeSelector.Click += new System.EventHandler(this.designerRangeSelector_Click);
            // 
            // designerPen
            // 
            this.designerPen.CheckOnClick = true;
            this.designerPen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.designerPen.Image = ((System.Drawing.Image)(resources.GetObject("designerPen.Image")));
            this.designerPen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.designerPen.Name = "designerPen";
            this.designerPen.Size = new System.Drawing.Size(23, 22);
            this.designerPen.Text = "toolStripButton3";
            this.designerPen.ToolTipText = "Pencil ";
            this.designerPen.Click += new System.EventHandler(this.designerPen_Click);
            // 
            // designerFloodFill
            // 
            this.designerFloodFill.CheckOnClick = true;
            this.designerFloodFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.designerFloodFill.Image = ((System.Drawing.Image)(resources.GetObject("designerFloodFill.Image")));
            this.designerFloodFill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.designerFloodFill.Name = "designerFloodFill";
            this.designerFloodFill.Size = new System.Drawing.Size(23, 22);
            this.designerFloodFill.Text = "Paint Bucket";
            this.designerFloodFill.Click += new System.EventHandler(this.designerFloodFill_Click);
            // 
            // OpaqueCheckBox
            // 
            this.OpaqueCheckBox.AutoSize = true;
            this.OpaqueCheckBox.Location = new System.Drawing.Point(32, 463);
            this.OpaqueCheckBox.Name = "OpaqueCheckBox";
            this.OpaqueCheckBox.Size = new System.Drawing.Size(79, 19);
            this.OpaqueCheckBox.TabIndex = 4;
            this.OpaqueCheckBox.TabStop = false;
            this.OpaqueCheckBox.Text = "Is Opaque";
            this.OpaqueCheckBox.UseVisualStyleBackColor = true;
            this.OpaqueCheckBox.CheckedChanged += new System.EventHandler(this.UpdateTileAttributes);
            // 
            // KillCheckBox
            // 
            this.KillCheckBox.AutoSize = true;
            this.KillCheckBox.Location = new System.Drawing.Point(32, 440);
            this.KillCheckBox.Name = "KillCheckBox";
            this.KillCheckBox.Size = new System.Drawing.Size(88, 19);
            this.KillCheckBox.TabIndex = 3;
            this.KillCheckBox.TabStop = false;
            this.KillCheckBox.Text = "Will Kill You";
            this.KillCheckBox.UseVisualStyleBackColor = true;
            this.KillCheckBox.CheckedChanged += new System.EventHandler(this.UpdateTileAttributes);
            // 
            // FlyCheckBox
            // 
            this.FlyCheckBox.AutoSize = true;
            this.FlyCheckBox.Location = new System.Drawing.Point(32, 415);
            this.FlyCheckBox.Name = "FlyCheckBox";
            this.FlyCheckBox.Size = new System.Drawing.Size(84, 19);
            this.FlyCheckBox.TabIndex = 3;
            this.FlyCheckBox.TabStop = false;
            this.FlyCheckBox.Text = "Can Fly On";
            this.FlyCheckBox.UseVisualStyleBackColor = true;
            this.FlyCheckBox.CheckedChanged += new System.EventHandler(this.UpdateTileAttributes);
            // 
            // WalkCheckBox
            // 
            this.WalkCheckBox.AutoSize = true;
            this.WalkCheckBox.Location = new System.Drawing.Point(32, 390);
            this.WalkCheckBox.Name = "WalkCheckBox";
            this.WalkCheckBox.Size = new System.Drawing.Size(95, 19);
            this.WalkCheckBox.TabIndex = 3;
            this.WalkCheckBox.TabStop = false;
            this.WalkCheckBox.Text = "Can Walk On";
            this.WalkCheckBox.UseVisualStyleBackColor = true;
            this.WalkCheckBox.CheckedChanged += new System.EventHandler(this.UpdateTileAttributes);
            // 
            // tileTypeComboBox
            // 
            this.tileTypeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tileTypeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tileTypeComboBox.FormattingEnabled = true;
            this.tileTypeComboBox.Items.AddRange(new object[] {
            "Grass ",
            "Stone",
            "Water",
            "Wall",
            "Wood"});
            this.tileTypeComboBox.Location = new System.Drawing.Point(12, 361);
            this.tileTypeComboBox.Name = "tileTypeComboBox";
            this.tileTypeComboBox.Size = new System.Drawing.Size(154, 23);
            this.tileTypeComboBox.TabIndex = 2;
            this.tileTypeComboBox.TabStop = false;
            this.tileTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.tileTypeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tile Designer";
            // 
            // mapPictureBox
            // 
            this.mapPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapPictureBox.Location = new System.Drawing.Point(235, 27);
            this.mapPictureBox.Name = "mapPictureBox";
            this.mapPictureBox.Size = new System.Drawing.Size(321, 476);
            this.mapPictureBox.TabIndex = 2;
            this.mapPictureBox.TabStop = false;
            this.mapPictureBox.Click += new System.EventHandler(this.mapPictureBox_Click);
            this.mapPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPictureBox_Paint);
            this.mapPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapPictureBox_MouseDown);
            this.mapPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapPictureBox_MouseMove);
            this.mapPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapPictureBox_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.adminToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(808, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.reloadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reloadToolStripMenuItem.Text = "&Reload";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.characterInfoToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // characterInfoToolStripMenuItem
            // 
            this.characterInfoToolStripMenuItem.Name = "characterInfoToolStripMenuItem";
            this.characterInfoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.characterInfoToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.characterInfoToolStripMenuItem.Text = "Character &Info";
            this.characterInfoToolStripMenuItem.Click += new System.EventHandler(this.characterInfoToolStripMenuItem_Click);
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.designerToolStripMenuItem,
            this.mapResetToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.adminToolStripMenuItem.Text = "&Admin";
            // 
            // designerToolStripMenuItem
            // 
            this.designerToolStripMenuItem.Name = "designerToolStripMenuItem";
            this.designerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.designerToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.designerToolStripMenuItem.Text = "Designer";
            this.designerToolStripMenuItem.Click += new System.EventHandler(this.designerToolStripMenuItem_Click);
            // 
            // mapResetToolStripMenuItem
            // 
            this.mapResetToolStripMenuItem.Name = "mapResetToolStripMenuItem";
            this.mapResetToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.mapResetToolStripMenuItem.Text = "Map &Reset";
            this.mapResetToolStripMenuItem.Click += new System.EventHandler(this.mapResetToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Location = new System.Drawing.Point(559, 24);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 476);
            this.vScrollBar1.TabIndex = 12;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Location = new System.Drawing.Point(233, 503);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(323, 17);
            this.hScrollBar1.TabIndex = 13;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(327, 408);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 14;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // mapPanel
            // 
            this.mapPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapPanel.Location = new System.Drawing.Point(232, 0);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(743, 494);
            this.mapPanel.TabIndex = 0;
            // 
            // zoomTrackBar
            // 
            this.zoomTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.zoomTrackBar.LargeChange = 3;
            this.zoomTrackBar.Location = new System.Drawing.Point(18, 438);
            this.zoomTrackBar.Maximum = 50;
            this.zoomTrackBar.Minimum = 3;
            this.zoomTrackBar.Name = "zoomTrackBar";
            this.zoomTrackBar.Size = new System.Drawing.Size(205, 45);
            this.zoomTrackBar.TabIndex = 10;
            this.zoomTrackBar.TabStop = false;
            this.zoomTrackBar.Value = 10;
            this.zoomTrackBar.Scroll += new System.EventHandler(this.zoomTrackBar_Scroll);
            // 
            // characterPanel
            // 
            this.characterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.characterPanel.BackColor = System.Drawing.Color.White;
            this.characterPanel.Controls.Add(this.label3);
            this.characterPanel.Controls.Add(this.playerStatLine);
            this.characterPanel.Controls.Add(this.label4);
            this.characterPanel.Controls.Add(this.label2);
            this.characterPanel.Controls.Add(this.tileInventory);
            this.characterPanel.Controls.Add(this.messagesTextBox);
            this.characterPanel.Controls.Add(this.zoomTrackBar);
            this.characterPanel.Controls.Add(this.mapPanel);
            this.characterPanel.Location = new System.Drawing.Point(1, 27);
            this.characterPanel.Name = "characterPanel";
            this.characterPanel.Size = new System.Drawing.Size(231, 493);
            this.characterPanel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Journal";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // playerStatLine
            // 
            this.playerStatLine.AutoSize = true;
            this.playerStatLine.Location = new System.Drawing.Point(11, 28);
            this.playerStatLine.Name = "playerStatLine";
            this.playerStatLine.Size = new System.Drawing.Size(129, 15);
            this.playerStatLine.TabIndex = 13;
            this.playerStatLine.Text = "HP 25 (25)  MANA 0 (0)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Character";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "What\'s on the ground";
            // 
            // tileInventory
            // 
            this.tileInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tileInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tileInventory.FormattingEnabled = true;
            this.tileInventory.ItemHeight = 15;
            this.tileInventory.Location = new System.Drawing.Point(11, 342);
            this.tileInventory.Name = "tileInventory";
            this.tileInventory.Size = new System.Drawing.Size(212, 90);
            this.tileInventory.TabIndex = 12;
            this.tileInventory.TabStop = false;
            this.tileInventory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tileInventory_MouseDoubleClick);
            this.tileInventory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tileInventory_MouseDown);
            this.tileInventory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tileInventory_MouseUp);
            // 
            // messagesTextBox
            // 
            this.messagesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.messagesTextBox.BackColor = System.Drawing.Color.White;
            this.messagesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messagesTextBox.Location = new System.Drawing.Point(6, 69);
            this.messagesTextBox.Multiline = true;
            this.messagesTextBox.Name = "messagesTextBox";
            this.messagesTextBox.ReadOnly = true;
            this.messagesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messagesTextBox.Size = new System.Drawing.Size(212, 249);
            this.messagesTextBox.TabIndex = 11;
            this.messagesTextBox.TabStop = false;
            // 
            // characterInfoPanel
            // 
            this.characterInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.characterInfoPanel.BackColor = System.Drawing.Color.White;
            this.characterInfoPanel.Controls.Add(this.SecondaryWeaponLabel);
            this.characterInfoPanel.Controls.Add(this.PrimaryWeaponLabel);
            this.characterInfoPanel.Controls.Add(this.playerInventoryListBox);
            this.characterInfoPanel.Controls.Add(this.label6);
            this.characterInfoPanel.Controls.Add(this.label5);
            this.characterInfoPanel.Location = new System.Drawing.Point(235, 27);
            this.characterInfoPanel.Name = "characterInfoPanel";
            this.characterInfoPanel.Size = new System.Drawing.Size(192, 494);
            this.characterInfoPanel.TabIndex = 15;
            this.characterInfoPanel.Visible = false;
            // 
            // SecondaryWeaponLabel
            // 
            this.SecondaryWeaponLabel.AutoSize = true;
            this.SecondaryWeaponLabel.Location = new System.Drawing.Point(8, 66);
            this.SecondaryWeaponLabel.Name = "SecondaryWeaponLabel";
            this.SecondaryWeaponLabel.Size = new System.Drawing.Size(106, 15);
            this.SecondaryWeaponLabel.TabIndex = 3;
            this.SecondaryWeaponLabel.Text = "SecondaryWeapon";
            // 
            // PrimaryWeaponLabel
            // 
            this.PrimaryWeaponLabel.AutoSize = true;
            this.PrimaryWeaponLabel.Location = new System.Drawing.Point(8, 48);
            this.PrimaryWeaponLabel.Name = "PrimaryWeaponLabel";
            this.PrimaryWeaponLabel.Size = new System.Drawing.Size(92, 15);
            this.PrimaryWeaponLabel.TabIndex = 2;
            this.PrimaryWeaponLabel.Text = "PrimaryWeapon";
            // 
            // playerInventoryListBox
            // 
            this.playerInventoryListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.playerInventoryListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playerInventoryListBox.FormattingEnabled = true;
            this.playerInventoryListBox.ItemHeight = 15;
            this.playerInventoryListBox.Location = new System.Drawing.Point(22, 152);
            this.playerInventoryListBox.Name = "playerInventoryListBox";
            this.playerInventoryListBox.Size = new System.Drawing.Size(167, 315);
            this.playerInventoryListBox.TabIndex = 1;
            this.playerInventoryListBox.TabStop = false;
            this.playerInventoryListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.playerInventoryListBox_MouseDoubleClick);
            this.playerInventoryListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.playerInventoryListBox_MouseDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Inventory";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Player Stats";
            // 
            // inventoryContextMenu
            // 
            this.inventoryContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveToToolStripMenuItem});
            this.inventoryContextMenu.Name = "inventoryContextMenu";
            this.inventoryContextMenu.Size = new System.Drawing.Size(120, 26);
            // 
            // moveToToolStripMenuItem
            // 
            this.moveToToolStripMenuItem.Name = "moveToToolStripMenuItem";
            this.moveToToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.moveToToolStripMenuItem.Text = "Move To";
            // 
            // SimmentalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 522);
            this.Controls.Add(this.characterInfoPanel);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.mapPictureBox);
            this.Controls.Add(this.designerPanel);
            this.Controls.Add(this.characterPanel);
            this.Controls.Add(this.textBox1);
            this.Name = "SimmentalForm";
            this.Text = "Simmental";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimmentalForm_FormClosing);
            this.Load += new System.EventHandler(this.SimmentalForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SimmentalForm_KeyDown);
            this.designerPanel.ResumeLayout(false);
            this.designerPanel.PerformLayout();
            this.tileDesignerToolstrip.ResumeLayout(false);
            this.tileDesignerToolstrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).EndInit();
            this.characterPanel.ResumeLayout(false);
            this.characterPanel.PerformLayout();
            this.characterInfoPanel.ResumeLayout(false);
            this.characterInfoPanel.PerformLayout();
            this.inventoryContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel designerPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox mapPictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.ComboBox tileTypeComboBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox KillCheckBox;
        private System.Windows.Forms.CheckBox FlyCheckBox;
        private System.Windows.Forms.CheckBox WalkCheckBox;
        private System.Windows.Forms.CheckBox OpaqueCheckBox;
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.TrackBar zoomTrackBar;
        private System.Windows.Forms.Panel characterPanel;
        private System.Windows.Forms.TextBox messagesTextBox;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem designerToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox tileInventory;
        private System.Windows.Forms.Label playerStatLine;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem mapResetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem characterInfoToolStripMenuItem;
        private System.Windows.Forms.Panel characterInfoPanel;
        private System.Windows.Forms.ListBox playerInventoryListBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip inventoryContextMenu;
        private System.Windows.Forms.ToolStripMenuItem moveToToolStripMenuItem;
        private System.Windows.Forms.Label SecondaryWeaponLabel;
        private System.Windows.Forms.Label PrimaryWeaponLabel;
        private System.Windows.Forms.ToolStrip tileDesignerToolstrip;
        private System.Windows.Forms.ToolStripButton designerEyeDropper;
        private System.Windows.Forms.ToolStripButton designerRangeSelector;
        private System.Windows.Forms.ToolStripButton designerPen;
        private System.Windows.Forms.ToolStripButton designerFloodFill;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.TextBox tileInventoryTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button tileInventoryApplyButton;
        private System.Windows.Forms.Label inventoryErrorLabel;
        private System.Windows.Forms.Label helperBar;
        private System.Windows.Forms.ListBox npcListBox;
        private System.Windows.Forms.LinkLabel DeleteNpcButton;
        private System.Windows.Forms.LinkLabel AddNpcButton;
    }
}

