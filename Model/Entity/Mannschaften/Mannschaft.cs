//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Mannschaft.cs
//Datum:        05.08.2021
//Beschreibung: Abstrakte Basisklasse Mannschaft

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tunierverwaltung.Model.Entity.Mannschaften
{
    public abstract class Mannschaft
    {
        #region Eigenschaften
        private int _mannschaftID;
        private string _name;
        private string _sitz;
        private string _gruendung;
        #endregion

        #region Modifier / Accessoren
        public string Name { get => _name; set => _name = value; }
        public string Sitz { get => _sitz; set => _sitz = value; }
        public string Gruendung { get => _gruendung; set => _gruendung = value; }
        public int MannschaftID { get => _mannschaftID; set => _mannschaftID = value; }
        #endregion

        #region Konstruktoren
        #endregion


        #region Worker
        #endregion
    }
}