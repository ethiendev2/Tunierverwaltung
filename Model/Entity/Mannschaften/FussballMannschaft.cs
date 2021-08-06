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
        private int _fussballmannschaftID;
        private string _liga;
        private List<Teilnehmer> _mitglieder;
        #endregion

        #region Modifier / Accessoren
        public string Liga { get => _liga; set => _liga = value; }
        public int FussballmannschaftID { get => _fussballmannschaftID; set => _fussballmannschaftID = value; }
        public List<Teilnehmer> Mitglieder { get => _mitglieder; set => _mitglieder = value; }

        #endregion

        #region Konstruktoren
        public FussballMannschaft(int v1, string v2, string v3, string v4, int v5, string v6 , List<Teilnehmer> v7)
        {
            MannschaftID = v1;
            Name = v2;
            Sitz = v3;
            Gruendung = v4;

            FussballmannschaftID = v5;
            Liga = v6;
            Mitglieder = v7;
        }

        public FussballMannschaft()
        {

        }
        #endregion


        #region Worker

        #endregion
    }
}
