using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingLedger.Logic;

namespace BankingLedger_Test
{
    [TestClass]
    public class DepositTests
    {
        [TestMethod]
        public void TestDeposit()
        {
            var cacheLogic = new CacheLogic();
            cacheLogic.ClearCache();

            var registerLogic = new RegistrationLogic();
            registerLogic.RegisterNewUser("alex", "pass");

            DepositLogic depositLogic = new DepositLogic();
            depositLogic.CreateDeposit("Customer1", "Check", 55, "alex");

            TransactionLogic transactionLogic = new TransactionLogic();

            var currentBalance = transactionLogic.GetBalance("alex");

            Assert.AreEqual(55, currentBalance);
        }
    }
}
