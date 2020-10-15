using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace normativasAPI.Models
{
    public class Normas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public float esquema { get; set; }
        public string nombre { get; set; }
        public string edicion { get; set; }
        public string descripcion { get; set; }
        public int gestion { get; set; }
        //public List<Capitulos> capitulos { get; set; }
    }
}
