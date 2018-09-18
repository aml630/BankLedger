using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingLedger.Logic;

namespace BankingLedger_Test
{
    [TestClass]
    public class RegistrationTests
    {
        [TestMethod]
        public void RegistrationTest()
        {
            var cacheLogic = new CacheLogic();

            cacheLogic.ClearCache();

            var registerLogic = new RegistrationLogic();

            registerLogic.RegisterNewUser("alex", "pass");

            var authLogic = new AuthenticationLogic();

            var findUser = authLogic.CheckSystemForUsername("alex");

            var findUser2 = authLogic.CheckSystemForUsername("jon");

            Assert.AreEqual(findUser, true);

            Assert.AreEqual(findUser2, false);
        }
    }
}
