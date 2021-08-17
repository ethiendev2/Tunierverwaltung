//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        RankingTunierView.aspx.cs
//Datum:        05.08.2021
//Beschreibung: View für das Ranking innerhalb eines Tuniers


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
    public partial class RankingTunierView : Page
    {

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

            Global.TunierController.GetRanking();
            generateRankingTable();
        }



        protected void generateRankingTable()
        {


            for(int i = 0; i < Global.TunierController.Ranking.Count; i++)
            {
                string name = Global.TunierController.CurrTunier.Mannschaften.Find(x => x.MannschaftID == Global.TunierController.Ranking[i].Key).Name;
                int punkte = Global.TunierController.Ranking[i].Value;

                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();

                cell1.Text = $"{i+1}";
                cell2.Text = $"{name}";
                cell3.Text = $"{punkte}";

                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);

                tblRanking.Rows.Add(row);

            }

        }
    }
}