//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Fussballspieler.cs
//Datum:        27.06.2021
//Beschreibung: Klasse Fussballspieler
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tunierverwaltung.Model
{
    public class Fussballspieler : Teilnehmer
    {
        #region Eigenschaften
        private int _fussballspielerId;
        private PositionFusball _position;
        private int _tore;
        private int _gespielteSpiele;

        #endregion

        #region Modifier / Accessoren
        public int FussballspielerId { get => _fussballspielerId; set => _fussballspielerId = value; }
        public int Tore { get => _tore; set => _tore = value; }
        internal PositionFusball Position { get => _position; set => _position = value; }

        public int GespielteSpiele { get => _gespielteSpiele; set => _gespielteSpiele = value; }

        #endregion

        #region Konstruktoren
        public Fussballspieler(int v1, string v2, string v3, DateTime v4, int v5, PositionFusball v6, int v7, int v8) 
        {
            TeilnehmerId = v1;
            Vorname = v2;
            Nachname = v3;
            Geburtstag = v4;
            FussballspielerId = v5;
            Position = v6;
            Tore = v7;
            GespielteSpiele = v8;
        }
        #endregion


        #region Worker

        #endregion
    }
}