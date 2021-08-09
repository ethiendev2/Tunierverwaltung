//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        TunierController.cs
//Datum:        27.06.2021
//Beschreibung: Klasse TunierController
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Tunierverwaltung.Model.DataMappers;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Mannschaften;
using Tunierverwaltung.Model.Entity.Personen;
using Tunierverwaltung.Model.Entity.Tunier;

namespace Tunierverwaltung.Controller
{
    public class TunierController
    {
        #region Eigenschaften
        private List<Tunier> _tuniere;
        private TunierDataMapper _dataMapper;
        private Tunier _currTunier;
        #endregion

        #region Modifier / Accessoren


        public List<Tunier> Tuniere { get => _tuniere; set => _tuniere = value; }
        public Tunier CurrTunier { get => _currTunier; set => _currTunier = value; }
        public TunierDataMapper DataMapper { get => _dataMapper; set => _dataMapper = value; }

        #endregion

        #region Konstruktoren
        public TunierController() 
        {
            //Fussballspieler = FussballspielerDataMapper.GetAll();
            DataMapper = new TunierDataMapper();
        }
        #endregion


        #region Worker
        public void TunierHinzufuegen(Tunier t)
        {
            DataMapper.CreateOrUpdate(t);
        }

        public void TunierEntfernen(int id)
        {
            DataMapper.Delete(id);
        }

        public List<Tunier> getAlleTuniere()
        {
            Tuniere = DataMapper.GetAll();
            return Tuniere;
        }

        public List<Spiel> GetCurrSpiele()
        {
            CurrTunier.Spiele = DataMapper.GetSpiele(CurrTunier.TunierID);
            return CurrTunier.Spiele;
        }

        public List<Mannschaft> GetCurrMannschaften()
        {
            CurrTunier.Mannschaften = DataMapper.GetMannschaften(CurrTunier.TunierID);
            return CurrTunier.Mannschaften;
        }
        #endregion
    }
}
