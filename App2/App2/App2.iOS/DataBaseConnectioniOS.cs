using System;
using System.IO;

using App2.Infraestrutura;
using App2.iOS;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(DataBaseConnectioniOS))]
namespace App2.iOS
{
    public class DataBaseConnectioniOS : IDatabaseConnection
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