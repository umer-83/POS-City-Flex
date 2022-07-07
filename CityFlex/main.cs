using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CityFlex
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        private string name;
        public string Staff
        {
            get { return name; }
            set { name = value; }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            items ite = new items();
            ite.Show();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? You want to exit.", "Please Confrim", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            products p = new products();
            p.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            sales s = new sales();

            s.Show();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            report r = new report();
            r.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            users u = new users();
            u.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            stock st = new stock();
            st.Show();
        }

        private void main_Load(object sender, EventArgs e)
        {
            label7.Text = Staff;
        }
    }
}
