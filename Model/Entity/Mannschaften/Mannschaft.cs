//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Mannschaft.cs
//Datum:        05.08.2021
//Beschreibung: Abstrakte Basisklasse Mannschaft

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.Entity.Mannschaften
{
    public class Mannschaft
    {
        #region Eigenschaften
        private int _mannschaftID;
        private string _name;
        private string _sitz;
        private string _gruendung;
        private Sportart _sportart;
        private List<Teilnehmer> _mitglieder;
        #endregion

        #region Modifier / Accessoren
        public string Name { get => _name; set => _name = value; }
        public string Sitz { get => _sitz; set => _sitz = value; }
        public string Gruendung { get => _gruendung; set => _gruendung = value; }
        public int MannschaftID { get => _mannschaftID; set => _mannschaftID = value; }
        public Sportart Sportart { get => _sportart; set => _sportart = value; }
        public List<Teilnehmer> Mitglieder { get => _mitglieder; set => _mitglieder = value; }
        #endregion

        #region Konstruktoren
        public Mannschaft(int v1, string v2, string v3, string v4, Sportart v5, List<Teilnehmer> v6)
        {
            MannschaftID = v1;
            Name = v2;
            Sitz = v3;
            Gruendung = v4;
            Sportart = v5;
            Mitglieder = v6;
        }

        public Mannschaft()
        {

        }
        #endregion


        #region Worker
        #endregion
    }
}