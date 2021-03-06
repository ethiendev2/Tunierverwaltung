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
        private List<User> _users;
        #endregion 

        #region Modifier / Accessoren
        public User User { get => _user; set => _user = value; }
        public UserDataMapper UserDataMapper { get => userDataMapper; set => userDataMapper = value; }
        public List<User> Users { get => _users; set => _users = value; }

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
            if(UserDataMapper.ValidateUser(username, password))
            {
                User = UserDataMapper.GetByName(username);
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

        public bool isGuest()
        {
            return User?.Role == Role.Guest;
        }

        public bool isAdmin()
        {
            return User?.Role == Role.Admin;
        }

        public List<User> getAlleUsers()
        {
            return Users = UserDataMapper.GetAllUser();
        }

        public void UserEntfernen(string username)
        {
            UserDataMapper.Delete(username);
        }

        public void UserHinzufuegen(string username, string password, Role role)
        {
            UserDataMapper.CreateUser(username, password, role);
        }

        public void UpdateRole(string username, Role r)
        {
            UserDataMapper.UpdateRole(username, r);
        }
        public void UpdatePassword(string username, string password)
        {
            UserDataMapper.UpdatePassword(username, password);
        }

        #endregion
    }
}
