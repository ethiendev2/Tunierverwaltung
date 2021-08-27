//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        TunierMannschaftenView.aspx.cs
//Datum:        05.08.2021
//Beschreibung: View für die Mannschaften in einem Tunier




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
    public partial class TunierMannschaftenView : Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            if (!Global.UserController.isloggedin())
            {
                Response.Redirect("Default.aspx");
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();

            cell1.Text = $"{Global.TunierController.CurrTunier.Name}";
            cell2.Text = $"{Global.TunierController.CurrTunier.Ort}";
            cell3.Text = $"{Global.TunierController.CurrTunier.Datum}";
            cell4.Text = $"{Global.TunierController.CurrTunier.Sportart}";

            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);

            tblTunier.Rows.AddAt(1, row);

            if (!Page.IsPostBack)
            {
                CheckRole();
                BindGridMannschaft();
                BindGridMannschaftOverview();
            }
        }

        protected void CheckRole()
        {

            if (Global.UserController.User.isGuest())
            {
                GridViewMannschaftenOverview.Visible = false;
                header2.Visible = false;
            }
            else
            {
                GridViewMannschaftenOverview.Visible = true;
                header2.Visible = true;
            }
        }

        public void BindGridMannschaft()
        {
            GridViewMannschaften.DataSource = Global.TunierController.GetCurrMannschaften();
            GridViewMannschaften.DataBind();
        }

        public void BindGridMannschaftOverview()
        {
            GridViewMannschaftenOverview.DataSource = alleMannschaften();
            GridViewMannschaftenOverview.DataBind();
        }

        public List<Mannschaft> alleMannschaften()
        {
            List<Mannschaft> mannschaften = Global.TunierController.CurrTunier.Mannschaften;
            List<Mannschaft> mannschaftenOverview = new List<Mannschaft>();

            foreach(Mannschaft m in Global.MannschaftController.getAlleMannschaften())
            {

                if(!mannschaften.Exists(i => i.MannschaftID == m.MannschaftID))
                {
                    mannschaftenOverview.Add(m);
                }

            }

            return mannschaftenOverview;
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

                TunierDataMapper t = new TunierDataMapper();
                t.RemoveMannschaft(Global.TunierController.CurrTunier.TunierID, id);


                BindGridMannschaft();
                BindGridMannschaftOverview();
            }
        }

        protected void btnHinzufuegen_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Button btn = (Button)sender;
                GridViewRow gvr = (GridViewRow)btn.Parent.Parent;
                int row = gvr.RowIndex;
                HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                int id = Convert.ToInt32(hf1.Value);

                TunierDataMapper t = new TunierDataMapper();
                t.AddMannschaft(Global.TunierController.CurrTunier.TunierID, id);

                BindGridMannschaft();
                BindGridMannschaftOverview();
            }

        }

        protected void GridViewMannschaften_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Global.UserController.isGuest())
            {
                int row = e.Row.Cells.Count - 1;
                e.Row.Cells[row].Visible = false;

            }
        }
    }

}