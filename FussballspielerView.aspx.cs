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
    public partial class FussballspielerView : Page
    {
        bool[] rowChanged;

        protected override void OnPreInit(EventArgs e)
        {
            if (!Global.UserController.isloggedin())
            {
                Response.Redirect("Default.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                CheckRole();

                BindGrid();

                setDropDownList();
            }

            int totalRows = GridViewFussballspieler.Rows.Count;
            rowChanged = new bool[totalRows];

            lblError.Visible = false;

        }

        protected void CheckRole()
        {

            if (Global.UserController.isGuest())
            {
                btnUpdate.Visible = false;
                GridViewFussballspieler.ShowFooter = false;
            }
            else
            {
                btnUpdate.Visible = true;
                GridViewFussballspieler.ShowFooter = true;
            }
        }

        public void setDropDownList()
        {
            // GridViewFussballspieler.Rows.Count
            for (int i = 0; i < Global.TeilnehmerController.Fussballspieler.Count; i++)
            {
                GridViewRow gvr = GridViewFussballspieler.Rows[i];
                DropDownList ddl = (DropDownList)gvr.FindControl("ddlPosition");
                ddl.ClearSelection();
                string value = Global.TeilnehmerController.Fussballspieler[i].Position.ToString();
                ddl.Items.FindByText(value).Selected = true;

            }
        }
        public void BindGrid()
        {
            GridViewFussballspieler.DataSource = Global.TeilnehmerController.getAllFussballspieler();
            EnsureGridViewFooter<Fussballspieler>(GridViewFussballspieler);
            GridViewFussballspieler.DataBind();

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
                int row = gvr.RowIndex;
                HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                int id = Convert.ToInt32(hf1.Value);

                Global.TeilnehmerController.TeilnehmerEntfernen(id);

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
                    TextBox vorname = (TextBox)GridViewFussballspieler.FooterRow.FindControl("tbVorname");
                    TextBox nachname = (TextBox)GridViewFussballspieler.FooterRow.FindControl("tbNachname");
                    TextBox geburtstag = (TextBox)GridViewFussballspieler.FooterRow.FindControl("tbGeburtstag");
                    DropDownList position = (DropDownList)GridViewFussballspieler.FooterRow.FindControl("ddlPosition");

                    PositionFusball pos;
                    Enum.TryParse<PositionFusball>(position.Text, out pos);


                    Fussballspieler f = new Fussballspieler(0, vorname.Text, nachname.Text, geburtstag.Text, 0, pos);

                    Global.TeilnehmerController.FussballspielerHinzufuegen(f);

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
                int totalRows = GridViewFussballspieler.Rows.Count;
                for (int i = 0; i < totalRows; i++)
                {
                    if(rowChanged[i])
                    {
                        try
                        {
                            GridViewRow gvr = GridViewFussballspieler.Rows[i];
                            HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                            int id = Convert.ToInt32(hf1.Value);

                            TextBox tbVorname = (TextBox)gvr.FindControl("tbVorname");
                            string vorname = tbVorname.Text;

                            TextBox tbNachname = (TextBox)gvr.FindControl("tbNachname");
                            string nachname = tbNachname.Text;

                            TextBox tbGeburtstag = (TextBox)gvr.FindControl("tbGeburtstag");
                            string gb = tbGeburtstag.Text;

                            DropDownList ddlPosition = (DropDownList)gvr.FindControl("ddlPosition");
                            PositionFusball pos;
                            Enum.TryParse<PositionFusball>(ddlPosition.Text, out pos);



                            Fussballspieler f = Global.TeilnehmerController.Fussballspieler.Find(x => x.TeilnehmerID == id);
                            f.Vorname = vorname;
                            f.Nachname = nachname;
                            f.Geburtstag = gb;
                            f.Position = pos;


                            Global.TeilnehmerController.FussballspielerHinzufuegen(f);

                        } catch (Exception ex)
                        {
                            lblError.Text = "Anpassen bei mindestens einem Spieler fehlgeschlagen, bitte versuchen sie es erneut.";
                            lblError.ForeColor = System.Drawing.Color.Red;

                            lblError.Visible = true;
                        }

                    }
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

        protected void GridViewFussballspieler_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Global.UserController.isGuest())
            {
                int row = e.Row.Cells.Count - 1;
                e.Row.Cells[row].Visible = false;

                btnUpdate.Visible = false;        
            }

        }
    }
}