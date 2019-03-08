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

        public static CancellationTokenSource cts = new CancellationTokenSource();

        public static bool IsPrime(int n, CancellationToken ct)
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
                else ct.ThrowIfCancellationRequested();
            }

            // Throw exception here
            throw new Exception(string.Format("{0} is not evaluated", n));

            return true;
        }

        public static List<int> Task1()
        {
            try
            {

                var primes = from item in ParallelEnumerable.Range(lowTest, totalTest)
                            .WithCancellation(cts.Token)
                             where IsPrime(item, cts.Token)
                             select item;

                return primes.ToList();
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
                return new List<int>();
            }

        }

        public static List<int> Task3()
        {
            var primes = from item in ParallelEnumerable.Range(lowTest, totalTest)
                         .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                         where IsPrime(item, cts.Token)
                         select item;

            return primes.ToList();
        }


        public static void Task4()
        {
            var primes = from item in ParallelEnumerable.Range(lowTest, totalTest)
                            .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                         where IsPrime(item, cts.Token)
                         select item;

            int qty = 0;
            foreach (int prime in primes)
            {
                Console.WriteLine(prime);
                if (qty++ > 50) break;
            }

        }

        public static List<int> Task5()
        {
            try
            {
                var primes =
                    from item in ParallelEnumerable.Range(lowTest, totalTest)
                    where IsPrime(item, cts.Token)
                    select item;

                return primes.ToList();
            }
            catch (AggregateException e)
            {
                foreach (Exception ex in e.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return new List<int>();
        }

    }
}
