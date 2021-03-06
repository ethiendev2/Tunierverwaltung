//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        PersonenView.aspx.cs
//Datum:        05.08.2021
//Beschreibung: View für Übersicht über alle Teilnehmer


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
    public partial class PersonenView : Page
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


            int totalRows = GridViewPersonen.Rows.Count;
            rowChanged = new bool[totalRows];
          

            if (!Page.IsPostBack)
            {

                BindGrid();
            }
        }
        public void BindGrid()
        {
            GridViewPersonen.DataSource = Global.TeilnehmerController.AlleTeilnehmer();
            GridViewPersonen.DataBind();
        }

    }

}