using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Models
{
    public class Transaction
    {
        public string Type { get; set; }
        public string Note { get; set; }
        public string PaymentMethod { get; set; }
        public string OtherParty { get; set; }
        public decimal Amount { get; set; }
        public string UserName { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
