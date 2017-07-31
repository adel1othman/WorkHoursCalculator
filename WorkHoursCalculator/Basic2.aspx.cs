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
    public partial class Basic2 : System.Web.UI.Page
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
            //promijenite con

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@V1", Convert.ToDateTime(Calendar1.SelectedDate));

            //trenutno ovo '1' je id korisnika koji trebamo povezati sa loginom
            cmd.CommandText = $"insert into dbo.Kalkulacije values( '1' , @V1, Null, Null,'{TbxWorkedHouresOnThisDay.Text}', '{TbxHourPrice.Text}')";
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Fak yea it is saved :D');</script>");
            con.Close();
        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //promijenite con

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string datum = Calendar1.SelectedDate.ToShortDateString();

            // možda napraviti try catch

            try
            {
                // prilagoditi d.m.yyy. ili dd.mm.yyyy.
                string converted = DateTime.ParseExact(datum, "d.M.yyyy.", CultureInfo.InvariantCulture)
                              .ToString("yyyy-MM-dd");


                cmd.Parameters.AddWithValue("@V1", converted);
                cmd.CommandText = "select * from Kalkulacije where Datum LIKE @V1";

                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
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

                        //{
                        //    // you know that the parsing attempt
                        //    // was successful
                        //}



                    }
                }
            }
            finally
            {
                // da mi ne zablokira app na nepotpunjenim datumima tako da mogu spremiti današnji dan
                //radi sa praznim finally
            }

            con.Close();

        }
    }
}