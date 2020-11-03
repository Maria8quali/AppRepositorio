
using System.IO;

using App2.Droid;
using App2.Infraestrutura;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace App2.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
         public SQLiteConnection DbConnection()
         {
             var dbName = "ocorrencias.db3";
             string documentsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
             string path = Path.Combine(documentsFolder, dbName);
             return new SQLiteConnection(path);

         }
      
    }
}