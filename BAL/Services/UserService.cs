using BAL.IServices;
using Portfolio_Backend.Models;
using Portfolio_Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly PortfolioDbContext _context;
        public UserService(PortfolioDbContext context)
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }
    }
}
