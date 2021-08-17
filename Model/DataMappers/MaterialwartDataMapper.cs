// Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        MaterialwartDataMapper.cs
//Datum:        05.08.2021
//Beschreibung: DataMapper für Materialwarte

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public class MaterialwartDataMapper : AbstractTeilnehmerDataMapper<Materialwart>
    {

        private const string SELECT = "select * from materialwart p join teilnehmer t on p.TeilnehmerID = t.TeilnehmerID where p.MaterialwartID = @MaterialwartID";
        private const string SELECT_BY_TEILNEHMERID = "select * from materialwart p join teilnehmer t on p.TeilnehmerID = t.TeilnehmerID where p.TeilnehmerID = @TeilnehmerID";
        private const string CREATE_MATERIALWART = "insert into materialwart values (null, @TeilnehmerID, @Berufserfahrung)";
        private const string UPDATE_MATERIALWART = "UPDATE materialwart set MaterialwartID = @MaterialwartID, TeilnehmerID = @TeilnehmerID, Berufserfahrung = @Berufserfahrung WHERE MaterialwartID = @MaterialwartID";
        private const string SELECT_ALL = "SELECT * FROM materialwart p  join teilnehmer t on p.TeilnehmerID = t.TeilnehmerID";

        
        public new List<Materialwart> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Materialwart> spieler = new List<Materialwart>();

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
                            int trainerID = (int)reader["MaterialwartID"];
                            int berufserfahrung = (int)reader["Berufserfahrung"];
                            //Logic for mapping String to Enum

                            spieler.Add(new Materialwart(teilnehmerid, vorname, nachname, gb, trainerID, berufserfahrung));

                        }
                    }
                    return spieler;
                }
            }
        }

        public override Materialwart GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) 
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@MaterialwartID", id);

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
                        int trainerID = (int)reader["MaterialwartID"];
                        int berufserfahrung = (int)reader["Berufserfahrung"];


                        return new Materialwart(teilnehmerid, vorname, nachname, gb, id, berufserfahrung);

                    }
                }
            }
            return null;
        }

        public Materialwart GetByTeilnehmerID(int id)
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
                        int trainerID = (int)reader["MaterialwartID"];
                        int berufserfahrung = (int)reader["Berufserfahrung"];

                        return new Materialwart(teilnehmerid, vorname, nachname, gb, id, berufserfahrung);

                    }
                }
            }
            return null;
        }

        public void CreateOrUpdate(Materialwart f)
        {
            base.CreateOrUpdate(f);

            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (f.MaterialwartID == 0)
                    {
                        command.CommandText = CREATE_MATERIALWART;
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Berufserfahrung", f.Berufserfahrung);
                        command.ExecuteNonQuery();
                        f.MaterialwartID = Convert.ToInt32(command.LastInsertedId);
                    }
                    else
                    {
                        command.CommandText = UPDATE_MATERIALWART;
                        command.Parameters.AddWithValue("@MaterialwartID", f.MaterialwartID);
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Berufserfahrung", f.Berufserfahrung);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
