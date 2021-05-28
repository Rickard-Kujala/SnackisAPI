using Microsoft.AspNetCore.Mvc;
using SnackisAPI.Dal;
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SnackisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IPostDAL _postDAL;

        public CategoryController(IPostDAL postDAL)
        {
            _postDAL = postDAL;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return _postDAL.GetAllcetegories().Distinct();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Create()
        {

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _postDAL.DeleteCategoryToDB(id);
        }
    }
}
