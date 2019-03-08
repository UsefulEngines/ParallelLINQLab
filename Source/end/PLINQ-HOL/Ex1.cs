using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HOL
{
    public class Ex1
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
            return true;
        }

        public static List<int> Task1_LINQ()
        {
            var primes = from item in Enumerable.Range(lowTest, totalTest)
                         where IsPrime(item)
                         select item;

            return primes.ToList();
        }

        public static List<int> Task2_PLINQ()
        {
            var primes = from item in ParallelEnumerable.Range(lowTest, totalTest)
                         where IsPrime(item)
                         select item;

            return primes.ToList();
        }

        public static List<int> Task3()
        {
            List<int> primes = new List<int>();
            int complete = 0;
            AutoResetEvent wait = new AutoResetEvent(false);

            for (int i = lowTest; i < highTest; i++)
            {
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    if (IsPrime((int)o))
                        lock (primes) { primes.Add((int)o); }

                    if (Interlocked.Increment(ref complete) == totalTest)
                        wait.Set();
                }, i);
            }

            wait.WaitOne();
            return primes;
        }

    }
}
