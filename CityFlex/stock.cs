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
    public partial class stock : Form
    {
        public stock()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UCM5P8G\\SQLEXPRESS;Initial Catalog=cityflex;Persist Security Info=True;User ID=umer;Password=678");
        public void load()
        {
            SqlDataAdapter ada = new SqlDataAdapter("SELECT product.id,media.name,product.name,product.qty FROM product INNER JOIN media ON product.medianame = media.id", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Item Code";
            dataGridView1.Columns[1].HeaderText = "Media Name";
            dataGridView1.Columns[2].HeaderText = "Product Name";
            dataGridView1.Columns[3].HeaderText = "StockIn";
            label2.Text = "0";
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                label2.Text = Convert.ToString(float.Parse(label2.Text) + float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
            }
        }
        public void loadb()
        {
            SqlDataAdapter ada = new SqlDataAdapter("SELECT product.id,media.name,product.name,product.qty FROM product INNER JOIN media ON product.medianame = media.id WHERE product.name='" + textBox1.Text + "'", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Item Code";
            dataGridView1.Columns[1].HeaderText = "Media Name";
            dataGridView1.Columns[2].HeaderText = "Product Name";
            dataGridView1.Columns[3].HeaderText = "StockIn";
            label2.Text = "0";
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                label2.Text = Convert.ToString(float.Parse(label2.Text) + float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
            }
        }


        private void stock_Load(object sender, EventArgs e)
        {
            load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadb();
        }
    }
}
