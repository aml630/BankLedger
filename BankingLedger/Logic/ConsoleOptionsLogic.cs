﻿using log4net;
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

                Console.WriteLine("Welcome to the worlds greatest banking ledger! Type the number of the action you'd like to take from the list below" + Environment.NewLine);

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
                    throw new Exception("Test exception");
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

                Console.WriteLine("(1. Record Deposit) - (2. Record Withdrawl) - (3. Check Balance) - (4. Transaction History) - (5. Logout)");

                var loggedInInput = Console.ReadLine();

                HandleLoggedInUserInput(loggedInInput, userNameInput);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                LoggedOutScreen("There was a technical problem.  Please try your action again later");
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

            LoggedInScreen(userNameInput, "Please input a number indicating an option above");
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
                LoggedInScreen(newUserName, createUserResult);
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
        }
    }
}
