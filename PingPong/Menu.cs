using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Menu : Form
    {
        public string Tries;
        public Menu()
        {
            InitializeComponent();
            MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void start_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                label2.Visible = false;
                label1.Visible = true;
            }
            else
            {
                int parsedValue;
                label1.Visible = false;
                if (!int.TryParse(textBox2.Text, out parsedValue))
                {
                    label2.Visible = true;
                }
                else
                {
                    label1.Visible = false;
                    label2.Visible = false;
                    this.DialogResult = DialogResult.OK;
                    Tries = textBox2.Text;
                    Close();
                }
            }

        }

        private void Menu_Load(object sender, EventArgs e)
        {
           
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
