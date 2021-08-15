//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Tennisspieler.cs
//Datum:        27.06.2021
//Beschreibung: Klasse Tennisspieler
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;

namespace Tunierverwaltung.Model.Entity.Personen
{
    public class Tennisspieler : Teilnehmer
    {
        #region Eigenschaften
        private int _tennisspielerID;
        private HauptHand _hand;

        public int TennisspielerID { get => _tennisspielerID; set => _tennisspielerID = value; }
        public HauptHand Hand { get => _hand; set => _hand = value; }
        #endregion

        #region Modifier / Accessoren

        #endregion

        #region Konstruktoren
        public Tennisspieler(int v1, string v2, string v3, string v4, int v5, HauptHand v6)
        {
            TeilnehmerID = v1;
            Vorname = v2;
            Nachname = v3;
            Geburtstag = v4;
            TennisspielerID = v5;
            Hand = v6;
        }

        public Tennisspieler()
        {

        }
        #endregion


        #region Worker

        #endregion
    }
}