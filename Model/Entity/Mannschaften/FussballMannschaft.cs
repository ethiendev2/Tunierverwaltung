//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Fussballspieler.cs
//Datum:        27.06.2021
//Beschreibung: Klasse Fussballspieler
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.Entity.Mannschaften
{
    public class FussballMannschaft : Mannschaft
    {
        #region Eigenschaften
        private string _liga;
        private List<Teilnehmer> _spieler;
        #endregion

        #region Modifier / Accessoren
        public List<Teilnehmer> Spieler { get => _spieler; set => _spieler = value; }
        public string Liga { get => _liga; set => _liga = value; }

        #endregion

        #region Konstruktoren
        public FussballMannschaft(int v1, string v2, string v3, string v4, int v5, int v6, int v7, string v8, List<Teilnehmer> v9)
        {
            Id = v1;
            Name = v2;
            Sitz = v3;
            Gruendung = v4;
            Rang = v5;
            Siege = v6;
            Niederlagen = v7;
            Liga = v8;
            Spieler = v9;
        }

        #endregion


        #region Worker

        #endregion
    }
}
