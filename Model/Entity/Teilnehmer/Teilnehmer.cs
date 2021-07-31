//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        Teilnehmer.cs
//Datum:        27.06.2021
//Beschreibung: Abstrakte Basisklasse Teilnehmer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tunierverwaltung.Model.Entity.Teilnehmer
{
    public abstract class Teilnehmer
    {
        #region Eigenschaften
        private int _Id;
        private string _vorname;
        private string _nachname;
        private string _geburtstag;
        #endregion

        #region Modifier / Accessoren

        public string Vorname { get => _vorname; set => _vorname = value; }
        public string Nachname { get => _nachname; set => _nachname = value; }
        public string Geburtstag { get => _geburtstag; set => _geburtstag = value; }
        public int Id { get => _Id; set => _Id = value; }

        #endregion

        #region Konstruktoren
        #endregion


        #region Worker

        #endregion
    }
}