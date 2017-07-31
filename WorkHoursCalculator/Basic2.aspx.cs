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
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K1I0JMC\SQLEXPRESS;Initial Catalog=WorkHours;Integrated Security=True;Pooling=False");

            // vađenje podataka iz sql i upisivanje u textboxeve

            
            string selectSql = "select * from Kalkulacije where Ukupno_odradeno_sati";
            SqlCommand cmd = new SqlCommand(selectSql, con);

            try
            {
                con.Open();

                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        TbxTotalHours.Text = (read["Ukupno_odradeno_sati"].ToString());

                    }
                }
            }
            finally
            {
                con.Close();
            }
        }

        //upisivanje nema problema
    }
}