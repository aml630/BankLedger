using BankingLedger.Logic;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleOptionsLogic.CreateAdminUser();

            ConsoleOptionsLogic.LoggedOutScreen();
        }    
    }
}
