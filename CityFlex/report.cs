using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CityFlex
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UCM5P8G\\SQLEXPRESS;Initial Catalog=cityflex;Persist Security Info=True;User ID=umer;Password=678");

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlDataAdapter ada = new SqlDataAdapter("select * from salex where date between '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "'and'" + dateTimePicker2.Value.ToString("dd/MM/yyyy") + "'", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView1.DataSource = dt;
            label1.Text = "0";
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                label1.Text = Convert.ToString(int.Parse(label1.Text) + int.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString()));
            }

        }
    }
}
