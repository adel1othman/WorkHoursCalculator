using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkHoursCalculator
{
    public partial class Basic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Korisnici"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }
        protected void BtnSaveChanges_Click(object sender, EventArgs e)
        {
           // promijenite conn
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");
            SqlCommand cmd = new SqlCommand("select * from Kalkulacije where Id_korisnik = @id and Datum like @myDatum", con);
            cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
            cmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));

            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                con.Close();

                SqlCommand updateCmd = new SqlCommand($"update Kalkulacije set Ukupno_odradeno_sati = '{TbxWorkedHouresOnThisDay.Text}', Satnica ='{TbxHourPrice.Text}' where Id_korisnik = @id and Datum like @myDatum", con);
                updateCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                updateCmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));

                con.Open();
                updateCmd.ExecuteNonQuery();
                con.Close();
                
                int TotalHours = 0;
                Int32.TryParse(TbxWorkedHouresOnThisDay.Text, out TotalHours);
                TbxTotalHours.Text = TotalHours.ToString();
                int TotalEarnings = 0;
                Int32.TryParse(TbxHourPrice.Text, out TotalEarnings);
                var Ukupno = TotalHours * TotalEarnings;
                TbxTotalEarnings.Text = Ukupno.ToString();
            }
            else
            {
                con.Close();

                SqlCommand insertCmd = new SqlCommand($"insert into Kalkulacije values( @id , @myDatum, Null, Null,'{TbxWorkedHouresOnThisDay.Text}', '{TbxHourPrice.Text}')", con);
                insertCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                insertCmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
                
                con.Open();
                insertCmd.ExecuteNonQuery();
                con.Close();
                int TotalHours = 0;
                Int32.TryParse(TbxWorkedHouresOnThisDay.Text, out TotalHours);
                TbxTotalHours.Text = TotalHours.ToString();
                int TotalEarnings = 0;
                Int32.TryParse(TbxHourPrice.Text, out TotalEarnings);
                var Ukupno = TotalHours * TotalEarnings;
                TbxTotalEarnings.Text = Ukupno.ToString();
            }
        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //promijenite con
            TbxWorkedHouresOnThisDay.Text = "Worked Houres On This Day";
            TbxHourPrice.Text = "Hour Price";

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string datum = Calendar1.SelectedDate.ToShortDateString();

            try
            {
                // prilagoditi d.m.yyy. ili dd.mm.yyyy.
                string converted = DateTime.ParseExact(datum, "d.M.yyyy.", CultureInfo.InvariantCulture)
                              .ToString("yyyy-MM-dd");


                cmd.Parameters.AddWithValue("@V1", converted);
                cmd.CommandText = "select * from Kalkulacije where Datum LIKE @V1";

                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        // čitanje iz baze i upisivanje u tbx TbxTotalHours
                        TbxTotalHours.Text = (read["Ukupno_odradeno_sati"].ToString());
                        var TotalHours = 0;
                        Int32.TryParse(TbxTotalHours.Text, out TotalHours);

                        // čitanje iz baze
                        var TotalEarnings = (read["Satnica"].ToString());
                        var _TotalEarnings = 0;
                        Int32.TryParse(TotalEarnings, out _TotalEarnings);

                        // upisivanje u TbxTotalEarnings
                        var Ukupno = TotalHours * _TotalEarnings;
                        TbxTotalEarnings.Text = Ukupno.ToString();

                    }
                    else
                    {
                        TbxTotalHours.Text = "U havent work on this day";
                        TbxTotalEarnings.Text = "U havent work on this day";
                    }
                }
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }
    }
}