using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoopTest
{
    class Program
    {
        private static string _option = "1";

        private static void Method1()
        {
            for (var i=0; i<100000;i++)
            {

            }
            Console.WriteLine("Method 1");
        }

        private static void Method2()
        {
            for (var i = 0; i < 100000; i++)
            {

            }
            Console.WriteLine("Method 2");
        }
        private static void Method3()
        {
            for (var i = 0; i < 100000; i++)
            {

            }
            Console.WriteLine("Method 3");
        }

        private static void Method4()
        {
            for (var i = 0; i < 100000; i++)
            {

            }
            Console.WriteLine("Method 4");
        }

        private static void Method5()
        {
            for (var i = 0; i < 100000; i++)
            {

            }
            Console.WriteLine("Method 5");
        }
        private static void Method6()
        {
            for (var i = 0; i < 100000; i++)
            {

            }
            Console.WriteLine("Method 6");
        }
        private static void Loop()
        {
            var rand = new Random();
            if (_option == "1")
            {
                while (true)
                {
                    var r = rand.Next(100);
                    if (r > 66)
                    {
                        Method1();
                    }
                    else if (r > 33)
                    {
                        Method2();
                    }
                    else
                    {
                        Method3();
                    }
                }
            }
            else
            {
                while (true)
                {
                    var r = rand.Next(100);
                    if (r > 66)
                    {
                        Method4();
                    }
                    else if (r > 33)
                    {
                        Method5();
                    }
                    else
                    {
                        Method6();
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Choose option 1 or 2");

            _option = Console.ReadLine();

            var thread = new System.Threading.Thread(new System.Threading.ThreadStart(Loop));
            thread.Start();
            Console.ReadKey();

        }
    }
}
