// Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        TennisspielerDataMapper.cs
//Datum:        05.08.2021
//Beschreibung: DataMapper für Tennisspieler

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public class TennisspielerDataMapper : AbstractTeilnehmerDataMapper<Tennisspieler>
    {

        private const string SELECT = "select * from tennisspieler ts join teilnehmer t on ts.TeilnehmerID = t.TeilnehmerID where ts.TennisspielerID = @TennisspielerID";
        private const string SELECT_BY_TEILNEHMERID = "select * from tennisspieler ts join teilnehmer t on ts.TeilnehmerID = t.TeilnehmerID where ts.TeilnehmerID = @TeilnehmerID";
        private const string CREATE_TENNISSPIELER = "insert into tennisspieler values (null, @TeilnehmerID, @Hand)";
        private const string UPDATE_TENNISSPIELER = "UPDATE tennisspieler set TennisspielerID = @TennisspielerID, TeilnehmerID = @TeilnehmerID, Hand = @Hand WHERE TennisspielerID = @TennisspielerID";
        private const string SELECT_ALL = "SELECT * FROM tennisspieler ts join teilnehmer t on ts.TeilnehmerID = t.TeilnehmerID";

        
        public new List<Tennisspieler> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Tennisspieler> spieler = new List<Tennisspieler>();

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
                            int tennisID = (int)reader["TennisspielerID"];
                            //Logic for mapping String to Enum
                            string hand = (string)reader["Hand"];
                            HauptHand h;
                            Enum.TryParse<HauptHand>(hand, out h);
                            
                            spieler.Add(new Tennisspieler(teilnehmerid, vorname, nachname, gb, tennisID, h));

                        }
                    }
                    return spieler;
                }
            }
        }

        public override Tennisspieler GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) 
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@TennisspielerID", id);

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
                        string hand = (string)reader["Hand"];
                        HauptHand h;
                        Enum.TryParse<HauptHand>(hand, out h);

                        return new Tennisspieler(teilnehmerID, vorname, nachname, gb, id, h);

                    }
                }
            }
            return null;
        }
        //Here
        public Tennisspieler GetByTeilnehmerID(int id)
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
                        string hand = (string)reader["Hand"];
                        HauptHand h;
                        Enum.TryParse<HauptHand>(hand, out h);

                        return new Tennisspieler(teilnehmerID, vorname, nachname, gb, id,h);

                    }
                }
            }
            return null;
        }

        public void CreateOrUpdate(Tennisspieler f)
        {
            base.CreateOrUpdate(f);

            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (f.TennisspielerID == 0)
                    {
                        command.CommandText = CREATE_TENNISSPIELER;
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Hand", f.Hand.ToString());
                        command.ExecuteNonQuery();
                        f.TennisspielerID = Convert.ToInt32(command.LastInsertedId);
                    }
                    else
                    {
                        command.CommandText = UPDATE_TENNISSPIELER;
                        command.Parameters.AddWithValue("@TennisspielerID", f.TennisspielerID);
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Hand", f.Hand.ToString());
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
