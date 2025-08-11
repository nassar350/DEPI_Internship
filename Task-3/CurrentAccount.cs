using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public class CurrentAccount : BankAccount
    {
        readonly decimal OverDraftLimit;

        public CurrentAccount(string name,string nationalID, int pnumber, string address, int accountnumber, decimal overdraftlimit)
            : base(name, nationalID, pnumber, address, accountnumber)
        {
            OverDraftLimit = overdraftlimit;
        }

        public override decimal CalculateInterest()
        {
            return base.CalculateInterest();
        }

        public override void showAccountDetails()
        {
            base.showAccountDetails();
            Console.WriteLine($"Over Draft Limit is: {OverDraftLimit}");
        }
    }
}
