using App2.Infraestrutura;
using App2.Modelo;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace App2.DAL
{
   public class DTODisposicaoInicialDAL
    {
        private SQLiteConnection sqlConnection;

        public DTODisposicaoInicialDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<DTODisposicaoInicial>();
        }

        public IEnumerable<DTODisposicaoInicial> GetAll()
        {

            return (from t in sqlConnection.Table<DTODisposicaoInicial>() select t).ToList();

        }

        public DTODisposicaoInicial GetItemById(long Id)
        {

            return sqlConnection.Table<DTODisposicaoInicial>().FirstOrDefault(t => t.Id == Id);

        }

        public void DeleteById(long Id)
        {
            sqlConnection.Delete<DTODisposicaoInicial>(Id);
        }
        public void Add(DTODisposicaoInicial ocorrencias)
        {
            sqlConnection.Insert(ocorrencias);
        }

        public void Update(DTODisposicaoInicial ocorrencias)
        {
            sqlConnection.Update(ocorrencias);
        }
    }
}
