using BAL.IServices;
using Microsoft.EntityFrameworkCore;
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

        public User AddUser(User user)
        {
            User newUser = _context.Users.Add(user).Entity;

            _context.SaveChanges(); 

            return newUser;
        }

        public User CheckUser(User user)
        {
            User foundUser = _context.Users.Include(u => u.role)
                .Where(u => u.UserName.Equals(user.UserName) || u.Email.Equals(user.Email))
                .FirstOrDefault();

            return foundUser;
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
