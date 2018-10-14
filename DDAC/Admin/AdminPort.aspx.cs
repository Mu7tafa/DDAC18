using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDAC.Admin
{
    public partial class AdminPort : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cont2.Visible = false;
            if (Session["Admins"] != null)
            {
                BindStaff();
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        private void BindStaff()
        {
            SqlConnection con = DB_helper.GetConnection();
            con.Open();
            String cquery = "SELECT * FROM Port";
            SqlCommand ccmd = new SqlCommand(cquery, con);
            DataTable dataTable = new DataTable();
            CRepeater.DataSource = ccmd.ExecuteReader();
            CRepeater.DataBind();
            con.Close();
        }

        protected void Addnew_Click(object sender, EventArgs e)
        {
            cont2.Visible = true;
            cont1.Visible = false;
        }

        protected void Delete(object sender, CommandEventArgs e)
        {
            try
            {
                SqlConnection con = DB_helper.GetConnection();
                con.Open();
                string query = "DELETE FROM Port WHERE pid = @pid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pid", e.CommandArgument.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("AdminPort.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("ErrorL  " + ex.ToString());
            }
        }
        protected void Register_Click(object sender, EventArgs e)
        {
            SqlConnection con = DB_helper.GetConnection();
            con.Open();

            String guid = System.Guid.NewGuid().ToString();
            try
            {
                String query2 = "INSERT INTO Port (pid, name, location) values(@pid, @name, @location)";

                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@pid", guid);
                cmd2.Parameters.AddWithValue("@name", email.Value);
                cmd2.Parameters.AddWithValue("@location", password.Value);
                cmd2.ExecuteNonQuery();
                Response.Redirect("AdminPort.aspx");

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }
    }
}