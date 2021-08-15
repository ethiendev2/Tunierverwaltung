using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public class TrainerDataMapper : AbstractTeilnehmerDataMapper<Trainer>
    {

        private const string SELECT = "select * from trainer f join teilnehmer t on f.TeilnehmerID = t.TeilnehmerID where f.TrainerID = @TrainerID ";
        private const string SELECT_BY_TEILNEHMERID = "select * from trainer f join teilnehmer t on f.TeilnehmerID = t.TeilnehmerID where f.TeilnehmerID = @TeilnehmerID";
        private const string CREATE_TRAINER = "insert into trainer values (null, @TeilnehmerID, @Berufserfahrung, @Sportart)";
        private const string UPDATE_TRAINER = "UPDATE trainer set TrainerID = @TrainerID, TeilnehmerID = @TeilnehmerID, Berufserfahrung = @Berufserfahrung, Sportart = @Sportart WHERE TrainerID = @TrainerID";
        private const string SELECT_ALL = "SELECT * FROM trainer f join teilnehmer t on f.TeilnehmerID = t.TeilnehmerID";

        
        public new List<Trainer> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Trainer> spieler = new List<Trainer>();

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
                            int trainerID = (int)reader["TrainerID"];
                            int berufserfahrung = (int)reader["Berufserfahrung"];
                            //Logic for mapping String to Enum
                            string sportart= (string)reader["Sportart"];
                            Sportart s;
                            Enum.TryParse<Sportart>(sportart, out s);

                            spieler.Add(new Trainer(teilnehmerid, vorname, nachname, gb, trainerID, berufserfahrung,s));

                        }
                    }
                    return spieler;
                }
            }
        }

        public override Trainer GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) 
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@TrainerID", id);

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
                        int trainerID = (int)reader["TrainerID"];
                        int berufserfahrung = (int)reader["Berufserfahrung"];
                        //Logic for mapping String to Enum
                        string sportart = (string)reader["Sportart"];
                        Sportart s;
                        Enum.TryParse<Sportart>(sportart, out s);

                        return new Trainer(teilnehmerid, vorname, nachname, gb, id, berufserfahrung, s);

                    }
                }
            }
            return null;
        }

        public Trainer GetByTeilnehmerID(int id)
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
                        int trainerID = (int)reader["TrainerID"];
                        int berufserfahrung = (int)reader["Berufserfahrung"];
                        //Logic for mapping String to Enum
                        string sportart = (string)reader["Sportart"];
                        Sportart s;
                        Enum.TryParse<Sportart>(sportart, out s);

                        return new Trainer(teilnehmerid, vorname, nachname, gb, id, berufserfahrung, s);

                    }
                }
            }
            return null;
        }

        public void CreateOrUpdate(Trainer f)
        {
            base.CreateOrUpdate(f);

            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (f.TrainerID == 0)
                    {
                        command.CommandText = CREATE_TRAINER;
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Berufserfahrung", f.Berufserfahrung);
                        command.Parameters.AddWithValue("@Sportart", f.Sportart.ToString());
                        command.ExecuteNonQuery();
                        f.TrainerID = Convert.ToInt32(command.LastInsertedId);
                    }
                    else
                    {
                        command.CommandText = UPDATE_TRAINER;
                        command.Parameters.AddWithValue("@TrainerID", f.TrainerID);
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Berufserfahrung", f.Berufserfahrung);
                        command.Parameters.AddWithValue("@Sportart", f.Sportart.ToString());
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
