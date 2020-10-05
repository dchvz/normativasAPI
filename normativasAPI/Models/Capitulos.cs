using System;
namespace normativasAPI.Models
{
    public class Capitulos
    {
        public Capitulos()
        {
        }

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int [] Articulos { get; set; }
    }
}
