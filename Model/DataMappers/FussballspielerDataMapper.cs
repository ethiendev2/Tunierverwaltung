// Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        FussballspielerDataMapper.cs
//Datum:        05.08.2021
//Beschreibung: DataMapper für Fussballspieler
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public class FussballspielerDataMapper : AbstractTeilnehmerDataMapper<Fussballspieler>
    {

        private const string SELECT = "select * from fussballspieler f join teilnehmer t on f.TeilnehmerID = t.TeilnehmerID where f.FussballspielerID = @FussballspielerID";
        private const string SELECT_BY_TEILNEHMERID = "select * from fussballspieler f join teilnehmer t on f.TeilnehmerID = t.TeilnehmerID where f.TeilnehmerID = @TeilnehmerID";
        private const string CREATE_FUSSBALLSPIELER = "insert into fussballspieler values (null, @TeilnehmerID, @Position)";
        private const string UPDATE_FUSSBALLSPIELER = "UPDATE fussballspieler set FussballspielerID = @FussballspielerID, TeilnehmerID = @TeilnehmerID, Position = @Position WHERE FussballspielerID = @FussballspielerID";
        private const string SELECT_ALL = "SELECT * FROM fussballspieler f join teilnehmer t on f.TeilnehmerID = t.TeilnehmerID";

        
        public new List<Fussballspieler> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Fussballspieler> spieler = new List<Fussballspieler>();

                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            int teilnehmerid = (int)reader["TeilnehmerID"];
                            string vorname = (string)reader["Vorname"];
                            string nachname = (string)reader["Nachname"];
                            //Mapping Date from Db to String
                            DateTime geburtstag = (DateTime)reader["Geburtstag"];
                            string gb = geburtstag.ToString(DATE_FORMAT);
                            int fussballspielerID = (int)reader["FussballspielerID"];
                            //Logic for mapping String to Enum
                            string position = (string)reader["Position"];
                            PositionFusball pos;
                            Enum.TryParse<PositionFusball>(position, out pos);
 

                            spieler.Add(new Fussballspieler(teilnehmerid, vorname, nachname, gb, fussballspielerID, pos));

                        }
                    }
                    return spieler;
                }
            }
        }

        public override Fussballspieler GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) 
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@FussballspielerID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int teilnehmerID = (int)reader["TeilnehmerID"];
                        string vorname = (string)reader["Vorname"];
                        string nachname = (string)reader["Nachname"];
                        //Mapping Date from Db to String
                        DateTime geburtstag = (DateTime)reader["Geburtstag"];
                        string gb = geburtstag.ToString(DATE_FORMAT);
                        //Logic for mapping String to Enum
                        string position = (string)reader["Position"];
                        PositionFusball pos;
                        Enum.TryParse<PositionFusball>(position, out pos);

                        return new Fussballspieler(teilnehmerID, vorname, nachname, gb, id, pos);

                    }
                }
            }
            return null;
        }

        public Fussballspieler GetByTeilnehmerID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_BY_TEILNEHMERID;
                    command.Parameters.AddWithValue("@TeilnehmerID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int teilnehmerID = (int)reader["TeilnehmerID"];
                        string vorname = (string)reader["Vorname"];
                        string nachname = (string)reader["Nachname"];
                        //Mapping Date from Db to String
                        DateTime geburtstag = (DateTime)reader["Geburtstag"];
                        string gb = geburtstag.ToString(DATE_FORMAT);
                        //Logic for mapping String to Enum
                        string position = (string)reader["Position"];
                        PositionFusball pos;
                        Enum.TryParse<PositionFusball>(position, out pos);

                        return new Fussballspieler(teilnehmerID, vorname, nachname, gb, id, pos);

                    }
                }
            }
            return null;
        }

        public void CreateOrUpdate(Fussballspieler f)
        {
            base.CreateOrUpdate(f);

            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (f.FussballspielerID == 0)
                    {
                        command.CommandText = CREATE_FUSSBALLSPIELER;
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Position", f.Position.ToString());
                        command.ExecuteNonQuery();
                        f.FussballspielerID = Convert.ToInt32(command.LastInsertedId);
                    }
                    else
                    {
                        command.CommandText = UPDATE_FUSSBALLSPIELER;
                        command.Parameters.AddWithValue("@FussballspielerID", f.FussballspielerID);
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Position", f.Position.ToString());
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
