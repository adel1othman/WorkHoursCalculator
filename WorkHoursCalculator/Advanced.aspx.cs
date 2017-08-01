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
    public partial class Advanced : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblError1.Visible = false;
            if (Session["Korisnici"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");
            SqlCommand cmd = new SqlCommand("select * from Kalkulacije where Id_korisnik = @id and Datum like @myDatum", con);
            cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
            cmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));

            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                int fromh = 0, toh = 0, hr = 0;
                while (read.Read())
                {
                    int.TryParse(read["Pocetak_rv"].ToString(), out fromh);
                    int.TryParse(read["Kraj_rv"].ToString(), out toh);
                    int.TryParse(read["Satnica"].ToString(), out hr);
                }

                if (fromh != 0 || toh != 0)
                {
                    TextBox1.Text = fromh.ToString();
                    TextBox2.Text = toh.ToString();
                    TextBox3.Text = hr.ToString();
                }
            }
            con.Close();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void ddlSelection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsToday)
            {
                e.Cell.BackColor = System.Drawing.Color.Aqua;
            }

            if (e.Day.IsSelected)
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            int fromh = 0, toh = 0, hr = 0;

            if (int.TryParse(TextBox1.Text, out fromh) && int.TryParse(TextBox2.Text, out toh) && int.TryParse(TextBox3.Text, out hr))
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");
                SqlCommand cmd = new SqlCommand("select * from Kalkulacije where Id_korisnik = @id and Datum like @myDatum", con);
                cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                cmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));

                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    con.Close();
                    con.Open();

                    SqlCommand updateCmd = new SqlCommand("update Kalkulacije set Pocetak_rv = @prv, Kraj_rv = @krv, Satnica = @hr where Id_korisnik = @id and Datum like @myDatum", con);
                    updateCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    updateCmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
                    updateCmd.Parameters.AddWithValue("@prv", fromh.ToString() + ":00");
                    updateCmd.Parameters.AddWithValue("@krv", toh.ToString() + ":00");
                    updateCmd.Parameters.AddWithValue("@hr", hr);

                    int rowsAffected = updateCmd.ExecuteNonQuery();
                }
                else
                {
                    con.Close();
                    con.Open();

                    SqlCommand insertCmd = new SqlCommand("insert into Kalkulacije values(@id, @myDatum, @prv, @krv, @ukupno, @hr)", con);
                    insertCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    insertCmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
                    insertCmd.Parameters.AddWithValue("@prv", fromh.ToString() + ":00");
                    insertCmd.Parameters.AddWithValue("@krv", toh.ToString() + ":00");
                    insertCmd.Parameters.AddWithValue("@hr", hr);
                    insertCmd.Parameters.AddWithValue("@ukupno", toh - fromh);

                    int rowsAffected = insertCmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}