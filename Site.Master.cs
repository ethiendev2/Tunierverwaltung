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
            adminpage.Visible = Global.UserController.isAdmin();
            navigation.Visible = Global.UserController.isloggedin();
        }
    }
}