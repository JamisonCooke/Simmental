using Simmental.Game.Items;
using Simmental.Game.Signatures;
using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simmental
{
    public partial class CharacterSheet : Form
    {
        public CharacterSheet()
        {
            InitializeComponent();
        }

        private void CharacterSheet_Load(object sender, EventArgs e)
        {

        }

        private void SelectAllText(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private void LoadCharacter(ICharacter npc)
        {
            this.Text = $"{npc.Name} - {npc.Race}";

            txtName.Text = npc.Name;
            txtRace.Text = $"{npc.Race}";
            txtDescription.Text = npc.Description;
            txtHPMaxHP.Text = $"{npc.HP}/{npc.GetMaxHP()}";
            txtAC.Text = $"{npc.AC}";
            txtLV.Text = $"{npc.Level}";
            //txtEXP.Text = $"{npc}";
            txtSTR.Text = $"{npc.Strength}";
            txtDEX.Text = $"{npc.Dexterity}";
            txtCON.Text = $"{npc.Constitution}";
            txtINT.Text = $"{npc.Intelligence}";
            txtWIS.Text = $"{npc.Wisdom}";
            txtCHR.Text = $"{npc.Charisma}";
            txtPrimaryWeapon.Text = npc.PrimaryWeapon?.GetSignature();
            txtSecondaryWeapon.Text = npc.SecondaryWeapon?.GetSignature();

            var newInventory = new Inventory();
            foreach(var item in npc.Inventory.Items)
            {
                if (item == npc.PrimaryWeapon || item == npc.SecondaryWeapon)
                    continue;
                newInventory.Add(item);
            }
            txtInventory.Text = newInventory.GetInventorySignatures();
        }

        public void SetCharacter(ICharacter npc)
        {
            LoadCharacter(npc);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ResetErrors();
            ValidateForm();

            if (_formHasErrors)
            {
                Console.Beep(1000, 300);
                return;
            }

            //SaveForm();
            this.Close();
            
        }

        /// <summary>
        /// Removes all error message tool tips from all of the labels with errors
        /// </summary>
        private void ResetErrors()
        {
            toolTip.RemoveAll();
            _formHasErrors = false;
            var textBoxFromLabel = GetLabelTextboxMap();

            foreach(var label in textBoxFromLabel.Keys)
            {
                label.ForeColor = Color.Black;
            }
        }

        private Dictionary<Label, TextBox> GetLabelTextboxMap()
        {
            var textBoxFromLabel = new Dictionary<Label, TextBox>()
            {
                [lblName] = txtName,
                [lblRace] = txtRace,
                [lblDescription] = txtDescription,
                [lblHP] = txtHPMaxHP,
                [lblAC] = txtAC,
                [lblLV] = txtLV,
                [lblEXP] = txtEXP,
                [lblSTR] = txtSTR,
                [lblDEX] = txtDEX,
                [lblCON] = txtCON,
                [lblINT] = txtINT,
                [lblWIS] = txtWIS,
                [lblCHR] = txtCHR,
                [lblPrimaryWeapon] = txtPrimaryWeapon,
                [lblSecondaryWeapon] = txtSecondaryWeapon,
                [lblInventory] = txtInventory
            };

            return textBoxFromLabel;
        }

        private void ValidateForm()
        {
            // MAP from label -> textBox
            var textBoxFromLabel = GetLabelTextboxMap();

            // Which controls fall into what validation rules
            List<Label> required = new() { lblName, lblRace };
            List<Label> characterStats = new() { lblSTR, lblDEX, lblCON, lblINT, lblWIS, lblCHR };
            List<Label> singleSignature = new() { lblPrimaryWeapon, lblSecondaryWeapon };
            List<Label> multipleSignatures = new() { lblInventory };
            List<Label> numeric = new() { lblAC, lblLV, lblEXP };


            // Check every textbox that should NOT be blank
            foreach (var label in required)
            {
                string text = textBoxFromLabel[label].Text;
                if (string.IsNullOrEmpty(text))
                    SetError(label, "Required field");
            }

            // Validate character stats are numbers from 1-20
            foreach (var label in characterStats)
            {
                string text = textBoxFromLabel[label].Text;
                if (int.TryParse(text, out int stat))
                {
                    if (stat > 20 || stat < 1)
                        SetError(label, "Must be between 1 and 20");
                }
                else
                {
                    SetError(label, "Stats must be numbers");
                }
            }

            foreach (var label in singleSignature)
            {
                string text = textBoxFromLabel[label].Text;
                var sf = new SignatureFactory();
                string errorMessage = sf.ValidateSignature(text);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    SetError(label, errorMessage);
                }
            }

            foreach (var label in multipleSignatures)
            {
                string text = textBoxFromLabel[label].Text;
                var sf = new SignatureFactory();
                string errorMessage = sf.ValidateMultipleSignatures(text);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    SetError(label, errorMessage);
                }
            }

            foreach (var label in numeric)
            {
                string text = textBoxFromLabel[label].Text;
                if (int.TryParse(text, out int number))
                {
                    if (number < 0)
                    {
                        SetError(label, "No negative numbers");
                    }
                }
                else
                {
                    SetError(label, "Must be a number");
                }
            }
        }

        private bool _formHasErrors = false;
        private void SetError(Label label, string message)
        {
            label.ForeColor = Color.Red;
            toolTip.SetToolTip(label, message);
            _formHasErrors = true;
        }

    }
}
