using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.DTO.UserDTOs;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Returns the User from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired User.</param>
        /// <returns></returns>
        public abstract Task<User> GetUserAsync(int id);

        /// <summary>
        /// Returns the User from the database with the matching email.
        /// </summary>
        /// <param name="email">The email of the desired User.</param>
        /// <returns></returns>
        public abstract Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// Returns the API key of the User from the database with the matching email and password.
        /// </summary>
        /// <param name="email">The email of the desired User.</param>
        /// <param name="password">The password of the desired User.</param>
        /// <returns></returns>
        public abstract Task<string> GetApiKeyAsync(string email, string password);

        /// <summary>
        /// Returns the User from the database with the matching API key.
        /// </summary>
        /// <param name="apiKey">The API key of the desired User.</param>
        /// <returns></returns>
        public abstract User GetUserByApiKeyAsync(string apiKey);

        /// <summary>
        /// Returns a list of all Users from the database.
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<User>> GetUserListAsync();

        /// <summary>
        /// Creates a new User in the database.
        /// </summary>
        /// <param name="user">The UserDTO to be added to the database.</param>
        /// <returns></returns>
        public abstract Task<User> CreateUserAsync(CreateUserDTO user);

        /// <summary>
        /// Updates the User on the database with the specified ID with new values.
        /// </summary>
        /// <param name="id">The ID of the User to be updated.</param>
        /// <param name="user">The replacement User.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateUserAsync(int id, User user);

        /// <summary>
        /// Deletes the User from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the User to be deleted.</param>
        /// <returns></returns>
        public abstract Task<bool> DeleteUserAsync(int id);
    }
}
