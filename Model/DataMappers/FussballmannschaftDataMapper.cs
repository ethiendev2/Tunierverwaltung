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
    public class FussballmannschaftDataMapper : AbstractMannschaftDataMapper<FussballMannschaft>
    {
        private const string SELECT = "select * from fussballmannschaft f join mannschaft m on f.MannschaftID = m.MannschaftID where f.FussballmannschaftID = @FussballmannschaftID";
        private const string CREATE_FUSSBALLMANNSCHAFT = "insert into fussballmannschaft values (null, @MannschaftID, @Liga)";
        private const string UPDATE_FUSSBALLMANNSCHAFT = "UPDATE fussballmannschaft set FussballmannschaftID = @FussballmannschaftID, MannschaftID = @MannschaftID, Liga = @Liga WHERE FussballmannschaftID = @FussballmannschaftID";
        private const string SELECT_ALL = "SELECT * FROM fussballmannschaft f join mannschaft m on f.MannschaftID = m.MannschaftID";
        
        public override List<FussballMannschaft> GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_ALL;

                    MySqlDataReader reader = command.ExecuteReader();

                    List<FussballMannschaft> mannschaften = new List<FussballMannschaft>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int mannschaftID = (int)reader["MannschaftID"];
                            string name = (string)reader["Name"];
                            string sitz = (string)reader["Sitz"];
                            //Mapping Date from Db to String
                            DateTime gruendung = (DateTime)reader["Gruendung"];
                            string gd = gruendung.ToString(DATE_FORMAT);
                            int fussballmanschaftID = (int)reader["FussballmannschaftID"];
                            string liga = (string)reader["Liga"];

                            List<Teilnehmer> mitglieder = GetMitglieder(mannschaftID);

                            mannschaften.Add(new FussballMannschaft(mannschaftID, name, sitz, gd, fussballmanschaftID, liga, mitglieder));

                        }
                    }
                    return mannschaften;
                }
            }
        }

        public override FussballMannschaft GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) 
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT;
                    command.Parameters.AddWithValue("@FussballmanschaftID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        
                        int mannschaftID = (int)reader["MannschaftID"];
                        string name = (string)reader["Name"];
                        string sitz = (string)reader["Sitz"];
                        //Mapping Date from Db to String
                        DateTime gruendung = (DateTime)reader["Gruendung"];
                        string gd = gruendung.ToString(DATE_FORMAT);
                        int fussballmanschaftID = (int)reader["FussballmannschaftID"];
                        string liga = (string)reader["Liga"];

                        List<Teilnehmer> mitglieder = GetMitglieder(mannschaftID);

                        return new FussballMannschaft(mannschaftID, name, sitz, gd, fussballmanschaftID, liga, mitglieder);
                    }
                }
            }
            return null;
        }
        

        public void CreateOrUpdate(FussballMannschaft f)
        {
            base.CreateOrUpdate(f);

            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (f.FussballmannschaftID == 0)
                    {
                        command.CommandText = CREATE_FUSSBALLMANNSCHAFT;
                        command.Parameters.AddWithValue("@MannschaftID", f.MannschaftID);
                        command.Parameters.AddWithValue("@Liga", f.Liga);
                        command.ExecuteNonQuery();
                        f.FussballmannschaftID = Convert.ToInt32(command.LastInsertedId);

                    }
                    else
                    {
                        command.CommandText = UPDATE_FUSSBALLMANNSCHAFT;
                        command.Parameters.AddWithValue("@FussballmannschaftID", f.FussballmannschaftID);
                        command.Parameters.AddWithValue("@MannschaftID", f.MannschaftID);
                        command.Parameters.AddWithValue("@Liga", f.Liga);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
