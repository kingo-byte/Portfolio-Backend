using DAL.Models;
using Portfolio_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IRefreshTokenServices
    {
        public RefreshToken AddToken(RefreshToken refreshToken);

        public RefreshToken GetUserToken(int userId);
    }
}
