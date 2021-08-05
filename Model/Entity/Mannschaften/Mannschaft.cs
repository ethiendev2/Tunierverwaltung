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
        private int _Id;
        private string _name;
        private string _sitz;
        private string _gruendung;
        private int _rang;
        private int _siege;
        private int _niederlagen;
        #endregion

        #region Modifier / Accessoren
        public int Id { get => _Id; set => _Id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Sitz { get => _sitz; set => _sitz = value; }
        public string Gruendung { get => _gruendung; set => _gruendung = value; }
        public int Rang { get => _rang; set => _rang = value; }
        public int Siege { get => _siege; set => _siege = value; }
        public int Niederlagen { get => _niederlagen; set => _niederlagen = value; }
        #endregion

        #region Konstruktoren
        #endregion


        #region Worker
        #endregion
    }
}