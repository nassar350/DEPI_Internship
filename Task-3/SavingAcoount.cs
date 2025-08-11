using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public class SavingAcoount : BankAccount
    {
        readonly decimal InterestRate;

        public SavingAcoount(string name, string nationalID, int pnumber, string address, int accountnumber, int balance, decimal interestrate)
            :base(name, nationalID, pnumber, address, accountnumber, balance)
        {
            InterestRate = interestrate;
        }

        public override decimal CalculateInterest()
        {
            return ((Balance * InterestRate) / 100);
        }

        public override void showAccountDetails()
        {
            base.showAccountDetails();
            Console.WriteLine($"InTerest Rate is: {InterestRate}");
        }
    }
}
