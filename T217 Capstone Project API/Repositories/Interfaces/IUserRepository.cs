using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public abstract Task<User> GetUserAsync(int id);

        public abstract Task<User> GetUserByEmailAsync(string email);

        public abstract Task<string> GetApiKeyAsync(string email, string password);

        public abstract Task<User> GetUserByApiKeyAsync(string apiKey);

        public abstract Task<List<User>> GetUserListAsync();

        public abstract Task<User> CreateUserAsync(UserDTO user);

        public abstract Task<int> UpdateUserAsync(int id, User user);

        public abstract Task<bool> DeleteUserAsync(int id);
    }
}
