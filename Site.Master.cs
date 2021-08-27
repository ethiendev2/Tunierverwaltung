using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tunierverwaltung.Model.Entity.Enums;

namespace Tunierverwaltung
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(! Global.UserController.isloggedin() && HttpContext.Current.Request.Url.AbsolutePath != "/Default")
            {
                Response.Redirect("Default.aspx");
            }
        }


        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Global.UserController.User.Role == Role.Guest)
            {
                int row = e.Row.Cells.Count - 1;
                e.Row.Cells[row].Visible = false;

            }

        }
    }
}