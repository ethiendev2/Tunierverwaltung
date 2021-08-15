//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Handballspieler.cs
//Datum:        27.06.2021
//Beschreibung: Klasse Handballspieler
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;

namespace Tunierverwaltung.Model.Entity.Personen
{
    public class Handballspieler : Teilnehmer
    {
        #region Eigenschaften
        private int _handballspielerID;
        private PositionHandball _position;

        public PositionHandball Position { get => _position; set => _position = value; }
        public int HandballspielerID { get => _handballspielerID; set => _handballspielerID = value; }
        #endregion

        #region Modifier / Accessoren

        #endregion

        #region Konstruktoren
        public Handballspieler(int v1, string v2, string v3, string v4, int v5, PositionHandball v6)
        {
            TeilnehmerID = v1;
            Vorname = v2;
            Nachname = v3;
            Geburtstag = v4;
            HandballspielerID = v5;
            Position = v6;
        }

        public Handballspieler()
        {

        }
        #endregion


        #region Worker

        #endregion
    }
}