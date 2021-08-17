// Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        AbstractTeilnehmerDataMapper.cs
//Datum:        05.08.2021
//Beschreibung: Abstrake Basisklasse für alle Teilnehmer Data Mapper, Implementationen für Delete und Create/Update für die Teilnehmer Tabelle

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public abstract class AbstractTeilnehmerDataMapper<T> : AbstractDataMapper<Teilnehmer>
    {

        private const string DELETE = "DELETE FROM TEILNEHMER WHERE TeilnehmerID = @TeilnehmerID";
        private const string CREATE_TEILNEHMER = "insert into teilnehmer values (null, @Vorname, @Nachname, @Geburtstag)";
        private const string UPDATE_TEILNEHMER = "UPDATE teilnehmer set TeilnehmerID = @TeilnehmerID, Vorname = @Vorname, Nachname = @Nachname, Geburtstag = @Geburtstag WHERE TeilnehmerID = @TeilnehmerID";
        
        public List<Teilnehmer> GetAll()
        {
            return new List<Teilnehmer>();
        }

        public abstract T GetByID(int id);
        
        public override void Delete(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = DELETE;
                    command.Parameters.AddWithValue("@TeilnehmerID", id);

                    command.ExecuteNonQuery();

                }
            }
        }

        public override void CreateOrUpdate(Teilnehmer f)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (f.TeilnehmerID == 0)
                    {
                        // First insert Teilnehmer and get it's ID

                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = CREATE_TEILNEHMER;
                        command.Parameters.AddWithValue("@Vorname", f.Vorname);
                        command.Parameters.AddWithValue("@Nachname", f.Nachname);
                        command.Parameters.AddWithValue("@Geburtstag", Convert.ToDateTime(f.Geburtstag));
                        command.ExecuteNonQuery();
                        f.TeilnehmerID = Convert.ToInt32(command.LastInsertedId);

                    }
                    else
                    {
                        //Update Teilnehmer
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = UPDATE_TEILNEHMER;
                        command.Parameters.AddWithValue("@TeilnehmerID", f.TeilnehmerID);
                        command.Parameters.AddWithValue("@Vorname", f.Vorname);
                        command.Parameters.AddWithValue("@Nachname", f.Nachname);
                        command.Parameters.AddWithValue("@Geburtstag", Convert.ToDateTime(f.Geburtstag));
                        command.ExecuteNonQuery();

                    }

                }
            }
        }
    }
}