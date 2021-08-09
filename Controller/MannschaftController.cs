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
    public class MannschaftController
    {
        #region Eigenschaften
        private List<Mannschaft> _mannschaften;
        private MannschaftDataMapper _dataMapper;
        private Mannschaft _currMannschaft;
        #endregion

        #region Modifier / Accessoren

        public List<Mannschaft> Mannschaften { get => _mannschaften; set => _mannschaften = value; }
        public MannschaftDataMapper DataMapper { get => _dataMapper; set => _dataMapper = value; }
        public Mannschaft CurrMannschaft { get => _currMannschaft; set => _currMannschaft = value; }

        #endregion

        #region Konstruktoren
        public MannschaftController() 
        {
            //Fussballspieler = FussballspielerDataMapper.GetAll();
            DataMapper = new MannschaftDataMapper();
        }
        #endregion


        #region Worker
        public void MannschaftHinzufuegen(Mannschaft m)
        {
            DataMapper.CreateOrUpdate(m);
        }

        public List<Mannschaft> getAlleMannschaften()
        {
            Mannschaften = DataMapper.GetAll();
            return Mannschaften;
        }

        public List<Teilnehmer> GetCurrMitglieder()
        {
            CurrMannschaft.Mitglieder = DataMapper.GetMitglieder(CurrMannschaft.MannschaftID);
            return CurrMannschaft.Mitglieder;
        }
        #endregion
    }
}
