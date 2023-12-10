using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.Models;

namespace Portfolio_Backend.Repository
{
    public class PortfolioDbContext : DbContext 
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Experience> Experiences { get; set; }

        public DbSet<Skill> Skills { get; set; }    

        public DbSet<Language> Languages { get; set; }   

        public DbSet<RefreshToken> RefreshTokens { get; set; }  
    }
}
