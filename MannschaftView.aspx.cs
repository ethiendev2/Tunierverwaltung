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
using Tunierverwaltung.Model.Entity.Mannschaften;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung
{
    public partial class MannschaftView : Page
    {
        bool[] rowChanged;
        protected void Page_Load(object sender, EventArgs e)
        {

            int totalRows = GridViewMannschaft.Rows.Count;
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

            for (int i = 0; i < Global.MannschaftController.Mannschaften.Count; i++)
            {
                GridViewRow gvr = GridViewMannschaft.Rows[i];
                DropDownList ddl = (DropDownList)gvr.FindControl("ddlSportart");
                ddl.ClearSelection();
                string value = Global.MannschaftController.Mannschaften[i].Sportart.ToString();
                ddl.Items.FindByText(value).Selected = true;

            }
            

        }

        public void BindGrid()
        {
            GridViewMannschaft.DataSource = Global.MannschaftController.getAlleMannschaften();
            EnsureGridViewFooter<Mannschaft>(GridViewMannschaft);
            GridViewMannschaft.DataBind();

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

                MannschaftDataMapper x = new MannschaftDataMapper();
                x.Delete(id);

                BindGrid();

                setDropDownList();
            }
        }

        protected void btnMitglieder_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Button btn = (Button)sender;
                GridViewRow gvr = (GridViewRow)btn.Parent.Parent;
                int row = gvr.RowIndex;

                HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                int id = Convert.ToInt32(hf1.Value);

                Global.MannschaftController.CurrMannschaft = Global.MannschaftController.Mannschaften.Find(x => x.MannschaftID == id);

                Response.Redirect("MannschaftMitgliederView.aspx");
            }
        }

        protected void btnHinzufuegen_Click(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
            {

                try
                {
                    TextBox name = (TextBox)GridViewMannschaft.FooterRow.FindControl("tbName");
                    TextBox sitz = (TextBox)GridViewMannschaft.FooterRow.FindControl("tbSitz");
                    TextBox gruendung = (TextBox)GridViewMannschaft.FooterRow.FindControl("tbGruendung");
                    DropDownList sportart = (DropDownList)GridViewMannschaft.FooterRow.FindControl("ddlSportart");

                    Sportart sa;
                    Enum.TryParse<Sportart>(sportart.Text, out sa);

                    Mannschaft m = new Mannschaft(0, name.Text, sitz.Text, gruendung.Text, sa, new List<Teilnehmer>());


                    Global.MannschaftController.MannschaftHinzufuegen(m);

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
                int totalRows = GridViewMannschaft.Rows.Count;
                for (int i = 0; i < totalRows; i++)
                {
                    if(rowChanged[i])
                    {
                        try
                        {
                            GridViewRow gvr = GridViewMannschaft.Rows[i];
                            HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                            int id = Convert.ToInt32(hf1.Value);

                            TextBox tbName = (TextBox)gvr.FindControl("tbName");
                            string name = tbName.Text;

                            TextBox tbSitz = (TextBox)gvr.FindControl("tbSitz");
                            string sitz = tbSitz.Text;

                            TextBox tbGruendung = (TextBox)gvr.FindControl("tbGruendung");
                            string gd = tbGruendung.Text;

                            DropDownList ddlSportart = (DropDownList)gvr.FindControl("ddlSportart");
                            Sportart sa;
                            Enum.TryParse<Sportart>(ddlSportart.Text, out sa);

                            Mannschaft m = Global.MannschaftController.Mannschaften.Find(x => x.MannschaftID == id);
                            m.Name = name;
                            m.Sitz = sitz;
                            m.Gruendung = gd;
                            m.Sportart = sa;


                            Global.MannschaftController.MannschaftHinzufuegen(m);

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
    }
}