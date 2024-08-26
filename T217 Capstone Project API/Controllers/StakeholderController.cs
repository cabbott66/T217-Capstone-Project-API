using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T217_Capstone_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StakeholderController : ControllerBase
    {
        // GET: api/<StakeholderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StakeholderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StakeholderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StakeholderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StakeholderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
