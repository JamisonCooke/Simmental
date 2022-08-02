namespace Simmental
{
    partial class CharacterSheet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSTR = new System.Windows.Forms.TextBox();
            this.lblSTR = new System.Windows.Forms.Label();
            this.txtDEX = new System.Windows.Forms.TextBox();
            this.lblDEX = new System.Windows.Forms.Label();
            this.txtCON = new System.Windows.Forms.TextBox();
            this.lblCON = new System.Windows.Forms.Label();
            this.txtINT = new System.Windows.Forms.TextBox();
            this.lblINT = new System.Windows.Forms.Label();
            this.txtWIS = new System.Windows.Forms.TextBox();
            this.lblWIS = new System.Windows.Forms.Label();
            this.txtCHR = new System.Windows.Forms.TextBox();
            this.lblCHR = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRace = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtHPMaxHP = new System.Windows.Forms.TextBox();
            this.lblHP = new System.Windows.Forms.Label();
            this.txtAC = new System.Windows.Forms.TextBox();
            this.lblAC = new System.Windows.Forms.Label();
            this.txtEXP = new System.Windows.Forms.TextBox();
            this.lblEXP = new System.Windows.Forms.Label();
            this.lblPrimaryWeapon = new System.Windows.Forms.Label();
            this.txtPrimaryWeapon = new System.Windows.Forms.TextBox();
            this.txtSecondaryWeapon = new System.Windows.Forms.TextBox();
            this.lblSecondaryWeapon = new System.Windows.Forms.Label();
            this.lblInventory = new System.Windows.Forms.Label();
            this.txtInventory = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.txtLV = new System.Windows.Forms.TextBox();
            this.lblLV = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "&Name";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Location = new System.Drawing.Point(57, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(302, 16);
            this.txtName.TabIndex = 1;
            // 
            // txtSTR
            // 
            this.txtSTR.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSTR.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSTR.Location = new System.Drawing.Point(25, 129);
            this.txtSTR.Name = "txtSTR";
            this.txtSTR.Size = new System.Drawing.Size(40, 22);
            this.txtSTR.TabIndex = 15;
            this.txtSTR.Text = "17";
            this.txtSTR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSTR.Enter += new System.EventHandler(this.SelectAllText);
            // 
            // lblSTR
            // 
            this.lblSTR.BackColor = System.Drawing.SystemColors.Window;
            this.lblSTR.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSTR.Location = new System.Drawing.Point(25, 125);
            this.lblSTR.Name = "lblSTR";
            this.lblSTR.Size = new System.Drawing.Size(40, 42);
            this.lblSTR.TabIndex = 14;
            this.lblSTR.Text = "STR";
            this.lblSTR.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtDEX
            // 
            this.txtDEX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDEX.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDEX.Location = new System.Drawing.Point(82, 129);
            this.txtDEX.Name = "txtDEX";
            this.txtDEX.Size = new System.Drawing.Size(40, 22);
            this.txtDEX.TabIndex = 17;
            this.txtDEX.Text = "17";
            this.txtDEX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDEX.Enter += new System.EventHandler(this.SelectAllText);
            // 
            // lblDEX
            // 
            this.lblDEX.BackColor = System.Drawing.SystemColors.Window;
            this.lblDEX.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDEX.Location = new System.Drawing.Point(82, 125);
            this.lblDEX.Name = "lblDEX";
            this.lblDEX.Size = new System.Drawing.Size(40, 42);
            this.lblDEX.TabIndex = 16;
            this.lblDEX.Text = "DEX";
            this.lblDEX.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtCON
            // 
            this.txtCON.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCON.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCON.Location = new System.Drawing.Point(140, 129);
            this.txtCON.Name = "txtCON";
            this.txtCON.Size = new System.Drawing.Size(40, 22);
            this.txtCON.TabIndex = 19;
            this.txtCON.Text = "17";
            this.txtCON.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCON.Enter += new System.EventHandler(this.SelectAllText);
            // 
            // lblCON
            // 
            this.lblCON.BackColor = System.Drawing.SystemColors.Window;
            this.lblCON.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCON.Location = new System.Drawing.Point(140, 125);
            this.lblCON.Name = "lblCON";
            this.lblCON.Size = new System.Drawing.Size(40, 42);
            this.lblCON.TabIndex = 18;
            this.lblCON.Text = "CON";
            this.lblCON.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtINT
            // 
            this.txtINT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtINT.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtINT.Location = new System.Drawing.Point(197, 129);
            this.txtINT.Name = "txtINT";
            this.txtINT.Size = new System.Drawing.Size(40, 22);
            this.txtINT.TabIndex = 21;
            this.txtINT.Text = "17";
            this.txtINT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtINT.Enter += new System.EventHandler(this.SelectAllText);
            // 
            // lblINT
            // 
            this.lblINT.BackColor = System.Drawing.SystemColors.Window;
            this.lblINT.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblINT.Location = new System.Drawing.Point(197, 125);
            this.lblINT.Name = "lblINT";
            this.lblINT.Size = new System.Drawing.Size(40, 42);
            this.lblINT.TabIndex = 20;
            this.lblINT.Text = "INT";
            this.lblINT.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtWIS
            // 
            this.txtWIS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWIS.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtWIS.Location = new System.Drawing.Point(253, 129);
            this.txtWIS.Name = "txtWIS";
            this.txtWIS.Size = new System.Drawing.Size(40, 22);
            this.txtWIS.TabIndex = 23;
            this.txtWIS.Text = "17";
            this.txtWIS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWIS.Enter += new System.EventHandler(this.SelectAllText);
            // 
            // lblWIS
            // 
            this.lblWIS.BackColor = System.Drawing.SystemColors.Window;
            this.lblWIS.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWIS.Location = new System.Drawing.Point(253, 125);
            this.lblWIS.Name = "lblWIS";
            this.lblWIS.Size = new System.Drawing.Size(40, 42);
            this.lblWIS.TabIndex = 22;
            this.lblWIS.Text = "WIS";
            this.lblWIS.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtCHR
            // 
            this.txtCHR.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCHR.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCHR.Location = new System.Drawing.Point(310, 129);
            this.txtCHR.Name = "txtCHR";
            this.txtCHR.Size = new System.Drawing.Size(40, 22);
            this.txtCHR.TabIndex = 25;
            this.txtCHR.Text = "17";
            this.txtCHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCHR.Enter += new System.EventHandler(this.SelectAllText);
            // 
            // lblCHR
            // 
            this.lblCHR.BackColor = System.Drawing.SystemColors.Window;
            this.lblCHR.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCHR.Location = new System.Drawing.Point(310, 125);
            this.lblCHR.Name = "lblCHR";
            this.lblCHR.Size = new System.Drawing.Size(40, 42);
            this.lblCHR.TabIndex = 24;
            this.lblCHR.Text = "CHR";
            this.lblCHR.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "&Race";
            // 
            // txtRace
            // 
            this.txtRace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRace.AutoCompleteCustomSource.AddRange(new string[] {
            "Human",
            "Orc"});
            this.txtRace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtRace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRace.Location = new System.Drawing.Point(57, 35);
            this.txtRace.Name = "txtRace";
            this.txtRace.Size = new System.Drawing.Size(302, 16);
            this.txtRace.TabIndex = 3;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Location = new System.Drawing.Point(57, 57);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(302, 31);
            this.txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(19, 57);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(32, 15);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "&Desc";
            // 
            // txtHPMaxHP
            // 
            this.txtHPMaxHP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHPMaxHP.Location = new System.Drawing.Point(57, 94);
            this.txtHPMaxHP.Name = "txtHPMaxHP";
            this.txtHPMaxHP.Size = new System.Drawing.Size(40, 16);
            this.txtHPMaxHP.TabIndex = 7;
            this.txtHPMaxHP.Text = "35/35";
            this.txtHPMaxHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblHP
            // 
            this.lblHP.AutoSize = true;
            this.lblHP.Location = new System.Drawing.Point(28, 94);
            this.lblHP.Name = "lblHP";
            this.lblHP.Size = new System.Drawing.Size(23, 15);
            this.lblHP.TabIndex = 6;
            this.lblHP.Text = "&HP";
            // 
            // txtAC
            // 
            this.txtAC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAC.Location = new System.Drawing.Point(142, 94);
            this.txtAC.Name = "txtAC";
            this.txtAC.Size = new System.Drawing.Size(21, 16);
            this.txtAC.TabIndex = 9;
            this.txtAC.Text = "15";
            this.txtAC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblAC
            // 
            this.lblAC.AutoSize = true;
            this.lblAC.Location = new System.Drawing.Point(113, 94);
            this.lblAC.Name = "lblAC";
            this.lblAC.Size = new System.Drawing.Size(23, 15);
            this.lblAC.TabIndex = 8;
            this.lblAC.Text = "&AC";
            // 
            // txtEXP
            // 
            this.txtEXP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEXP.Location = new System.Drawing.Point(282, 94);
            this.txtEXP.Name = "txtEXP";
            this.txtEXP.Size = new System.Drawing.Size(59, 16);
            this.txtEXP.TabIndex = 13;
            this.txtEXP.Text = "4000";
            this.txtEXP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEXP
            // 
            this.lblEXP.AutoSize = true;
            this.lblEXP.Location = new System.Drawing.Point(249, 94);
            this.lblEXP.Name = "lblEXP";
            this.lblEXP.Size = new System.Drawing.Size(27, 15);
            this.lblEXP.TabIndex = 12;
            this.lblEXP.Text = "&EXP";
            // 
            // lblPrimaryWeapon
            // 
            this.lblPrimaryWeapon.AutoSize = true;
            this.lblPrimaryWeapon.Location = new System.Drawing.Point(12, 189);
            this.lblPrimaryWeapon.Name = "lblPrimaryWeapon";
            this.lblPrimaryWeapon.Size = new System.Drawing.Size(95, 15);
            this.lblPrimaryWeapon.TabIndex = 26;
            this.lblPrimaryWeapon.Text = "&Primary Weapon";
            // 
            // txtPrimaryWeapon
            // 
            this.txtPrimaryWeapon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrimaryWeapon.AutoCompleteCustomSource.AddRange(new string[] {
            "Human",
            "Orc"});
            this.txtPrimaryWeapon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtPrimaryWeapon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrimaryWeapon.Location = new System.Drawing.Point(12, 207);
            this.txtPrimaryWeapon.Name = "txtPrimaryWeapon";
            this.txtPrimaryWeapon.Size = new System.Drawing.Size(347, 16);
            this.txtPrimaryWeapon.TabIndex = 27;
            // 
            // txtSecondaryWeapon
            // 
            this.txtSecondaryWeapon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecondaryWeapon.AutoCompleteCustomSource.AddRange(new string[] {
            "Human",
            "Orc"});
            this.txtSecondaryWeapon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSecondaryWeapon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSecondaryWeapon.Location = new System.Drawing.Point(12, 246);
            this.txtSecondaryWeapon.Name = "txtSecondaryWeapon";
            this.txtSecondaryWeapon.Size = new System.Drawing.Size(347, 16);
            this.txtSecondaryWeapon.TabIndex = 29;
            // 
            // lblSecondaryWeapon
            // 
            this.lblSecondaryWeapon.AutoSize = true;
            this.lblSecondaryWeapon.Location = new System.Drawing.Point(12, 228);
            this.lblSecondaryWeapon.Name = "lblSecondaryWeapon";
            this.lblSecondaryWeapon.Size = new System.Drawing.Size(109, 15);
            this.lblSecondaryWeapon.TabIndex = 28;
            this.lblSecondaryWeapon.Text = "&Secondary Weapon";
            // 
            // lblInventory
            // 
            this.lblInventory.AutoSize = true;
            this.lblInventory.Location = new System.Drawing.Point(12, 274);
            this.lblInventory.Name = "lblInventory";
            this.lblInventory.Size = new System.Drawing.Size(57, 15);
            this.lblInventory.TabIndex = 30;
            this.lblInventory.Text = "&Inventory";
            // 
            // txtInventory
            // 
            this.txtInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInventory.AutoCompleteCustomSource.AddRange(new string[] {
            "Human",
            "Orc"});
            this.txtInventory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInventory.Location = new System.Drawing.Point(12, 292);
            this.txtInventory.Multiline = true;
            this.txtInventory.Name = "txtInventory";
            this.txtInventory.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInventory.Size = new System.Drawing.Size(347, 100);
            this.txtInventory.TabIndex = 31;
            this.txtInventory.WordWrap = false;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(284, 400);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 32;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // txtLV
            // 
            this.txtLV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLV.Location = new System.Drawing.Point(211, 94);
            this.txtLV.Name = "txtLV";
            this.txtLV.Size = new System.Drawing.Size(21, 16);
            this.txtLV.TabIndex = 11;
            this.txtLV.Text = "8";
            this.txtLV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblLV
            // 
            this.lblLV.AutoSize = true;
            this.lblLV.Location = new System.Drawing.Point(186, 94);
            this.lblLV.Name = "lblLV";
            this.lblLV.Size = new System.Drawing.Size(19, 15);
            this.lblLV.TabIndex = 10;
            this.lblLV.Text = "&LV";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CharacterSheet
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(370, 435);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtInventory);
            this.Controls.Add(this.lblInventory);
            this.Controls.Add(this.txtSecondaryWeapon);
            this.Controls.Add(this.lblSecondaryWeapon);
            this.Controls.Add(this.lblEXP);
            this.Controls.Add(this.lblLV);
            this.Controls.Add(this.lblAC);
            this.Controls.Add(this.lblHP);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCHR);
            this.Controls.Add(this.lblCHR);
            this.Controls.Add(this.txtWIS);
            this.Controls.Add(this.lblWIS);
            this.Controls.Add(this.txtINT);
            this.Controls.Add(this.lblINT);
            this.Controls.Add(this.txtCON);
            this.Controls.Add(this.lblCON);
            this.Controls.Add(this.txtDEX);
            this.Controls.Add(this.lblDEX);
            this.Controls.Add(this.txtSTR);
            this.Controls.Add(this.lblSTR);
            this.Controls.Add(this.txtPrimaryWeapon);
            this.Controls.Add(this.txtRace);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtLV);
            this.Controls.Add(this.txtEXP);
            this.Controls.Add(this.txtAC);
            this.Controls.Add(this.txtHPMaxHP);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblPrimaryWeapon);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CharacterSheet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "CharacterSheet";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CharacterSheet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSTR;
        private System.Windows.Forms.Label lblSTR;
        private System.Windows.Forms.TextBox txtDEX;
        private System.Windows.Forms.Label lblDEX;
        private System.Windows.Forms.TextBox txtCON;
        private System.Windows.Forms.Label lblCON;
        private System.Windows.Forms.TextBox txtINT;
        private System.Windows.Forms.Label lblINT;
        private System.Windows.Forms.TextBox txtWIS;
        private System.Windows.Forms.Label lblWIS;
        private System.Windows.Forms.TextBox txtCHR;
        private System.Windows.Forms.Label lblCHR;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRace;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtHPMaxHP;
        private System.Windows.Forms.Label lblHP;
        private System.Windows.Forms.TextBox txtAC;
        private System.Windows.Forms.Label lblAC;
        private System.Windows.Forms.TextBox txtEXP;
        private System.Windows.Forms.Label lblEXP;
        private System.Windows.Forms.Label lblPrimaryWeapon;
        private System.Windows.Forms.TextBox txtPrimaryWeapon;
        private System.Windows.Forms.TextBox txtSecondaryWeapon;
        private System.Windows.Forms.Label lblSecondaryWeapon;
        private System.Windows.Forms.Label lblInventory;
        private System.Windows.Forms.TextBox txtInventory;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtLV;
        private System.Windows.Forms.Label lblLV;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}