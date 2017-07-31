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
        DateTime selected;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Korisnici"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            if (ddlSelection.SelectedIndex == 0)
            {
                selected = Calendar1.SelectedDate;
            }
            else
            {

            }
        }
    }
}