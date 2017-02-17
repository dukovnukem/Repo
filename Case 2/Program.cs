using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChoosePairs
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTest();
            Console.ReadLine();
        }

        private static void RunTest()
        {
            const int X = 7;
            var testArrayX = new[] { -3, 1, 6, 9, 10, 7, 2, 5, 0, -2 };

            const int Y = 98;
            var testArrayY = new[] { -23, 17, 121, 6, 9, 95, 96, 7, 2, 5, 3, 108, -10, 578, 56, -480};

            var pairsX = CPairSelector.Select(testArrayX, X);
            var pairsY = CPairSelector.Select(testArrayY, Y);

            ShowResults(X, testArrayX, pairsX);
            ShowResults(Y, testArrayY, pairsY);
        }

        private static void ShowResults(Int32 x, Int32[] array, IEnumerable<Tuple<Int32, Int32>> pairs)
        {
            StringBuilder output = new StringBuilder();
            output.AppendFormat("X = {0:D}{1}", x, Environment.NewLine);
            output.Append(@"Array: { ");

            for(int i = 0; i < array.Length; i++)                
                output.AppendFormat("{0} {1}", array[i], i != array.Length - 1 ? @"," : String.Empty);

            output.AppendLine(@"}");

            output.AppendLine("Pairs:");
            output.AppendLine(@"{");
            
            foreach (var item in pairs)
                output.AppendFormat(" {0:D}, {1:D}{2}", item.Item1, item.Item2, Environment.NewLine);
            
            output.AppendLine(@"}");

            Console.WriteLine(output);
        }
    }
}
