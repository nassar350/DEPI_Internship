using System.Security.Cryptography.X509Certificates;

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
            //Cache<string,int> cache = new Cache<string,int>();
            //cache.Add("Ali", 150);
            //cache.Add("Ahmed", 200);

            //Console.WriteLine(cache.Get("Ahmed"));
            //Console.WriteLine(cache.Get("Ali"));



            // dictionary to store name-phone pair (add - remove - search)
            //Contacts contact = new Contacts();
            //contact.add("ali", "1230");
            //contact.add("ahmed", "4587");
            //Console.WriteLine(contact.search("ahmed"));
            //contact.remove("ali");
            //Console.WriteLine(contact.search("ali"));



            // shopping cart list->items dict->quantities set->discounts
            //ShoppingCart cart = new ShoppingCart();
            //cart.items.Add("tea");
            //cart.cart.Add(cart.items[0],2);
            //cart.shops.Add(0.05f);



            // method calculate average of nullable integers handle nulls
            //static decimal? avg(List<int?> nums)
            //{
            //    decimal? sum = 0;
            //    int count = 0;
            //    foreach(var i in nums)
            //    {
            //        if (i.HasValue)
            //        {
            //            sum += i;
            //            count++;
            //        }
            //    }
            //    if (sum.HasValue)
            //        return (sum/count);
            //    else return 0;
            //}
            //Console.WriteLine(avg([1,2,3,5,null,6,3]));



            // person class with nullable properties (middlename-dateofbirth) - safe string representation
            //Person person = new Person();
            //person.fName = "ali";
            //person.LName = "Mohamed";
            //person.Mname = "Ahmed";
            //person.DateofBirth = DateTime.Now;
            //Console.WriteLine(person);



            // 

        }
    }
}
