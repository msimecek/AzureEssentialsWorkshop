using Microsoft.AspNetCore.Mvc;
using STCapi.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace STCapi.Controllers
{
    [Route("api/[controller]")]
    public class LinksController : Controller
    {
        private DataContext _dbContext;

        public LinksController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/links
        [HttpGet]
        public IEnumerable<Links> Get()
        {
            return _dbContext.Links.ToList();
        }

        // GET api/links/5
        [HttpGet("{id}")]
        public Links Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/links
        [HttpPost]
        public IActionResult Post([FromBody]Links value)
        {
            _dbContext.Links.Add(value);
            _dbContext.SaveChanges();

            return Ok();
        }

        // PUT api/links/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Links value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/links/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
