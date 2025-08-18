using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    public class Transaction
    {
        public string Type { get; }
        public decimal Amount { get; }
        public BankAccount Account { get; }
        public DateTime DateTime { get; }
        public Transaction(string type, decimal amount, BankAccount account)
        {
            Type = type;
            Amount = amount;
            Account = account;
            DateTime = DateTime.Now;
        }
    }
}
