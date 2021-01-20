using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using normativasAPI.APIData;
using normativasAPI.Models;

// controlador que maneja APIs para la gestion de articulos de las normas registradas

namespace normativasAPI.Controllers
{
    [Route("api/[controller]")]
    public class ArticulosController : Controller
    {
        // la variable para el contexto
        private IMongoCollection<Articulos> _normativasContext;
        // el nombre de la coleccion que usa este controlador
        private readonly string Coleccion = "Articulos";
        // se iniciailza el contexto con las variables definidas en la configuracion de BD
        public ArticulosController(NormativasDBService normasDB)
        {
            _normativasContext = normasDB.database.GetCollection<Articulos>(Coleccion);
        }

        // actualiza cambios emergentes, puede cambiar
        [HttpPut("updateStuff")]
        public async Task<IActionResult> UpdateStuff()
        {
            var articulos = _normativasContext.Find<Articulos>(x => x.articulo.EndsWith(":")).SortBy(x => x.indice).ToList();
            // si no hay articulos en la norma
            if (articulos.Count == 0)
            {
                return NotFound("No se han encontrado articulos para la norma seleccionada");
            }
            List<string> correctos = new List<string>();
            foreach (var articulo in articulos) {
                correctos.Add(articulo.articulo.Remove(articulo.articulo.Length - 1));
            }

            for (int i=0;i< articulos.Count;i++) {
                articulos[i].articulo = correctos[i];
                var update = await _normativasContext.ReplaceOneAsync(x => x.id.Equals(articulos[i].id), articulos[i]);
                if (!update.IsAcknowledged)
                {
                    return BadRequest("no se pudo actualizar");
                }
            }
            
            // se devuelve una lista de articulos si existe
            return Ok(articulos);
        }

        [HttpGet("GetChildrenArticles/{article}")]
        public async Task<IActionResult> GetChildrenArticles(string article)
        {
            //x.articulo.Count(puntos => puntos == '.') == 1
            var articulos = await _normativasContext.Find<Articulos>(x => x.padreId.Equals(article)).ToListAsync();
            // si no hay articulos en la norma
            if (articulos.Count == 0)
            {
                return NotFound("No se han encontrado articulos para la norma seleccionada");
            }
            // se devuelve una lista de articulos si existe
            return Ok(articulos);
        }

        // devuelve cada articulo de una norma en particular, por Id
        [HttpGet("ArticlesByNormative/{normaId}")]
        public IActionResult GetArticlesPerNormative(string normaId)
        {
            var articulos = _normativasContext.Find<Articulos>(x => x.normaId == normaId && x.parrafos.Count>0).SortBy(x=>x.indice).ToList();
            // si no hay articulos en la norma
            if (articulos.Count == 0) {
                return NotFound("No se han encontrado articulos para la norma seleccionada");
            }
            // se devuelve una lista de articulos si existe
            return Ok(articulos);
        }

        // devuelve un articulo por id
        [HttpGet("ArticleById/{id}")]
        public IActionResult GetArticle(string id)
        {
            var articulo = _normativasContext.Find<Articulos>(x => x.id == id).FirstOrDefault();
            // si no existe un articulo con ese Id
            if (articulo == null)
            {
                return NotFound("No se ha encontrado un articulo con el Id: " + id);
            }
            // se devuelve el articulo si existe
            return Ok(articulo);
        }

        // devuelve un articulo por id
        [HttpGet("ArticleByIndex/{indice}")]
        public IActionResult GetByIndex(string indice)
        {
            var articulo = _normativasContext.Find<Articulos>(x => x.indice == indice).FirstOrDefault();
            // si no existe un articulo con ese Id
            if (articulo == null)
            {
                return NotFound("No se ha encontrado un articulo con el indice: " + indice);
            }
            // se devuelve el articulo si existe
            return Ok(articulo);
        }

        // inserta un nuevo articulo, segun el Id de la norma y del articulo padre
        [HttpPost]
        public IActionResult Post([FromBody]Articulos art)
        {
            if (!ModelState.IsValid) {
                return BadRequest("Los datos para el registro no son validos");
            }
            _normativasContext.InsertOne(art);
            return CreatedAtRoute("PostArticle", art);
        }

        // actualiza un articulo por Id
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Articulos value)
        {
            var articulo = _normativasContext.Find<Articulos>(x => x.id == id).FirstOrDefault();
            // si no existe el articulo con el id para actualizar
            if (articulo == null)
            {
                return NotFound("No se ha encontrado un articulo con el Id: " + id);
            }
            // se actualiza el articulo si existe
            _normativasContext.ReplaceOne(x => x.id == id, value);
            return NoContent();
        }

        // elimina un articulo por Id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var articulo = _normativasContext.Find<Articulos>(x => x.id == id).FirstOrDefault();
            // si no existe el articulo para eliminar
            if (articulo == null)
            {
                return NotFound("No se ha encontrado un articulo con el Id: " + id);
            }
            // se elimina el articulo de la norma si existe
            _normativasContext.DeleteOne(x => x.id == id);
            return NoContent();
        }
    }
}
