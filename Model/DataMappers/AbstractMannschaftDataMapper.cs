using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Tunierverwaltung.Model.Entity.Mannschaften;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public abstract class AbstractMannschaftDataMapper<T> : AbstractDataMapper<Mannschaft>
    {

        private const string DELETE = "DELETE FROM MANNSCHAFT WHERE MannschaftID = @MannschaftID";
        private const string CREATE_MANNSCHAFT = "insert into mannschaft values (null, @Name, @Sitz, @Gruendung)";
        private const string UPDATE_MANNSCHAFT = "UPDATE mannschaft set MannschaftID = @MannschaftID, Name = @Name, Sitz = @Sitz, Gruendung = @Gruendung WHERE MannschaftID = @MannschaftID";
        private const string SELECT_MITGLIEDER = "SELECT * FROM mannschaftmitglieder where MannschaftID = @MannschaftID";
        public abstract List<T> GetAll();

        public abstract T GetByID(int id);

        public List<Teilnehmer> GetMitglieder(int id) 
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                FussballspielerDataMapper fdm = new FussballspielerDataMapper();

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
                                    Fussballspieler s = fdm.GetByID(teilnehmerID);
                                    mitglieder.Add(s);
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

                        command.ExecuteNonQuery();
                        m.MannschaftID = Convert.ToInt32(command.LastInsertedId);

                    }
                    else
                    {
                        //Update Teilnehmer
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = UPDATE_MANNSCHAFT;
                        command.Parameters.AddWithValue("@MannschaftID", m.MannschaftID);
                        command.Parameters.AddWithValue("@Name", m.Name);
                        command.Parameters.AddWithValue("@Sitz", m.Sitz);
                        command.Parameters.AddWithValue("@Gruendung", Convert.ToDateTime(m.Gruendung));
                        command.ExecuteNonQuery();

                    }

                }
            }
        }
    }
}