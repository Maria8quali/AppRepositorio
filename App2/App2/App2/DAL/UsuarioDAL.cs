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
    public class UsuarioDAL
    {

        private SQLiteConnection sqlConnection;        public UsuarioDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Usuario>();
        }

        public Usuario GetItemById(long Id)
        {

            return sqlConnection.Table<Usuario>().FirstOrDefault(t => t.Id == Id);
        }        public void DeleteById(long Id)
        {
            sqlConnection.Delete<Usuario>(Id);
        }


        public IEnumerable<Usuario> GetAll()
        {
            return (from t in sqlConnection.Table<Usuario>() select t).OrderBy(i => i.Nome).ToList();
        }

        public void Add(Usuario usuario)
        {
            sqlConnection.Insert(usuario);
        }
        public void Update(Usuario usuarios)
        {
            sqlConnection.Update(usuarios);
        }
    }
}
