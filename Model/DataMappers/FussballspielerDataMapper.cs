using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Teilnehmer;

namespace Tunierverwaltung.Model.DataMappers
{
    public class FussballspielerDataMapper
    {
        private const string CONNECTION_STRING = "Server=127.0.0.1;Database=Tunierverwaltung;Uid=user;Pwd=password;";
        private const string DATE_FORMAT = "yyyy-MM-dd";
        private const string SELECT = "SELECT * FROM fussballspieler f WHERE f.ID = @ID";
        private const string DELETE = "DELETE FROM fussballspieler WHERE ID = @ID";
        private const string CREATE = "insert into fussballspieler values(null,@Vorname, @Nachname, @Geburtstag, @Position, @Tore, @AnzahlSpiele)";
        private const string UPDATE = "UPDATE fussballspieler set ID = @ID, Vorname = @Vorname, Nachname = @Nachname, Geburtstag = @Geburstag, Position = @Position, Tore = @Tore, AnzahlSpiele = @AnzahlSpiele WHERE ID = @ID";
        private const string SELECT_ALL = "SELECT * FROM fussballspieler";


        public static List<Fussballspieler> GetAll()
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
                            int id = (int)reader["ID"];
                            string vorname = (string)reader["Vorname"];
                            string nachname = (string)reader["Nachname"];
                            DateTime geburtstag = (DateTime)reader["Geburtstag"];
                            //Logic for mapping String to Enum
                            string position = (string)reader["Position"];
                            PositionFusball pos;
                            Enum.TryParse<PositionFusball>(position, out pos);
                            int tore = (int)reader["Tore"];
                            int spiele = (int)reader["AnzahlSpiele"];

                            spieler.Add(new Fussballspieler(id, vorname, nachname, geburtstag, pos, tore, spiele));

                        }
                    }
                    return spieler;
                }
            }
        }

        public static Fussballspieler GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) 
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@ID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        string vorname = (string)reader["Vorname"];
                        string nachname = (string)reader["Nachname"];
                        DateTime geburtstag = (DateTime)reader["Geburtstag"];
                        //Logic for mapping String to Enum
                        string position = (string)reader["Position"];
                        PositionFusball pos;
                        Enum.TryParse<PositionFusball>(position, out pos);
                        int tore = (int)reader["Tore"];
                        int spiele = (int)reader["AnzahlSpiele"];

                        return new Fussballspieler(id, vorname, nachname, geburtstag, pos, tore, spiele);

                    }
                }
            }
            return null;
        }

        public static void Delete(Fussballspieler f)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = DELETE;
                    command.Parameters.AddWithValue("@ID", f.TeilnehmerId);

                    command.ExecuteNonQuery();

                }
            }
        }

        public static void CreateOrUpdate(Fussballspieler f)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (f.TeilnehmerId == 0)
                    {
                        command.CommandType = System.Data.CommandType.Text;

                        command.CommandText = CREATE;
                        command.Parameters.AddWithValue("@ID", f.TeilnehmerId);
                        command.Parameters.AddWithValue("@Vorname", f.Vorname);
                        command.Parameters.AddWithValue("@Nachname", f.Nachname);
                        command.Parameters.AddWithValue("@Geburtstag", f.Geburtstag.Date.ToString(DATE_FORMAT));
                        command.Parameters.AddWithValue("@Position", f.Position.ToString());
                        command.Parameters.AddWithValue("@Tore", f.Tore);
                        command.Parameters.AddWithValue("@AnzahlSpiele", f.AnzahlSpiele);
                        command.ExecuteNonQuery();
                        f.TeilnehmerId = Convert.ToInt32(command.LastInsertedId);

                    }
                    else
                    {
                        command.CommandType = System.Data.CommandType.Text;

                        command.CommandText = UPDATE;
                        command.Parameters.AddWithValue("@ID", f.TeilnehmerId);
                        command.Parameters.AddWithValue("@Vorname", f.Vorname);
                        command.Parameters.AddWithValue("@Nachname", f.Nachname);
                        command.Parameters.AddWithValue("@Geburstag", f.Geburtstag.Date.ToString(DATE_FORMAT));
                        command.Parameters.AddWithValue("@Position", f.Position.ToString());
                        command.Parameters.AddWithValue("@Tore", f.Tore);
                        command.Parameters.AddWithValue("@AnzahlSpiele", f.AnzahlSpiele);

                        command.ExecuteNonQuery();

                    }

                    }
                }
            }
        }
    }
