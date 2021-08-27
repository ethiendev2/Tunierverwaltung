//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        FussballspielerView.aspx.cs
//Datum:        05.08.2021
//Beschreibung: View für Fussballspieler

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tunierverwaltung.Controller;
using Tunierverwaltung.Model;
using Tunierverwaltung.Model.DataMappers;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung
{
    public partial class AdminView : Page
    {
        bool[] rowChanged;

        protected override void OnPreInit(EventArgs e)
        {
            if (!Global.UserController.isAdmin())
            {
                Response.Redirect("Default.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                BindGrid();

                setDropDownList();
            }

            int totalRows = GridViewAdmin.Rows.Count;
            rowChanged = new bool[totalRows];

            lblError.Visible = false;

        }


        public void setDropDownList()
        {
            // GridViewFussballspieler.Rows.Count
            for (int i = 0; i < Global.UserController.Users.Count; i++)
            {
                GridViewRow gvr = GridViewAdmin.Rows[i];
                DropDownList ddl = (DropDownList)gvr.FindControl("ddlRole");
                ddl.ClearSelection();
                string value = Global.UserController.Users[i].Role.ToString();
                ddl.Items.FindByText(value).Selected = true;

            }
        }
        public void BindGrid()
        {
            GridViewAdmin.DataSource = Global.UserController.getAlleUsers();
            EnsureGridViewFooter<User>(GridViewAdmin);
            GridViewAdmin.DataBind();

        }

        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)tb.Parent.Parent;
            int row = gvr.RowIndex;
            rowChanged[row] = true;
        }

        protected void DropDownList_TextChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddl.Parent.Parent;
            int row = gvr.RowIndex;
            rowChanged[row] = true;
        }

        protected void btnEntfernen_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Button btn = (Button)sender;
                GridViewRow gvr = (GridViewRow)btn.Parent.Parent;

                TextBox tbUsername = (TextBox)gvr.FindControl("tbUsername");
                string username = tbUsername.Text;

                Global.UserController.UserEntfernen(username);
                BindGrid();
                setDropDownList();
            }
        }

        protected void btnHinzufuegen_Click(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
            {

                try
                {
                    TextBox username = (TextBox)GridViewAdmin.FooterRow.FindControl("tbUsername");
                    TextBox password = (TextBox)GridViewAdmin.FooterRow.FindControl("tbPassword");
                    DropDownList role = (DropDownList)GridViewAdmin.FooterRow.FindControl("ddlRole");

                    Role r;
                    Enum.TryParse<Role>(role.Text, out r);

                    Global.UserController.UserHinzufuegen(username.Text, password.Text, r);

                } catch (Exception ex)
                {
                    lblError.Text = "Anlegen fehlgeschlagen, bitte versuchen sie es erneut.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    
                    lblError.Visible = true;
                }

                BindGrid();
                setDropDownList();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                try { 

                Button btn = (Button)sender;
                GridViewRow gvr = (GridViewRow)btn.Parent.Parent;


                TextBox tbUsername = (TextBox)gvr.FindControl("tbUsername");
                string username = tbUsername.Text;

                TextBox tbPassword = (TextBox)gvr.FindControl("tbPassword");
                string password = tbPassword.Text;


                DropDownList ddlPosition = (DropDownList)gvr.FindControl("ddlRole");
                Role r;
                Enum.TryParse<Role>(ddlPosition.Text, out r);

                Global.UserController.UpdateRole(username, r);

                if (password != "****")
                {
                    Global.UserController.UpdatePassword(username, password);
                }


                } catch (Exception ex)
             {
                lblError.Text = "Update ist fehlgeschlagen.";
                lblError.ForeColor = System.Drawing.Color.Red;

                lblError.Visible = true;
            }

                 BindGrid();
                
                setDropDownList();
            }
        }

        public static void EnsureGridViewFooter<T>(GridView gridView) where T : new()
        {
            if (gridView == null)
                throw new ArgumentNullException("gridView");

            if (gridView.DataSource != null && gridView.DataSource is IEnumerable<T> && (gridView.DataSource as IEnumerable<T>).Count() > 0)
                return;

            // If nothing has been assigned to the grid or it generated no rows we are going to add an empty one.
            var emptySource = new List<T>();
            var blankItem = new T();
            emptySource.Add(blankItem);
            gridView.DataSource = emptySource;

            // On databinding make sure the empty row is set to invisible so it hides it from display.
            gridView.RowDataBound += delegate (object sender, GridViewRowEventArgs e)
            {
                if (e.Row.DataItem == (object)blankItem)
                    e.Row.Visible = false;
            };
        }

    }
}