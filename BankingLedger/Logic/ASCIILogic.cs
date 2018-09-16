using BankingLedger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Logic
{
    public static class ASCIILogic
    {
        static public string Computer()
        {
            return @"
                    ,---------------------------,
                          |  /---------------------\  |
                          | |                       | |
                          | |        Words          | |
                          | |      Greatest         | |
                          | |       Banking         | |
                          | |        Ledger         | |
                          |  \_____________________/  |
                          |___________________________|
                        ,---\_____     []     _______/------,
                      /         /______________\           /|
                    /___________________________________ /  | ___
                    |                                   |   |    )
                    |  _ _ _                 [-------]  |   |   (
                    |  o o o                 [-------]  |  /    _)_
                    |__________________________________ |/     /  /
                /-------------------------------------/|      ( )/
              /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/ /
            /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/ /
            ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~     
                                                                             ";
        }

        static public string LoggedInScreen ()
        {
           return @"
                         .,,uod8B8bou,,.
              ..,uod8BBBBBBBBBBBBBBBBRPFT?l!i:.
         ,=m8BBBBBBBBBBBBBBBRPFT?!||||||||||||||
         !...:!TVBBBRPFT||||||||||!!^^""'   ||||
         !.......:!?|||||!!^^""'            ||||
         !.........||||                     ||||
         !.........||||  ##                 ||||
         !.........||||        Welcome      ||||
         !.........||||        To Your      ||||
         !.........||||        Banking      ||||
         !.........||||        Account      ||||
         !.........||||                     ||||
         `.........||||                    ,||||
          .;.......||||               _.-!!|||||
   .,uodWBBBBb.....||||       _.-!!|||||||||!:'
!YBBBBBBBBBBBBBBb..!|||:..-!!|||||||!iof68BBBBBb....
!..YBBBBBBBBBBBBBBb!!||||||||!iof68BBBBBBRPFT?!::   `.
!....YBBBBBBBBBBBBBBbaaitf68BBBBBBRPFT?!:::::::::     `.
!......YBBBBBBBBBBBBBBBBBBBRPFT?!::::::;:!^:::       `.
!........YBBBBBBBBBBRPFT ? !::::::::::^ ''...::::::;       iBBbo.
`..........YBRPFT ? !::::::::::::::::::::::::; iof68bo.     WBBBBbo.
  `..........:::::::::::::::::::::::; iof688888888888b.     `YBBBP ^ '
    `........::::::::::::::::; iof688888888888888888888b.     `
      `......:::::::::; iof688888888888888888888888888888b.
        `....:::; iof688888888888888888888888888888888899fT!
          `..::!8888888888888888888888888888888899fT | !^ '
            `' !!988888888888888888888888899fT|!^'
                `!!8888888888888888899fT | !^ '
                  `!988888888899fT | !^ '
                    `!9899fT | !^ '
                      `!^ '
                                                                             ";
        }

        static public string TransactionTable(List<Transaction> transactionList)
        {
            string transactionTablePrintout = "";

            string structuredHeader =string.Format("{0,-25} {1,-8} {2,14} {3,15}\n", "Date","Party", "Withdrawl", "Debit");

            transactionTablePrintout += structuredHeader;

            foreach (var transaction in transactionList)
            {

                if (transaction.Type == "Deposit")
                {
                    transactionTablePrintout += Environment.NewLine + string.Format("{0,-25} {1,-8} {2,10} {3,15}\n", transaction.DateAdded, transaction.OtherParty, "", transaction.Amount);
                }
                else
                {
                     transactionTablePrintout += Environment.NewLine + string.Format("{0,-25} {1,-8} {2,10} {3,15}\n", transaction.DateAdded, transaction.OtherParty, transaction.Amount, "");
                }
            }

            transactionTablePrintout += Environment.NewLine + "Account Total: $" + transactionList.Sum(x => x.Amount);

            return transactionTablePrintout;
        }

    }
}
