using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingLedger.Logic;

namespace BankingLedger_Test
{
    [TestClass]
    public class WithdrawlTests
    {
        [TestMethod]
        public void TestWithdrawl()
        {
            var cacheLogic = new CacheLogic();
            cacheLogic.ClearCache();

            var registerLogic = new RegistrationLogic();
            registerLogic.RegisterNewUser("alex", "pass");

            WithdrawlLogic withdrawlLogic = new WithdrawlLogic();
            withdrawlLogic.CreateWithdrawl("Customer1", "Check", 22, "alex");

            TransactionLogic transactionLogic = new TransactionLogic();

            var currentBalance = transactionLogic.GetBalance("alex");

            Assert.AreEqual(-22, currentBalance);
        }
    }
}
