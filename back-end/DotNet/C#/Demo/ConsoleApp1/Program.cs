using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (true)
            {
                Console.WriteLine("Hello World!");
                List<int> list = new List<int> { };
                foreach (var i in list.GroupBy(s => s))
                {
                }
                Console.WriteLine();
                foreach (int item in list)
                {

                }
                
                string s = "zxc";
                if (s == null)
                    throw new ArgumentNullException(nameof(s));
            }


        }

        public int GetInt(string str)
        {
            return Convert.ToInt32(str);
        }

        public int Foo(string input)
        {
            int sss = 0;
            return 0;
        }


    }

    class person
    {
        public person()
        {
            string d = null;
        }

        public int ss { get; set; }
        public int sss { get; set; }
    }
}