using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDAC.Customer
{
    public partial class CutomerHome : System.Web.UI.Page
    {
        public string uid = String.Empty;
        public string shid;
        protected void Page_Load(object sender, EventArgs e)
        {
            cont2.Visible = false;
            if (Session["Userses"] != null)
            {
                CustomerClass customer = (CustomerClass)Session["Userses"];
                uid = customer.id;
                BindShipment();
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private void BindShipment()
        {
            SqlConnection con = DB_helper.GetConnection();
            con.Open();
            String cquery = "SELECT * FROM Shipment WHERE customerID = '" + uid + "'";
            SqlCommand ccmd = new SqlCommand(cquery, con);
            DataTable dataTable = new DataTable();
            CRepeater.DataSource = ccmd.ExecuteReader();
            CRepeater.DataBind();
            con.Close();
        }

        protected void Resubmit(object sender, CommandEventArgs e)
        {
            Populate_Dropdwown();
            if (e.CommandArgument.ToString() != null)
            {
                Session["ship"] = e.CommandArgument.ToString();
                shid = e.CommandArgument.ToString();
                Populate_Edit();
            }
            

        }

        private void Populate_Edit()
        {
            SqlConnection con = DB_helper.GetConnection();
            con.Open();
            String cquery = "SELECT * from Shipment Where shipID = '" + shid + "'";
            SqlCommand command = new SqlCommand(cquery, con);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    datepicker.Value = String.Format("{0}", reader["date"]);
                    portar.SelectedValue = String.Format("{0}", reader["arPID"]);
                    portdp.SelectedValue = String.Format("{0}", reader["depPID"]);
                    description.Value = String.Format("{0}", reader["details"]);
                }
            }
            con.Close();
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

                    portar.DataSource = subjects;
                    portar.DataTextField = "location";
                    portar.DataValueField = "pid";
                    portar.DataBind();
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

        protected void Addnew_Click(object sender, EventArgs e)
        {
            Populate_Dropdwown();
        }

        protected void Log_Click(object sender, EventArgs e)
        {
            SqlConnection con = DB_helper.GetConnection();
            con.Open();
            String guid = System.Guid.NewGuid().ToString();
            
            if (Session["ship"] == null)
            {
                try
                {

                    String query2 = "INSERT INTO Shipment (shipID, arPID, depPID, statusAP, statusAR, customerID, details, date) " +
                                                   "values(@shipID, @arPID, @depPID, @statusAP, @statusAR, @customerID, @details, @date)";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@shipID", guid);
                    cmd2.Parameters.AddWithValue("@arPID", portar.SelectedValue);
                    cmd2.Parameters.AddWithValue("@depPID", portdp.SelectedValue);
                    cmd2.Parameters.AddWithValue("@statusAP", "In Progress");
                    cmd2.Parameters.AddWithValue("@statusAR", "Not Arrived");
                    cmd2.Parameters.AddWithValue("@CustomerID", uid);
                    cmd2.Parameters.AddWithValue("@details", description.Value);
                    cmd2.Parameters.AddWithValue("@date", datepicker.Value);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("CustomerHome.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.ToString());
                }
            }
            else
            {
                try
                {

                    String query2 = "UPDATE Shipment SET arPID = @arPID, depPID = @depPID, details = @details, date = @date " +
                        "WHERE shipID = @shipID";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@shipID", Session["ship"].ToString());
                    cmd2.Parameters.AddWithValue("@arPID", portar.SelectedValue);
                    cmd2.Parameters.AddWithValue("@depPID", portdp.SelectedValue);
                    cmd2.Parameters.AddWithValue("@details", description.Value);
                    cmd2.Parameters.AddWithValue("@date", datepicker.Value);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    Session["ship"] = null;
                    Response.Redirect("CustomerHome.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.ToString() + "TTTTTTTT" + uid + "TTTTTTTT" + portar.SelectedValue);
                }
            }
        }
    }
}