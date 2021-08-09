//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Spiel.cs
//Datum:        05.08.2021
//Beschreibung: Spiel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.Entity.Mannschaften
{
    public class Spiel
    {
        #region Eigenschaften
        private int _spielID;
        private int _tunierID;
        private int _m1ID;
        private int _m1Punkte;
        private int _m2ID;
        private int _m2Punkte;
        #endregion

        #region Modifier / Accessoren
        public int SpielID { get => _spielID; set => _spielID = value; }
        public int TunierID { get => _tunierID; set => _tunierID = value; }
        public int M1ID { get => _m1ID; set => _m1ID = value; }
        public int M1Punkte { get => _m1Punkte; set => _m1Punkte = value; }
        public int M2ID { get => _m2ID; set => _m2ID = value; }
        public int M2Punkte { get => _m2Punkte; set => _m2Punkte = value; }

        #endregion

        #region Konstruktoren
        public Spiel(int v1, int v2, int v3, int v4, int v5, int v6)
        {
            SpielID = v1;
            TunierID = v2;
            M1ID = v3;
            M1Punkte = v4;
            M2ID = v5;
            M2Punkte = v6;

        }

        public Spiel()
        {

        }
        #endregion


        #region Worker
        #endregion
    }
}