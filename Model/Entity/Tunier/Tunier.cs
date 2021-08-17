//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Tunier.cs
//Datum:        05.08.2021
//Beschreibung:  Klasse Tunier

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Mannschaften;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.Entity.Tunier
{
    public class Tunier
    {
        #region Eigenschaften
        private int _tunierID;
        private string _name;
        private string _ort;
        private string _datum;
        private Sportart _sportart;
        private List<Mannschaft> _mannschaften;
        private List<Spiel> _spiele;
        #endregion

        #region Modifier / Accessoren
        public int TunierID { get => _tunierID; set => _tunierID = value; }
        public string Ort { get => _ort; set => _ort = value; }
        public Sportart Sportart { get => _sportart; set => _sportart = value; }
        public List<Mannschaft> Mannschaften { get => _mannschaften; set => _mannschaften = value; }
        public string Name { get => _name; set => _name = value; }
        public string Datum { get => _datum; set => _datum = value; }
        public List<Spiel> Spiele { get => _spiele; set => _spiele = value; }

        #endregion

        #region Konstruktoren
        public Tunier(int v1, string v2, string v3, string v4, Sportart v5, List<Mannschaft> v6, List<Spiel> v7)
        {
            TunierID = v1;
            Name = v2;
            Ort = v3;
            Datum = v4;
            Sportart = v5;
            Mannschaften = v6;
            Spiele = v7;
        }

        public Tunier()
        {

        }
        #endregion


        #region Worker
        #endregion
    }
}