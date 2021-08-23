using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Demo1
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("Hello World!");
           File.OpenText("");
           List<int> list = new List<int>();
           list.Add(1);
           foreach (var i in list.GroupBy(s=>s))
           {
             
           }

           var ss= list.GroupBy(s => s).Select(s => s);
        }
    }
}