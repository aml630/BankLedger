using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Logic
{
    public class RegistrationLogic
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

        public static void CreateAdminUser()
        {
            var RegisterLogic = new RegistrationLogic();
            RegisterLogic.RegisterNewUser("alex", "pass");
            DepositLogic depositLogic = new DepositLogic();
            depositLogic.CreateDeposit("Customer1", "Cash", 35, "alex");
            depositLogic.CreateDeposit("Customer2", "Cash", 43, "alex");
            depositLogic.CreateDeposit("Customer3", "Cash", 225, "alex");
            depositLogic.CreateDeposit("Customer4", "Cash", 45, "alex");

            WithdrawlLogic WithdrawlLogic = new WithdrawlLogic();
            WithdrawlLogic.CreateWithdrawl("Customer1", "Check", 22, "alex");
            WithdrawlLogic.CreateWithdrawl("Customer2", "Check", 32, "alex");
            WithdrawlLogic.CreateWithdrawl("Customer3", "Check", 22, "alex");
            WithdrawlLogic.CreateWithdrawl("Customer4", "Check", 45, "alex");


            RegisterLogic.RegisterNewUser("john", "pass");

            depositLogic.CreateDeposit("Customer1", "Cash", 55, "john");
            depositLogic.CreateDeposit("Customer2", "Cash", 44, "john");
            depositLogic.CreateDeposit("Customer3", "Cash", 67, "john");
            depositLogic.CreateDeposit("Customer4", "Cash", 86, "john");

            WithdrawlLogic.CreateWithdrawl("Customer1", "Check", 22, "john");
            WithdrawlLogic.CreateWithdrawl("Customer2", "Check", 4, "john");
            WithdrawlLogic.CreateWithdrawl("Customer3", "Check", 71, "john");
            WithdrawlLogic.CreateWithdrawl("Customer4", "Check", 12, "john");

            RegisterLogic.RegisterNewUser("bob", "pass");

            depositLogic.CreateDeposit("Customer1", "Cash", 55, "bob");
            depositLogic.CreateDeposit("Customer2", "Cash", 44, "bob");
            depositLogic.CreateDeposit("Customer3", "Cash", 67, "bob");
            depositLogic.CreateDeposit("Customer4", "Cash", 86, "bob");

            WithdrawlLogic.CreateWithdrawl("Customer1", "Check", 22, "bob");
            WithdrawlLogic.CreateWithdrawl("Customer2", "Check", 4, "bob");
            WithdrawlLogic.CreateWithdrawl("Customer3", "Check", 71, "bob");
            WithdrawlLogic.CreateWithdrawl("Customer4", "Check", 12, "bob");
        }
    }
}
