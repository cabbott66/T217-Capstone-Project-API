using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models;

namespace T217_Capstone_Project_API.Repositories
{
    public class UserAuthRepository
    {
        private readonly StakeholderRisksContext _context = new StakeholderRisksContext();

        public User GetUser(int id)
        {
            User user = _context.Users.Where(x => x.UserID == id).FirstOrDefault();

            if (user == null)
            {
                user = new User();
            }
            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = _context.Users.Where(x => x.UserEmail == email).FirstOrDefault();

            if (user == null)
            {
                user = new User();
            }
            return user;
        }

        public User GetUserByApiKey(string apikey)
        {
            User user = _context.Users.Where(x => x.ApiKey == apikey).FirstOrDefault();

            if (user == null)
            {
                user = new User();
            }
            return user;
        }
        public IEnumerable<User> GetUserList()
        {
            IEnumerable<User> userList = _context.Users.OrderBy(x => x.UserID).ToList();

            if (userList == null)
            {
                userList = new List<User>();
            }
            return userList;
        }

        public void CreateUser(UserDTO user)
        {
            User newUser = new User();

            newUser.UserEmail = user.UserEmail;
            newUser.UserFirstName = user.UserFirstName;
            newUser.UserLastName = user.UserLastName;
            newUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password);

            _context.Add(newUser);
            _context.SaveChanges();
        }

        public string GetApiKey(string email, string password)
        {
            User user = _context.Users.Where(x => x.UserEmail == email).FirstOrDefault();
            string key = "";

            if (user == null)
            {
                key = "USER NOT FOUND";
            }
            else
            {
                bool passwordCorrect = BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password);

                if (passwordCorrect)
                {
                    key = user.ApiKey;
                }
                else key = "PASSWORD INCORRECT";
            }
            return key;
        }
    }
}
