//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        FussballmannschaftController.cs
//Datum:        27.06.2021
//Beschreibung: Klasse FussballmannschaftController
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Tunierverwaltung.Model.DataMappers;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Mannschaften;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Controller
{
    public class FussballmannschaftController
    {
        #region Eigenschaften
        private List<FussballMannschaft> fussballMannschaften;
        private FussballmannschaftDataMapper _dataMapper;
        private FussballMannschaft _currMannschaft;
        #endregion

        #region Modifier / Accessoren
        public List<FussballMannschaft> FussballMannschaften { get => fussballMannschaften; set => fussballMannschaften = value; }
        public FussballmannschaftDataMapper DataMapper { get => _dataMapper; set => _dataMapper = value; }
        public FussballMannschaft CurrMannschaft { get => _currMannschaft; set => _currMannschaft = value; }

        #endregion

        #region Konstruktoren
        public FussballmannschaftController() 
        {
            //Fussballspieler = FussballspielerDataMapper.GetAll();
            DataMapper = new FussballmannschaftDataMapper();
        }
        #endregion


        #region Worker
        public void FussballMannschaftHinzufuegen(FussballMannschaft m)
        {
            DataMapper.CreateOrUpdate(m);
        }

        public List<FussballMannschaft> getAlleFussballmannschaften()
        {
            FussballMannschaften = DataMapper.GetAll();
            return FussballMannschaften;
        }

        #endregion
    }
}
