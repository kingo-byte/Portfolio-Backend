using BAL.IServices;
using DAL.DapperAccess;
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
        private readonly DapperAccess _dapper;

        public UserService(PortfolioDbContext context, DapperAccess dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public User AddUser(User user)
        {
            User newUser = _context.Users.Add(user).Entity;

            _context.SaveChanges(); 

            return newUser;
        }

        public int BulkInsert(List<User> users)
        {
            var objects = new List<object>();

            foreach (var user in users) 
            {
                objects.Add(new { UserName = user.UserName, Email = user.Email, PasswordHash = user.PasswordHash, PasswordSalt = user.PasswordSalt, RoleId = user.RoleId });
            }

            int affectedRows = _dapper.BulkExecute("UsersBulkInsert", objects);

            return affectedRows;
        }

        public User CheckUser(User user)
        {
            User foundUser = _context.Users.Include(u => u.role)
                .Where(u => u.UserName.Equals(user.UserName) || u.Email.Equals(user.Email))
                .FirstOrDefault()!;

            return foundUser;
        }

        public User GetUser(int id)
        {
            return _context.Users.Include(u => u.role).FirstOrDefault(x => x.Id == id)!;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }
    }
}
