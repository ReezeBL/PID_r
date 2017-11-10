using System;
using System.Collections.Generic;
using System.Windows;
using PID_r.GUI;
using PID_r.Helpers;

namespace PID_r
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            var application = new Application();
            var mainForm = new MainWindow();
            application.Run(mainForm);

            //var test = new List<List<int>> {new List<int> {1, 2, 3, 4}, new List<int> {1, 2, 3}, new List<int>{1, 2, 3}};
            //do
            //{
            //    foreach (var c in test)
            //    {
            //        Console.WriteLine(string.Join(" ", c));
            //    }
            //    Console.WriteLine();
            //} while (CombHelper.NextMultyPermutation(test));
            //const int iterations = 10;
            //GC.Collect();
            //var sw = Stopwatch.StartNew();
            //for (var i = 0; i < iterations; i++)
            //{
            //    while (CombHelper.NextPermuation(test))
            //    {
            //    }
            //}
            //sw.Stop();
            //Console.WriteLine(((double)sw.ElapsedMilliseconds/ iterations).ToString(CultureInfo.InvariantCulture));
        }
    }
}
