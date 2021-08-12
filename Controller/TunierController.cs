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
        private SpielDataMapper spielDataMapper;
        private Tunier _currTunier;
        private List<KeyValuePair<int, int>> _ranking;
        #endregion

        #region Modifier / Accessoren


        public List<Tunier> Tuniere { get => _tuniere; set => _tuniere = value; }
        public Tunier CurrTunier { get => _currTunier; set => _currTunier = value; }
        public TunierDataMapper DataMapper { get => _dataMapper; set => _dataMapper = value; }
        public SpielDataMapper SpielDataMapper { get => spielDataMapper; set => spielDataMapper = value; }
        public List<KeyValuePair<int, int>> Ranking { get => _ranking; set => _ranking = value; }

        #endregion

        #region Konstruktoren
        public TunierController() 
        {
            //Fussballspieler = FussballspielerDataMapper.GetAll();
            DataMapper = new TunierDataMapper();
            SpielDataMapper = new SpielDataMapper();
        }
        #endregion


        #region Worker
        public void TunierHinzufuegen(Tunier t)
        {
            DataMapper.CreateOrUpdate(t);
        }

        public void GetRanking()
        {
            Ranking = new List<KeyValuePair<int, int>>();

            foreach(Mannschaft m in CurrTunier.Mannschaften)
            {
                Ranking.Add(SpielDataMapper.getRanking(CurrTunier.TunierID, m.MannschaftID));
            }

            Ranking.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
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

        public void addSpiel(Spiel s)
        {
            SpielDataMapper.CreateOrUpdate(s);
            DataMapper.AddSPiel(s.SpielID, CurrTunier.TunierID);
        }
        public void updateSpiel(Spiel s)
        {
            SpielDataMapper.CreateOrUpdate(s);
        }

        #endregion
    }
}
