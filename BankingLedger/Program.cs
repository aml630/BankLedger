using BankingLedger.Logic;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistrationLogic.CreateAdminUser();

            ConsoleOptionsLogic.LoggedOutScreen();
        }    
    }
}
