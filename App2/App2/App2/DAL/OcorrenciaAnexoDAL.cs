﻿using App2.Infraestrutura;
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
        private SQLiteConnection sqlConnection;
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<OcorrenciaAnexo>();
        }
        {

            return (from t in sqlConnection.Table<OcorrenciaAnexo>() select t).ToList();
        }
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
        }
        {
            sqlConnection.Insert(ocorrenciaAnexo);
        }
        {
            sqlConnection.Update(ocorrenciaAnexo);
        }
        {
            sqlConnection.Update(ocorrenciaAnexo);
        }

     
    }
}