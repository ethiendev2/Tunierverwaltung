﻿using System;
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
using Tunierverwaltung.Model.Entity.Teilnehmer;

namespace Tunierverwaltung
{
    public partial class FussballspielerView : Page
    {
        bool[] rowChanged;
        protected void Page_Load(object sender, EventArgs e)
        {

            int totalRows = GridViewFussballspieler.Rows.Count;
            rowChanged = new bool[totalRows];

            if(!Page.IsPostBack)
            {
                BindGrid();

                setDropDownList();
            }


        }

        public void setDropDownList()
        {
            for (int i = 0; i < GridViewFussballspieler.Rows.Count; i++)
            {
                GridViewRow gvr = GridViewFussballspieler.Rows[i];
                DropDownList ddl = (DropDownList)gvr.FindControl("ddlPosition");
                ddl.ClearSelection();
                string value = Global.FussballspielerController.Fussballspieler[i].Position.ToString();
                ddl.Items.FindByText(value).Selected = true;

            }
        }
        public void BindGrid()
        {
            GridViewFussballspieler.DataSource = Global.FussballspielerController.getAllFussballspieler();

            GridViewFussballspieler.DataBind();

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

                FussballspielerDataMapper.Delete(id);

                BindGrid();
                setDropDownList();
            }
        }

        protected void btnHinzufuegen_Click(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
            {
                TextBox vorname = (TextBox) GridViewFussballspieler.FooterRow.FindControl("tbVorname");
                TextBox nachname = (TextBox)GridViewFussballspieler.FooterRow.FindControl("tbNachname");
                TextBox geburtstag = (TextBox)GridViewFussballspieler.FooterRow.FindControl("tbGeburtstag");
                DropDownList position = (DropDownList)GridViewFussballspieler.FooterRow.FindControl("ddlPosition");
                TextBox tore = (TextBox)GridViewFussballspieler.FooterRow.FindControl("tbTore");
                TextBox spiele = (TextBox)GridViewFussballspieler.FooterRow.FindControl("tbSpiele");

                PositionFusball pos;
                Enum.TryParse<PositionFusball>(position.Text, out pos);

                Fussballspieler f = new Fussballspieler(0, vorname.Text, nachname.Text, geburtstag.Text, pos, Convert.ToInt32(tore.Text), Convert.ToInt32(spiele.Text));

                FussballspielerDataMapper.CreateOrUpdate(f);

                BindGrid();
                setDropDownList();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                int totalRows = GridViewFussballspieler.Rows.Count;
                for (int i = 0; i < totalRows; i++)
                {
                    if(rowChanged[i])
                    {
                        GridViewRow gvr = GridViewFussballspieler.Rows[i];
                        HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                        int id = Convert.ToInt32(hf1.Value);

                        TextBox tbVorname = (TextBox)gvr.FindControl("TextBox1");
                        string vorname = tbVorname.Text;

                        TextBox tbNachname = (TextBox)gvr.FindControl("TextBox2");
                        string nachname = tbNachname.Text;

                        TextBox tbGeburtstag = (TextBox)gvr.FindControl("tbGeburtstag");
                        string gb = tbGeburtstag.Text;

                        DropDownList ddlPosition = (DropDownList)gvr.FindControl("ddlPosition");
                        PositionFusball pos;
                        Enum.TryParse<PositionFusball>(ddlPosition.Text, out pos);

                        TextBox tbTore = (TextBox)gvr.FindControl("tbTore");
                        int tore = Convert.ToInt32(tbTore.Text);

                        TextBox tbSpiele = (TextBox)gvr.FindControl("tbSpiele");
                        int spiele = Convert.ToInt32(tbSpiele.Text);



                        Fussballspieler f = Global.FussballspielerController.Fussballspieler.Find(x => x.Id == id);
                        f.Vorname = vorname;
                        f.Nachname = nachname;
                        f.Geburtstag = gb;
                        f.Position = pos;
                        f.Tore = tore;
                        f.AnzahlSpiele = spiele;
                        FussballspielerDataMapper.CreateOrUpdate(f);
                   
                    }
                }
                //GridViewFussballspieler.DataBind();
                BindGrid();
                setDropDownList();
            }
        }
    }
}