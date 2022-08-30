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
//using System.Threading.Tasks;
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
            if (npc == null) return;

            this.Text = $"{npc.Name} - {npc.Race}";

            txtName.Text = npc.Name;
            txtRace.Text = $"{npc.Race}";
            txtDescription.Text = npc.Description;
            txtHPMaxHP.Text = $"{npc.HP}/{npc.MaxHP}";
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

            var tf = new TaskFactory();
            txtTasks.Text = tf.GetTaskSignatures(npc.Tasks);
        }

        /// <summary>
        /// Returns an NPC, either the original one updated, or a new one, with all the
        /// properties from the form applied to it.
        /// </summary>
        /// <returns></returns>
        private ICharacter SaveNpc()
        {
            ICharacter npc;

            // Make sure the characters Race is reflected in the npc.
            var race = (RaceEnum)Enum.Parse(typeof(RaceEnum), txtRace.Text);
            var characterHelper = new Game.Characters.CharacterHelper();

            npc = characterHelper.FactoryCreate(race);

            SaveToCharacter(npc);
            return npc;
        }

        private void SaveToCharacter(ICharacter npc)
        {
            npc.SetPositionInternal(_position);
            npc.Name = txtName.Text;

            // The race is already set by the NPC being passed in
            // npc.Race = (RaceEnum) Enum.Parse(typeof(RaceEnum), txtRace.Text);

            npc.Description = txtDescription.Text;

            npc.AC = int.Parse(txtAC.Text);
            npc.Level = int.Parse(txtLV.Text);
            npc.Strength = int.Parse(txtSTR.Text);
            npc.Dexterity = int.Parse(txtDEX.Text);
            npc.Constitution = int.Parse(txtCON.Text);
            npc.Intelligence = int.Parse(txtINT.Text);
            npc.Wisdom = int.Parse(txtWIS.Text);
            npc.Charisma = int.Parse(txtCHR.Text);

            npc.HP = int.Parse(txtHPMaxHP.Text.Split('/')[0]);
            npc.MaxHP = int.Parse(txtHPMaxHP.Text.Split('/')[1]);

            string signatures = "";
            if (!string.IsNullOrEmpty(txtPrimaryWeapon.Text))
                signatures += txtPrimaryWeapon.Text + Environment.NewLine;
            if (!string.IsNullOrEmpty(txtSecondaryWeapon.Text))
                signatures += txtSecondaryWeapon.Text + Environment.NewLine;
            signatures += txtInventory.Text;
            npc.Inventory.SetInventorySignatures(signatures);
            if (!string.IsNullOrEmpty(txtPrimaryWeapon.Text))
            {
                npc.PrimaryWeapon = (IWeapon)npc.Inventory.Items.ElementAt(0);
            }
            if (!string.IsNullOrEmpty(txtSecondaryWeapon.Text))
            {
                if (!string.IsNullOrEmpty(txtPrimaryWeapon.Text))
                {
                    npc.SecondaryWeapon = (IWeapon)npc.Inventory.Items.ElementAt(1);
                }
                else
                {
                    npc.SecondaryWeapon = (IWeapon)npc.Inventory.Items.ElementAt(0);
                }
            }
            var tf = new TaskFactory();
            npc.ClearTasks(_game);
            foreach (ITask task in tf.CreateTasks(txtTasks.Text))
            {
                npc.AddTask(_game, task);
            }

        }

        private Action<ICharacter, ICharacter, CharacterSheet> _saveUpdateNpc;
        private ICharacter _oldNpc;
        private Position _position;
        private IGame _game;

        public void SetCharacter(IGame game, ICharacter npc, Action<ICharacter, ICharacter, CharacterSheet> saveUpdateNpc, Position position)
        {
            // void saveUpdateNpc(ICharacter oldNpc, ICharacter newNpc, CharacterSheet character)
            _game = game;
            _oldNpc = npc;
            _saveUpdateNpc = saveUpdateNpc;
            _position = new Position(position);
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

            var newNpc = SaveNpc();
            _saveUpdateNpc(_oldNpc, newNpc, this);
            
            _oldNpc = null;     // Clear the oldNPC so we don't save it again later
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
                [lblInventory] = txtInventory,
                [lblTasks] = txtTasks,
            };

            return textBoxFromLabel;
        }
        private void UpdateSignatureFormatHelper()
        {
            var tf = new TaskFactory();

            // We want to find the text of text where the cursor is currently placed
            int cursorAt = txtTasks.SelectionStart;
            var lines = txtTasks.Text.Split(Environment.NewLine);

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
                    string stamp = line.Split(' ')[0];
                    if (TaskFactory.IsValidStamp(stamp))
                        lblTaskHelper.Text = tf.GetPrettySignatureFormat(stamp);
                    else
                        lblTaskHelper.Text = TaskFactory.AllTasksNames;
                    return;
                }
            }
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

            // HP / MaxHP -- specific validation logic
            var hpArray = txtHPMaxHP.Text.Split('/');
            if (hpArray.Length != 2 || !ValidInt(hpArray[0]) || !ValidInt(hpArray[1]))
            {
                SetError(lblHP, "Enter a number for HP and a number for MaxHP divided by /");
            }

            // Check Race -- it must parse from the enum
            //npc.Race = (RaceEnum)Enum.Parse(typeof(RaceEnum), txtRace.Text);
            if (!Enum.TryParse(typeof(RaceEnum), txtRace.Text, out object o))
                SetError(lblRace, $"Race must be: {string.Join(", ", Enum.GetNames(typeof(RaceEnum)))}");

            var tf = new TaskFactory();
            string error = tf.ValidateMultipleSignatures(txtTasks.Text);
            if (!string.IsNullOrEmpty(error))
            {
                SetError(lblTasks, error);
            }

        }

        private bool ValidInt(string text)
        {
            return int.TryParse(text, out int n) && n > 0;
        }

        private bool _formHasErrors = false;
        private void SetError(Label label, string message)
        {
            label.ForeColor = Color.Red;
            toolTip.SetToolTip(label, message);
            _formHasErrors = true;
        }

        private void CharacterSheet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_oldNpc != null)
            {
                // Let the caller know we are now closed w/out changes--
                _saveUpdateNpc(_oldNpc, null, this);
                _oldNpc = null;
            }
        }

        private void txtTasks_TextChanged(object sender, EventArgs e)
        {
            UpdateSignatureFormatHelper();
        }

        private void txtTasks_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateSignatureFormatHelper();
        }

        private void txtTasks_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateSignatureFormatHelper();
        }
    }
}
