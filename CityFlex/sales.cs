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
    public partial class sales : Form
    {
        public sales()
        {
            InitializeComponent();
            fillcombo1();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UCM5P8G\\SQLEXPRESS;Initial Catalog=cityflex;Persist Security Info=True;User ID=umer;Password=678");
        public void invoice()
        {
            if (con.State != ConnectionState.Open)
            { con.Open(); }
            string query = "select max(id) from salez";
            SqlCommand cmd1 = new SqlCommand(query, con);
            SqlDataReader dr;
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    label13.Text = "1";
                }
                else
                {
                    int a;
                    a = int.Parse(dr[0].ToString());
                    a = a + 1;
                    label13.Text = a.ToString();
                }
                con.Close();
            }
        }
        public void updatebd()
        {
            for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
            {
                string itmno = dataGridView1.Rows[row].Cells[1].Value.ToString();
                string itmlngth = dataGridView1.Rows[row].Cells[3].Value.ToString();
                string itmqty = dataGridView1.Rows[row].Cells[4].Value.ToString();
                string t = "select * from product where name='" + itmno + "'";
                string oldqty = "", newqty = "";
                Connew.DbSearch(t);
                while (Connew.dr.Read())
                {
                    oldqty = Connew.dr[4].ToString();
                    newqty = (float.Parse(oldqty) - (float.Parse(itmlngth) * float.Parse(itmqty))).ToString();


                }
                string t2 = "update product SET qty='" + newqty + "'WHERE name='" + itmno + "'";
                Connew.DbUpdate(t2);
            }
        }
        private string name;
        public string Staff
        {
            get { return name; }
            set { name = value; }
        }
        public static float totalprice = 0;
        public static float paymnt = 0;
        public static float blnc = 0;
        int i;
        void fillcombo1()
        {


            string pquery = "select * from product";
            SqlCommand cmd = new SqlCommand(pquery, con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            con.Close();


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.Enabled = true;
                textBox2.Focus();
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox3.Enabled = true;
                textBox3.Focus();
            }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox4.Enabled = true;
                textBox4.Focus();
            }

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox5.Enabled = true;
                textBox5.Focus();
            }

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox6.Enabled = true;
                textBox6.Focus();
            }

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string txt = "select * from product where name='" + comboBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(txt, con);
                if (con.State != ConnectionState.Open)
                { con.Open(); }
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    float price = float.Parse(textBox3.Text.ToString()) * float.Parse(textBox4.Text.ToString()) * float.Parse(textBox6.Text.ToString()) * float.Parse(textBox5.Text.ToString());
                    totalprice = totalprice + price;
                    dataGridView1.Rows.Add(dataGridView1.RowCount, r[1], textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim(), price);
                }
                label11.Text = "" + (dataGridView1.RowCount - 1) + "";
                label10.Text = "" + totalprice + "";
                con.Close();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into salex(clientname,phone,productname,width,length,qty,price,total,date,time)values(@clientname,@phone,@productname,@width,@length,@qty,@price,@total,@date,@time)";
                    cmd.Parameters.AddWithValue("@clientname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox2.Text);

                    cmd.Parameters.AddWithValue("@productname", dataGridView1.Rows[i].Cells[1].Value);
                    cmd.Parameters.AddWithValue("@width", dataGridView1.Rows[i].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@length", dataGridView1.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@qty", dataGridView1.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@price", dataGridView1.Rows[i].Cells[5].Value);
                    cmd.Parameters.AddWithValue("@total", dataGridView1.Rows[i].Cells[6].Value);
                    cmd.Parameters.AddWithValue("@date", lbldate.Text);
                    cmd.Parameters.AddWithValue("@time", lbltime.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    updatebd();
                    con.Close();

                }
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandText = "insert into salez(id,remaining,phone,clientname,paid,grosstotal)values(@id,@remaining,@phone,@clientname,@paid,@grosstotal)";
                cmd2.Parameters.AddWithValue("@id", label13.Text);
                cmd2.Parameters.AddWithValue("@remaining", label15.Text);
                cmd2.Parameters.AddWithValue("@phone", textBox2.Text);
                cmd2.Parameters.AddWithValue("@clientname", textBox1.Text);
                cmd2.Parameters.AddWithValue("@paid", label16.Text);
                cmd2.Parameters.AddWithValue("@grosstotal", label10.Text);
                con.Open();

                cmd2.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            printDocument1.Print();
            MessageBox.Show("Record Added!");
            dataGridView1.Rows.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            label11.Text = "0";
            label10.Text = "0";
            label15.Text = "0";
            label16.Text = "0";
            textBox1.Focus();
            totalprice = 0;
            invoice();
            

        }

        private void sales_Load(object sender, EventArgs e)
        {
            label12.Text = Staff;
            lbldate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lbltime.Text = DateTime.Now.ToShortTimeString();
            invoice();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void removeitm()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string tot = row.Cells[6].Value.ToString();
                if (!row.IsNewRow)
                {
                    dataGridView1.Rows.Remove(row);

                    label10.Text = (float.Parse(label10.Text) - float.Parse(tot)).ToString();
                    float finaltot = float.Parse(label10.Text);
                    totalprice = finaltot;
                    label11.Text = "" + (dataGridView1.RowCount - 1) + "";

                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("No Transactions");
                textBox1.Focus();
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you going to delete this row?", "Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    removeitm();
                    textBox1.Focus();
                }

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            payment py = new payment();
            py.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawString("Invoice No : " + label13.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(630, 113));
            e.Graphics.DrawString("City Flex Jhang", new Font("Courier New", 36, FontStyle.Regular), Brushes.Black, new Point(190, 3));
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, 48));
            e.Graphics.DrawString("Adress# Burji Wala Chowk Near Educational Complex.     Phone No# 0300-8651619", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(30, 68));
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, 83));

            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(25, 113));
            e.Graphics.DrawString("Customer's Slip  ", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(630, 143));
            e.Graphics.DrawString("Client Name:  " + textBox1.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(25, 143));
            e.Graphics.DrawString("Phone  No:    " + textBox2.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(25, 173));

            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, 188));
            e.Graphics.DrawString("No#", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(30, 208));
            e.Graphics.DrawString("Product Name", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(80, 208));
            e.Graphics.DrawString("Width", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(310, 208));
            e.Graphics.DrawString("Length", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(410, 208));
            e.Graphics.DrawString("Quantity", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(510, 208));
            e.Graphics.DrawString("Price", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(630, 208));
            e.Graphics.DrawString("Total", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(730, 208));
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, 233));
            int yPos = 253;

            for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
               
                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[0].Value.ToString(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(30, yPos));
                        e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[1].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(80, yPos));
                        e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[2].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(310, yPos));
                        e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[3].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(410, yPos));
                        e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[4].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(510, yPos));
                        e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[5].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(630, yPos));
                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[6].Value.ToString(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(730, yPos));

                        yPos += 30;
                   
            }

            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, yPos));

            e.Graphics.DrawString("Total Amount:   Rs#" + label10.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(550, yPos + 30));
            e.Graphics.DrawString("Paid Amount:    Rs#" + label16.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(550, yPos + 60));
            e.Graphics.DrawString("Remaining:      Rs#" + label15.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(550, yPos + 90));
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, 545));
            e.Graphics.DrawString("Invoice No : " + label13.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(630, 660));
            e.Graphics.DrawString("City Flex Jhang", new Font("Courier New", 36, FontStyle.Regular), Brushes.Black, new Point(190, 560));
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, 605));
            e.Graphics.DrawString("Adress# Burji Wala Chowk Near Educational Complex.     Phone No# 0300-8651619", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(30, 625));
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, 640));

            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(25, 660));
            e.Graphics.DrawString("Company's Slip  ", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(630, 690));
            e.Graphics.DrawString("Client Name:  " + textBox1.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(25, 690));
            e.Graphics.DrawString("Phone  No:    " + textBox2.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(25, 720));

            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, 735));
            e.Graphics.DrawString("No#", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(30, 755));
            e.Graphics.DrawString("Product Name", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(80, 755));
            e.Graphics.DrawString("Width", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(310, 755));
            e.Graphics.DrawString("Length", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(410, 755));
            e.Graphics.DrawString("Quantity", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(510, 755));
            e.Graphics.DrawString("Price", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(630, 755));
            e.Graphics.DrawString("Total", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(730, 755));
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, 780));
            int yos = 800;

            for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[0].Value.ToString(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(30, yos));
                e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[1].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(80, yos));
                e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[2].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(310, yos));
                e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[3].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(410, yos));
                e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[4].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(510, yos));
                e.Graphics.DrawString((string)dataGridView1.Rows[i].Cells[5].Value, new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(630, yos));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[6].Value.ToString(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(730, yos));

                yos += 30;

            }

            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------", new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(0, yos));

            e.Graphics.DrawString("Total Amount:   Rs#" + label10.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(550, yos + 30));
            e.Graphics.DrawString("Paid Amount:    Rs#" + label16.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(550, yos + 60));
            e.Graphics.DrawString("Remaining:      Rs#" + label15.Text.Trim(), new Font("Courier New", 12, FontStyle.Regular), Brushes.Black, new Point(550, yos + 90));

            
        }

        private void sales_Activated(object sender, EventArgs e)
        {
            label15.Text = ""+blnc;
            label16.Text = "" +paymnt;
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
