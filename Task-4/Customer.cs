using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    public class Customer
    {
        public int CustomerID { get; }
        public string CustomerName { get; set; }
        public string NationalID { get; }
        public DateOnly BirthDate { get; set; }
        static int ID = 1;

        public List<BankAccount> Accounts = new List<BankAccount>();
        List<Transaction> transactions = new List<Transaction>();
        public Customer(string name, string nationalid, DateOnly birthdate)
        {
            CustomerID = ID;
            ID++;

            CustomerName = name;
            NationalID = nationalid;
            BirthDate = birthdate;
        }
        public void updatename (string name)
        {
            CustomerName = name;
        }

        public void updateDateofBirth (DateOnly dateofbirth)
        {
            BirthDate = dateofbirth;
        }

        public void AddAccount(BankAccount account)
        {
            Accounts.Add(account);
        }

        public void Deposite(decimal amount, BankAccount account)
        {
            account.updateBalance("Deposite", amount, out bool? canWithdraw);
            transactions.Add(new Transaction("Deposite", amount, account));
        }

        public void Withdraw(decimal amount, BankAccount account)
        {
            account.updateBalance("Withdraw", amount, out bool? canWithdraw);

            if (canWithdraw == true)
            {
                transactions.Add(new Transaction("Withdraw", amount, account));
            }
        }

        public void Transfer(decimal amount, BankAccount senderaccount, BankAccount recieveraccount)
        {
            senderaccount.updateBalance("Withdraw", amount, out bool? canWithdraw);
            if (canWithdraw == true)
            {
                transactions.Add(new Transaction("Withdraw", amount, senderaccount));

                recieveraccount.updateBalance("Deposite", amount, out bool? canWithdraw2);
                transactions.Add(new Transaction("Deposite", amount, recieveraccount));

                Console.WriteLine("Operation Completed Successfully");
            }
            else
            {
                Console.WriteLine("Can not Complete Operation");
            }
        }

        public void GetCustomerDetails(Customer customer)
        {
            decimal TotalAmount = 0;

            Console.WriteLine($"Customer Name is: {customer.CustomerName}\n" +
                $"Customer National ID is: {customer.NationalID}\n" +
                $"Customer BirthDate is: {customer.BirthDate}");
            Console.WriteLine("-----------------------------------------------------------------");

            foreach (BankAccount account in Accounts)
            {
                TotalAmount += account.GetBalance();
                Console.WriteLine($"Account Number: {account.AccountNumber}\n" +
                    $"Account Type: {account.Type}\n" +
                    $"Account Balance: {account.GetBalance()}\n" +
                    $"Created Date: {account.CreatedDate}");
                Console.WriteLine("-----------------------------------------------------------------");
            }
            Console.WriteLine($"Total Customer Balance is: {TotalAmount}");
            Console.WriteLine("-----------------------------------------------------------------");
            
        }

    }
}
