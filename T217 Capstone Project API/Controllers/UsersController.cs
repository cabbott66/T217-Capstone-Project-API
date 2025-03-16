using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API;
using T217_Capstone_Project_API.Authentication;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.DTO.UserDTOs;
using T217_Capstone_Project_API.Repositories;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Users
        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(UserAuthenticationFilterAdmin))]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _repo.GetUserListAsync();

            if (!users.Any())
            {
                return NotFound();
            }

            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilterAdmin))]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repo.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("GetUserDetails")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<UserDetailsDTO>> GetUserDetails()
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var user = await _repo.GetUserByApiKeyAsync(apiKey!);

            UserDetailsDTO userDetails = new UserDetailsDTO(user.UserEmail, user.UserFirstName, user.UserLastName);

            return Ok(userDetails);
        }

        [HttpPost("GetApiKey")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetApiKey(LoginDTO login)
        {
            var apiKey = await _repo.GetApiKeyAsync(login.Email, login.Password);

            if (apiKey == "")
            {
                return Unauthorized();
            }

            return apiKey;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilterAdmin))]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            int updateStatus = await _repo.UpdateUserAsync(id, user);

            if (id != user.UserID)
            {
                return BadRequest();
            }

            switch(updateStatus)
            {
                case 0:
                    return NoContent();
                case 1:
                    return BadRequest();
                case 2:
                    return NotFound();
                default:
                    return BadRequest();
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(UserLoginDTO user)
        {
            var newUser = await _repo.CreateUserAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = newUser.UserID }, newUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilterAdmin))]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var wasDeleted = await _repo.DeleteUserAsync(id);

            if (!wasDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
