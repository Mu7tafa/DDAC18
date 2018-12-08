using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDAC.Staff
{
    public partial class StaffHome : System.Web.UI.Page
    {
        public string uid = String.Empty;
        public string port = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Userses"] != null)
            {

                CustomerClass customer = (CustomerClass)Session["Userses"];
                uid = customer.id;
                SqlConnection con = DB_helper.GetConnection();
                con.Open();
                String cquery = "SELECT * from Staff Where sid = '" + uid + "'";
                SqlCommand command = new SqlCommand(cquery, con);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        port = String.Format("{0}", reader["portID"]);
                    }
                }
                BindShipment(port);
                con.Close();

            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        private void BindShipment(String portID)
        {
            SqlConnection con = DB_helper.GetConnection();
            con.Open();
            String query = "SELECT * FROM Shipment, Port WHERE pid = depPID AND arPID = '" + portID + "' AND statusAP = 'Approved' AND statusAR != 'Delivered'";
            String cquery = "SELECT * FROM Shipment, Port WHERE pid = arPID AND depPID = '"+ portID +"' AND statusAP != 'Approved'";
            SqlCommand ccmd = new SqlCommand(cquery, con);
            CRepeater.DataSource = ccmd.ExecuteReader();
            CRepeater.DataBind();
            con.Close();

            SqlConnection con2 = DB_helper.GetConnection();
            con2.Open();
            SqlCommand ccmd2 = new SqlCommand(query, con2);
            CRepeater2.DataSource = ccmd2.ExecuteReader();
            CRepeater2.DataBind();
            con.Close();
        }

        protected void Approve(object sender, CommandEventArgs e)
        {
            try
            {
                SqlConnection con = DB_helper.GetConnection();
                con.Open();
                string query = "UPDATE Shipment SET statusAP = @ap WHERE shipID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", e.CommandArgument.ToString());
                cmd.Parameters.AddWithValue("@ap", "Approved");
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("StaffHome.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("ErrorL  " + ex.ToString());
            }
        }
        protected void Decline(object sender, CommandEventArgs e)
        {
            try
            {
                SqlConnection con = DB_helper.GetConnection();
                con.Open();
                string query = "UPDATE Shipment SET statusAP = @ap WHERE shipID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", e.CommandArgument.ToString());
                cmd.Parameters.AddWithValue("@ap", "Declined");
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("StaffHome.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("ErrorL  " + ex.ToString());
            }
        }
        protected void Confirm(object sender, CommandEventArgs e)
        {
            try
            {
                SqlConnection con = DB_helper.GetConnection();
                con.Open();
                string query = "UPDATE Shipment SET statusAP = @ap WHERE shipID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", e.CommandArgument.ToString());
                cmd.Parameters.AddWithValue("@ap", "Delivered");
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("StaffHome.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("ErrorL  " + ex.ToString());
            }
        }
    }
}