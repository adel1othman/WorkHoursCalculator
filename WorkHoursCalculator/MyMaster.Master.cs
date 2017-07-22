using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkHoursCalculator
{
    public partial class MyMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {   //Promijenite con
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Korisnici where Korisnicko_ime =@username and Lozinka=@password", con);
            string myUsername = Username.Value;
            string myPassword = Password.Value;
            cmd.Parameters.AddWithValue("@username", myUsername);
            cmd.Parameters.AddWithValue("@password", myPassword);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session.Add("Korisnici", Username);
                Response.Redirect("Advanced.aspx");

            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

        }
    }
}