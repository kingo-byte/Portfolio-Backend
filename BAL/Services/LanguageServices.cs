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
    public class LanguageServices : ILanguageService
    {
        private readonly PortfolioDbContext _context;
         
        public LanguageServices(PortfolioDbContext context)
        {
            _context = context;
        }

        public Language AddLanguage(Language language)
        {
            Language addedLanguage = _context.Languages.Add(language).Entity;
            _context.SaveChanges();

            return addedLanguage;
        }

        public Language GetLanguage(int id)
        {
            Language language = _context.Languages.Find(id)!;

            return language;
        }

        public List<Language> GetLanguages()
        {
            List<Language> languages = _context.Languages.ToList(); 

            return languages;
        }
    }
}
