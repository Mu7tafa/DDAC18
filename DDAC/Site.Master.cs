using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDAC
{
    public partial class SiteMaster : MasterPage
    {
        public string email = String.Empty;
        public string userrole = String.Empty;
        public string username = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Userses"] != null)
            {
                CustomerClass customer = (CustomerClass)Session["Userses"];
                email = customer.Email;
                userrole = customer.role;
            }
            
            if (Session["Admins"] != null)
            {
                email = Session["Admins"].ToString();
                
            }
        }
        protected void Login_Click(object sender, EventArgs e)
        {
            SqlConnection con = DB_helper.GetConnection();
            con.Open();
            String cquery = "SELECT * from Users Where email = '" + lemail.Value + "' AND password = '" + lpassword.Value + "'";
            SqlCommand command = new SqlCommand(cquery, con);

            if (lemail.Value.Equals("Admin@ddac.com") && lpassword.Value.Equals("12345"))
            {
                Session["Admins"] = "Admin@ddac.com";
                Response.Write(email);
                Response.Redirect("~/Admin/AdminHome.aspx");

            }

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    createSession(String.Format("{0}", reader["uid"]), email = String.Format("{0}", reader["email"]), String.Format("{0}", reader["userrole"]));
                    if (String.Format("{0}", reader["userrole"]).ToLower() == "customer")
                        Response.Redirect("~/Customer/CustomerHome.aspx");
                    else if (String.Format("{0}", reader["userrole"]) == "Staff")
                        Response.Redirect("~/Staff/StaffHome.aspx");
                    else
                        Response.Redirect("~/Default.aspx");
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Login Unsuccessful, please try again')", true);
            }
            con.Close();
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            SqlConnection con = DB_helper.GetConnection();
            con.Open();
            String cquery = "SELECT COUNT(*) FROM Users WHERE email='" + remail.Value + "'";
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
                    String query = "INSERT INTO Customer (cid, Name, companyName, contact, email, personInCharge) " +
                        "values(@ID, @Name, @CN, @Contact, @Email, @PC)";

                    String query2 = "INSERT INTO Users (uid, email, password, userrole) values(@uid, @Email, @Password, @Role)";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@uid", guid);
                    cmd2.Parameters.AddWithValue("@Email", remail.Value);
                    cmd2.Parameters.AddWithValue("@Password", rpassword.Value);
                    cmd2.Parameters.AddWithValue("@Role", "Customer");
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", guid);
                    cmd.Parameters.AddWithValue("@Name", rname.Value);
                    cmd.Parameters.AddWithValue("@CN", rcompanyname.Value);
                    cmd.Parameters.AddWithValue("@Contact", remail.Value);
                    cmd.Parameters.AddWithValue("@PC", rperson.Value);
                    cmd.Parameters.AddWithValue("@Email", remail.Value);

                    cmd.ExecuteNonQuery();

                    createSession(guid, remail.Value, "Customer");

                    //Response.Redirect("Defualt.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.ToString());
                }

            }
        }

        private void createSession(string guid, string Email, string role)
        {
            CustomerClass customer = new CustomerClass();
            customer.id = guid;
            customer.Email = email = Email;
            customer.role = role;

            Session["Userses"] = customer;
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            email = "";
            userrole = "";
            Session["Userses"] = null;
            Session["Admins"] = null;
            Response.Redirect("~/Default.aspx");
        }
    }
}