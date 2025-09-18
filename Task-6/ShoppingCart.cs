using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class ShoppingCart
    {
        public List<string> items;
        public Dictionary<string, int> cart;
        public HashSet<float> shops;

        public ShoppingCart()
        {
            items = new List<string>();
            cart = new Dictionary<string, int>();
            shops = new HashSet<float>();
        }

        
    }
}
