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
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");
            string Username = Username2.Value;
            string email = Email.Value;
            string password = Password2.Value;
            SqlCommand cmd = new SqlCommand("insert into Korisnici values(@fullname, @password, @email)", con);
            cmd.Parameters.AddWithValue("@fullname", Username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@email", email);

            con.Open();

            int rowsAffected = cmd.ExecuteNonQuery();

            con.Close();
            
            if (rowsAffected != 0)
            {
                Session.Add("Korisnici", Username);
                Response.Redirect("Advanced.aspx");
            }
        }

        //private void MyValidationEnabled()
        //{
        //    RequiredFieldValidatorUN.Enabled = true;
        //    RequiredFieldValidatorE.Enabled = true;
        //    RequiredFieldValidatorP.Enabled = true;
        //    RequiredFieldValidator1PC.Enabled = true;
        //    CompareValidator1.Enabled = true;
        //    RegularExpressionValidatorUN.Enabled = true;
        //    RegularExpressionValidatorE.Enabled = true;
        //    RegularExpressionValidatorP.Enabled = true;
        //}

        //private void MyValidationDisabled()
        //{
        //    RequiredFieldValidatorUN.Enabled = false;
        //    RequiredFieldValidatorE.Enabled = false;
        //    RequiredFieldValidatorP.Enabled = false;
        //    RequiredFieldValidator1PC.Enabled = false;
        //    CompareValidator1.Enabled = false;
        //    RegularExpressionValidatorUN.Enabled = false;
        //    RegularExpressionValidatorE.Enabled = false;
        //    RegularExpressionValidatorP.Enabled = false;
        //}

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