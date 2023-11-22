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

        public User AddUser(User user);

        public User CheckUser(User user);

        public int BulkInsert(List<User> users);
    }
}
