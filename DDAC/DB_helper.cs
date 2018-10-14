using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DDAC
{
    public class DB_helper
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            return con;
        }

        public static String userLogin(String Email, String Password)
        {
            SqlConnection con = GetConnection();
            con.Open();
            String cquery = "SELECT uid, email, userrole FROM Users WHERE email = '" + Email + "' AND Password = '" + Password + "'";
            SqlCommand ccmd = new SqlCommand(cquery, con);
            Int32 result = (Int32)ccmd.ExecuteScalar();
            Console.WriteLine(String.Format("{0}", result));
            con.Close();
            return null;
        }
    }
}