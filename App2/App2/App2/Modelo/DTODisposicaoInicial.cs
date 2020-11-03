using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace App2.Modelo
{
    [DataContract]

    public class DTODisposicaoInicial
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public string LoginDoResponsavel { get; set; }

        [ForeignKey(typeof(Ocorrencias))]
        public int? OcorrenciaId { get; set; }
    }
}
