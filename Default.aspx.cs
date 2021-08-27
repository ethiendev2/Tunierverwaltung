using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tunierverwaltung.Controller;
using Tunierverwaltung.Model;

namespace Tunierverwaltung
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.UserController.isloggedin())
            {
                //Logged in
                lblwelcome.Text = String.Format("Hallo {0}", Global.UserController.User.Username);
                lblusername.Visible = false;
                lblpassword.Visible = false;
                tbusername.Visible = false;
                tbpassword.Visible = false;
                btnAnmelden.Visible = false;

            }
            else
            {
                lblwelcome.Text = String.Format("Bitte melden Sie sich an.");
                lblusername.Visible = true;
                lblpassword.Visible = true;
                tbusername.Visible = true;
                tbpassword.Visible = true;
            }
        }

        protected void btnAnmelden_Click(object sender, EventArgs e)
        {
            if(tbusername.Text != "" || tbusername.Text != null)
            {
                Global.UserController.login(tbusername.Text, tbpassword.Text);
            }
            else
            {
                //Login failed, show error
            }
                Response.Redirect("Default.aspx");
        }

    }
}