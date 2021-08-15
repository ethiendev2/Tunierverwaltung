//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Physio.cs
//Datum:        27.06.2021
//Beschreibung: Klasse Physio
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;

namespace Tunierverwaltung.Model.Entity.Personen
{
    public class Physio : Teilnehmer
    {
        #region Eigenschaften
        private int _physioID;
        private int _berufserfahrung;

        #endregion

        #region Modifier / Accessoren
        public int Berufserfahrung { get => _berufserfahrung; set => _berufserfahrung = value; }
        public int PhysioID { get => _physioID; set => _physioID = value; }

        #endregion

        #region Konstruktoren
        public Physio(int v1, string v2, string v3, string v4, int v5,int v6)
        {
            TeilnehmerID = v1;
            Vorname = v2;
            Nachname = v3;
            Geburtstag = v4;
            PhysioID = v5;
            Berufserfahrung = v6;

        }

        public Physio()
        {

        }
        #endregion


        #region Worker

        #endregion
    }
}