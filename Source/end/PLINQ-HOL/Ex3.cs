using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HOL
{
    public class Ex3
    {
        public static void Task1()
        {
            var values = from num in Enumerable.Range(1, 100).WithSampling(5).AsParallel()
                         where num < 50
                         select num;

            foreach (int val in values)
                Console.WriteLine(val);
        }

    }
}
