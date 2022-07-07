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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        //string connString = @"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename="D:\D\New Volume\vs\repos\CityFlex\CityFlexcityflex.mdf";Integrated Security=True";
       
        SqlConnection con = new SqlConnection("(LocalDB)\\MSSQLLocalDB;AttachDbFilename=;Integrated Security=True");
        int count;
        private void button1_Click(object sender, EventArgs e)
        {
            string username, password;
            username = textBox1.Text;
            password = textBox2.Text;
              {
                MessageBox.Show("System has been Blocked", "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else if (username == "" && password == "")
            {
                //label3.Text = "Username and Password cannot be blank";
            }
            else
            {
                try
                {
                    string query = "select * from login where username = '" + textBox1.Text.Trim() + "' and password='" + textBox2.Text.Trim() + "'";
                    SqlDataAdapter data = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    data.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        label3.Text = "Welcome to System!";
                        main ma = new main();
                        this.Hide();
                        ma.Staff = textBox1.Text;
                        ma.Show();

                    }
                    else
                    {
                        label3.Text = "Try Again!";
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Focus();
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }

            }



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            users u = new users();
            u.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
