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
    public class PostController : ControllerBase
    {
        private readonly IPostDAL _postDAL;

        public PostController(IPostDAL postDAL)
        {
            _postDAL = postDAL;
        }
        // GET: api/<SnackisController>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_postDAL.GetAllPosts());
        }

        // GET api/<SnackisController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SnackisController>
        [HttpPost]
        public void Create([FromBody] Post model )
        {
            if (ModelState.IsValid)
            {
                _postDAL.CreatePost(model);
            }
        }

        // PUT api/<SnackisController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<SnackisController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
