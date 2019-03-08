using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HOL
{
    public class Ex2
    {
        static int lowTest = 5000000,
            highTest = 10000000,
            totalTest = highTest - lowTest;

        public static bool IsPrime(int n)
        {
            // zero, one, and negative numbers are not prime
            if (n < 2) return false;
            // 2 is the only even prime
            else if (n == 2) return true;
            // Check for evens
            else if ((n & 1) == 0) return false;

            double sq = Math.Sqrt(n);
            for (int i = 3; i <= sq; i += 2)
            {
                if (n % i == 0) return false;
            }

            // Throw exception here

            return true;
        }

        public static List<int> Task1()
        {
            var primes = from item in
                             ParallelEnumerable.Range(lowTest, totalTest)
                         where IsPrime(item)
                         select item;

            return primes.ToList();
        }
    }
}
