//Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        UserController.cs
//Datum:        27.08.2021
//Beschreibung: Klasse UserController
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Tunierverwaltung.Model.DataMappers;
using Tunierverwaltung.Model.Entity.Enums;
using Tunierverwaltung.Model.Entity.Personen;

namespace Tunierverwaltung.Controller
{
    public class UserController
    {
        #region Eigenschaften
        private User _user;
        private UserDataMapper userDataMapper;
        #endregion 

        #region Modifier / Accessoren
        public User User { get => _user; set => _user = value; }
        public UserDataMapper UserDataMapper { get => userDataMapper; set => userDataMapper = value; }

        #endregion

        #region Konstruktoren
        public UserController() 
        {
            UserDataMapper = new UserDataMapper();
        }
        #endregion


        #region Worker
        public void login(string username, string password)
        {
            User tmp = UserDataMapper.GetByName(username);
            if (tmp.Password == password)
            {
                User = tmp;
            }
            else
            {
                //Password doesn't match, throw error
            }
        }

        public void logout()
        {
            User = null;
        }

        public bool isloggedin()
        {
            return User != null;
        }
        #endregion
    }
}
