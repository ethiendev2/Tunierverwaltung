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
            lblError.Visible = false;

            if (Global.UserController.isloggedin())
            {
                //Logged in
                lblwelcome.Text = String.Format("Hallo {0}", Global.UserController.User.Username);
                lblusername.Visible = false;
                lblpassword.Visible = false;
                tbusername.Visible = false;
                tbpassword.Visible = false;
                btnAnmelden.Visible = false;
                btnAbmelden.Visible = true;

            }
            else
            {
                lblwelcome.Text = String.Format("Bitte melden Sie sich an.");
                lblusername.Visible = true;
                lblpassword.Visible = true;
                tbusername.Visible = true;
                tbpassword.Visible = true;
                btnAbmelden.Visible = false;
            }
        }

        protected void btnAnmelden_Click(object sender, EventArgs e)
        {
            try
            {
                Global.UserController.login(tbusername.Text, tbpassword.Text);
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Anmelden fehlgeschlagen, diese Nutzer/Passwort Kombination ist nicht gültig.";
                lblError.ForeColor = System.Drawing.Color.Red;

                lblError.Visible = true;
            }
            
        }

        protected void btnAbmelden_Click(object sender, EventArgs e)
        {
            Global.UserController.logout();
            Response.Redirect("Default.aspx");
        }

    }
}