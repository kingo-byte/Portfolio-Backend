using BAL.IServices;
using DAL.Models;
using Portfolio_Backend.Models;
using Portfolio_Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class RefreshTokenServices : IRefreshTokenServices
    {
        private readonly PortfolioDbContext _context;
        public RefreshTokenServices(PortfolioDbContext context) 
        {
            _context = context; 
        }

        public RefreshToken AddToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = _context.RefreshTokens.Add(refreshToken).Entity;

            _context.SaveChanges();

            return addedRefreshToken;
        }

        public RefreshToken GetUserToken(int userId)
        {
            var userToken = _context.RefreshTokens
                .OrderByDescending(t => t.CreatedAt)
                .Where(t => t.UserId == userId)
                .FirstOrDefault();

            return userToken!;
        }
    }
}
