using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public class HandballspielerDataMapper : AbstractTeilnehmerDataMapper<Handballspieler>
    {

        private const string SELECT = "select * from handballspieler h join teilnehmer t on h.TeilnehmerID = t.TeilnehmerID where h.HandballspielerID = @HandballspielerID";
        private const string SELECT_BY_TEILNEHMERID = "select * from handballspieler h join teilnehmer t on h.TeilnehmerID = t.TeilnehmerID where h.TeilnehmerID = @TeilnehmerID";
        private const string CREATE_HANDBALLSPIELER = "insert into handballspieler values (null, @TeilnehmerID, @Position)";
        private const string UPDATE_HANDBALLSPIELER = "UPDATE handballspieler set HandballspielerID = @HandballspielerID, TeilnehmerID = @TeilnehmerID, Position = @Position WHERE HandballspielerID = @HandballspielerID";
        private const string SELECT_ALL = "SELECT * FROM handballspieler h join teilnehmer t on h.TeilnehmerID = t.TeilnehmerID";

        
        public new List<Handballspieler> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Handballspieler> spieler = new List<Handballspieler>();

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
                            int fussballspielerID = (int)reader["HandballspielerID"];
                            //Logic for mapping String to Enum
                            string position = (string)reader["Position"];
                            PositionHandball pos;
                            Enum.TryParse<PositionHandball>(position, out pos);


                            spieler.Add(new Handballspieler(teilnehmerid, vorname, nachname, gb, fussballspielerID, pos));

                        }
                    }
                    return spieler;
                }
            }
        }

        public override Handballspieler GetByID(int id)
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
                        PositionHandball pos;
                        Enum.TryParse<PositionHandball>(position, out pos);

                        return new Handballspieler(teilnehmerID, vorname, nachname, gb, id, pos);

                    }
                }
            }
            return null;
        }

        public Handballspieler GetByTeilnehmerID(int id)
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
                        PositionHandball pos;
                        Enum.TryParse<PositionHandball>(position, out pos);

                        return new Handballspieler(teilnehmerID, vorname, nachname, gb, id, pos);

                    }
                }
            }
            return null;
        }

        public void CreateOrUpdate(Handballspieler f)
        {
            base.CreateOrUpdate(f);

            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (f.HandballspielerID == 0)
                    {
                        command.CommandText = CREATE_HANDBALLSPIELER;
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Position", f.Position.ToString());
                        command.ExecuteNonQuery();
                        f.HandballspielerID = Convert.ToInt32(command.LastInsertedId);
                    }
                    else
                    {
                        command.CommandText = UPDATE_HANDBALLSPIELER;
                        command.Parameters.AddWithValue("@HandballspielerID", f.HandballspielerID);
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Position", f.Position.ToString());
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
