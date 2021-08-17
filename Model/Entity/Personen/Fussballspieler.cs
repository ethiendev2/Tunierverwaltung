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

namespace Tunierverwaltung.Model.Entity.Personen
{
    public class Fussballspieler : Teilnehmer
    {
        #region Eigenschaften
        private int _fussballspielerID;
        private PositionFusball _position;

        #endregion

        #region Modifier / Accessoren

        internal PositionFusball Position { get => _position; set => _position = value; }

        public int FussballspielerID { get => _fussballspielerID; set => _fussballspielerID = value; }

        #endregion

        #region Konstruktoren
        public Fussballspieler(int v1, string v2, string v3, string v4, int v5, PositionFusball v6)
        {
            TeilnehmerID = v1;
            Vorname = v2;
            Nachname = v3;
            Geburtstag = v4;
            FussballspielerID = v5;
            Position = v6;


        }

        public Fussballspieler()
        {

        }
        #endregion


        #region Worker

        #endregion
    }
}