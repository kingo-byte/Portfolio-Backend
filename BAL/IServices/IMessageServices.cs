using Portfolio_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IMessageServices
    {
        public List<Message> GetMessages();

        public Message GetMessage(int id);

        public Message AddMessage(Message message);
    }
}
