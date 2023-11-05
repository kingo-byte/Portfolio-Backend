using Portfolio_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IUserService
    {
        public User GetUser(int id);
        
        public IEnumerable<User> GetUsers();
    }
}
