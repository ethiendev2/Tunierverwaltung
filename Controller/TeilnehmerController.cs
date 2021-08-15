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

        #endregion

        #region Modifier / Accessoren
        public List<Fussballspieler> Fussballspieler { get => fussballspieler; set => fussballspieler = value; }
        public FussballspielerDataMapper FussballspielerDataMapper { get => fussballspielerDataMapper; set => fussballspielerDataMapper = value; }
        public List<Tennisspieler> Tennisspieler { get => _tennisspieler; set => _tennisspieler = value; }
        public TennisspielerDataMapper TennisspielerDataMapper { get => tennisspielerDataMapper; set => tennisspielerDataMapper = value; }
        public List<Handballspieler> Handballspieler { get => handballspieler; set => handballspieler = value; }
        public HandballspielerDataMapper HandballspielerDataMapper { get => _handballspielerDataMapper; set => _handballspielerDataMapper = value; }


        #endregion

        #region Konstruktoren
        public TeilnehmerController() 
        {
            //Fussballspieler = FussballspielerDataMapper.GetAll();
            FussballspielerDataMapper = new FussballspielerDataMapper();
            TennisspielerDataMapper = new TennisspielerDataMapper();
            HandballspielerDataMapper = new HandballspielerDataMapper();
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
        #endregion
    }
}
