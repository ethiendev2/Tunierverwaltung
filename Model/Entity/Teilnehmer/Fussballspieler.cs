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

namespace Tunierverwaltung.Model.Entity.Teilnehmer
{
    public class Fussballspieler : Teilnehmer
    {
        #region Eigenschaften
        private PositionFusball _position;
        private int _tore;
        private int _anzahlSpiele;

        #endregion

        #region Modifier / Accessoren
        public int Tore { get => _tore; set => _tore = value; }
        internal PositionFusball Position { get => _position; set => _position = value; }

        public int AnzahlSpiele { get => _anzahlSpiele; set => _anzahlSpiele = value; }

        #endregion

        #region Konstruktoren
        public Fussballspieler(int v1, string v2, string v3, string v4, PositionFusball v5, int v6, int v7)
        {
            Id = v1;
            Vorname = v2;
            Nachname = v3;
            Geburtstag = v4;
            Position = v5;
            Tore = v6;
            AnzahlSpiele = v7;

        }
        #endregion


        #region Worker

        #endregion
    }
}