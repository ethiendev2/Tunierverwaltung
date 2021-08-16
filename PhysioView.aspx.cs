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
    public partial class PhysioView : Page
    {
        bool[] rowChanged;
        protected void Page_Load(object sender, EventArgs e)
        {

            int totalRows = GridViewPhysio.Rows.Count;
            rowChanged = new bool[totalRows];

            lblError.Visible = false;


            if (!Page.IsPostBack)
            {

                BindGrid();

            }


        }

        public void BindGrid()
        {
            GridViewPhysio.DataSource = Global.TeilnehmerController.getAllePhysio();
            EnsureGridViewFooter<Physio>(GridViewPhysio);
            GridViewPhysio.DataBind();

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
            }
        }

        protected void btnHinzufuegen_Click(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
            {

                try
                {
                    TextBox vorname = (TextBox)GridViewPhysio.FooterRow.FindControl("tbVorname");
                    TextBox nachname = (TextBox)GridViewPhysio.FooterRow.FindControl("tbNachname");
                    TextBox geburtstag = (TextBox)GridViewPhysio.FooterRow.FindControl("tbGeburtstag");
                    TextBox tore = (TextBox)GridViewPhysio.FooterRow.FindControl("tbBerufserfahrung");


                    Physio f = new Physio(0, vorname.Text, nachname.Text, geburtstag.Text, 0, Convert.ToInt32(tore.Text));

                    Global.TeilnehmerController.PhysioHinzufuegen(f);

                } catch (Exception ex)
                {
                    lblError.Text = "Anlegen fehlgeschlagen, bitte versuchen sie es erneut.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    
                    lblError.Visible = true;
                }

                BindGrid();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                int totalRows = GridViewPhysio.Rows.Count;
                for (int i = 0; i < totalRows; i++)
                {
                    if(rowChanged[i])
                    {
                        try
                        {
                            GridViewRow gvr = GridViewPhysio.Rows[i];
                            HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                            int id = Convert.ToInt32(hf1.Value);

                            TextBox tbVorname = (TextBox)gvr.FindControl("tbVorname");
                            string vorname = tbVorname.Text;

                            TextBox tbNachname = (TextBox)gvr.FindControl("tbNachname");
                            string nachname = tbNachname.Text;

                            TextBox tbGeburtstag = (TextBox)gvr.FindControl("tbGeburtstag");
                            string gb = tbGeburtstag.Text;

                            TextBox tbBerufserfahrung = (TextBox)gvr.FindControl("tbBerufserfahrung");
                            int tore = Convert.ToInt32(tbBerufserfahrung.Text);





                            Physio f = Global.TeilnehmerController.Physio.Find(x => x.TeilnehmerID == id);
                            f.Vorname = vorname;
                            f.Nachname = nachname;
                            f.Geburtstag = gb;
                            f.Berufserfahrung = tore;


                            Global.TeilnehmerController.PhysioHinzufuegen(f);

                        } catch (Exception ex)
                        {
                            lblError.Text = "Anpassen bei mindestens einem Spieler fehlgeschlagen, bitte versuchen sie es erneut.";
                            lblError.ForeColor = System.Drawing.Color.Red;

                            lblError.Visible = true;
                        }

                    }
                }

                BindGrid();
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