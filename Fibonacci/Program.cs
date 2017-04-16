using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            var num = 150;
            long fibo = CalculateFibonacci.FibonacciUsingCache(num);
            Console.WriteLine(string.Format("Fibonacci({0}): {1}", num, fibo));

            Console.ReadLine();
        }

      

    }
}
