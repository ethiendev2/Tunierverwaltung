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
        private List<Teilnehmer> _notInCurrent;
        #endregion

        #region Modifier / Accessoren

        public List<Mannschaft> Mannschaften { get => _mannschaften; set => _mannschaften = value; }
        public MannschaftDataMapper DataMapper { get => _dataMapper; set => _dataMapper = value; }
        public Mannschaft CurrMannschaft { get => _currMannschaft; set => _currMannschaft = value; }
        public List<Teilnehmer> NotInCurrent { get => _notInCurrent; set => _notInCurrent = value; }

        #endregion

        #region Konstruktoren
        public MannschaftController() 
        {
            //Fussballspieler = FussballspielerDataMapper.GetAll();
            DataMapper = new MannschaftDataMapper();
        }
        #endregion


        #region Worker
        public void GetTeilnehmerNotInCurrent()
        {
                List<Teilnehmer> mitgieder = CurrMannschaft.Mitglieder;
                List<Teilnehmer> teilnehmer = new List<Teilnehmer>();

                foreach (Fussballspieler f in Global.TeilnehmerController.getAllFussballspieler())
                {
                    bool exist = false;
                    foreach (Teilnehmer m in mitgieder)
                    {
                        if (m.TeilnehmerID == f.TeilnehmerID)
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    { }
                    else
                    {
                        teilnehmer.Add(f);
                    }
                }

                foreach (Tennisspieler t in Global.TeilnehmerController.getAlleTennisspeiler())
                {
                    bool exist = false;
                    foreach (Teilnehmer m in mitgieder)
                    {
                        if (m.TeilnehmerID == t.TeilnehmerID)
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    { }
                    else
                    {
                        teilnehmer.Add(t);
                    }
                }


                foreach (Handballspieler h in Global.TeilnehmerController.getAlleHandballspieler())
                {
                    bool exist = false;
                    foreach (Teilnehmer m in mitgieder)
                    {
                        if (m.TeilnehmerID == h.TeilnehmerID)
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    { }
                    else
                    {
                        teilnehmer.Add(h);
                    }
                }


                foreach (Trainer t2 in Global.TeilnehmerController.getAlleTrainer())
                {
                    bool exist = false;
                    foreach (Teilnehmer m in mitgieder)
                    {
                        if (m.TeilnehmerID == t2.TeilnehmerID)
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    { }
                    else
                    {
                        teilnehmer.Add(t2);
                    }
                }

                foreach (Physio p in Global.TeilnehmerController.getAllePhysio())
                {
                    bool exist = false;
                    foreach (Teilnehmer m in mitgieder)
                    {
                        if (m.TeilnehmerID == p.TeilnehmerID)
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    { }
                    else
                    {
                        teilnehmer.Add(p);
                    }
                }

                foreach (Materialwart x in Global.TeilnehmerController.getAlleMaterialwart())
                {
                    bool exist = false;
                    foreach (Teilnehmer m in mitgieder)
                    {
                        if (m.TeilnehmerID == x.TeilnehmerID)
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    { }
                    else
                    {
                        teilnehmer.Add(x);
                    }
                }

            NotInCurrent = teilnehmer;
            
        }
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
        public void MannschaftEntfernen(int id)
        {
            DataMapper.Delete(id);
        }
        #endregion
    }
}
