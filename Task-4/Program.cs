using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace Task_4
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Bank> banks = new List<Bank>();
            bool program = true;

            while (program == true)
            {
                Console.WriteLine("Welcome to The Bank System");
                Console.WriteLine("1 - Create a Bank");
                Console.WriteLine("2 - Customer Management");
                Console.WriteLine("3 - Account Management");
                Console.WriteLine("4 - Reports");
                Console.WriteLine("5 - Exit");
                Console.Write("Enter Your Option: ");
                string? selection = Console.ReadLine();
                Console.WriteLine("----------------------------------------------------");
                switch (selection)
                {
                    case "1":
                        Console.Write("Enter Bank Name: ");
                        string? BankName = Console.ReadLine();
                        Console.Write("Enter Bank Code: ");
                        string? BankCode = Console.ReadLine();
                        Console.WriteLine();

                        banks.Add(new Bank(BankName, BankCode));
                        Console.WriteLine("Bank Created Successfully!");
                        Console.WriteLine("----------------------------------------------------");
                        break;
                    case "2":
                        bool customerPart = true;
                        while (customerPart == true)
                        {
                            Console.WriteLine("1 - Add New Customer");
                            Console.WriteLine("2 - Update Customer Information");
                            Console.WriteLine("3 - Remove Customer");
                            Console.WriteLine("4 - Search For Customer");
                            Console.WriteLine("5 - Back to Main Menu");
                            Console.Write("Enter Your Option: ");
                            string? CustomerSelection = Console.ReadLine();
                            Console.WriteLine("----------------------------------------------------");
                            if (CustomerSelection == "5") break;
                            if (banks.Count() == 0)
                            {
                                Console.WriteLine("There is no Registered Banks in the System");
                                Console.WriteLine("----------------------------------------------------");
                                break;
                            }
                            Console.WriteLine("Select A Bank: ");
                            for (int i = 0; i < banks.Count(); i++)
                            {
                                Console.WriteLine($"{i + 1} - {banks[i].BankName}");
                            }
                            Console.Write("Enter your Bank Selection Number: ");
                            int bankid = Convert.ToInt32(Console.ReadLine()) - 1;
                            Console.WriteLine("----------------------------------------------------");

                            switch (CustomerSelection)
                            {
                                case "1":                                   

                                    Console.Write("Enter Customer Name: ");
                                    string? CustomerName = Console.ReadLine();
                                    Console.Write("Enter Customer National ID: ");
                                    string? CustomerNationalID = Console.ReadLine();
                                    if (CustomerNationalID.Length != 14)
                                    {
                                        Console.WriteLine("National ID is Not Valid, Enter a Valid One");
                                        Console.WriteLine("----------------------------------------------------");
                                        break;
                                    }
                                    Customer? customerFound;
                                    if (banks[bankid].SearchCustomerbyNationalID(CustomerNationalID, out customerFound))
                                    {
                                        Console.WriteLine("A Customer With This National ID Already Exists");
                                        Console.WriteLine("----------------------------------------------------");
                                        break;
                                    }
                                    
                                    Console.Write("Enter Customer Birth Date (yyyy-mm-dd): ");
                                    DateTime CustomerBirthDate = Convert.ToDateTime(Console.ReadLine());
                                    DateOnly CustomerBirthDate2 = DateOnly.FromDateTime(CustomerBirthDate);
                                    Customer newcustomer = new Customer(CustomerName, CustomerNationalID, CustomerBirthDate2);

                                    banks[bankid].AddCustomer(newcustomer);
                                    Console.WriteLine("----------------------------------------------------");
                                    Console.WriteLine("Customer Added Successfully!");
                                    Console.WriteLine("----------------------------------------------------");
                                    break;
                                case "2":
                                    Console.WriteLine("Select Which Field you Want to Update: ");
                                    Console.WriteLine("1 - Name");
                                    Console.WriteLine("2 - Date of Birth");
                                    Console.Write("Enter your Choice: ");
                                    string? Fieldselection = Console.ReadLine();
                                    Console.WriteLine("----------------------------------------------------");
                                    Console.Write("Enter Customer National ID: ");
                                    string? customerNationalid = Console.ReadLine();
                                    Customer? customer;
                                    bool? found = banks[bankid].SearchCustomerbyNationalID(customerNationalid, out customer);
                                    if (found == true)
                                    {
                                        if (Fieldselection == "1")
                                        {
                                            Console.Write("Enter the New Name: ");
                                            string? newname = Console.ReadLine();
                                            customer.updatename(newname);
                                            Console.WriteLine("Name Changed Successfully!");
                                        }else if (Fieldselection == "2")
                                        {
                                            Console.Write("Enter Customer Birth Date (yyyy-mm-dd): ");
                                            DateTime CustomernewBirthDate = Convert.ToDateTime(Console.ReadLine());
                                            DateOnly CustomernewBirthDate2 = DateOnly.FromDateTime(CustomernewBirthDate);
                                            customer.updateDateofBirth(CustomernewBirthDate2);
                                            Console.WriteLine("Date changed successfully!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Customer Not Found");
                                    }
                                    Console.WriteLine("----------------------------------------------------");
                                    break;
                                case "3":
                                    Console.Write("Enter Customer National ID: ");
                                    string? customernationalid = Console.ReadLine();
                                    Console.WriteLine("----------------------------------------------------");
                                    Customer? customer2;
                                    bool? isfound = banks[bankid].SearchCustomerbyNationalID(customernationalid, out customer2);
                                    if (isfound == true)
                                    {
                                        banks[bankid].RemoveCustomer(customer2);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Customer Not Found");
                                    }
                                    Console.WriteLine("----------------------------------------------------");
                                    break;
                                case "4":
                                    Console.WriteLine("Enter a method for Searching: ");
                                    Console.WriteLine("1 - by National ID");
                                    Console.WriteLine("2 - by Name");
                                    Console.Write("Enter Your Choice: ");
                                    string? searchselection = Console.ReadLine();
                                    Console.WriteLine("----------------------------------------------------");

                                    if (searchselection == "1")
                                    {
                                        Customer? customer3;
                                        Console.Write("Enter Customer National ID: ");
                                        string? nationalid = Console.ReadLine();
                                        Console.WriteLine("----------------------------------------------------");
                                        bool isFound = banks[bankid].SearchCustomerbyNationalID(nationalid, out customer3);

                                        if (isFound == true)
                                        {
                                            Console.WriteLine("Customer Found");
                                            Console.WriteLine($"Customer ID: {customer3.CustomerID}\n" +
                                                $"Customer Name: {customer3.CustomerName}\n" +
                                                $"Customer National ID: {customer3.NationalID}\n" +
                                                $"Customer Date of Birth: {customer3.BirthDate}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Customer Not Found");
                                        }
                                    }
                                    else if (searchselection == "2")
                                    {
                                        Customer? customer3;
                                        Console.Write("Enter Customer Name: ");
                                        string? name = Console.ReadLine();
                                        Console.WriteLine("----------------------------------------------------");
                                        bool isFound = banks[bankid].SearchCustomerbyName(name, out customer3);

                                        if (isFound == true)
                                        {
                                            Console.WriteLine("Customer Found");
                                            Console.WriteLine($"Customer ID: {customer3.CustomerID}\n" +
                                                $"Customer Name: {customer3.CustomerName}\n" +
                                                $"Customer National ID: {customer3.NationalID}\n" +
                                                $"Customer Date of Birth: {customer3.BirthDate}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Customer Not Found");
                                        }
                                    }
                                    Console.WriteLine("----------------------------------------------------");
                                    break;
                                case "5":
                                    customerPart = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid Option, Please Enter A Valid One");
                                    break;
                            }
                        }
                        break;
                    case "3":
                        bool accountselection = true;
                        while(accountselection == true)
                        {
                            Console.WriteLine("1 - Open A New Account");
                            Console.WriteLine("2 - Deposit Money");
                            Console.WriteLine("3 - Withdraw Money");
                            Console.WriteLine("4 - Transfer Money");
                            Console.WriteLine("5 - Back To Main Menu");
                            Console.Write("Enter Your Choice: ");
                            string? accountselect = Console.ReadLine();
                            if (accountselect == "5") break;
                            Console.WriteLine("----------------------------------------------------");

                            if (banks.Count() == 0)
                            {
                                Console.WriteLine("There is no Registered Banks in the System");
                                Console.WriteLine("----------------------------------------------------");
                                break;
                            }
                            Console.WriteLine("Select A Bank: ");
                            for (int i = 0; i < banks.Count(); i++)
                            {
                                Console.WriteLine($"{i + 1} - {banks[i].BankName}");
                            }
                            Console.Write("Enter your Bank Selection Number: ");
                            int bankid = Convert.ToInt32(Console.ReadLine()) - 1;
                            Console.WriteLine("----------------------------------------------------");

                            Console.Write("Enter Customer National ID: ");
                            string? customerNationalid = Console.ReadLine();
                            Console.WriteLine();
                            Customer? customer;
                            bool? found = banks[bankid].SearchCustomerbyNationalID(customerNationalid, out customer);
                            if (found == false)
                            {
                                Console.WriteLine("Customer Not Found");
                            }
                            Console.WriteLine("----------------------------------------------------");

                            switch (accountselect)
                            {
                                case "1":
                                    Console.WriteLine("Select Account Type: ");
                                    Console.WriteLine("1 - Saving Account");
                                    Console.WriteLine("2 - Current Account");
                                    Console.Write("Enter your Choice: ");
                                    string? choice = Console.ReadLine();
                                    Console.WriteLine("----------------------------------------------------");
                                    if (choice == "1")
                                    {
                                        Console.Write("Enter Your Interest Rate: ");
                                        decimal interestrate = Convert.ToDecimal(Console.ReadLine());
                                        Console.Write("Enter your Deposit Amount: ");
                                        decimal depositamount = Convert.ToDecimal(Console.ReadLine());
                                        Console.WriteLine();

                                        SavingAccount account = new SavingAccount(interestrate, depositamount);
                                        customer.AddAccount(account);
                                        Console.WriteLine("Account Created Successfully!");
                                    }
                                    else if (choice == "2")
                                    {
                                        Console.Write("Enter Your Over Draft Limit: ");
                                        decimal overdraftlimit = Convert.ToDecimal(Console.ReadLine());
                                        Console.Write("Enter your Deposit Amount: ");
                                        decimal depositamount = Convert.ToDecimal(Console.ReadLine());
                                        Console.WriteLine();

                                        CurrentAccount account = new CurrentAccount(overdraftlimit, depositamount);
                                        customer.AddAccount(account);
                                        Console.WriteLine("Account Created Successfully!");
                                    }
                                    Console.WriteLine("----------------------------------------------------");
                                    break;
                                case "2":
                                    if (customer.Accounts.Count() == 0)
                                    {
                                        Console.WriteLine("Customer Does Not Have Any Accounts");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Choose one of Your Accounts: ");
                                        for (int i = 0; i < customer.Accounts.Count(); i++)
                                        {
                                            Console.WriteLine($"{i+1} - {customer.Accounts[i].AccountNumber}");
                                        }
                                        Console.Write("Enter Your Choice: ");
                                        int accountid = Convert.ToInt32(Console.ReadLine()) - 1;
                                        Console.WriteLine("----------------------------------------------------");

                                        Console.Write("Enter Amount you Want to Deposit: ");
                                        decimal amount = Convert.ToDecimal(Console.ReadLine());
                                        customer.Deposite(amount, customer.Accounts[accountid]);
                                        Console.WriteLine("Money Has Been Deposited Successfully!");

                                    }
                                    Console.WriteLine("----------------------------------------------------");
                                    break;
                                case "3":
                                    if (customer.Accounts.Count() == 0)
                                    {
                                        Console.WriteLine("Customer Does Not Have Any Accounts");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Choose one of Your Accounts: ");
                                        for (int i = 0; i < customer.Accounts.Count(); i++)
                                        {
                                            Console.WriteLine($"{i + 1} - {customer.Accounts[i].AccountNumber}");
                                        }
                                        Console.Write("Enter Your Choice: ");
                                        int accountid = Convert.ToInt32(Console.ReadLine()) - 1;
                                        Console.WriteLine("----------------------------------------------------");

                                        Console.Write("Enter Amount you Want to Withdraw: ");
                                        decimal amount = Convert.ToDecimal(Console.ReadLine());
                                        if (customer.Withdraw(amount, customer.Accounts[accountid]))
                                        {
                                            Console.WriteLine("Money Has Been Withdrawed Successfully!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Can not make a Withdraw, No Sufficient Amount");
                                        }

                                    }
                                    Console.WriteLine("----------------------------------------------------");
                                    break;
                                case "4":
                                    if (customer.Accounts.Count() == 0)
                                    {
                                        Console.WriteLine("Customer Does Not Have Any Accounts");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Choose one of Your Accounts: ");
                                        for (int i = 0; i < customer.Accounts.Count(); i++)
                                        {
                                            Console.WriteLine($"{i + 1} - {customer.Accounts[i].AccountNumber}");
                                        }
                                        Console.Write("Enter Your Choice: ");
                                        int accountid = Convert.ToInt32(Console.ReadLine()) - 1;
                                        Console.WriteLine("----------------------------------------------------");

                                        Console.Write("Enter Recevier Account Number: ");
                                        int recevieraccountnumber = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("Enter Amount you Want to Transfer: ");
                                        decimal amount = Convert.ToDecimal(Console.ReadLine());
                                        Console.WriteLine("----------------------------------------------------");

                                        BankAccount? accountFound;
                                        bool accountfound = false;
                                        foreach (Bank bnk in banks)
                                        {
                                            if (bnk.SearchForAccountNumber(recevieraccountnumber, out accountFound) == true)
                                            {
                                                accountfound = true;
                                                customer.Transfer(amount,customer.Accounts[accountid], accountFound);
                                                break;
                                            }
                                        }
                                        if (accountfound == false)
                                        {
                                            Console.WriteLine("Recevier Account is Not A Valid Account");
                                        }
                                    }
                                    Console.WriteLine("----------------------------------------------------");
                                    break;
                                case "5":
                                    accountselection = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid Option, Please Enter A Valid One");
                                    break;
                            }
                        }
                        Console.WriteLine("----------------------------------------------------");
                        break;
                    case "4":
                        bool reportselection = true;
                        while(reportselection == true)
                        {
                            Console.WriteLine("1 - Bank Report");
                            Console.WriteLine("2 - Customer Report");
                            Console.WriteLine("3 - Customer Transaction History");
                            Console.WriteLine("4 - Back To Main Menu");
                            Console.Write("Enter Your Choice: ");
                            string? reportselect = Console.ReadLine();
                            Console.WriteLine("----------------------------------------------------");
                            if (reportselect == "4") break;

                            if (banks.Count() == 0)
                            {
                                Console.WriteLine("There is no Registered Banks in the System");
                                Console.WriteLine("----------------------------------------------------");
                                break;
                            }
                            Console.WriteLine("Select A Bank: ");
                            for (int i = 0; i < banks.Count(); i++)
                            {
                                Console.WriteLine($"{i + 1} - {banks[i].BankName}");
                            }
                            Console.Write("Enter your Bank Selection Number: ");
                            int bankid = Convert.ToInt32(Console.ReadLine()) - 1;
                            Console.WriteLine("----------------------------------------------------");

                            switch (reportselect)
                            {
                                case "1":
                                    banks[bankid].BankReport();
                                    break;
                                case "2":
                                    Console.Write("Enter Customer National ID: ");
                                    string? customerNationalid = Console.ReadLine();
                                    Console.WriteLine();
                                    Customer? customer;
                                    bool? found = banks[bankid].SearchCustomerbyNationalID(customerNationalid, out customer);
                                    if (found == false)
                                    {
                                        Console.WriteLine("Customer Not Found");
                                        Console.WriteLine("----------------------------------------------------");
                                    }
                                    else
                                    {
                                        Console.WriteLine("----------------------------------------------------");
                                        customer.GetCustomerDetails();
                                    }
                                    break;
                                case "3":
                                    Console.Write("Enter Customer National ID: ");
                                    string? customernationalid = Console.ReadLine();
                                    Console.WriteLine();
                                    Customer? customer5;
                                    bool? found2 = banks[bankid].SearchCustomerbyNationalID(customernationalid, out customer5);
                                    if (found2 == false)
                                    {
                                        Console.WriteLine("Customer Not Found");
                                        Console.WriteLine("----------------------------------------------------");
                                    }
                                    else
                                    {
                                        Console.WriteLine("----------------------------------------------------");
                                        customer5.TransactionHistory();
                                    }
                                    break;
                                case "4":
                                    reportselection = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid Option, Please Enter A Valid One");
                                    break;
                            }
                        }
                        break;
                    case "5":
                        program = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Option, Please Enter A Valid One");
                        break;
                }
            }
        }
    }
}
