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
    public partial class products : Form
    {
        public products()
        {
            InitializeComponent();
            fillcombo1();
            fillcombo2();

        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UCM5P8G\\SQLEXPRESS;Initial Catalog=cityflex;Persist Security Info=True;User ID=umer;Password=678");
        public void load()
        {
            SqlDataAdapter ada = new SqlDataAdapter("select * from product", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void fillcombo1()
        {


            string mequery = "select * from media";
            SqlCommand cmd = new SqlCommand(mequery, con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            con.Close();


        }
        void fillcombo2()
        {


            string squery = "select * from size";
            SqlCommand cmd1 = new SqlCommand(squery, con);
            con.Open();
            DataSet ds1 = new DataSet();
            SqlDataAdapter ada1 = new SqlDataAdapter(cmd1);
            ada1.Fill(ds1);
            comboBox2.DataSource = ds1.Tables[0];
            comboBox2.DisplayMember = "size";
            comboBox2.ValueMember = "id";
            con.Close();


        }


        private void products_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand();
            cm.Connection = con;
            cm.CommandText = "insert into	product(name,medianame,sizename,qty)values(@name,@medianame,@sizename,@qty)";
            cm.Parameters.AddWithValue("@name", textBox1.Text);
            cm.Parameters.AddWithValue("@medianame", comboBox1.SelectedValue);
            cm.Parameters.AddWithValue("@sizename", comboBox2.SelectedValue);
            cm.Parameters.AddWithValue("@qty", textBox2.Text);
            con.Open();
            cm.ExecuteNonQuery();
            MessageBox.Show("Record added successfully!");
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
            SqlDataAdapter ada = new SqlDataAdapter("select * from product where id='" + id + "'", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                textBox4.Text = dr["id"].ToString();
                textBox3.Text = dr["name"].ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from	product where id = '" + textBox4.Text + "'";

            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record deleted successfully!");
            con.Close();
            load();

        }
    }
}
