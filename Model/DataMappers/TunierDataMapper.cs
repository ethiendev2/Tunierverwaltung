using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Mannschaften;
using Tunierverwaltung.Model.Entity.Personen;
using Tunierverwaltung.Model.Entity.Tunier;

namespace Tunierverwaltung.Model.DataMappers
{
    public class TunierDataMapper : AbstractDataMapper<Tunier>
    {
        private const string SELECT = "select * from tunier WWHERE TunierID = @TunierID";
        private const string SELECT_ALL = "select * from tunier";
        private const string DELETE = "DELETE FROM tunier WHERE TunierID = @TunierID";
        private const string CREATE_TUNIER = "insert into tunier values (null, @Name, @Ort, @Datum, @Sportart)";
        private const string UPDATE_TUNIER = "UPDATE tunier set TunierID = @TunierID, Name = @Name, Ort = @Ort, Datum = @Datum, Sportart = @Sportart WHERE TunierID = @TunierID";
        
        private const string SELECT_MANNSCHAFTEN = "SELECT * FROM tuniermannschaften where TunierID = @TunierID"; 
        private const string DELETE_MANNSCHAFT = "DELETE FROM tuniermannschaften WHERE TunierID = @TunierID AND MannschaftID = @MannschaftID";
        private const string ADD_MANNSCHAFT = "insert into tuniermannschaften values (null, @TunierID, @MannschaftID)";

        private const string SELECT_SPIELE = "SELECT * FROM tunierspiele where TunierID = @TunierID";
        private const string DELETE_SPIEL = "DELETE FROM tunierspiele WHERE TunierID = @TunierID AND SpielID = @SpielID";
        private const string ADD_SPIEL = "insert into tunierspiele values (null, @TunierID, @SpielID)";


        public List<Tunier> GetAll() 
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Tunier> tuniere = new List<Tunier>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int tunierID = (int)reader["TunierID"];
                            string name = (string)reader["Name"];
                            string ort = (string)reader["Ort"];
                            DateTime datum = (DateTime)reader["Datum"];
                            string dt = datum.ToString(DATE_FORMAT);
                            string sportart = (string)reader["Sportart"];
                            Sportart sa;
                            Enum.TryParse<Sportart>(sportart, out sa);

                            // Get Spiele
                            // Get Mannschaften
                            List<Mannschaft> mannschaften = GetMannschaften(tunierID);

                            List<Spiel> spiele = GetSpiele(tunierID);

                            tuniere.Add(new Tunier(tunierID, name, ort, dt, sa, mannschaften, spiele));

                        }
                    }
                    return tuniere;
                }
            }
        }

        public List<Mannschaft> GetMannschaften(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                MannschaftDataMapper mdm = new MannschaftDataMapper();

                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_MANNSCHAFTEN;
                    command.Parameters.AddWithValue("@TunierID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Mannschaft> mannschaften = new List<Mannschaft>();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int mannschaftID = (int)reader["MannschaftID"];
                            mannschaften.Add(mdm.GetByID(mannschaftID));

                        }
                    }
                    return mannschaften;
                }
         
            }
        }

        public List<Spiel> GetSpiele(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                SpielDataMapper sdm = new SpielDataMapper();

                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_SPIELE;
                    command.Parameters.AddWithValue("@TunierID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Spiel> spiele = new List<Spiel>();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int spielID = (int)reader["SpielID"];
                            spiele.Add(sdm.GetByID(spielID));

                        }
                    }
                    return spiele;
                }

            }
        }


        public Tunier GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@TunierID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        int tunierID = (int)reader["TunierID"];
                        string name = (string)reader["Name"];
                        string ort = (string)reader["Ort"];
                        DateTime datum = (DateTime)reader["Datum"];
                        string dt = datum.ToString(DATE_FORMAT);
                        string sportart = (string)reader["Sportart"];
                        Sportart sa;
                        Enum.TryParse<Sportart>(sportart, out sa);

                        // Get Mannschaften
                        List<Mannschaft> mannschaften = GetMannschaften(tunierID);
                        // Get Spiele

                        List<Spiel> spiele = GetSpiele(tunierID);


                        return new Tunier(tunierID, name, ort, dt, sa, mannschaften, spiele);
                    }
                }
            }
            return null;
        }

        public void RemoveMannschaft(int tunierID, int mannschaftID)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = DELETE_MANNSCHAFT;
                    command.Parameters.AddWithValue("@TunierID", tunierID);
                    command.Parameters.AddWithValue("@MannschaftID", mannschaftID);


                    command.ExecuteNonQuery();

                }
            }
        }

        public void AddMannschaft(int tunierID, int mannschaftid)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = ADD_MANNSCHAFT;
                    command.Parameters.AddWithValue("@TunierID", tunierID);
                    command.Parameters.AddWithValue("@MannschaftID", mannschaftid);

                    command.ExecuteNonQuery();

                }
            }
        }

        public void RemoveSpiel(int tunierID, int spielID)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = DELETE_SPIEL;
                    command.Parameters.AddWithValue("@TunierID", tunierID);
                    command.Parameters.AddWithValue("@SpielID", spielID);


                    command.ExecuteNonQuery();

                }
            }
        }

        public void AddSPiel(int spielid, int tunierid)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = ADD_SPIEL;
                    command.Parameters.AddWithValue("@SpielID", spielid);
                    command.Parameters.AddWithValue("@TunierID", tunierid);

                    command.ExecuteNonQuery();

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
                    command.Parameters.AddWithValue("@TunierID", id);

                    command.ExecuteNonQuery();

                }
            }
        }

        public override void CreateOrUpdate(Tunier t)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (t.TunierID == 0)
                    {

                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = CREATE_TUNIER;
                        command.Parameters.AddWithValue("@Name", t.Name);
                        command.Parameters.AddWithValue("@Ort", t.Ort);
                        command.Parameters.AddWithValue("@Datum", Convert.ToDateTime(t.Datum));
                        command.Parameters.AddWithValue("@Sportart", t.Sportart.ToString());

                        command.ExecuteNonQuery();
                        t.TunierID = Convert.ToInt32(command.LastInsertedId);

                    }
                    else
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = UPDATE_TUNIER;
                        command.Parameters.AddWithValue("@TunierID", t.TunierID);
                        command.Parameters.AddWithValue("@Name", t.Name);
                        command.Parameters.AddWithValue("@Ort", t.Ort);
                        command.Parameters.AddWithValue("@Datum", Convert.ToDateTime(t.Datum));
                        command.Parameters.AddWithValue("@Sportart", t.Sportart.ToString());

                        command.ExecuteNonQuery();

                    }

                }
            }
        }
    }
}
