using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite.Query.Internal;
using T217_Capstone_Project_API.Authentication;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T217_Capstone_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAuthRepository _repo = new UserAuthRepository();

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var userList = _repo.GetUserList();
            return userList;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var user = _repo.GetUser(id);
            return user;
        }

        [HttpPost("getApiKey")]
        [AllowAnonymous]
        public string GetApiKey([FromBody] LoginDTO login)
        {
            return _repo.GetApiKey(login.Email, login.Password);
        }

        // POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        public void Post([FromBody] UserDTO value)
        {
            _repo.CreateUser(value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
