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
    public partial class items : Form
    {
        public items()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UCM5P8G\\SQLEXPRESS;Initial Catalog=cityflex;Persist Security Info=True;User ID=umer;Password=678");
        public void load()
        {
            SqlDataAdapter ada = new SqlDataAdapter("select * from media", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public void loads()
        {
            SqlDataAdapter ada = new SqlDataAdapter("select * from size", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into	media(name)values(@name)";
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record added successfully!");
            con.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
            SqlDataAdapter ada = new SqlDataAdapter("select * from media where id='" + id + "'", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                textBox2.Text = dr["id"].ToString();
                textBox3.Text = dr["name"].ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from	media where id = '" + textBox2.Text + "'";

            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record deleted successfully!");
            con.Close();
            load();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            loads();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into	size(size)values(@size)";
            cmd.Parameters.AddWithValue("@size", textBox6.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record added successfully!");
            con.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["id"].Value.ToString());
            SqlDataAdapter ada = new SqlDataAdapter("select * from size where id='" + id + "'", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                textBox5.Text = dr["id"].ToString();
                textBox4.Text = dr["size"].ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from	size where id = '" + textBox5.Text + "'";

            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record deleted successfully!");
            con.Close();
            loads();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
