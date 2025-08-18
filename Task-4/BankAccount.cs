using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    public class BankAccount
    {
        public int AccountNumber { get; }
        private decimal Balance { get; set; }
        public string Type { get; }
        public DateTime CreatedDate { get; }
        static int Number = 1;

        public BankAccount(decimal balance, string type)
        {
            CreatedDate = DateTime.Now;
            AccountNumber = Number;
            Number++;
            Balance = balance;
            Type = type;
        }

        public void updateBalance (string type,  decimal balance, out bool canWithdraw)
        {
            canWithdraw = false;
            if (type == "Deposite")
            {
                Balance += balance;
            }
            else if (type == "Withdraw")
            {
                if (Balance > balance)
                {
                    Balance -= balance;
                    canWithdraw = true;
                }
                else
                {
                    canWithdraw = false;
                    //Console.WriteLine("Can not make a Withdraw, No Sufficient Amount");
                }
            }
        }

        public decimal GetBalance()
        {
            return Balance;
        }
    }
}
