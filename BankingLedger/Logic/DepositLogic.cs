using BankingLedger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Logic
{
    public class DepositLogic
    {
        public Deposit CreateDeposit(string receivedFrom, string paymentMethod, decimal amount, string userName, string note = "")
        {
            var newDeposit = new Deposit()
            {
                ReceivedFrom = receivedFrom,
                PaymentMethod = paymentMethod,
                Amount = amount,
                Note = note,
                UserName = userName,
                DateAdded = DateTime.Now
            };

            var currentDepositList = (List<Deposit>)MemoryCache.Default["depositFor-" + userName];

            if (currentDepositList == null)
            {
                var newDepositList = new List<Deposit>();

                newDepositList.Add(newDeposit);

                MemoryCache.Default["depositFor-" + userName] = newDepositList;

            }else
            {
                currentDepositList.Add(newDeposit);

                MemoryCache.Default["depositFor-" + userName] = currentDepositList;
            }

            return newDeposit;
        }
    }
}
