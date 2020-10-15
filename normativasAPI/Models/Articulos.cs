using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace normativasAPI.Models
{
    public class Articulos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public float esquema { get; set; }
        public string indice { get; set; }
        public string articulo { get; set; }
        public List<Parrafos> parrafos { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string normaId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string padreId { get; set; }
    }
}
