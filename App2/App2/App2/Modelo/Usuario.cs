using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace App2.Modelo
{
    [DataContract()]
    public class Usuario
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        [DataMember()]
        public string email { get; set; }
        public string senha { get; set; }
        public bool usuarioLogado { get; set; }





    }
}
