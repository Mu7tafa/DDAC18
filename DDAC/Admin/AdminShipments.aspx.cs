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
    public partial class AdminShipments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            String cquery = "SELECT * FROM Shipment";
            SqlCommand ccmd = new SqlCommand(cquery, con);
            DataTable dataTable = new DataTable();
            CRepeater.DataSource = ccmd.ExecuteReader();
            CRepeater.DataBind();
            con.Close();
        }

        protected void Delete(object sender, CommandEventArgs e)
        {
            try
            {
                SqlConnection con = DB_helper.GetConnection();
                con.Open();
                string query = "DELETE FROM Shipment WHERE shipID = @pid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pid", e.CommandArgument.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("AdminShipments.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("ErrorL  " + ex.ToString());
            }
        }
    }
}