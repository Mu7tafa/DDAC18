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
    public partial class AdminStaff : System.Web.UI.Page
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
            String cquery = "SELECT * FROM Staff";
            SqlCommand ccmd = new SqlCommand(cquery, con);
            DataTable dataTable = new DataTable();
            CRepeater.DataSource = ccmd.ExecuteReader();
            CRepeater.DataBind();
            con.Close();
        }

        protected void Addnew_Click(object sender, EventArgs e)
        {
            Populate_Dropdwown();
        }

        protected void Delete(object sender, CommandEventArgs e)
        {
            try
            {
                SqlConnection con = DB_helper.GetConnection();
                con.Open();
                string query = "DELETE FROM Staff WHERE sid = @sid";
                string query2 = "DELETE FROM Users WHERE uid = @uid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@sid", e.CommandArgument.ToString());
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@uid", e.CommandArgument.ToString());
                cmd2.ExecuteNonQuery();
                con.Close();
                
                Response.Redirect("Admin/AdminStaff.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("ErrorL  " + ex.ToString());
            }
        }
        private void Populate_Dropdwown()
        {
            DataTable subjects = new DataTable();


            using (SqlConnection con = DB_helper.GetConnection())
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT pid, location FROM Port", con);
                    adapter.Fill(subjects);

                    portdp.DataSource = subjects;
                    portdp.DataTextField = "location";
                    portdp.DataValueField = "pid";
                    portdp.DataBind();

                    cont1.Visible = false;
                    cont2.Visible = true;
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
        }
        protected void Register_Click(object sender, EventArgs e)
        {
            SqlConnection con = DB_helper.GetConnection();
            con.Open();
            String cquery = "SELECT COUNT(*) FROM Users WHERE email='" + email.Value + "'";
            SqlCommand ccmd = new SqlCommand(cquery, con);
            int temp = Convert.ToInt32(ccmd.ExecuteScalar().ToString());
            if (temp == 1)
            {
                Response.Write("User already exists");
            }
            else
            {
                String guid = System.Guid.NewGuid().ToString();
                try
                {
                    String query = "INSERT INTO Staff (sid, portID, contact, name, email) " +
                        "values(@ID, @Port, @Contact, @Name, @Email)";

                    String query2 = "INSERT INTO Users (uid, email, password, userrole) values(@uid, @Email, @Password, @Role)";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@uid", guid);
                    cmd2.Parameters.AddWithValue("@Email", email.Value);
                    cmd2.Parameters.AddWithValue("@Password", password.Value);
                    cmd2.Parameters.AddWithValue("@Role", "Staff");
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", guid);
                    cmd.Parameters.AddWithValue("@Port", portdp.SelectedValue);
                    cmd.Parameters.AddWithValue("@Contact", contact.Value);
                    cmd.Parameters.AddWithValue("@Name", name.Value);
                    cmd.Parameters.AddWithValue("@Email", email.Value);

                    cmd.ExecuteNonQuery();
                    Response.Redirect("~/Admin/AdminStaff.aspx");

                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.ToString());
                }

            }

        }
    }
}