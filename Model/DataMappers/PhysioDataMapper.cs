using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public class PhysioDataMapper : AbstractTeilnehmerDataMapper<Physio>
    {

        private const string SELECT = "select * from physio p join teilnehmer t on p.TeilnehmerID = t.TeilnehmerID where p.PhysioID= @PhysioID";
        private const string SELECT_BY_TEILNEHMERID = "select * from physio p join teilnehmer t on p.TeilnehmerID = t.TeilnehmerID where p.TeilnehmerID = @TeilnehmerID";
        private const string CREATE_PHYSIO = "insert into physio values (null, @TeilnehmerID, @Berufserfahrung)";
        private const string UPDATE_PHYSIO = "UPDATE physio set PhysioID = @PhysioID, TeilnehmerID = @TeilnehmerID, Berufserfahrung = @Berufserfahrung WHERE PhysioID = @PhysioID";
        private const string SELECT_ALL = "SELECT * FROM physio p  join teilnehmer t on p.TeilnehmerID = t.TeilnehmerID";

        
        public new List<Physio> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Physio> spieler = new List<Physio>();

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
                            int trainerID = (int)reader["PhysioID"];
                            int berufserfahrung = (int)reader["Berufserfahrung"];
                            //Logic for mapping String to Enum

                            spieler.Add(new Physio(teilnehmerid, vorname, nachname, gb, trainerID, berufserfahrung));

                        }
                    }
                    return spieler;
                }
            }
        }

        public override Physio GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) 
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@PhysioID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int teilnehmerid = (int)reader["TeilnehmerID"];
                        string vorname = (string)reader["Vorname"];
                        string nachname = (string)reader["Nachname"];
                        //Mapping Date from Db to String
                        DateTime geburtstag = (DateTime)reader["Geburtstag"];
                        string gb = geburtstag.ToString(DATE_FORMAT);
                        int trainerID = (int)reader["PhysioID"];
                        int berufserfahrung = (int)reader["Berufserfahrung"];


                        return new Physio(teilnehmerid, vorname, nachname, gb, id, berufserfahrung);

                    }
                }
            }
            return null;
        }

        public Physio GetByTeilnehmerID(int id)
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
                        int teilnehmerid = (int)reader["TeilnehmerID"];
                        string vorname = (string)reader["Vorname"];
                        string nachname = (string)reader["Nachname"];
                        //Mapping Date from Db to String
                        DateTime geburtstag = (DateTime)reader["Geburtstag"];
                        string gb = geburtstag.ToString(DATE_FORMAT);
                        int trainerID = (int)reader["PhysioID"];
                        int berufserfahrung = (int)reader["Berufserfahrung"];

                        return new Physio(teilnehmerid, vorname, nachname, gb, id, berufserfahrung);

                    }
                }
            }
            return null;
        }

        public void CreateOrUpdate(Physio f)
        {
            base.CreateOrUpdate(f);

            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (f.PhysioID == 0)
                    {
                        command.CommandText = CREATE_PHYSIO;
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Berufserfahrung", f.Berufserfahrung);
                        command.ExecuteNonQuery();
                        f.PhysioID = Convert.ToInt32(command.LastInsertedId);
                    }
                    else
                    {
                        command.CommandText = UPDATE_PHYSIO;
                        command.Parameters.AddWithValue("@PhysioID", f.PhysioID);
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Berufserfahrung", f.Berufserfahrung);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
