using App2.Infraestrutura;
using App2.Modelo;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace App2.DAL
{
   public class OcorenciaDAL
    {
        //Criando métodos de acesso ao banco
        private SQLiteConnection sqlConnection;        public OcorenciaDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Ocorrencias>();
        }        public IEnumerable<Ocorrencias> GetAll()
        {

            return (from t in sqlConnection.Table<Ocorrencias>()select t).ToList();
        }        public Ocorrencias GetItemById(long Id)
        {

            return sqlConnection.Table<Ocorrencias>().FirstOrDefault(t => t.OcorrenciaId == Id);
        }                public void DeleteById(long Id)
        {
            sqlConnection.Delete<Ocorrencias>(Id);
        }
        public void DeleteAll()
        {
            sqlConnection.DeleteAll<Ocorrencias>();
            sqlConnection.DropTable<Ocorrencias>();

        }        public void Add(Ocorrencias ocorrencias)
        {
            sqlConnection.Insert(ocorrencias);
        }        public void Update(Ocorrencias ocorrencias)
        {
            sqlConnection.Update(ocorrencias);
        }

   

    }
}
