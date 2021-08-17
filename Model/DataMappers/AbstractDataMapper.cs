// Autor:        Henk Roberg
//Klasse:       IA119
//Datei:        AbstractDataMapper.cs
//Datum:        05.08.2021
//Beschreibung: Abstrake Basisklasse für alle DataMapper


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