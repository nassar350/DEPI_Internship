using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    public class CurrentAccount : BankAccount
    {
        public decimal OverDraftLimit { get; }

        public CurrentAccount(decimal overdraftlimit, decimal balance) : base(balance, "Current Account")
        {
            OverDraftLimit = overdraftlimit;
        }

        public Decimal OverDraft()
        {
            return 0;
        }
    }
}
