using BankingLedger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Logic
{
    class TransactionLogic
    {

        public decimal GetBalance(string userName)
        {
           var fullTransactionList = GetTransactionList(userName);

            return fullTransactionList.Sum(x => x.Amount);
        }

        public List<Transaction> GetTransactionList(string userName)
        {
            var wholeTransactionList = new List<Transaction>();

            var currentDepositList = (List<Deposit>)MemoryCache.Default["depositFor-" + userName];

            var currentWithdrawlList = (List<Withdrawl>)MemoryCache.Default["withdrawlFor-" + userName];


            if (currentDepositList != null)
            {
                foreach (var deposit in currentDepositList)
                {
                    var newDepositTransaction = new Transaction()
                    {
                        Type = "Deposit",
                        Note = deposit.Note,
                        PaymentMethod = deposit.PaymentMethod,
                        Amount = deposit.Amount,
                        UserName = deposit.UserName,
                        DateAdded = deposit.DateAdded,
                        OtherParty = deposit.ReceivedFrom
                    };

                    wholeTransactionList.Add(newDepositTransaction);
                }
            }

            if (currentWithdrawlList != null)
            {
                foreach (var withdrawl in currentWithdrawlList)
                {
                    var newWithdrawlTransaction = new Transaction()
                    {
                        Type = "Withdrawl",
                        Note = withdrawl.Note,
                        PaymentMethod = withdrawl.PaymentMethod,
                        Amount = -(withdrawl.Amount),
                        UserName = withdrawl.UserName,
                        DateAdded = withdrawl.DateAdded,
                        OtherParty = withdrawl.PaymentTo
                    };
                    wholeTransactionList.Add(newWithdrawlTransaction);
                }
            }

            return wholeTransactionList.OrderBy(x=>x.DateAdded).ToList();
        }
    }
}
