﻿//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Teilnehmer.cs
//Datum:        27.06.2021
//Beschreibung: Abstrakte Basisklasse Teilnehmer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tunierverwaltung.Model
{
    public abstract class Teilnehmer
    {
        #region Eigenschaften
        private int _teilnehmerId;
        private string _vorname;
        private string _nachname;
        private DateTime _geburtstag;
        #endregion

        #region Modifier / Accessoren
        public int TeilnehmerId { get => _teilnehmerId; set => _teilnehmerId = value; }

        public string Vorname { get => _vorname; set => _vorname = value; }
        public string Nachname { get => _nachname; set => _nachname = value; }
        public DateTime Geburtstag { get => _geburtstag; set => _geburtstag = value; }

        #endregion

        #region Konstruktoren
        #endregion


        #region Worker

        #endregion
    }
}