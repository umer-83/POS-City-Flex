using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CityFlex
{
    class Connew
    {
        public static SqlConnection con = new SqlConnection("Data Source=DESKTOP-UCM5P8G\\SQLEXPRESS;Initial Catalog=cityflex;Persist Security Info=True;User ID=umer;Password=678");
        public static SqlCommand poscmd = new SqlCommand();
        public static SqlDataReader dr;
        public static void DbSearch(string txt)
        {
            con.Close();
            poscmd.Connection = con;
            poscmd.CommandText = txt;
            con.Open();
            dr = poscmd.ExecuteReader();
        }
        public static void DbUpdate(string txt)
        {
            con.Close();
            poscmd.Connection = con;
            poscmd.CommandText = txt;
            con.Open();
            poscmd.ExecuteNonQuery();


        }

    }
}
