using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Logic
{
    public class AuthenticationLogic
    {
        public bool CheckSystemForUsername(string username)
        {
            var cachedUsername = MemoryCache.Default["userName_" + username];

            if (cachedUsername == null)
            {
                return false;
            }

            return true;
        }


        public bool LoginUser(string username, string password)
        {
            var cachedCredentials = MemoryCache.Default["userLogin_" + username + "-" + password];

            if (cachedCredentials == null)
            {
                return false;
            }

            return true;
        }

    }
}
