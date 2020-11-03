﻿using App2.Infraestrutura;
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
        private SQLiteConnection sqlConnection;
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Ocorrencias>();
        }
        {

            return (from t in sqlConnection.Table<Ocorrencias>()select t).ToList();
        }
        {

            return sqlConnection.Table<Ocorrencias>().FirstOrDefault(t => t.OcorrenciaId == Id);
        }
        {
            sqlConnection.Delete<Ocorrencias>(Id);
        }
        public void DeleteAll()
        {
            sqlConnection.DeleteAll<Ocorrencias>();
            sqlConnection.DropTable<Ocorrencias>();

        }
        {
            sqlConnection.Insert(ocorrencias);
        }
        {
            sqlConnection.Update(ocorrencias);
        }

   

    }
}