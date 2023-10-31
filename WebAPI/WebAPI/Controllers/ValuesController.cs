using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController(AuthenticationContext context)
        {
                
        }

        // GET api/values
        [HttpGet]
        [Route("Get")]

        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        [Route("Get/{id}")]

        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("Post/{value}")]

        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        [Route("Put/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // tesing CICD
        // DELETE api/values/5
        [HttpDelete]
        [Route("Delete/{id}")]

        public void Delete(int id)
        {
        }
    }
}
