using System.Threading.Channels;

namespace SimpleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long a, b, res;
            char? c = null;
            Console.WriteLine("Hello!");

            Console.Write("Input the first number: ");
            a = Convert.ToInt64(Console.ReadLine());
            Console.Write("Input the second number: ");
            b = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("What do you want to do with those numbers?");
            Console.WriteLine("[A]dd");
            Console.WriteLine("[S]ubtract");
            Console.WriteLine("[M]ultiply");

            Console.Write("Enter Your Choice as a Char: ");
            string str = Console.ReadLine();

            if (str != "")
            {
                c = Convert.ToChar(str);
            }

            if (c == 'a' || c == 'A')
            {
                res = a + b;
                Console.WriteLine($"Your Result is: {res}");
            }
            else if (c == 's' || c == 'S')
            {
                res = a - b;
                Console.WriteLine($"Your Result is: {res}");
            }
            else if (c == 'm' || c == 'M')
            {
                res = a * b;
                Console.WriteLine($"Your Result is: {res}");
            }
            else
            {
                Console.WriteLine("Not A Valid Operation");
            }
        }
    }
}
