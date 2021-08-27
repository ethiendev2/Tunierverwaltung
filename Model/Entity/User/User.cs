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
        private string _username;
        private Role _role;
        #endregion

        #region Modifier / Accessoren
        public string Username { get => _username; set => _username = value; }
        public Role Role { get => _role; set => _role = value; }

        #endregion

        #region Konstruktoren
        public User(string v2, Role v4)
        {
            Username = v2;
            Role = v4;
        }
        public User()
        { }
        #endregion


        #region Worker
        public bool isGuest()
        {
            return Role == Role.Guest;
        }
        #endregion
    }
}