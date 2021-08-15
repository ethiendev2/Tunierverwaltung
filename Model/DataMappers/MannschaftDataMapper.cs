using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Mannschaften;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public class MannschaftDataMapper : AbstractDataMapper<Mannschaft>
    {
        private const string SELECT = "select * from mannschaft WHERE MannschaftID = @MannschaftID";
        private const string SELECT_ALL = "select * from mannschaft";
        private const string DELETE = "DELETE FROM MANNSCHAFT WHERE MannschaftID = @MannschaftID";
        private const string CREATE_MANNSCHAFT = "insert into mannschaft values (null, @Name, @Sitz, @Gruendung, @Sportart)";
        private const string UPDATE_MANNSCHAFT = "UPDATE mannschaft set MannschaftID = @MannschaftID, Name = @Name, Sitz = @Sitz, Gruendung = @Gruendung, Sportart = @Sportart WHERE MannschaftID = @MannschaftID";
        private const string SELECT_MITGLIEDER = "SELECT * FROM mannschaftmitglieder where MannschaftID = @MannschaftID";
        private const string DELETE_MITGLIED = "DELETE FROM mannschaftmitglieder WHERE MannschaftID = @MannschaftID AND TeilnehmerID = @TeilnehmerID";
        private const string ADD_MITGLIED = "insert into mannschaftmitglieder values (null, @MannschaftID, @TeilnehmerID, @Typ)";

        public List<Mannschaft> GetAll() 
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Mannschaft> mannschaften = new List<Mannschaft>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int mannschaftID = (int)reader["MannschaftID"];
                            string name = (string)reader["Name"];
                            string sitz = (string)reader["Sitz"];
                            //Mapping Date from Db to String
                            DateTime gruendung = (DateTime)reader["Gruendung"];
                            string gd = gruendung.ToString(DATE_FORMAT);
                            // Enum mapping
                            string sportart = (string)reader["Sportart"];
                            Sportart sa;
                            Enum.TryParse<Sportart>(sportart, out sa);


                            List<Teilnehmer> mitglieder = GetMitglieder(mannschaftID);

                            mannschaften.Add(new Mannschaft(mannschaftID, name, sitz, gd, sa, mitglieder));

                        }
                    }
                    return mannschaften;
                }
            }
        }
        public Mannschaft GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@MannschaftID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        int mannschaftID = (int)reader["MannschaftID"];
                        string name = (string)reader["Name"];
                        string sitz = (string)reader["Sitz"];
                        //Mapping Date from Db to String
                        DateTime gruendung = (DateTime)reader["Gruendung"];
                        string gd = gruendung.ToString(DATE_FORMAT);
                        // Enum mapping
                        string sportart = (string)reader["Sportart"];
                        Sportart sa;
                        Enum.TryParse<Sportart>(sportart, out sa);

                        List<Teilnehmer> mitglieder = GetMitglieder(mannschaftID);

                        return new Mannschaft(mannschaftID, name, sitz, gd, sa, mitglieder);
                    }
                }
            }
            return null;
        }

        public void RemoveMitglied(int mannschaftid, int teilnehmerid)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = DELETE_MITGLIED;
                    command.Parameters.AddWithValue("@MannschaftID", mannschaftid);
                    command.Parameters.AddWithValue("@TeilnehmerID", teilnehmerid);


                    command.ExecuteNonQuery();

                }
            }
        }

        public void AddMitglied(int mannschaftid, int teilnehmerid)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = ADD_MITGLIED;
                    command.Parameters.AddWithValue("@MannschaftID", mannschaftid);
                    command.Parameters.AddWithValue("@TeilnehmerID", teilnehmerid);

                    if (Global.TeilnehmerController.Fussballspieler.Exists(y => y.TeilnehmerID == teilnehmerid))
                    {
                        command.Parameters.AddWithValue("@Typ", "Fussballspieler");

                    }
                    else if (Global.TeilnehmerController.Tennisspieler.Exists(y => y.TeilnehmerID == teilnehmerid))
                    {
                        command.Parameters.AddWithValue("@Typ", "Tennisspieler");
                    }
                    else if (Global.TeilnehmerController.Handballspieler.Exists(y => y.TeilnehmerID == teilnehmerid))
                    {
                        command.Parameters.AddWithValue("@Typ", "Handballspieler");
                    }
                    else 
                    {
                        //teilnehmer not found
                    }
                    


                    command.ExecuteNonQuery();

                }
            }
        }

        public List<Teilnehmer> GetMitglieder(int id) 
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                FussballspielerDataMapper fdm = new FussballspielerDataMapper();
                TennisspielerDataMapper tdm = new TennisspielerDataMapper();
                HandballspielerDataMapper hdm = new HandballspielerDataMapper();

                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_MITGLIEDER;
                    command.Parameters.AddWithValue("@MannschaftID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Teilnehmer> mitglieder = new List<Teilnehmer>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int teilnehmerID = (int)reader["TeilnehmerID"];
                            string typ = (string)reader["Typ"];

                            switch (typ)
                            {
                                case "Fussballspieler":
                                    Fussballspieler s = fdm.GetByTeilnehmerID(teilnehmerID);
                                    mitglieder.Add(s);
                                    break;
                                case "Tennisspieler":
                                    Tennisspieler t = tdm.GetByTeilnehmerID(teilnehmerID);
                                    mitglieder.Add(t);
                                    break;
                                case "Handballspieler":
                                    Handballspieler h = hdm.GetByTeilnehmerID(teilnehmerID);
                                    mitglieder.Add(h);
                                    break;
                                default:
                                    Console.WriteLine("Error");
                                    break;
                            }

                        }
                    }
                    return mitglieder;
                }
            } 
        }


        public override void Delete(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = DELETE;
                    command.Parameters.AddWithValue("@MannschaftID", id);

                    command.ExecuteNonQuery();

                }
            }
        }

        public override void CreateOrUpdate(Mannschaft m)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (m.MannschaftID == 0)
                    {

                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = CREATE_MANNSCHAFT;
                        command.Parameters.AddWithValue("@Name", m.Name);
                        command.Parameters.AddWithValue("@Sitz", m.Sitz);
                        command.Parameters.AddWithValue("@Gruendung", Convert.ToDateTime(m.Gruendung));
                        command.Parameters.AddWithValue("@Sportart", m.Sportart.ToString());

                        command.ExecuteNonQuery();
                        m.MannschaftID = Convert.ToInt32(command.LastInsertedId);

                    }
                    else
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = UPDATE_MANNSCHAFT;
                        command.Parameters.AddWithValue("@MannschaftID", m.MannschaftID);
                        command.Parameters.AddWithValue("@Name", m.Name);
                        command.Parameters.AddWithValue("@Sitz", m.Sitz);
                        command.Parameters.AddWithValue("@Gruendung", Convert.ToDateTime(m.Gruendung));
                        command.Parameters.AddWithValue("@Sportart", m.Sportart.ToString());

                        command.ExecuteNonQuery();

                    }

                }
            }
        }
    }
}
