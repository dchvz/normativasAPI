using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using normativasAPI.APIData;
using normativasAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace normativasAPI.Controllers
{
    [Route("api/[controller]")]
    public class NormativasController : Controller
    {
        private readonly NormativasDBService _normativasContext;

        public NormativasController (NormativasDBService normasDB) {
            _normativasContext = normasDB;
        }
        // GET: api/values
        [HttpGet]
        public List<Normas> Get()
        {
            // comment
            return _normativasContext.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Normas norma)
        {
            _normativasContext.Create(norma);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
