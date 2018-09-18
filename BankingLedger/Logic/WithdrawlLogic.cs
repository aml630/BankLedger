using BankingLedger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Logic
{
   public class WithdrawlLogic
    {
        public Withdrawl CreateWithdrawl(string paymentTo, string paymentMethod, decimal amount, string userName, string note = "")
        {
            var newWithdrawl = new Withdrawl()
            {
                PaymentTo = paymentTo,
                PaymentMethod = paymentMethod,
                Amount = amount,
                Note = note,
                UserName = userName,
                DateAdded = DateTime.Now
            };

            var currentWithdrawlList = (List<Withdrawl>)MemoryCache.Default["withdrawlFor-" + userName];

            if (currentWithdrawlList == null)
            {
                var newWithdrawlList = new List<Withdrawl>();

                newWithdrawlList.Add(newWithdrawl);

                MemoryCache.Default["withdrawlFor-" + userName] = newWithdrawlList;

            }else
            {
                currentWithdrawlList.Add(newWithdrawl);

                MemoryCache.Default["withdrawlFor-" + userName] = currentWithdrawlList;
            }

            return newWithdrawl;
        }
    }
}
