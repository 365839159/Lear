using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletenDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                Singleten1 singleten1 = Singleten1.CreateInstance();
                singleten1.Show();
            }

            Console.ReadKey();
        }
    }

    public class Singleten1
    {
        /************************************************************
         * 单线程
         *1、私有化构造函数
         *2、私有静态变量
         *3、共有访问入口
         ************************************************************
         */
        private static Singleten1 Singleten = null;
        private Singleten1()
        {
            Console.WriteLine(this.GetType()+"被构造！");
        }

        public static Singleten1 CreateInstance()
        {
            if (Singleten is null)
                Singleten = new Singleten1();
            return Singleten;
        }

        public void Show()
        {
            Console.WriteLine(this.GetType() + ".show()");
        }
    }



}
