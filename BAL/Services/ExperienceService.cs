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
    public class ExperienceService : IExperienceService
    {
        private readonly PortfolioDbContext _context;

        public ExperienceService(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<Experience> AddExperience(Experience experience)
        {
            var addedExperience = await _context.Experiences.AddAsync(experience);

            await _context.SaveChangesAsync();

            return addedExperience.Entity;
        }

        public async Task<Experience> GetExperience(int id)
        {
            var experience =  await _context.Experiences.FindAsync(id);

            return experience!;
        }   

        public async Task<List<Experience>> GetExperiences()
        {
            return await _context.Experiences.ToListAsync();
        }
    }
}
