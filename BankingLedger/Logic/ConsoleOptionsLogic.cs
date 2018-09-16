using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static BankingLedger.Models.Enums;

namespace BankingLedger.Logic
{
    public class ConsoleOptionsLogic
    {
        private static readonly ILog log = LoggerLogic.CreateLogger();

        public static void LoggedOutScreen(string optionalMessage = "")
        {
            try
            {
                Console.Clear();

                Console.WriteLine(ASCIILogic.Computer());

                if (optionalMessage != "")
                {
                    Console.WriteLine(optionalMessage, Environment.NewLine);
                }

                Console.WriteLine("Welcome to the worlds greatest banking ledger! Type the number of the action you'd like to take from the list below." + Environment.NewLine);

                Console.WriteLine("(1. Login) - (2. Create Account)");

                var userNameInput = Console.ReadLine();

                if (userNameInput == "1")
                {
                    LoginUser();
                    return;
                }

                if (userNameInput == "2")
                {
                    RegisterUser();
                    return;
                }

                if (userNameInput == "3")
                {
                    throw new Exception("Trigger test exception");
                }

                LoggedOutScreen("Please input number indicating which action you'd like to take");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                LoggedOutScreen("There was a technical problem.  Please try your action again later");
            }
        }

        public static void LoggedInScreen(string userNameInput, string optionalMessage = "")
        {
            try
            {
                Console.Clear();

                Console.WriteLine(ASCIILogic.LoggedInScreen());

                if (optionalMessage != "")
                {
                    Console.WriteLine(optionalMessage, Environment.NewLine);
                }

                Console.WriteLine("Welcome back " + userNameInput + ".", Environment.NewLine);

                Console.WriteLine("(1. Record Deposit) - (2. Record Withdrawl) - (3. Check Balance) - (4. Transaction History) - (5. Logout) - (6. Send Money)");

                var loggedInInput = Console.ReadLine();

                HandleLoggedInUserInput(loggedInInput, userNameInput);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                LoggedInScreen(userNameInput, "There was a technical problem.  Please try your action again later");
            }
        }

        public static void HandleLoggedInUserInput(string loggedInInput, string userNameInput)
        {
            if (loggedInInput == "1")
            {
                DepositOrWithdrawl(userNameInput, BankActionTypes.Deposit);
                return;
            }

            if (loggedInInput == "2")
            {
                DepositOrWithdrawl(userNameInput, BankActionTypes.Withdrawl);
                return;
            }

            if (loggedInInput == "3")
            {
                GetAccountBalance(userNameInput);
                return;
            }

            if (loggedInInput == "4")
            {
                DisplayTransactions(userNameInput);
                return;
            }

            if (loggedInInput == "5")
            {
                LoggedOutScreen();
                return;
            }

            if (loggedInInput == "6")
            {
                TransferFunds(userNameInput);
                return;
            }

            LoggedInScreen(userNameInput, "Please input a number indicating an option above");
        }

        public static void TransferFunds(string userNameInput)
        {
            var transactionLogic = new TransactionLogic();

            Console.WriteLine("which friend would you like to send money to?");

            var friendList = transactionLogic.GetFriendList(userNameInput);

            foreach (var friend in friendList)
            {
                Console.WriteLine(friend);
            }

            var friendName = Console.ReadLine();

            string amountToSend = "";

            if (friendList.IndexOf(friendName) != -1)
            {
                Console.WriteLine("Wonderful!  How much would you like to send?");

                amountToSend = Console.ReadLine();

                decimal parsedSendAmount;

                if (!decimal.TryParse(amountToSend, out parsedSendAmount))
                {
                    LoggedInScreen(userNameInput, "Amount deposited must be a decimal");
                    return;
                }

                var newDepositLogic = new DepositLogic();

                newDepositLogic.CreateDeposit(userNameInput, "Online Transfer", parsedSendAmount, friendName);

                var newWithdrawlLogic = new WithdrawlLogic();

                newWithdrawlLogic.CreateWithdrawl(friendName, "Online Transfer", parsedSendAmount, userNameInput);

            }else
            {
                LoggedInScreen(userNameInput, "Type friend name exactly to send funds");
                return;
            }

            LoggedInScreen(userNameInput, "Successfully sent " + amountToSend + " to " + friendName);
        }

        public static void RegisterUser()
        {
            Console.WriteLine("Please enter your user name you'd like to use for your account", Environment.NewLine);

            var newUserName = Console.ReadLine();

            Console.WriteLine("Great, now what password would you like to use", Environment.NewLine);

            var newPassword = Console.ReadLine();

            var RegisterLogic = new RegistrationLogic();

            var createUserResult = RegisterLogic.RegisterNewUser(newUserName, newPassword);

            if (createUserResult == newUserName)
            {
                LoggedInScreen(newUserName, "New account created for " + newUserName);
            }
            else
            {
                LoggedOutScreen(createUserResult);
            }
        }

        public static void LoginUser()
        {
            var AuthLogic = new AuthenticationLogic();

            Console.WriteLine("Please enter your user name", Environment.NewLine);

            var userNameInput = Console.ReadLine();

            if (!AuthLogic.CheckSystemForUsername(userNameInput))
            {
                LoggedOutScreen("That username does not exist in our system");

                return;
            }
            Console.WriteLine("Please enter your password", Environment.NewLine);

            var passwordInput = Console.ReadLine();

            if (!AuthLogic.LoginUser(userNameInput, passwordInput))
            {
                LoggedOutScreen("That username/password doesn't exist in our system. Please create an account");

                return;
            }

            LoggedInScreen(userNameInput);
        }

        public static void GetAccountBalance(string userNameInput)
        {
            var transactionLogic = new TransactionLogic();

            var getBalance = transactionLogic.GetBalance(userNameInput);

            string accountBalanceMessage = "Your current account balance is $" + getBalance;

            LoggedInScreen(userNameInput, accountBalanceMessage);
        }

        public static void DisplayTransactions(string userNameInput)
        {
            TransactionLogic transLogic = new TransactionLogic();

            var transList = transLogic.GetTransactionList(userNameInput);

            var transactionTable = ASCIILogic.TransactionTable(transList);

            LoggedInScreen(userNameInput, transactionTable);
        }

        public static void DepositOrWithdrawl(string userNameInput, BankActionTypes transactionType)
        {
            string depositType = "";

            if (transactionType == BankActionTypes.Deposit)
            {
                depositType = "deposit";
            }
            else if (transactionType == BankActionTypes.Withdrawl)
            {
                depositType = "withdrawl";
            }
            else
            {
                LoggedInScreen(userNameInput, "Banking action not recognized");
            }

            Console.WriteLine("Alright, lets record a " + depositType);

            Console.WriteLine("What party is responsible for this " + depositType);

            var receivedFrom = Console.ReadLine();

            Console.WriteLine("What payment method was used?");

            var paymentMethod = Console.ReadLine();

            Console.WriteLine("How much was the " + depositType + " for?");

            var depositAmount = Console.ReadLine();

            decimal parsedDepositAmount;

            if (!decimal.TryParse(depositAmount, out parsedDepositAmount))
            {
                LoggedInScreen(userNameInput, "Amount deposited must be a decimal");
                return;
            }

            if (transactionType == BankActionTypes.Deposit)
            {
                DepositLogic depositLogic = new DepositLogic();

                var createdDeposit = depositLogic.CreateDeposit(receivedFrom, paymentMethod, parsedDepositAmount, userNameInput);
            }
            else if (transactionType == BankActionTypes.Withdrawl)
            {
                WithdrawlLogic withdrawlLogic = new WithdrawlLogic();

                var createdDeposit = withdrawlLogic.CreateWithdrawl(receivedFrom, paymentMethod, parsedDepositAmount, userNameInput);
            }
            else
            {
                LoggedInScreen(userNameInput, "Banking action not recognized");
            }

            LoggedInScreen(userNameInput, "Successfully recorded " + depositType + " for: $" + depositAmount);
        }
    }
}
