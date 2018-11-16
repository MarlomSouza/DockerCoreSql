using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using docker_app_compose.Data.Repository;
using docker_app_compose.Dominio;
using docker_app_compose.Dominio.Repository;
using Microsoft.AspNetCore.Mvc;

namespace docker_app_compose.Controllers
{
    [Route("api/[controller]")]
    public class PessoasController : Controller
    {
        private readonly IRepository<Pessoa> _repository;

        public PessoasController(IRepository<Pessoa> repository) => _repository = repository;

        // GET api/values
        [HttpGet]
        public IEnumerable<Pessoa> Get()
        {
            return _repository.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Pessoa Get(int id)
        {
            return _repository.Get(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Pessoa pessoa)
        {
            
            _repository.Save(pessoa);
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
