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
    /// <summary>
    /// API endpoint controller that manages all endpoints related to Users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<UsersController>/GetUserDetails
        /// <summary>
        /// Returns the UserDetails of the User associated with the included API key.
        /// </summary>
        /// <returns>The UserDetails of the User.</returns>
        [HttpGet("GetUserDetails")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<UserDetailsDTO>> GetUserDetails()
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            // DO NOT MAKE THIS METHOD ASYNCHRONOUS, IT BREAKS THE ENTIRE API!
            var user = _repo.GetUserByApiKeyAsync(apiKey!);

            UserDetailsDTO userDetails = new UserDetailsDTO(user.UserEmail, user.UserFirstName, user.UserLastName);

            return Ok(userDetails);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Replaces the User associated with the included API key with the new User.
        /// </summary>
        /// <param name="user">The User to replace the existing one.</param>
        /// <returns>A status code depending on the results of the update.</returns>
        [HttpPut("UpdateSelf")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> UpdateSelf(User user)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var userID = _repo.GetUserByApiKeyAsync(apiKey!).UserID;

            if (userID != user.UserID)
            {
                return BadRequest();
            }

            int updateStatus = await _repo.UpdateUserAsync(userID, user);

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
        /// <summary>
        /// Creates a new User with the supplied CreateUserDTO. Email must be unique.
        /// </summary>
        /// <param name="user">The User to be created.</param>
        /// <returns>A 201 Created status code, or a 409 Conflict status code if the email already exists.</returns>
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

        // DELETE: api/Users
        /// <summary>
        /// Deletes the User associated with the included API key.
        /// </summary>
        /// <returns>204 No Content status code, or 404 Not Found status code if the User doesn't exist.</returns>
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

        // POST: api/Users/GetApiKey
        /// <summary>
        /// Gets the API key associated with the User with the matching login details.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The API key of the associated User.</returns>
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
