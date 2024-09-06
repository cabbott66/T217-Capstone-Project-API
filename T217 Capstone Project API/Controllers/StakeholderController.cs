using Microsoft.AspNetCore.Mvc;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T217_Capstone_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StakeholderController : ControllerBase
    {
        private readonly JsonStakeholderRepository _repo = new JsonStakeholderRepository();

        // GET: api/<StakeholderController>
        [HttpGet]
        public IEnumerable<Stakeholder> Get()
        {
            var stakeholderList = _repo.GetStakeHoldersList();
            return stakeholderList;
        }

        // GET api/<StakeholderController>/5
        [HttpGet("{id}")]
        public Stakeholder Get(int id)
        {
            var stakeholder = _repo.GetStakeHolder(id);
            return stakeholder;
        }

        // POST api/<StakeholderController>
        [HttpPost]
        public void Post([FromBody] Stakeholder value)
        {
            _repo.CreateStakeholder(value);
        }

        // PUT api/<StakeholderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Stakeholder value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<StakeholderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.DeleteStakeholder(id);
        }
    }
}
