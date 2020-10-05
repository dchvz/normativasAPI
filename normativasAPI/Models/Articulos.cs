using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace normativasAPI.Models
{
    public class Articulos
    {
        public Articulos()
        {
        }

        [BsonId]
        public string Id { get; set; }
        public float Esquema { get; set; }
        public string Articulo { get; set; }
        public List<Parrafos> Parrafos { get; set; }
    }
}
