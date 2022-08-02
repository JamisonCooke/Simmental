using Simmental.UI;
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
            txtPrimaryWeapon.Text = npc.PrimaryWeapon.GetSignature();
            txtSecondaryWeapon.Text = npc.SecondaryWeapon.GetSignature();
            txtInventory.Text = npc.Inventory.GetInventorySignatures();
        }


    }
}
