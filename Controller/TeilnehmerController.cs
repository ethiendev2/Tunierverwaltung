//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        TeilnehmerController.cs
//Datum:        27.06.2021
//Beschreibung: Klasse TeilnehmerController
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
    public class TeilnehmerController
    {
        #region Eigenschaften
        private List<Fussballspieler> fussballspieler;
        private FussballspielerDataMapper fussballspielerDataMapper;

        private List<Tennisspieler> _tennisspieler;
        private TennisspielerDataMapper tennisspielerDataMapper;

        private List<Handballspieler> handballspieler;
        private HandballspielerDataMapper _handballspielerDataMapper;

        private List<Trainer> _trainer;
        private TrainerDataMapper trainerDataMapper;

        private List<Physio> _physio;
        private PhysioDataMapper physioDataMapper;

        private List<Materialwart> materialwart;
        private MaterialwartDataMapper materialwartDataMapper;
        #endregion

        #region Modifier / Accessoren
        public List<Fussballspieler> Fussballspieler { get => fussballspieler; set => fussballspieler = value; }
        public FussballspielerDataMapper FussballspielerDataMapper { get => fussballspielerDataMapper; set => fussballspielerDataMapper = value; }
        public List<Tennisspieler> Tennisspieler { get => _tennisspieler; set => _tennisspieler = value; }
        public TennisspielerDataMapper TennisspielerDataMapper { get => tennisspielerDataMapper; set => tennisspielerDataMapper = value; }
        public List<Handballspieler> Handballspieler { get => handballspieler; set => handballspieler = value; }
        public HandballspielerDataMapper HandballspielerDataMapper { get => _handballspielerDataMapper; set => _handballspielerDataMapper = value; }
        public List<Trainer> Trainer { get => _trainer; set => _trainer = value; }
        public TrainerDataMapper TrainerDataMapper { get => trainerDataMapper; set => trainerDataMapper = value; }
        public PhysioDataMapper PhysioDataMapper { get => physioDataMapper; set => physioDataMapper = value; }
        public List<Physio> Physio { get => _physio; set => _physio = value; }
        public List<Materialwart> Materialwart { get => materialwart; set => materialwart = value; }
        public MaterialwartDataMapper MaterialwartDataMapper { get => materialwartDataMapper; set => materialwartDataMapper = value; }


        #endregion

        #region Konstruktoren
        public TeilnehmerController() 
        {
            //Fussballspieler = FussballspielerDataMapper.GetAll();
            FussballspielerDataMapper = new FussballspielerDataMapper();
            TennisspielerDataMapper = new TennisspielerDataMapper();
            HandballspielerDataMapper = new HandballspielerDataMapper();
            TrainerDataMapper = new TrainerDataMapper();
            PhysioDataMapper = new PhysioDataMapper();
            MaterialwartDataMapper = new MaterialwartDataMapper();
        }
        #endregion


        #region Worker
        public void FussballspielerHinzufuegen(Fussballspieler f)
        {
            FussballspielerDataMapper.CreateOrUpdate(f);
        }

        public List<Fussballspieler> getAllFussballspieler()
        {
            Fussballspieler = FussballspielerDataMapper.GetAll();
            return Fussballspieler;
        }

        public void TennisspielerHinzufuegen(Tennisspieler f)
        {
            TennisspielerDataMapper.CreateOrUpdate(f);
        }

        public List<Tennisspieler> getAlleTennisspeiler()
        {
            Tennisspieler = TennisspielerDataMapper.GetAll();
            return Tennisspieler;
        }

        public void HandballspielerHinzufuegen(Handballspieler h)
        {
            HandballspielerDataMapper.CreateOrUpdate(h);
        }

        public List<Handballspieler> getAlleHandballspieler()
        {
            Handballspieler = HandballspielerDataMapper.GetAll();
            return Handballspieler;
        }

        public void TrainerHinzufuegen(Trainer t)
        {
            TrainerDataMapper.CreateOrUpdate(t);
        }

        public List<Trainer> getAlleTrainer()
        {
            Trainer = TrainerDataMapper.GetAll();
            return Trainer;
        }

        public void PhysioHinzufuegen(Physio p)
        {
            PhysioDataMapper.CreateOrUpdate(p);
        }

        public List<Physio> getAllePhysio()
        {
            Physio = PhysioDataMapper.GetAll();
            return Physio;
        }

        public void MaterialwartHinzufuegen(Materialwart m)
        {
            MaterialwartDataMapper.CreateOrUpdate(m);
        }

        public List<Materialwart> getAlleMaterialwart()
        {
            Materialwart = MaterialwartDataMapper.GetAll();
            return Materialwart;
        }
        #endregion
    }
}
