//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        FussballspielerController.cs
//Datum:        27.06.2021
//Beschreibung: Klasse FussballspielerController
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tunierverwaltung.Model
{
    public class FussballspielerController
    {
        #region Eigenschaften
        private List<Fussballspieler> fussballspieler;

        #endregion

        #region Modifier / Accessoren
        public List<Fussballspieler> Fussballspieler { get => fussballspieler; set => fussballspieler = value; }

        #endregion

        #region Konstruktoren
        public FussballspielerController() 
        {
            //Hohle Liste der Fußballspieler aus DB
        }
        #endregion


        #region Worker

        #endregion
    }
}
