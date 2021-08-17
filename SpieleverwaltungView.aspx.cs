//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        SpieleverwaltungView.aspx.cs
//Datum:        05.08.2021
//Beschreibung: View für das Anlegen von Spielen




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
    public partial class SpieleverwaltungView : Page
    {
        bool[] rowChanged;

        protected void Page_Load(object sender, EventArgs e)
        {
            int totalRows = GridViewSpiele.Rows.Count;
            rowChanged = new bool[totalRows];

            lblError.Visible = false;

            if (!Page.IsPostBack)
            {

                BindGrid();
                generateDropDownList();
                setDropDownList();
            }



        }

        public void BindGrid()
        {

            GridViewSpiele.DataSource = Global.TunierController.GetCurrSpiele();
            EnsureGridViewFooter<Spiel>(GridViewSpiele);
            GridViewSpiele.DataBind();

        }


        public void generateDropDownList()
        {
            GridViewRow gvr1 = GridViewSpiele.FooterRow;
            DropDownList ddl11 = (DropDownList)gvr1.FindControl("ddlM1");
            DropDownList ddl21 = (DropDownList)gvr1.FindControl("ddlM2");

            foreach (Mannschaft m in Global.TunierController.CurrTunier.Mannschaften)
            {
                ddl11.Items.Insert(0, new ListItem(m.Name, m.MannschaftID.ToString()));
                ddl21.Items.Insert(0, new ListItem(m.Name, m.MannschaftID.ToString()));

            }



            for (int i = 0; i < Global.TunierController.CurrTunier.Spiele.Count; i++)
            {
                GridViewRow gvr = GridViewSpiele.Rows[i];
                DropDownList ddl1 = (DropDownList)gvr.FindControl("ddlM1");
                DropDownList ddl2 = (DropDownList)gvr.FindControl("ddlM2");

                foreach(Mannschaft m in Global.TunierController.CurrTunier.Mannschaften)
                {
                    ddl1.Items.Insert(0, new ListItem(m.Name, m.MannschaftID.ToString()));
                    ddl2.Items.Insert(0, new ListItem(m.Name, m.MannschaftID.ToString()));

                }

            }



        }



        public void setDropDownList()
        {

            for (int i = 0; i < Global.TunierController.CurrTunier.Spiele.Count; i++)
            {
                GridViewRow gvr = GridViewSpiele.Rows[i];
                DropDownList ddl1 = (DropDownList)gvr.FindControl("ddlM1");
                DropDownList ddl2 = (DropDownList)gvr.FindControl("ddlM2");

                ddl1.ClearSelection();
                ddl2.ClearSelection();


                int m1id = Global.TunierController.CurrTunier.Spiele[i].M1ID;
                int m2id = Global.TunierController.CurrTunier.Spiele[i].M2ID;

                List<Mannschaft> mannschaften = Global.MannschaftController.getAlleMannschaften();

                string value1 = mannschaften.Find(x => x.MannschaftID == m1id).Name;
                string value2 = mannschaften.Find(x => x.MannschaftID == m2id).Name;


                ddl1.Items.FindByText(value1).Selected = true;
                ddl2.Items.FindByText(value2).Selected = true;

            }


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

                SpielDataMapper sdm = new SpielDataMapper();
                sdm.Delete(id);

                
                BindGrid();
                generateDropDownList();
                setDropDownList();
            }
        }

        protected void btnHinzufuegen_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {

                try
                {
                    DropDownList ddlm1 = (DropDownList)GridViewSpiele.FooterRow.FindControl("ddlM1");
                    TextBox p1 = (TextBox)GridViewSpiele.FooterRow.FindControl("tbPunkte1");


                    DropDownList ddlm2 = (DropDownList)GridViewSpiele.FooterRow.FindControl("ddlM2");
                    TextBox p2 = (TextBox)GridViewSpiele.FooterRow.FindControl("tbPunkte2");


                    Spiel s = new Spiel(0, Global.TunierController.CurrTunier.TunierID, Convert.ToInt32(ddlm1.SelectedValue), Convert.ToInt32(p1.Text), Convert.ToInt32(ddlm2.SelectedValue), Convert.ToInt32(p2.Text));


                    Global.TunierController.addSpiel(s);

                }
                catch (Exception ex)
                {
                    lblError.Text = "Anlegen fehlgeschlagen, bitte versuchen sie es erneut.";
                    lblError.ForeColor = System.Drawing.Color.Red;

                    lblError.Visible = true;
                }

                BindGrid();
                generateDropDownList();
                setDropDownList();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                int totalRows = GridViewSpiele.Rows.Count;
                for (int i = 0; i < totalRows; i++)
                {
                    if (rowChanged[i])
                    {
                        try
                        {
                            GridViewRow gvr = GridViewSpiele.Rows[i];
                            HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                            int id = Convert.ToInt32(hf1.Value);

                            DropDownList ddlm1 = (DropDownList)GridViewSpiele.Rows[i].FindControl("ddlM1");
                            int m1id = Convert.ToInt32(ddlm1.SelectedValue);
                            TextBox p1 = (TextBox)GridViewSpiele.Rows[i].FindControl("tbPunkte1");
                            int punkt1 = Convert.ToInt32(p1.Text);

                            DropDownList ddlm2 = (DropDownList)GridViewSpiele.Rows[i].FindControl("ddlM2");
                            int m2id = Convert.ToInt32(ddlm2.SelectedValue);

                            TextBox p2 = (TextBox)GridViewSpiele.Rows[i].FindControl("tbPunkte2");
                            int punkt2 = Convert.ToInt32(p2.Text);

                            Spiel s = Global.TunierController.CurrTunier.Spiele.Find(x => x.SpielID == id);
                            s.M1ID = m1id;
                            s.M1Punkte = punkt1;
                            s.M2ID = m2id;
                            s.M2Punkte = punkt2;

                            Global.TunierController.updateSpiel(s);


                        }
                        catch (Exception ex)
                        {
                            lblError.Text = "Anpassen bei mindestens einem Spieler fehlgeschlagen, bitte versuchen sie es erneut.";
                            lblError.ForeColor = System.Drawing.Color.Red;

                            lblError.Visible = true;
                        }

                    }
                }

                BindGrid();
                generateDropDownList();
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