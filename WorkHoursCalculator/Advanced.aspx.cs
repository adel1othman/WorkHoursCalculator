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

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsToday)
            {
                e.Cell.BackColor = System.Drawing.Color.Aqua;
            }
            TextBox t1 = new TextBox();
            t1.ID = "t1" + e.Day.DayNumberText + e.Day.Date.Month.ToString();
            t1.Width = 100;
            t1.Height = 50;
            t1.TextMode = TextBoxMode.MultiLine;
            t1.Text = "Event for day " + e.Day.DayNumberText;
            Panel p1 = new Panel();
            p1.ID = "p" + e.Day.DayNumberText + e.Day.Date.Month.ToString(); ;
            p1.Attributes.Add("style", "display:none;");
            p1.Controls.Add(t1);
            e.Cell.Controls.Add(p1);
            e.Cell.Height = 70;
            e.Cell.Attributes.Add("onmouseover", "ShowInfo('" + p1.ClientID + "')");
            e.Cell.Attributes.Add("onmouseout", "HideInfo('" + p1.ClientID + "')");

        }
    }
}