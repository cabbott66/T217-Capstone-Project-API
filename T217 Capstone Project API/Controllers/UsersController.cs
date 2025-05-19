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

        [HttpGet("GetUserDetails")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<UserDetailsDTO>> GetUserDetails()
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var user = _repo.GetUserByApiKeyAsync(apiKey!);

            UserDetailsDTO userDetails = new UserDetailsDTO(user.UserEmail, user.UserFirstName, user.UserLastName);

            return Ok(userDetails);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var userID = _repo.GetUserByApiKeyAsync(apiKey!).UserID;

            if (id != userID)
            {
                return Unauthorized();
            }

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
        public async Task<ActionResult<User>> PostUser(CreateUserDTO user)
        {
            var newUser = await _repo.CreateUserAsync(user);

            if (newUser.UserEmail == "DUPLICATE")
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            return StatusCode(StatusCodes.Status201Created, newUser);
        }

        // DELETE: api/Users/5
        [HttpDelete]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult> DeleteSelf()
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var userID = _repo.GetUserByApiKeyAsync(apiKey!).UserID;

            var wasDeleted = await _repo.DeleteUserAsync(userID);

            if (!wasDeleted)
            {
                return NotFound();
            }

            return NoContent();
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
    }
}
