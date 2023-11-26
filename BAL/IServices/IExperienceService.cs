using Portfolio_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IExperienceService
    {
        Task<List<Experience>> GetExperiences();
        Task<Experience> GetExperience(int id);
        Task<Experience> AddExperience(Experience experience);
    }
}
