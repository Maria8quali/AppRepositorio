
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

using System.Runtime.Serialization;

namespace App2.Modelo
{
    [DataContract]
    public class OcorrenciaAnexo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public byte[]  Anexo { get; set; }
        public string FilePath { get; set; }
        [ForeignKey(typeof(Ocorrencias))]
        public int? OcorrenciaId { get; set; }
        [DataMember]
        public string ExtencaoAnexo { get; set; }
        [DataMember]
        public string ImageBytes { get; set; }
        [DataMember]
        public string NomeAnexo { get;set; }
        public DateTime dataFoto { get; set; }

    }
}
