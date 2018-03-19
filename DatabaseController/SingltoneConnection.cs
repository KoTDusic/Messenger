using DatabaseController.Models;
using SQLite;

namespace DatabaseController
{
    public class SingltoneConnection
    {
        private static SQLiteConnection _connection;
        private static readonly object SyncObject = new object();
        public const string DatabaseFilename = "notes.db";

        //private const string NotesCreateScript = "CREATE TABLE ACCOUNT ( " +
        //                                         "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
        //                                         "username STRING, " +
        //                                         "email STRING, " +
        //                                         "password STRING);";

        public static SQLiteConnection GetInstance()
        {
            lock (SyncObject)
            {
                if (_connection != null)
                {
                    return _connection;
                }
                _connection = new SQLiteConnection(DatabaseFilename, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
                var tableName = _connection.Table<Account>().Table.TableName;
                var table = _connection.GetTableInfo(tableName);
                if (table.Count == 0)
                {
                    _connection.CreateTable<Account>();
                    //_connection.Execute(NotesCreateScript);
                }
                return _connection;
            }
        }
    }
}
