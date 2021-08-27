//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        TennisspielerView.aspx.cs
//Datum:        05.08.2021
//Beschreibung: View für das Anlegen von Tennisspielern




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
    public partial class TennisspielerView : Page
    {
        bool[] rowChanged;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.UserController.isGuest())
            {
                btnUpdate.Visible = false;
                GridViewTennisspieler.ShowFooter = false;
            }
            else
            {
                btnUpdate.Visible = true;
                GridViewTennisspieler.ShowFooter = true;
            }

            int totalRows = GridViewTennisspieler.Rows.Count;
            rowChanged = new bool[totalRows];

            lblError.Visible = false;


            if (!Page.IsPostBack)
            {

                BindGrid();

                setDropDownList();
            }


        }

        public void setDropDownList()
        {
            // GridViewFussballspieler.Rows.Count
            for (int i = 0; i < Global.TeilnehmerController.Tennisspieler.Count; i++)
            {
                GridViewRow gvr = GridViewTennisspieler.Rows[i];
                DropDownList ddl = (DropDownList)gvr.FindControl("ddlHand");
                ddl.ClearSelection();
                string value = Global.TeilnehmerController.Tennisspieler[i].Hand.ToString();
                ddl.Items.FindByText(value).Selected = true;

            }
        }
        public void BindGrid()
        {
            GridViewTennisspieler.DataSource = Global.TeilnehmerController.getAlleTennisspeiler();
            EnsureGridViewFooter<Tennisspieler>(GridViewTennisspieler);
            GridViewTennisspieler.DataBind();

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
                    TextBox vorname = (TextBox)GridViewTennisspieler.FooterRow.FindControl("tbVorname");
                    TextBox nachname = (TextBox)GridViewTennisspieler.FooterRow.FindControl("tbNachname");
                    TextBox geburtstag = (TextBox)GridViewTennisspieler.FooterRow.FindControl("tbGeburtstag");
                    DropDownList hand = (DropDownList)GridViewTennisspieler.FooterRow.FindControl("ddlHand");

                    HauptHand h;
                    Enum.TryParse<HauptHand>(hand.Text, out h);


                    Tennisspieler t = new Tennisspieler(0, vorname.Text, nachname.Text, geburtstag.Text, 0, h);

                    Global.TeilnehmerController.TennisspielerHinzufuegen(t);

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
                int totalRows = GridViewTennisspieler.Rows.Count;
                for (int i = 0; i < totalRows; i++)
                {
                    if(rowChanged[i])
                    {
                        try
                        {
                            GridViewRow gvr = GridViewTennisspieler.Rows[i];
                            HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                            int id = Convert.ToInt32(hf1.Value);

                            TextBox tbVorname = (TextBox)gvr.FindControl("tbVorname");
                            string vorname = tbVorname.Text;

                            TextBox tbNachname = (TextBox)gvr.FindControl("tbNachname");
                            string nachname = tbNachname.Text;

                            TextBox tbGeburtstag = (TextBox)gvr.FindControl("tbGeburtstag");
                            string gb = tbGeburtstag.Text;

                            DropDownList ddlHand = (DropDownList)gvr.FindControl("ddlHand");
                            HauptHand h;
                            Enum.TryParse<HauptHand>(ddlHand.Text, out h);



                            Tennisspieler t = Global.TeilnehmerController.Tennisspieler.Find(x => x.TeilnehmerID == id);
                            t.Vorname = vorname;
                            t.Nachname = nachname;
                            t.Geburtstag = gb;
                            t.Hand = h;
                            

                            Global.TeilnehmerController.TennisspielerHinzufuegen(t);

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

        protected void GridViewTennisspieler_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Global.UserController.User.Role == Role.Guest)
            {
                int row = e.Row.Cells.Count - 1;
                e.Row.Cells[row].Visible = false;

            }
        }
    }
}