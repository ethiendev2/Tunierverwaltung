//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        FussballspielerController.cs
//Datum:        27.06.2021
//Beschreibung: Klasse FussballspielerController
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Tunierverwaltung.Model.DataMappers;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Teilnehmer;

namespace Tunierverwaltung.Controller
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
            //Fussballspieler = FussballspielerDataMapper.GetAll();
        }
        #endregion


        #region Worker
        
        public List<Fussballspieler> getAllFussballspieler()
        {
            Fussballspieler = FussballspielerDataMapper.GetAll();
            return Fussballspieler;
        }

        public void test()       
        {
            Fussballspieler f = FussballspielerDataMapper.GetByID(7);

            f.Vorname = "Peter";

            Fussballspieler f2 = new Fussballspieler(
                0, "Test2", "Test2", "1990-01-01", PositionFusball.Aussenverteidiger, 10, 2);

            FussballspielerDataMapper.CreateOrUpdate(f2);

            FussballspielerDataMapper.CreateOrUpdate(f);

            Fussballspieler = FussballspielerDataMapper.GetAll();

            FussballspielerDataMapper.Delete(f.Id);


            System.Diagnostics.Debug.WriteLine(f.Id);
            System.Diagnostics.Debug.WriteLine(f.Vorname);
            System.Diagnostics.Debug.WriteLine(f.Nachname);
            System.Diagnostics.Debug.WriteLine(f.Geburtstag);
            System.Diagnostics.Debug.WriteLine(f.Tore);
            System.Diagnostics.Debug.WriteLine(f.AnzahlSpiele);
            System.Diagnostics.Debug.WriteLine(f.Position);

        }
        #endregion
    }
}
