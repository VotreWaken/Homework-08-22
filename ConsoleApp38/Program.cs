using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp38
{
    internal class Program
    {
        static void Main(string[] args)
        {

        List<object> collection = new List<object>
        {
            42, "Hello, World!", DateTime.Now, 3.14
        };

            Thread displayThread = new Thread(() => DisplayElements(collection));
            displayThread.Start();

            displayThread.Join();

            Console.WriteLine("Done");
        }

        static void DisplayElements(List<object> collection)
        {
            foreach (var element in collection)
            {
                string result = element.ToString();
                Console.WriteLine("Result: " + result);
            }
        }
    }
}
