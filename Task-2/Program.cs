
namespace Task_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank1 = new Bank();
            Bank bank2 = new Bank("Ahmed", "14578123471587", 01274828887, "cairo", 4012, 1500);
            Bank bank3 = new Bank("Ali", "47812479324150", 01074820087, "Nasr city", 8574);

            bank1.showAccountDetails();
            Console.WriteLine("===============================");
            bank2.showAccountDetails();
            Console.WriteLine("===============================");
            bank3.showAccountDetails();
            Console.WriteLine("===============================");
        }
    }
}
