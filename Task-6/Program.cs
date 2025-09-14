namespace Task_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // phoneBook
            //PhoneBook phoneBook = new PhoneBook();
            //phoneBook["ali"] = "123";
            //Console.WriteLine(phoneBook["ali"]);


            // schedule
            //WeeklySchedule schedule = new WeeklySchedule();
            //schedule["sunday"] = "Depi";
            //schedule["Monday"] = "Depi";
            //Console.WriteLine(schedule["sunday"]);
            //Console.WriteLine(schedule["tuesday"]);



            // matrix
            //Matrix mat = new Matrix(5,5);
            //mat[0,0] = 1;
            //mat[1,0] = 2;
            //Console.WriteLine(mat[1,0]);
            //Console.WriteLine(mat[2,0]);



            // stack
            //Stack<int> stack = new Stack<int>();
            //Stack<string> stack1 = new Stack<string>();

            //stack.push(1);
            //stack.push(2);

            //stack1.push("Ali");
            //stack1.push("Mohmaed");

            //Console.WriteLine(stack.pop());
            //Console.WriteLine(stack.peek());

            //Console.WriteLine(stack1.pop());
            //Console.WriteLine(stack.peek());



            // Pair
            //Pair<string, int> degree = new Pair<string, int>("Mohamed", 100);
            //Console.WriteLine(degree.ToString());

            //Pair<int, bool> good = new Pair<int, bool>(48, false);
            //Console.WriteLine(good.ToString());



            // Cache
            Cache<string,int> cache = new Cache<string,int>();
            cache.Add("Ali", 150);
            cache.Add("Ahmed", 200);

            Console.WriteLine(cache.Get("Ahmed"));
            Console.WriteLine(cache.Get("Ali"));


        }
    }
}
