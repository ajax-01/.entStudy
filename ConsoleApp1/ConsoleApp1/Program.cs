using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Test(30));
        }

        public static int Test(int count, int last = 1, int next = 1, int count2 = 2)
        {
            if (count2 == count)
            {
                return next;
            }

            return Test(count, next, last + next, ++count2);
        }
    }
}