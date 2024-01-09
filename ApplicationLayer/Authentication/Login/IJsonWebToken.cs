using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Login
{
    public interface IJsonWebToken
    {
        public string GenerateJWT(string userName, string UserType);
    }
}
