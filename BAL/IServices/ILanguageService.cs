using Portfolio_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface ILanguageService
    {
        public List<Language> GetLanguages();

        public Language GetLanguage(int id);

        public Language AddLanguage(Language language);
    }
}
