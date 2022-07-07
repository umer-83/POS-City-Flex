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
    public partial class payment : Form
    {
        public payment()
        {
            InitializeComponent();
        }
        public void paymentprocess()
        {
            float a = float.Parse(textBox2.Text);
            
                sales.paymnt = a;
                sales.blnc = a - sales.totalprice;
                this.Close();
                new sales().Activate();
            

        }

        private void payment_Load(object sender, EventArgs e)
        {
            textBox1.Text = sales.totalprice.ToString();

            textBox2.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            paymentprocess();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
