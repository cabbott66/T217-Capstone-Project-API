using T217_Capstone_Project_API.Models;

namespace T217_Capstone_Project_API.Repositories
{
    public class SqliteRepository
    {
        private StakeholderRisksContext _context = new StakeholderRisksContext();

        public User GetUser(int id)
        {
            User user = _context.Users.Where(x => x.UserID == id).FirstOrDefault();

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

        public void CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
