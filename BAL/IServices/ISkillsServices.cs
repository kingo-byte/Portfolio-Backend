using Portfolio_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface ISkillsServices
    {
        public List<Skill> GetSkills();

        public Skill GetSkill(int id);

        public Skill AddSkill(Skill skill);
    }
}
