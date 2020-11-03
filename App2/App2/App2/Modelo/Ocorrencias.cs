using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace App2.Modelo
{
    [DataContract()]
    public class Ocorrencias
    {
        [PrimaryKey, AutoIncrement]
        public long OcorrenciaId { get; set; }
        [DataMember()]
        public string Codigo { get; set; }
        [DataMember()]
        public string Descricao { get; set; }
        [DataMember()]

        public string UsuarioIncluiuLogin { get; set; }

        public string NomeUsuario { get; set; }
        [DataMember()]

        public DateTime DataInclusao { get; set; }
        [DataMember()]

        public int Chave { get; set; }

        public bool mostrarNome {get;set;}
        public bool sincronizar { get; set; }
        public bool NaoMostrarNome { get; set; }

        public byte[] Anexo { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        [DataMember]
        public OcorrenciaAnexo[] Anexos { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]

        [DataMember]
       public DTODisposicaoInicial[] DisposicaoInicials { get; set; }
        public bool  UserLogin{get;set;}

    
        public override int GetHashCode()
        {
            return OcorrenciaId.GetHashCode();
        }
   
    }
}
