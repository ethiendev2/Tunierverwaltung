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
    public partial class MannschaftMitgliederView : Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
                
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();

            cell1.Text = $"{Global.MannschaftController.CurrMannschaft.Name}";
            cell2.Text = $"{Global.MannschaftController.CurrMannschaft.Sitz}";
            cell3.Text = $"{Global.MannschaftController.CurrMannschaft.Gruendung}";
            cell4.Text = $"{Global.MannschaftController.CurrMannschaft.Sportart}";

            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);

            tblMannschaft.Rows.AddAt(1, row);

            if (!Page.IsPostBack)
            {

                BindGridMitglieder();
                BindGridTeilnehmer();
            }
        }

        public void BindGridMitglieder()
        {
            GridViewMitglieder.DataSource = Global.MannschaftController.GetCurrMitglieder();
            GridViewMitglieder.DataBind();
        }

        public void BindGridTeilnehmer()
        {
            GridViewTeilnehmer.DataSource = alleTeilnehmer();
            GridViewTeilnehmer.DataBind();
        }

        public List<Teilnehmer> alleTeilnehmer()
        {
            List<Teilnehmer> mitgieder = Global.MannschaftController.CurrMannschaft.Mitglieder;
            List<Teilnehmer> teilnehmer = new List<Teilnehmer>();

            foreach(Fussballspieler f in Global.TeilnehmerController.getAllFussballspieler())
            {
                bool exist = false;
                foreach(Teilnehmer m in mitgieder)
                {
                    if(m.TeilnehmerID == f.TeilnehmerID)
                    {
                        exist = true;
                    }
                }
                if (exist)
                { }
                else { 
                teilnehmer.Add(f);
                }
            }

            foreach (Tennisspieler t in Global.TeilnehmerController.getAlleTennisspeiler())
            {
                bool exist = false;
                foreach (Teilnehmer m in mitgieder)
                {
                    if (m.TeilnehmerID == t.TeilnehmerID)
                    {
                        exist = true;
                    }
                }
                if (exist)
                { }
                else
                {
                    teilnehmer.Add(t);
                }
            }


            foreach (Handballspieler h in Global.TeilnehmerController.getAlleHandballspieler())
            {
                bool exist = false;
                foreach (Teilnehmer m in mitgieder)
                {
                    if (m.TeilnehmerID == h.TeilnehmerID)
                    {
                        exist = true;
                    }
                }
                if (exist)
                { }
                else
                {
                    teilnehmer.Add(h);
                }
            }

            return teilnehmer;
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

                MannschaftDataMapper m = new MannschaftDataMapper();
                m.RemoveMitglied(Global.MannschaftController.CurrMannschaft.MannschaftID, id);

                
                BindGridMitglieder();
                BindGridTeilnehmer();
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

                MannschaftDataMapper m = new MannschaftDataMapper();
                m.AddMitglied(Global.MannschaftController.CurrMannschaft.MannschaftID, id);

                BindGridMitglieder();
                BindGridTeilnehmer();
            }

        }
    }

}