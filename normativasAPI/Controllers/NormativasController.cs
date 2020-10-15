using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using normativasAPI.APIData;
using normativasAPI.APIData.Configuration;
using normativasAPI.Models;

// controlador que maneja APIs para la gestion de las normas

namespace normativasAPI.Controllers
{
    [Route("api/[controller]")]
    public class NormativasController : Controller
    {
        // la variable para el contexto
        private IMongoCollection<Normas> _normativasContext;
        // el nombre de la coleccion que usa este controlador
        private readonly string Coleccion = "Normas";
        // se iniciailza el contexto con las variables definidas en la configuracion de BD
        public NormativasController (NormativasDBService normasDB) {       
            _normativasContext = normasDB.database.GetCollection<Normas>(Coleccion);
        }

        // devuelve una lista de todas las normas
        [HttpGet]
        public IActionResult Get()
        {
            var normas = _normativasContext.Find(x => true).ToList();
            if (normas.Count == 0)
            {
                return NotFound("No se han encontrado normas");
            }
            return Ok(normas);
        }

        // devuelve norma por Id
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var norma = _normativasContext.Find<Normas>(x => x.id == id).FirstOrDefault();
            // si no existe un articulo con ese Id
            if (norma == null)
            {
                return NotFound("No se ha encontrado una norma con el Id: " + id);
            }
            // se devuelve el articulo si existe
            return Ok(norma);
        }

        // inserta nueva norma
        [HttpPost]
        public IActionResult Post([FromBody]Normas norma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos para el registro no son validos");
            }
            _normativasContext.InsertOne(norma);
            return CreatedAtRoute("PostNormative", norma);
        }

        // actualiza la norma por Id
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Normas value)
        {
            var norma = _normativasContext.Find<Normas>(x => x.id == id).FirstOrDefault();
            // si no existe un articulo con ese Id
            if (norma == null)
            {
                return NotFound("No se ha encontrado una norma con el Id: " + id);
            }
            _normativasContext.ReplaceOne(x => x.id == id, value);
            return NoContent();
        }

        // elimina una norma por Id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var norma = _normativasContext.Find<Normas>(x => x.id == id).FirstOrDefault();
            // si no existe un articulo con ese Id
            if (norma == null)
            {
                return NotFound("No se ha encontrado una norma con el Id: " + id);
            }
            _normativasContext.DeleteOne(x => x.id == id);
            return NoContent();
        }
    }
}
