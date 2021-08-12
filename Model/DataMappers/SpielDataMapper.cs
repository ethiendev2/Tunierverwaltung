using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Mannschaften;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public class SpielDataMapper : AbstractDataMapper<Spiel>
    {

        private const string SELECT_ALL = "select * from spiel";
        private const string SELECT = "select * from spiel where SpielID = @SpielID";
        private const string CREATE_SPIEL = "insert into spiel values (null, @TunierID, @Mannschaft1ID, @Mannschaft1Punkte, @Mannschaft2ID, @Mannschaft2Punkte)";
        private const string UPDATE_SPIEL = "UPDATE spiel set SpielID = @SpielID, TunierID = @TunierID, Mannschaft1ID = @Mannschaft1ID, Mannschaft1Punkte = @Mannschaft1Punkte, Mannschaft2ID = @Mannschaft2ID, Mannschaft2Punkte = @Mannschaft2Punkte where SpielID = @SpielID";
        private const string DELETE = "DELETE FROM spiel WHERE SpielID = @SpielID";

        private const string GET_PUNKTE1 = "SELECT * FROM spiel where TunierID = @TunierID AND Mannschaft1ID = @Mannschaft1ID";
        private const string GET_PUNKTE2 = "SELECT * FROM spiel where TunierID = @TunierID AND Mannschaft2ID = @Mannschaft2ID";


        public KeyValuePair<int, int> getRanking(int tunierid, int mannschaftID)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();


                List<KeyValuePair<int, int>> ranking = new List<KeyValuePair<int, int>>();
                KeyValuePair<int, int> rank = new KeyValuePair<int, int>();
                int sum = 0;

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = GET_PUNKTE1;
                    command.Parameters.AddWithValue("@TunierID", tunierid);
                    command.Parameters.AddWithValue("@Mannschaft1ID", mannschaftID);



                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int punkte = (int)reader["Mannschaft1Punkte"];
                            sum += punkte;
                        }
                    }

                    reader.Close();

                }

                using (MySqlCommand command2 = connection.CreateCommand())
                {
                    command2.CommandType = System.Data.CommandType.Text;


                    command2.CommandText = GET_PUNKTE2;
                    command2.Parameters.AddWithValue("@TunierID", tunierid);
                    command2.Parameters.AddWithValue("@Mannschaft2ID", mannschaftID);

                    MySqlDataReader reader2 = command2.ExecuteReader();

                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {

                            int punkte = (int)reader2["Mannschaft2Punkte"];
                            sum += punkte;
                        }

                    }

                    rank = new KeyValuePair<int, int>(mannschaftID, sum);

                    return rank;
                }
            }
        }

        public List<Spiel> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Spiel> spiele = new List<Spiel>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int spielid = (int)reader["SpielID"];
                            int tunierid = (int)reader["TunierID"];
                            int m1id = (int)reader["Mannschaft1ID"];
                            int m1punkte = (int)reader["Mannschaft1Punkte"];
                            int m2id = (int)reader["Mannschaft2ID"];
                            int m2punkte = (int)reader["Mannschaft2Punkte"];


                            spiele.Add(new Spiel(spielid, tunierid, m1id, m1punkte, m2id, m2punkte));

                        }
                    }
                    return spiele;
                }
            }
        }

        public Spiel GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) 
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@SpielID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int spielid = (int)reader["SpielID"];
                        int tunierid = (int)reader["TunierID"];
                        int m1id = (int)reader["Mannschaft1ID"];
                        int m1punkte = (int)reader["Mannschaft1Punkte"];
                        int m2id = (int)reader["Mannschaft2ID"];
                        int m2punkte = (int)reader["Mannschaft2Punkte"];

                        return new Spiel(spielid, tunierid, m1id, m1punkte, m2id, m2punkte);

                    }
                }
            }
            return null;
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
                    command.Parameters.AddWithValue("@SpielID", id);

                    command.ExecuteNonQuery();

                }
            }
        }

        public override void CreateOrUpdate(Spiel s)
        {

            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (s.SpielID == 0)
                    {
                        command.CommandText = CREATE_SPIEL;
                        command.Parameters.AddWithValue("@TunierID", s.TunierID);
                        command.Parameters.AddWithValue("@Mannschaft1ID", s.M1ID);
                        command.Parameters.AddWithValue("@Mannschaft1Punkte", s.M1Punkte);
                        command.Parameters.AddWithValue("@Mannschaft2ID", s.M2ID);
                        command.Parameters.AddWithValue("@Mannschaft2Punkte", s.M2Punkte);
                        command.ExecuteNonQuery();
                        s.SpielID = Convert.ToInt32(command.LastInsertedId);
                    }
                    else
                    {
                        command.CommandText = UPDATE_SPIEL;
                        command.Parameters.AddWithValue("@SpielID", s.SpielID);
                        command.Parameters.AddWithValue("@TunierID", s.TunierID);
                        command.Parameters.AddWithValue("@Mannschaft1ID", s.M1ID);
                        command.Parameters.AddWithValue("@Mannschaft1Punkte", s.M1Punkte);
                        command.Parameters.AddWithValue("@Mannschaft2ID", s.M2ID);
                        command.Parameters.AddWithValue("@Mannschaft2Punkte", s.M2Punkte);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
