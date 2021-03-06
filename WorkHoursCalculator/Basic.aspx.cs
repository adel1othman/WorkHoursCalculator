﻿using System;
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
            if (!IsPostBack)
            {
                int i = 0;
                for (i = 2015; i <= 2020; i++)
                {
                    year.Items.Add(i.ToString());
                    OnlyYear.Items.Add(i.ToString());
                }

                // ovo je dropdown sakriven
                month.Visible = false;
                year.Visible = false;
                OnlyYear.Visible = false;
                GridView1.Visible = false;

                // ovaj dio služi samo za povlačenje podataka sa današnjim datumom u TbxThisPeriod, TbxTotalHours i LblTotalEarningsCalculation

                //promijenite con
                // Goran
                //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

                // Adel                
                //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");     

                //Ivana
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGVKO2V\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string datum = Calendar1.TodaysDate.ToShortDateString();
                //TbxThisPeriod.Text = datum;
                LblPeriod.Text = datum;

                try
                {

                    // prilagoditi d.m.yyy. ili dd.mm.yyyy.
                    string converted = DateTime.ParseExact(datum, "d.M.yyyy.", CultureInfo.InvariantCulture)
                                  .ToString("yyyy-MM-dd");


                    cmd.Parameters.AddWithValue("@V1", converted);
                    cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    cmd.CommandText = "select * from Kalkulacije where Datum LIKE @V1 and Id_korisnik = @id";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // čitanje iz baze i upisivanje u tbx TbxTotalHours
                            LblTotalHoursCalculation.Text = (reader["Ukupno_odradeno_sati"].ToString());
                            var TotalHours = 0;
                            Int32.TryParse(LblTotalHoursCalculation.Text, out TotalHours);

                            // čitanje iz baze
                            var TotalEarnings = (reader["Satnica"].ToString());
                            var _TotalEarnings = 0;
                            Int32.TryParse(TotalEarnings, out _TotalEarnings);

                            // upisivanje u LblTotalEarningsCalculation
                            var Ukupno = TotalHours * _TotalEarnings;
                            LblTotalEarningsCalculation.Text = Ukupno.ToString();

                        }
                        else
                        {
                            LblTotalHoursCalculation.Text = "U haven't worked this day";
                            LblTotalEarningsCalculation.Text = "U haven't worked this day";
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

           
            //// ovo je dropdown sakriven
            //month.Visible = false;
            //year.Visible = false;
            //OnlyYear.Visible = false;
           

            


        }

        // bojanje odrađenih dana

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            // Goran
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

            // Adel                
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");     

            //Ivana
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGVKO2V\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

            // Opens a Database Connection
            con.Open();
            DateTime LoadDays;

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Datum FROM Kalkulacije where Id_korisnik = @id", con);
                cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                cmd.CommandType = CommandType.Text;

                // Execute DataReader
                SqlDataReader reader = cmd.ExecuteReader();
                // Read DataReader till it reaches the end
                while (reader.Read())
                {
                    LoadDays = (DateTime)reader["Datum"];

                    if(LoadDays == e.Day.Date)
                    {
                        e.Cell.BackColor = System.Drawing.Color.OrangeRed;
                    }
                    
                }
                
               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('There was an error:" + ex.Message + "');</script>");
            }
            con.Close();
        }

        protected void BtnSaveChanges_Click(object sender, EventArgs e)
        {
           
            if (Calendar1.SelectedDate.Date == DateTime.MinValue)   //ovaj IF služi da provjeri ako nije selektiran datum, onda putem njega shvaća da je to današnji datum
            {
                // Goran
                //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

                // Adel                
                //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");     

                //Ivana
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGVKO2V\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

                SqlCommand cmd = new SqlCommand("select * from Kalkulacije where Id_korisnik = @id and Datum like @myDatum", con);
                cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                cmd.Parameters.AddWithValue("@myDatum", Calendar1.TodaysDate.ToString("yyyy-MM-dd"));
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    con.Close();

                    int TotalHours = 0;
                    Int32.TryParse(TbxWorkedHouresOnThisDay.Text, out TotalHours);
                    LblTotalHoursCalculation.Text = TotalHours.ToString();
                    int TotalEarnings = 0;
                    Int32.TryParse(TbxHourPrice.Text, out TotalEarnings);
                    var Ukupno = TotalHours * TotalEarnings;
                    LblTotalEarningsCalculation.Text = Ukupno.ToString();
                     

                    SqlCommand updateCmd = new SqlCommand($"update Kalkulacije set Ukupno_odradeno_sati = '{TbxWorkedHouresOnThisDay.Text}', Satnica ='{TbxHourPrice.Text}', Zaradeno = @zaradeno where Id_korisnik = @id and Datum like @myDatum", con);
                    updateCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    updateCmd.Parameters.AddWithValue("@zaradeno", Ukupno);
                    updateCmd.Parameters.AddWithValue("@myDatum", Calendar1.TodaysDate.ToString("yyyy-MM-dd"));

                    con.Open();
                    updateCmd.ExecuteNonQuery();
                    con.Close();

                }
                else
                {
                    int TotalHours = 0;
                    Int32.TryParse(TbxWorkedHouresOnThisDay.Text, out TotalHours);
                    LblTotalHoursCalculation.Text = TotalHours.ToString();
                    int TotalEarnings = 0;
                    Int32.TryParse(TbxHourPrice.Text, out TotalEarnings);
                    var Ukupno = TotalHours * TotalEarnings;
                    LblTotalEarningsCalculation.Text = Ukupno.ToString();

                    con.Close();

                    SqlCommand insertCmd = new SqlCommand($"insert into Kalkulacije values( @id , @myDatum, Null, Null,'" + TbxWorkedHouresOnThisDay.Text + "', '" + TbxHourPrice.Text + "', @zaradeno)", con);
                    insertCmd.Parameters.AddWithValue("@zaradeno", Ukupno);
                    insertCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    insertCmd.Parameters.AddWithValue("@myDatum", Calendar1.TodaysDate.ToString("yyyy-MM-dd"));

                    con.Open();
                    insertCmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            else       // normalno upisivanje po zabilježenom danu 
            {
                // Goran
                //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

                // Adel                
                //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");     

                //Ivana
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGVKO2V\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

                SqlCommand cmd = new SqlCommand("select * from Kalkulacije where Id_korisnik = @id and Datum like @myDatum", con);
                cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                cmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));

                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    con.Close();
                    int TotalHours = 0;
                    Int32.TryParse(TbxWorkedHouresOnThisDay.Text, out TotalHours);
                    LblTotalHoursCalculation.Text = TotalHours.ToString();
                    int TotalEarnings = 0;
                    Int32.TryParse(TbxHourPrice.Text, out TotalEarnings);
                    var Ukupno = TotalHours * TotalEarnings;
                    LblTotalEarningsCalculation.Text = Ukupno.ToString();
                 //   TbxThisPeriod.Text = Calendar1.SelectedDate.ToString("d.M.yyyy");
                    LblPeriod.Text= Calendar1.SelectedDate.ToString("d.M.yyyy");

                    SqlCommand updateCmd = new SqlCommand($"update Kalkulacije set Ukupno_odradeno_sati = '{TbxWorkedHouresOnThisDay.Text}', Satnica ='{TbxHourPrice.Text}', zaradeno = @zaradeno where Id_korisnik = @id and Datum like @myDatum", con);
                    updateCmd.Parameters.AddWithValue("@zaradeno", Ukupno);
                    updateCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    updateCmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));

                    con.Open();
                    updateCmd.ExecuteNonQuery();
                    con.Close();

                }
                else
                {
                    con.Close();
                    int TotalHours = 0;
                    Int32.TryParse(TbxWorkedHouresOnThisDay.Text, out TotalHours);
                    LblTotalHoursCalculation.Text = TotalHours.ToString();
                    int TotalEarnings = 0;
                    Int32.TryParse(TbxHourPrice.Text, out TotalEarnings);
                    var Ukupno = TotalHours * TotalEarnings;
                    LblTotalEarningsCalculation.Text = Ukupno.ToString();
                    //TbxThisPeriod.Text = Calendar1.SelectedDate.ToString("d.M.yyyy");
                    LblPeriod.Text = Calendar1.SelectedDate.ToString("d.M.yyyy");

                    SqlCommand insertCmd = new SqlCommand($"insert into Kalkulacije values( @id , @myDatum, Null, Null,'" + TbxWorkedHouresOnThisDay.Text + "', '" + TbxHourPrice.Text + "', @zaradeno)", con);
                    insertCmd.Parameters.AddWithValue("@zaradeno", Ukupno);
                    insertCmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    insertCmd.Parameters.AddWithValue("@myDatum", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));

                    con.Open();
                    insertCmd.ExecuteNonQuery();
                    con.Close();

                }
            }
           
        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)    // ovaj dio služi samo za povlačenje podataka sa odabranim datumom u TbxThisPeriod, TbxTotalHours i LblTotalEarningsCalculation
        {
            // vraćanje dropdowna na 1 day format prilikom biranja dana iz kalendara 
            ddlMode.SelectedIndex = 0;
            year.Visible = false;
            OnlyYear.Visible = false;
            month.Visible = false;
            LblWorkedHouresOnThisDay.Visible = true;
            LblHourPrice.Visible = true;
            TbxHourPrice.Visible = true;
            TbxWorkedHouresOnThisDay.Visible = true;
            LblPeriod.Visible = true;
            BtnSaveChanges.Visible = true;
            GridView1.Visible = false;


            //TbxWorkedHouresOnThisDay.Text = "Worked Houres On This Day";
            //TbxHourPrice.Text = "Hour Price";
            //promijenite con
            // Goran
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

            // Adel                
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");     

            //Ivana
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGVKO2V\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string datum = Calendar1.SelectedDate.ToShortDateString();
            LblPeriod.Text = datum;

            try
            {
                // prilagoditi d.m.yyy. ili dd.mm.yyyy.
                string converted = DateTime.ParseExact(datum, "d.M.yyyy.", CultureInfo.InvariantCulture)
                              .ToString("yyyy-MM-dd");


                cmd.Parameters.AddWithValue("@V1", converted);
                cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                cmd.CommandText = "select * from Kalkulacije where Datum LIKE @V1 and Id_korisnik = @id";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // čitanje iz baze i upisivanje u lblTotalHours
                        LblTotalHoursCalculation.Text = (reader["Ukupno_odradeno_sati"].ToString());
                        var TotalHours = 0;
                        Int32.TryParse(LblTotalHoursCalculation.Text, out TotalHours);
                        TbxWorkedHouresOnThisDay.Text = (reader["Ukupno_odradeno_sati"].ToString());



                        // čitanje iz baze
                        var TotalEarnings = (reader["Satnica"].ToString());
                        var _TotalEarnings = 0;
                        Int32.TryParse(TotalEarnings, out _TotalEarnings);
                        TbxHourPrice.Text = TotalEarnings;

                        // upisivanje u LblTotalEarningsCalculation
                        var Ukupno = TotalHours * _TotalEarnings;
                        LblTotalEarningsCalculation.Text = Ukupno.ToString();
                        
                    }
                    else
                    {
                        LblTotalHoursCalculation.Text = "U haven't worked this day";
                        LblTotalEarningsCalculation.Text = "U haven't worked this day";
                        LblPeriod.Visible = true;
                        month.Visible = false;
                        year.Visible = false;
                        OnlyYear.Visible = false;
                        ddlMode.SelectedValue = "1 day";

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

        protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)   // provjera odabranog... mjesec, dan.. godina...
        {

            string dropdown = ddlMode.SelectedItem.Text;
           
            switch (dropdown)
            {
                
                case "1 week":
                    Response.Write("<script>alert('Under construction :D');</script>");
                    break;

                case "1 month":

                  //  TbxThisPeriod.Visible = false;
                    LblPeriod.Visible = false;  
                    month.Visible = true;
                    year.Visible = true;
                    OnlyYear.Visible = false;
                    LblTotalHoursCalculation.Text = "";
                    LblTotalEarningsCalculation.Text = "";
                    LblHourPrice.Visible = false;
                    LblWorkedHouresOnThisDay.Visible = false;
                    TbxHourPrice.Visible = false;
                    TbxWorkedHouresOnThisDay.Visible = false;
                    BtnSaveChanges.Visible = false;
                    GridView1.Visible = false;
                    break;

                case "a year":
                 //   TbxThisPeriod.Visible = false;
                    LblPeriod.Visible = false;
                    month.Visible = false;
                    year.Visible = false;
                    OnlyYear.Visible = true;
                    LblTotalHoursCalculation.Text = "";
                    LblTotalEarningsCalculation.Text = "";
                    LblHourPrice.Visible = false;
                    LblWorkedHouresOnThisDay.Visible = false;
                    TbxHourPrice.Visible = false;
                    TbxWorkedHouresOnThisDay.Visible = false;
                    BtnSaveChanges.Visible = false;
                    GridView1.Visible = false;
                    break;

                default:
                    Response.Redirect("basic.aspx");
                    break;
            }

        }

        protected void month_SelectedIndexChanged(object sender, EventArgs e)
        {

           
                 //   TbxThisPeriod.Visible = false;
            LblPeriod.Visible = false;
            LblHourPrice.Visible = false;
            LblWorkedHouresOnThisDay.Visible = false;
            TbxHourPrice.Visible = false;
            TbxWorkedHouresOnThisDay.Visible = false;
            month.Visible = true;
            year.Visible = true;
            GridView1.Visible = true;
            // Goran
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

            // Adel                
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");     

            //Ivana
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGVKO2V\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

            SqlCommand cmd = new SqlCommand("select sum(zaradeno) From kalkulacije where Id_korisnik = @id and month(datum)=@myMonth and year(datum)=@myYear", con);

                    cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    cmd.Parameters.AddWithValue("@myYear", year.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@myMonth", month.SelectedValue.ToString());

                    con.Open();
                    LblTotalEarningsCalculation.Text = cmd.ExecuteScalar().ToString();
            con.Close();

            SqlCommand cmd2 = new SqlCommand("select sum(Ukupno_odradeno_sati) From kalkulacije where Id_korisnik = @id2 and month(datum)=@myMonth2 and year(datum)=@myYear2", con);

            cmd2.Parameters.AddWithValue("@id2", (int)Session["idKor"]);
            cmd2.Parameters.AddWithValue("@myYear2", year.SelectedValue.ToString());
            cmd2.Parameters.AddWithValue("@myMonth2", month.SelectedValue.ToString());

            con.Open();
            LblTotalHoursCalculation.Text = cmd2.ExecuteScalar().ToString();
            SqlCommand cmd3 = new SqlCommand("SELECT convert(varchar, Datum, 103) as Datum, Ukupno_odradeno_sati, satnica, zaradeno From kalkulacije where Id_korisnik = @id3 and month(datum)=@myMonth3 and year(datum)=@myYear3", con);
            cmd3.Parameters.AddWithValue("@id3", (int)Session["idKor"]);
            cmd3.Parameters.AddWithValue("@myYear3", year.SelectedValue.ToString());
            cmd3.Parameters.AddWithValue("@myMonth3", month.SelectedValue.ToString());
            SqlDataReader dr = cmd3.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();
          
            con.Close();


        }

        protected void year_SelectedIndexChanged(object sender, EventArgs e)
        {
                 //   TbxThisPeriod.Visible = false;
            LblPeriod.Visible = false;
            LblHourPrice.Visible = false;
            LblWorkedHouresOnThisDay.Visible = false;
            TbxHourPrice.Visible = false;
            TbxWorkedHouresOnThisDay.Visible = false;
            month.Visible = true;
            year.Visible = true;
            OnlyYear.Visible = false;
            GridView1.Visible = true;
            // Goran
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

            // Adel                
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");     

            //Ivana
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGVKO2V\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

            SqlCommand cmd = new SqlCommand("select sum(zaradeno) From kalkulacije where Id_korisnik = @id and month(datum)=@myMonth and year(datum)=@myYear", con);

                    cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
                    cmd.Parameters.AddWithValue("@myYear", year.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@myMonth", month.SelectedValue.ToString());

                    con.Open();
                    LblTotalEarningsCalculation.Text = cmd.ExecuteScalar().ToString();
            con.Close();

            SqlCommand cmd2 = new SqlCommand("select sum(Ukupno_odradeno_sati) From kalkulacije where Id_korisnik = @id2 and month(datum)=@myMonth2 and year(datum)=@myYear2", con);

            cmd2.Parameters.AddWithValue("@id2", (int)Session["idKor"]);
            cmd2.Parameters.AddWithValue("@myYear2", year.SelectedValue.ToString());
            cmd2.Parameters.AddWithValue("@myMonth2", month.SelectedValue.ToString());

            con.Open();
            LblTotalHoursCalculation.Text = cmd2.ExecuteScalar().ToString();
            SqlCommand cmd3 = new SqlCommand("SELECT convert(varchar, Datum, 103) as Datum, Ukupno_odradeno_sati, satnica, zaradeno From kalkulacije where Id_korisnik = @id3 and month(datum)=@myMonth3 and year(datum)=@myYear3", con);
            cmd3.Parameters.AddWithValue("@id3", (int)Session["idKor"]);
            cmd3.Parameters.AddWithValue("@myYear3", year.SelectedValue.ToString());
            cmd3.Parameters.AddWithValue("@myMonth3", month.SelectedValue.ToString());
            SqlDataReader dr = cmd3.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();
            con.Close();

        }

        protected void OnlyYear_SelectedIndexChanged(object sender, EventArgs e)
        {
           // TbxThisPeriod.Visible = false;
            LblPeriod.Visible = false;

            month.Visible = false;
            year.Visible = false;
            OnlyYear.Visible = true;
            GridView1.Visible = true;
            // Goran
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

            // Adel                
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FR7RPIJ\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");     

            //Ivana
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AGVKO2V\SQLEXPRESS;Initial Catalog=WorkHourCalculator;Integrated Security=True;Pooling=False");

            SqlCommand cmd = new SqlCommand("select sum(zaradeno) From kalkulacije where Id_korisnik = @id and year(datum)=@myYear", con);
            cmd.Parameters.AddWithValue("@id", (int)Session["idKor"]);
            cmd.Parameters.AddWithValue("@myYear", OnlyYear.SelectedValue.ToString());

            con.Open();
            LblTotalEarningsCalculation.Text = cmd.ExecuteScalar().ToString();
            con.Close();

            SqlCommand cmd2 = new SqlCommand("select sum(Ukupno_odradeno_sati) From kalkulacije where Id_korisnik = @id2 and year(datum)=@myYear2", con);
            cmd2.Parameters.AddWithValue("@id2", (int)Session["idKor"]);
            cmd2.Parameters.AddWithValue("@myYear2", OnlyYear.SelectedValue.ToString());

            con.Open();
            LblTotalHoursCalculation.Text = cmd2.ExecuteScalar().ToString();
            SqlCommand cmd3 = new SqlCommand("SELECT convert(varchar, Datum, 103) as Datum, Ukupno_odradeno_sati, satnica, zaradeno From kalkulacije where Id_korisnik = @id3 and year(datum)=@myYear3", con);
            cmd3.Parameters.AddWithValue("@id3", (int)Session["idKor"]);
            cmd3.Parameters.AddWithValue("@myYear3", OnlyYear.SelectedValue.ToString());
            SqlDataReader dr = cmd3.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();
            con.Close();
        }
    }
}