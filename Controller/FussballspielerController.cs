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
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Controller
{
    public class FussballspielerController
    {
        #region Eigenschaften
        private List<Fussballspieler> fussballspieler;
        private FussballspielerDataMapper _dataMapper;
        #endregion

        #region Modifier / Accessoren
        public List<Fussballspieler> Fussballspieler { get => fussballspieler; set => fussballspieler = value; }
        public FussballspielerDataMapper DataMapper { get => _dataMapper; set => _dataMapper = value; }

        #endregion

        #region Konstruktoren
        public FussballspielerController() 
        {
            //Fussballspieler = FussballspielerDataMapper.GetAll();
            DataMapper = new FussballspielerDataMapper();
        }
        #endregion


        #region Worker
        public void FussballspielerHinzufuegen(Fussballspieler f)
        {
            DataMapper.CreateOrUpdate(f);
        }

        public List<Fussballspieler> getAllFussballspieler()
        {
            Fussballspieler = DataMapper.GetAll();
            return Fussballspieler;
        }

        #endregion
    }
}
