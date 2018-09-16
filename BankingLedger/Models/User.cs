using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Models
{
    class User
    {
        string Username { get; set; }
        List<Deposit> Deposits { get; set; }
        List<Withdrawl> Withdrawls { get; set; }
    }
}
