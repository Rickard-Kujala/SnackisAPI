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
    public class ChatController : ControllerBase
    {
        private readonly IChatDAL _dAL;

        public ChatController(IChatDAL dAL)
        {
            _dAL = dAL;
        }
        // GET: api/<ChatController>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_dAL.GetAllChats());
        }

        // GET api/<ChatController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ChatController>
        [HttpPost]
        public void Create([FromBody] Chat model)
        {
            if (ModelState.IsValid)
            {
                _dAL.CreateChat(model);
            }
        }

        // PUT api/<ChatController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChatController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
           await _dAL.DeletechatById(id);
        }
    }
}
