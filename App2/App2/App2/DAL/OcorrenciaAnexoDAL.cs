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
   public class OcorrenciaAnexoDAL
    {
        private SQLiteConnection sqlConnection;        private List<OcorrenciaAnexo> lista = new List<OcorrenciaAnexo>();        public OcorrenciaAnexoDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<OcorrenciaAnexo>();
        }                 public IEnumerable<OcorrenciaAnexo> GetAll()
        {

            return (from t in sqlConnection.Table<OcorrenciaAnexo>() select t).ToList();
        }            public OcorrenciaAnexo GetItemById(long Id)
        {

            return sqlConnection.Table<OcorrenciaAnexo>().FirstOrDefault(t => t.OcorrenciaId == Id);
        }
       
        public void DeleteById(long Id)
        {


            sqlConnection.Delete<OcorrenciaAnexo>(Id);
        }
        public void DeleteAll()
        {
            sqlConnection.DeleteAll<OcorrenciaAnexo>();
        }        public void Add(OcorrenciaAnexo ocorrenciaAnexo)
        {
            sqlConnection.Insert(ocorrenciaAnexo);
        }        public void Uptade(OcorrenciaAnexo ocorrenciaAnexo)
        {
            sqlConnection.Update(ocorrenciaAnexo);
        }           public void Update(OcorrenciaAnexo ocorrenciaAnexo)
        {
            sqlConnection.Update(ocorrenciaAnexo);
        }

     
    }
}
