using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CityFlex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = progressBar1.Value + 1;
            if (progressBar1.Value >= 100)
            {
                login ln = new login();
                this.Hide();
                ln.Show();
                timer1.Enabled = true;
                progressBar1.Visible = false;
                timer1.Enabled = false;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
