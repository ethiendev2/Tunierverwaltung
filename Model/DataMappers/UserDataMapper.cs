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
    public class UserDataMapper : AbstractDataMapper<User>
    {

        private const string DELETE = "DELETE FROM USER WHERE UserID = @UserID";
        private const string CREATE_USER = "insert into user values (null, @Username, @Password, @Role)";
        private const string GET_USER = "select * from user where UserID = @UserID";
        private const string UPDATE_USER = "UPDATE user set UserID = @UserID, Username = @Username, Password = @Password, Role = @Role WHERE UserID = @UserID";
        private const string GET_BY_NAME = "select * from user where Username = @Username";
        public User GetByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = GET_USER;
                    command.Parameters.AddWithValue("@UserID", id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int userID = (int)reader["UserID"];
                        string username = (string)reader["Username"];
                        string password = (string)reader["Password"];
                        //Logic for mapping String to Enum
                        string role = (string)reader["Role"];
                        Role r;
                        Enum.TryParse<Role>(role, out r);

                        return new User(userID, username, password, r);

                    }
                }
                return null;
            }
        }

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
                        int userID = (int)reader["UserID"];
                        string name = (string)reader["Username"];
                        string password = (string)reader["Password"];
                        //Logic for mapping String to Enum
                        string role = (string)reader["Role"];
                        Role r;
                        Enum.TryParse<Role>(role, out r);

                        return new User(userID, username, password, r);

                    }
                }
                return null;
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
                    command.Parameters.AddWithValue("@UserID", id);

                    command.ExecuteNonQuery();

                }
            }
        }

        public override void CreateOrUpdate(User u)
        {
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (u.UserID == 0)
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = CREATE_USER;
                        command.Parameters.AddWithValue("@Username", u.Username);
                        command.Parameters.AddWithValue("@Password", u.Password);
                        command.Parameters.AddWithValue("@Role", u.Role.ToString());
                        command.ExecuteNonQuery();
                        u.UserID = Convert.ToInt32(command.LastInsertedId);

                    }
                    else
                    {
                        //Update Teilnehmer
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = UPDATE_USER;
                        command.Parameters.AddWithValue("@UserID", u.UserID);
                        command.Parameters.AddWithValue("@Username", u.Username);
                        command.Parameters.AddWithValue("@Password", u.Password);
                        command.Parameters.AddWithValue("@Role", u.Role.ToString());
                        command.ExecuteNonQuery();

                    }

                }
            }
        }
    }
}