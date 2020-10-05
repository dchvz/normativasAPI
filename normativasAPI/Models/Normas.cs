using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace normativasAPI.Models
{
    public class Normas
    {
        public Normas()
        {
        }
        [BsonId]
        public string Id { get; set; }
        public float Esquema { get; set; }
        public string Nombre { get; set; }
        public string Edicion { get; set; }
        public int Gestion { get; set; }
        public List<Capitulos> Capitulos { get; set; }
    }
}
