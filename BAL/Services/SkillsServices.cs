using BAL.IServices;
using DAL.DapperAccess;
using Portfolio_Backend.Models;
using Portfolio_Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class SkillsServices : ISkillsServices
    {
        private readonly PortfolioDbContext _context;   
        private readonly DapperAccess _dapper;  

        public SkillsServices(PortfolioDbContext context, DapperAccess dapper) 
        {
            _context = context;
            _dapper = dapper;
        }

        public Skill AddSkill(Skill skill)
        {
           Skill addedSkill = _context.Skills.Add(skill).Entity; 
            _context.SaveChanges();

            return addedSkill;
        }

        public Skill GetSkill(int id)
        {
            Skill skill = _context.Skills.Find(id)!;

            return skill;
        }

        public List<Skill> GetSkills()
        {
            List<Skill> skills = _context.Skills.ToList();

            return skills;
        }
    }
}
