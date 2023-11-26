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
    public class MessageServices : IMessageServices
    {
        private readonly PortfolioDbContext _context;
         
        public MessageServices(PortfolioDbContext context)
        {
            _context = context;
        }

        public Message AddMessage(Message message)
        {
            Message addedMessage = _context.Messages.Add(message).Entity;
            _context.SaveChanges();

            return addedMessage;
        }

        public Message GetMessage(int id)
        {
            Message message = _context.Messages.Find(id)!;

            return message;
        }

        public List<Message> GetMessages()
        {
            List<Message> messages = _context.Messages.ToList(); 

            return messages;
        }
    }
}
