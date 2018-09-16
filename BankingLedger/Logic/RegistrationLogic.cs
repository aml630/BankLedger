using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Logic
{
    class RegistrationLogic
    {
        public string RegisterNewUser(string userName, string password)
        {
            var confirmUsernameNotTaken = MemoryCache.Default["userName_" + userName];

            if(confirmUsernameNotTaken!=null)
            {
                return "That username already exists.  Please login";
            }

            MemoryCache.Default["userLogin_" + userName + "-" + password] = userName + "-" + password;

            MemoryCache.Default["userName_" + userName] = userName;

            var whatWeHave = MemoryCache.Default["userLogin_" + userName + "-" + password];

            return userName;
        }
    }
}
