namespace Task_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CurrentAccount Account1 = new CurrentAccount("Ahmed", "47814759874152", 01157984102,"Nasr city",57823014, (decimal)5.2);
            SavingAcoount Account2 = new SavingAcoount("Gamal", "58746921304782", 01258749230, "Cairo", 87214632, 5000,(decimal)15.5);

            List<BankAccount> Accounts = new List<BankAccount>();

            Accounts.Add(Account1);
            Accounts.Add(Account2);

            foreach (BankAccount Ba in Accounts)
            {
                Ba.showAccountDetails();
                Console.WriteLine($"Interest is: {Ba.CalculateInterest()} EGP");
                Console.WriteLine("============================================");
            }
        }
    }
}
