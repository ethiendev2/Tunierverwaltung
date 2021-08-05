using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Tunierverwaltung.Model.Entity.Enums;

namespace Tunierverwaltung.Model.DataMappers
{
    public abstract class AbstractDataMapper<T>
    {
        public const string CONNECTION_STRING = "Server=127.0.0.1;Database=Tunierverwaltung;Uid=root;Pwd=usbw;";
        public const string DATE_FORMAT = "yyyy-MM-dd";

        public abstract void Delete(int id);

        public abstract void CreateOrUpdate(T f);
    }
}