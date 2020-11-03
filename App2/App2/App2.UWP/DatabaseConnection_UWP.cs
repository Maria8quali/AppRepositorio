using App2.Infraestrutura;
using App2.UWP;
using SQLite;
using System.IO;

using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_UWP))]

namespace App2.UWP
{
    public class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "ocorrencias.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }
    }
}
