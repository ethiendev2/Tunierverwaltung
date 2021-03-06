//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Materialwart.cs
//Datum:        27.07.2021
//Beschreibung: Klasse Materialwart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;

namespace Tunierverwaltung.Model.Entity.Personen
{
    public class Materialwart : Teilnehmer
    {
        #region Eigenschaften
        private int _materialwartID;
        private int _berufserfahrung;

        #endregion

        #region Modifier / Accessoren
        public int Berufserfahrung { get => _berufserfahrung; set => _berufserfahrung = value; }
        public int MaterialwartID { get => _materialwartID; set => _materialwartID = value; }

        #endregion

        #region Konstruktoren
        public Materialwart(int v1, string v2, string v3, string v4, int v5,int v6)
        {
            TeilnehmerID = v1;
            Vorname = v2;
            Nachname = v3;
            Geburtstag = v4;
            MaterialwartID = v5;
            Berufserfahrung = v6;

        }

        public Materialwart()
        {

        }
        #endregion


        #region Worker

        #endregion
    }
}