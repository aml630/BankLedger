using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingLedger.Logic;

namespace BankingLedger_Test
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void TestGetBalance()
        {
            var cacheLogic = new CacheLogic();

            cacheLogic.ClearCache();

            var registerLogic = new RegistrationLogic();

            registerLogic.RegisterNewUser("alex", "pass");

            WithdrawlLogic withdrawlLogic = new WithdrawlLogic();

            withdrawlLogic.CreateWithdrawl("Customer1", "Check", 22, "alex");

            withdrawlLogic.CreateWithdrawl("Customer1", "Check", 2, "alex");

            DepositLogic depositLogic = new DepositLogic();

            depositLogic.CreateDeposit("Customer1", "Check", 55, "alex");

            TransactionLogic transactionLogic = new TransactionLogic();

            var currentBalance = transactionLogic.GetBalance("alex");

            Assert.AreEqual((55-22-2), currentBalance);
        }

        [TestMethod]
        public void TestTransactionList()
        {
            var cacheLogic = new CacheLogic();

            cacheLogic.ClearCache();

            var registerLogic = new RegistrationLogic();

            registerLogic.RegisterNewUser("alex", "pass");

            WithdrawlLogic withdrawlLogic = new WithdrawlLogic();

            var firstWithdrawl = withdrawlLogic.CreateWithdrawl("Customer1", "Check", 22, "alex");

            withdrawlLogic.CreateWithdrawl("Customer1", "Check", 2, "alex");

            DepositLogic depositLogic = new DepositLogic();

            var firstDeposit = depositLogic.CreateDeposit("Customer1", "Check", 55, "alex");

            TransactionLogic transactionLogic = new TransactionLogic();

            var transactionList = transactionLogic.GetTransactionList("alex");

            Assert.AreEqual(transactionList.Count, 3);

            Assert.AreEqual((-transactionList[1].Amount), firstWithdrawl.Amount);

            Assert.AreEqual((transactionList[0].Amount), firstDeposit.Amount);
        }
    }
}
