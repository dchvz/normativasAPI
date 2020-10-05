using System;
namespace normativasAPI.Models
{
    public class Parrafos
    {
        public Parrafos()
        {
        }
        public string Id { get; set; }
        public string Posicion { get; set; }
        public string Contenido { get; set; }
        public int Nivel { get; set; }
        public string [] Color { get; set; }
    }
}
