// Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        UserDataMapper.cs
//Datum:        27.08.2021
//Beschreibung: UserDataMapper

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Model.DataMappers
{
    public class UserDataMapper
    {
        public const string CONNECTION_STRING = "Server=127.0.0.1;Database=Tunierverwaltung;Uid=root;Pwd=usbw;";

        private const string DELETE = "DELETE FROM USER WHERE Username = @Username";
        private const string CREATE_USER = "insert into user values (@Username, @Password, @Role)";
        private const string GET_USER = "select * from user where Username = @Username";
        private const string SELECT_PW = "select Password from user where Username = @Username";

        private const string UPDATE_USER = "UPDATE user set Password = @Password, Role = @Role WHERE Username = @Username";


        public User GetByName(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = GET_USER;
                    command.Parameters.AddWithValue("@Username", username);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string name = (string)reader["Username"];
                        //Logic for mapping String to Enum
                        string role = (string)reader["Role"];
                        Role r;
                        Enum.TryParse<Role>(role, out r);

                        return new User(username, r);

                    }
                    return null;
                }
            }
        }

        public bool ValidateUser(string userName, string passWord)
        {
            string lookupPassword = null;

            if ((null == userName) || (0 == userName.Length) || (userName.Length > 15))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.");
                return false;
            }

            if ((null == passWord) || (0 == passWord.Length) || (passWord.Length > 25))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.");
                return false;
            }

            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = SELECT_PW;
                    command.Parameters.AddWithValue("@Username", userName);

                    lookupPassword = (string)command.ExecuteScalar();

                    MySqlDataReader reader = command.ExecuteReader();

                } 
            }

            if (null == lookupPassword)
            {
                // You could write failed login attempts here to event log for additional security.
                return false;
            }

            return (0 == string.Compare(lookupPassword, passWord, false));

        }

        public void Delete(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = DELETE;
                    command.Parameters.AddWithValue("@Username", username);

                    command.ExecuteNonQuery();

                }
            }
        }

        public void CreateUser(string username, string password, Role role)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = CREATE_USER;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Role", role.ToString());
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(string username, string password, Role role)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = UPDATE_USER;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Role", role.ToString());
                    command.ExecuteNonQuery();

                }
            }
        }

    }
}