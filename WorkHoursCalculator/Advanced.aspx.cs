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
            lblUkupno.Visible = false;
            lblUkupnoR.Visible = false;
            lblFee.Visible = false;
            lblFeeR.Visible = false;

            if (Session["Korisnici"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                Calendar1.SelectedDate = DateTime.Today.Date;
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            // Goran
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

            // Adel
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

            SqlCommand cmd = new SqlCommand("select * from Kalkulacije where Id_korisnik = @id and Datum like @myDatum", con);
            cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
            cmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));

            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (ddlSelection.SelectedIndex == 0 && read.Read())
            {
                var fromh = read["Pocetak_rv"];
                var toh = read["Kraj_rv"];
                var ukupno = read["Ukupno_odradeno_sati"];
                var hr = read["Satnica"];

                if (fromh.GetType() == typeof(TimeSpan) && toh.GetType() == typeof(TimeSpan))
                {
                    TextBox1.Text = ((TimeSpan)fromh).TotalHours.ToString();
                    TextBox2.Text = ((TimeSpan)toh).TotalHours.ToString();
                    TextBox3.Text = hr.ToString();
                }
                else
                {
                    TextBox1.Text = String.Empty;
                    TextBox2.Text = String.Empty;
                    TextBox3.Text = hr.ToString();
                }

                lblUkupno.Text = "Worked hours for date " + Calendar1.SelectedDate.ToShortDateString() + ":";
                lblUkupnoR.Text = ukupno.ToString();
                lblFee.Text = "You've earned:";
                lblFeeR.Text = ((int)hr * (int)ukupno).ToString();
                lblUkupno.Visible = true;
                lblUkupnoR.Visible = true;
                lblFee.Visible = true;
                lblFeeR.Visible = true;
            }
            else if (read.Read())
            {
                var fromh = read["Pocetak_rv"];
                var toh = read["Kraj_rv"];
                var ukupno = read["Ukupno_odradeno_sati"];
                var hr = read["Satnica"];

                if (fromh.GetType() == typeof(TimeSpan) && toh.GetType() == typeof(TimeSpan))
                {
                    TextBox1.Text = ((TimeSpan)fromh).TotalHours.ToString();
                    TextBox2.Text = ((TimeSpan)toh).TotalHours.ToString();
                    TextBox3.Text = hr.ToString();
                }
                else
                {
                    TextBox1.Text = String.Empty;
                    TextBox2.Text = String.Empty;
                    TextBox3.Text = hr.ToString();
                }
            }
            else
            {
                TextBox1.Text = String.Empty;
                TextBox2.Text = String.Empty;
                TextBox3.Text = String.Empty;
            }
            con.Close();

            if (ddlSelection.SelectedIndex == 1)
            {
                SqlCommand cmd1 = new SqlCommand("select sum(Ukupno_odradeno_sati) from Kalkulacije where Id_korisnik = @id and month(Datum)=@myMonth and year(Datum)=@myYear", con);
                cmd1.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                cmd1.Parameters.AddWithValue("@myMonth", Calendar1.SelectedDate.Month);
                cmd1.Parameters.AddWithValue("@myYear", Calendar1.SelectedDate.Year);

                int odradeno = 0;

                con.Open();
                odradeno = (int)cmd1.ExecuteScalar();
                con.Close();

                SqlCommand cmd2 = new SqlCommand("select sum(zaradeno) from Kalkulacije where Id_korisnik = @id and month(Datum)=@myMonth and year(Datum)=@myYear", con);
                cmd2.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                cmd2.Parameters.AddWithValue("@myMonth", Calendar1.SelectedDate.Month);
                cmd2.Parameters.AddWithValue("@myYear", Calendar1.SelectedDate.Year);

                int result = 0;

                con.Open();
                result = (int)cmd2.ExecuteScalar();
                con.Close();

                lblUkupno.Text = "Worked hours for this month: ";
                lblUkupnoR.Text = odradeno.ToString();
                lblFee.Text = "You've earned: ";
                lblFeeR.Text = result.ToString();
                lblUkupno.Visible = true;
                lblUkupnoR.Visible = true;
                lblFee.Visible = true;
                lblFeeR.Visible = true;
            }
            else if (ddlSelection.SelectedIndex == 2)
            {
                SqlCommand cmd3 = new SqlCommand("select sum(Ukupno_odradeno_sati) from Kalkulacije where Id_korisnik = @id and year(datum)=@myYear", con);
                cmd3.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                cmd3.Parameters.AddWithValue("@myYear", Calendar1.SelectedDate.Year);

                int odradeno = 0;

                con.Open();
                odradeno = (int)cmd3.ExecuteScalar();
                con.Close();

                SqlCommand cmd4 = new SqlCommand("select sum(zaradeno) from Kalkulacije where Id_korisnik = @id and year(datum)=@myYear", con);
                cmd4.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                cmd4.Parameters.AddWithValue("@myYear", Calendar1.SelectedDate.Year);

                int result = 0;

                con.Open();
                result = (int)cmd4.ExecuteScalar();
                con.Close();

                lblUkupno.Text = "Worked hours for this year: ";
                lblUkupnoR.Text = odradeno.ToString();
                lblFee.Text = "You've earned: ";
                lblFeeR.Text = result.ToString();
                lblUkupno.Visible = true;
                lblUkupnoR.Visible = true;
                lblFee.Visible = true;
                lblFeeR.Visible = true;
            }
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

            // Goran
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

            // Adel
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

            SqlCommand cmd = new SqlCommand("select * from Kalkulacije where Id_korisnik = @id and Datum like @myDatum", con);
            cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
            cmd.Parameters.AddWithValue("@myDatum", e.Day.Date.ToString("yyyy-MM-dd"));

            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                e.Cell.BackColor = System.Drawing.Color.DeepSkyBlue;
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            int fromh = 0, toh = 0, hr = 0;

            if (int.TryParse(TextBox1.Text, out fromh) && int.TryParse(TextBox2.Text, out toh) && int.TryParse(TextBox3.Text, out hr))
            {
                int ukupno = 0;
                if (toh > fromh)
                {
                    ukupno = toh - fromh;
                }
                else if (toh < fromh)
                {
                    ukupno = toh + (24 - fromh);
                }
                if (ddlSaveCalculation.SelectedIndex == 0)
                {
                    // Goran
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

                    // Adel
                    // SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

                    SqlCommand cmd = new SqlCommand("select * from Kalkulacije where Id_korisnik = @id and Datum like @myDatum", con);
                    cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    cmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));

                    con.Open();
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.Read())
                    {
                        con.Close();

                        SqlCommand updateCmd = new SqlCommand("update Kalkulacije set Pocetak_rv = @prv, Kraj_rv = @krv, Ukupno_odradeno_sati = @ukupno, Satnica = @hr, zaradeno = @earned where Id_korisnik = @id and Datum like @myDatum", con);
                        updateCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                        updateCmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
                        updateCmd.Parameters.AddWithValue("@prv", TimeSpan.FromHours(fromh));
                        updateCmd.Parameters.AddWithValue("@krv", TimeSpan.FromHours(toh));
                        updateCmd.Parameters.AddWithValue("@hr", hr);
                        updateCmd.Parameters.AddWithValue("@ukupno", ukupno);
                        updateCmd.Parameters.AddWithValue("@earned", ukupno * hr);

                        con.Open();

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        con.Close();

                        lblUkupno.Text = "Worked hours for date " + Calendar1.SelectedDate.ToShortDateString() + ":";
                        lblUkupnoR.Text = ukupno.ToString();
                        lblFee.Text = "You've earned:";
                        lblFeeR.Text = (hr * ukupno).ToString();
                        lblUkupno.Visible = true;
                        lblUkupnoR.Visible = true;
                        lblFee.Visible = true;
                        lblFeeR.Visible = true;
                    }
                    else
                    {
                        con.Close();

                        SqlCommand insertCmd = new SqlCommand("insert into Kalkulacije values(@id, @myDatum, @prv, @krv, @ukupno, @hr, @earned)", con);
                        insertCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                        insertCmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
                        insertCmd.Parameters.AddWithValue("@prv", TimeSpan.FromHours(fromh));
                        insertCmd.Parameters.AddWithValue("@krv", TimeSpan.FromHours(toh));
                        insertCmd.Parameters.AddWithValue("@hr", hr);
                        insertCmd.Parameters.AddWithValue("@ukupno", ukupno);
                        insertCmd.Parameters.AddWithValue("@earned", ukupno * hr);

                        con.Open();

                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        con.Close();

                        lblUkupno.Text = "Worked hours for date " + Calendar1.SelectedDate.ToShortDateString() + ":";
                        lblUkupnoR.Text = ukupno.ToString();
                        lblFee.Text = "You've earned:";
                        lblFeeR.Text = (hr * ukupno).ToString();
                        lblUkupno.Visible = true;
                        lblUkupnoR.Visible = true;
                        lblFee.Visible = true;
                        lblFeeR.Visible = true;
                    }
                }
                else
                {
                    
                }
            }
        }

        protected void ddlSaveCalculation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSaveCalculation.SelectedIndex == 0)
            {
                ddlSelection.SelectedIndex = 0;
                ddlSelection.Enabled = false;
            }
            else
            {
                ddlSelection.Enabled = true;
            }
        }
    }
}