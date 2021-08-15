//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Trainer.cs
//Datum:        27.06.2021
//Beschreibung: Klasse Trainer
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;

namespace Tunierverwaltung.Model.Entity.Personen
{
    public class Trainer : Teilnehmer
    {
        #region Eigenschaften
        private int _trainerID;
        private int _berufserfahrung;
        private Sportart _sportart;

        #endregion

        #region Modifier / Accessoren
        public int TrainerID { get => _trainerID; set => _trainerID = value; }
        public int Berufserfahrung { get => _berufserfahrung; set => _berufserfahrung = value; }
        public Sportart Sportart { get => _sportart; set => _sportart = value; }

        #endregion

        #region Konstruktoren
        public Trainer(int v1, string v2, string v3, string v4, int v5,int v6,Sportart v7)
        {
            TeilnehmerID = v1;
            Vorname = v2;
            Nachname = v3;
            Geburtstag = v4;
            TrainerID = v5;
            Berufserfahrung = v6;
            Sportart = v7;

        }

        public Trainer()
        {

        }
        #endregion


        #region Worker

        #endregion
    }
}