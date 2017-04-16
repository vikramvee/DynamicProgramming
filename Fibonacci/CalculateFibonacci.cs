using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    public class CalculateFibonacci
    {
        public static long Fibonacci(long n)
        {
            if (n <= 1)
                return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        static long[] cache = new long[200];
        public static long FibonacciUsingCache(long n)
        {
            if (n <= 1)
                cache[n] = 1;
            if (cache[n] == 0)
                cache[n] = FibonacciUsingCache(n - 1) + FibonacciUsingCache(n - 2);
            return cache[n];

        }
    }
}
