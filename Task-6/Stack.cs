using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class Stack<T>
    {
        List<T> stack;
        public Stack()
        {
            stack = new List<T>();
        }
        public void push(T value)
        {
            stack.Add(value);
        }

        public T pop()
        {
            T st = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            return st;
        }

        public T peek()
        {
            return stack[stack.Count - 1];
        }
    }
}
