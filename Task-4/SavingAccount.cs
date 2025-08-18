using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    public class SavingAccount : BankAccount
    {
        public decimal InterestRate { get; }
        public SavingAccount(decimal interestrate, decimal balance) : base(balance, "Saving Account")
        {
            InterestRate = interestrate;
        }

        public decimal Interest()
        {
            return (GetBalance() * (InterestRate / 100));
        }
    }
}
