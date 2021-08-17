//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        TunierView.aspx.cs
//Datum:        05.08.2021
//Beschreibung: View für die Tuniere


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
using Tunierverwaltung.Model.Entity.Tunier;

namespace Tunierverwaltung
{
    public partial class TunierView : Page
    {
        bool[] rowChanged;
        protected void Page_Load(object sender, EventArgs e)
        {

            int totalRows = GridViewTunier.Rows.Count;
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

            for (int i = 0; i < Global.TunierController.Tuniere.Count; i++)
            {
                GridViewRow gvr = GridViewTunier.Rows[i];
                DropDownList ddl = (DropDownList)gvr.FindControl("ddlSportart");
                ddl.ClearSelection();
                string value = Global.TunierController.Tuniere[i].Sportart.ToString();
                ddl.Items.FindByText(value).Selected = true;

            }
            

        }

        public void BindGrid()
        {
            GridViewTunier.DataSource = Global.TunierController.getAlleTuniere();
            EnsureGridViewFooter<Tunier>(GridViewTunier);
            GridViewTunier.DataBind();

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

                Global.TunierController.TunierEntfernen(id);

                BindGrid();

                setDropDownList();
            }
        }

        protected void btnMannschaften_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Button btn = (Button)sender;
                GridViewRow gvr = (GridViewRow)btn.Parent.Parent;
                int row = gvr.RowIndex;

                HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                int id = Convert.ToInt32(hf1.Value);

                Global.TunierController.CurrTunier = Global.TunierController.Tuniere.Find(x => x.TunierID == id);

                Response.Redirect("TunierMannschaftenView.aspx");
            }
        }

        protected void btnHinzufuegen_Click(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
            {

                try
                {
                    TextBox name = (TextBox)GridViewTunier.FooterRow.FindControl("tbName");
                    TextBox ort = (TextBox)GridViewTunier.FooterRow.FindControl("tbOrt");
                    TextBox datum = (TextBox)GridViewTunier.FooterRow.FindControl("tbDatum");
                    DropDownList sportart = (DropDownList)GridViewTunier.FooterRow.FindControl("ddlSportart");

                    Sportart sa;
                    Enum.TryParse<Sportart>(sportart.Text, out sa);

                    Tunier t = new Tunier(0, name.Text, ort.Text, datum.Text, sa, new List<Mannschaft>(), new List<Spiel>());



                    Global.TunierController.TunierHinzufuegen(t);

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
                int totalRows = GridViewTunier.Rows.Count;
                for (int i = 0; i < totalRows; i++)
                {
                    if(rowChanged[i])
                    {
                        try
                        {
                            GridViewRow gvr = GridViewTunier.Rows[i];
                            HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                            int id = Convert.ToInt32(hf1.Value);

                            TextBox tbName = (TextBox)gvr.FindControl("tbName");
                            string name = tbName.Text;

                            TextBox tbOrt = (TextBox)gvr.FindControl("tbOrt");
                            string ort = tbOrt.Text;

                            TextBox tbDatum = (TextBox)gvr.FindControl("tbDatum");
                            string dt = tbDatum.Text;

                            DropDownList ddlSportart = (DropDownList)gvr.FindControl("ddlSportart");
                            Sportart sa;
                            Enum.TryParse<Sportart>(ddlSportart.Text, out sa);


                            Tunier t = Global.TunierController.Tuniere.Find(y => y.TunierID == id);
                            t.Name = name;
                            t.Ort = ort;
                            t.Datum = dt;
                            t.Sportart = sa;

                            Global.TunierController.TunierHinzufuegen(t);


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