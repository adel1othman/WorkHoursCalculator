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
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                Session.Add("Korisnici", myUsername);
                Session.Add("idKor", read["Id_korisnik"]);
                // ovdje treba ići na koju stranicu da redirekta po loginu
                con.Close();
                Response.Redirect("Advanced.aspx");
            }
            con.Close();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = Username2.Value;
            string email = Email.Value;
            string password = Password1.Value;

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

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            BtnLogout.Visible = false;
            LblWelcome.Visible = false;
            Response.Redirect("Default.aspx");
        }
    }
}