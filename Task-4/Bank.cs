using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task_4
{
    public class Bank
    {
        public string BankName { get; }
        public string BankCode { get; }
        public Bank(string name, string code)
        {
            BankName = name;
            BankCode = code;
        }

        List<Customer> Customers = new List<Customer>();

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public bool CanRemoveCustomer(Customer customer)
        {
            foreach (BankAccount account in customer.Accounts)
            {
                if (account.GetBalance() != 0)
                {
                    return false;
                }
            }
            return true;
        }
        public void RemoveCustomer(Customer customer)
        {
            if (CanRemoveCustomer(customer))
            {
                Customers.Remove(customer);
                Console.WriteLine("Customer Removed Successfully!");
            }
            else
            {
                Console.WriteLine("Can not remove Customer");
            }
        }

        public bool SearchCustomerbyName(string name, out Customer? CustomerFound)
        {
            foreach (Customer customer in Customers)
            {
                if (customer.CustomerName == name)
                {
                    CustomerFound = customer;
                    return true;
                }
            }
            CustomerFound = null;
            return false;
        }

        public bool SearchCustomerbyNationalID(string nationalid, out Customer? CustomerFound)
        {
            foreach (Customer customer in Customers)
            {
                if (customer.NationalID == nationalid)
                {
                    CustomerFound = customer;
                    return true;
                }
            }
            CustomerFound = null;
            return false;
        }

        public bool SearchForAccountNumber(int accountNumber, out BankAccount? accountFound)
        {
            foreach (Customer customer in Customers)
            {
                foreach (BankAccount account in customer.Accounts)
                {
                    if (account.AccountNumber == accountNumber)
                    {
                        accountFound = account;
                        return true;
                    }
                }
            }
            accountFound = null;
            return false;
        }

        public void BankReport()
        {
            if (Customers.Count == 0)
            {
                Console.WriteLine("There is No Customers");
                Console.WriteLine("----------------------------------------------------");
            }
            foreach (Customer customer in Customers)
            {
                customer.GetCustomerDetails();
            }
        }

    }
}
