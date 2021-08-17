//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        TunierSpieleView.aspx.cs
//Datum:        05.08.2021
//Beschreibung: View für die Spiele in einem Tunier




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
    public partial class TunierSpieleView : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (!Page.IsPostBack)
            {

                BindGrid();

            }



        }

        public void BindGrid()
        {
            GridViewTunier.DataSource = Global.TunierController.getAlleTuniere();
            EnsureGridViewFooter<Tunier>(GridViewTunier);
            GridViewTunier.DataBind();

        }





        protected void btnAuswahl_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Button btn = (Button)sender;
                GridViewRow gvr = (GridViewRow)btn.Parent.Parent;
                int row = gvr.RowIndex;

                HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                int id = Convert.ToInt32(hf1.Value);

                Global.TunierController.CurrTunier = Global.TunierController.Tuniere.Find(x => x.TunierID== id);

                Response.Redirect("SpieleverwaltungView.aspx");
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