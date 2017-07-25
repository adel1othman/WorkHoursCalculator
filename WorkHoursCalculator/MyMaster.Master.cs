using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkHoursCalculator
{
    public partial class MyMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            if (Session["Korisnici"] != null)
            {
                container.Visible = false;
                BtnLogout.Visible = true;
                LblWelcome.Text = "Welcome " + (string)Session["Korisnici"];
                LblWelcome.Visible = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {   //Promijenite con
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Korisnici where (Korisnicko_ime like @username or Email like @username) and Lozinka like @password", con);
            string myUsername = Username.Value;
            string myPassword = Password.Value;
            cmd.Parameters.AddWithValue("@username", myUsername);
            cmd.Parameters.AddWithValue("@password", myPassword);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session.Add("Korisnici", myUsername);
                // ovdje treba ići na koju stranicu da redirekta po loginu
                Response.Redirect("Advanced.aspx");

            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Regex rUser = new Regex(@"[a-zA-Z0-9_\-]{6,20}");

            string username = Username2.Value;
            string email = Email.Value;
            string password = Password1.Value;
            
            if (rUser.IsMatch(username))
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");
                SqlCommand cmd = new SqlCommand("insert into Korisnici values(@username, @password, @email)", con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);

                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();

                if (rowsAffected != 0)
                {
                    Session.Add("Korisnici", username);
                    Response.Redirect("Advanced.aspx");
                }
            }
            else
            {
                lblError.Text = "Username must have 6-20 caracter";
                lblError.Visible = true;
            }
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            BtnLogout.Visible = false;
            LblWelcome.Visible = false;
            // ovdje treba ići na koju stranicu da redirekta po logoutu (zasad je login i logout postavljen ovako da se vidi da radi nešto)
            Response.Redirect("Basic2.aspx");
        }
    }
}