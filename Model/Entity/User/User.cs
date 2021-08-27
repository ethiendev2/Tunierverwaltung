//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        User.cs
//Datum:        27.08.2021
//Beschreibung: User

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tunierverwaltung.Model.Entity.Enums;

namespace Tunierverwaltung.Model.Entity.Personen
{
    public class User
    {
        #region Eigenschaften
        private int _userID;
        private string _username;
        private string _password;
        private Role _role;
        #endregion

        #region Modifier / Accessoren
        public int UserID { get => _userID; set => _userID = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public Role Role { get => _role; set => _role = value; }

        #endregion

        #region Konstruktoren
        public User(int v1, string v2, string v3, Role v4)
        {
            UserID = v1;
            Username = v2;
            Password = v3;
            Role = v4;
        }
        public User()
        { }
        #endregion


        #region Worker
        #endregion
    }
}