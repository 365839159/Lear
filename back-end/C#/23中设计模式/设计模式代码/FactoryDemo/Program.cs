using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             *  需求：
                请使用任意面向对象编程语言实现计算器控制台应用程序
             */

            #region 初学者实现
            //Console.WriteLine("请输入数字A");
            //double A = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("请输入操作符（+ - * /）");
            //string B = Console.ReadLine();
            //Console.WriteLine("请输入数字B");
            //double C = Convert.ToInt32(Console.ReadLine());

            //double result = 0;
            //if (B == "+")
            //{
            //    result = A + C;
            //}
            //if (B == "-")
            //{
            //    result = A - C;
            //}
            //if (B == "*")
            //{
            //    result = A * C;
            //}
            //if (B == "/")
            //{
            //    result = A / C;
            //}
            //Console.WriteLine($"{A}{B}{C}=" + result);
            #endregion


            #region 规范实现

            //Console.WriteLine("请输入数字A");
            //string numberA = Console.ReadLine();
            //Console.WriteLine("请输入操作符（+ - * /）");
            //string operate = Console.ReadLine();
            //Console.WriteLine("请输入数字B");
            //string numberB = Console.ReadLine();

            //string[] operates = new string[] { "+", "-", "*", "/" };
            //if (!operates.Contains(operate))
            //{
            //    throw new Exception("操作符输入有误");
            //}
            //double numA = 0;
            //double numB = 0;
            //if (!double.TryParse(numberA, out numA))
            //{
            //    throw new Exception("数字A输入有误");
            //}
            //if (!double.TryParse(numberB, out numB))
            //{
            //    throw new Exception("数字B输入有误");
            //}

            //double result1 = 0;
            //switch (operate)
            //{
            //    case "+":
            //        result1 = numA + numB;
            //        break;
            //    case "-":
            //        result1 = numA - numB;
            //        break;
            //    case "*":
            //        result1 = numA * numB;
            //        break;
            //    case "/":
            //        if (numB == 0)
            //        {
            //            throw new Exception("除数不能为0");
            //        }
            //        result1 = numA / numB;
            //        break;
            //    default: throw new Exception("啥也不是!");
            //}

            //Console.WriteLine($"{ numA}{ operate}{ numB}={result1}");
            #endregion


            #region 高复用、可扩展、简单工厂
            Console.WriteLine("请输入数字A");
            string numberA1 = Console.ReadLine();
            Console.WriteLine("请输入操作符（+ - * /）");
            string operate1 = Console.ReadLine();
            Console.WriteLine("请输入数字B");
            string numberB1 = Console.ReadLine();

            double numA1 = 0;
            double numB1 = 0;
            if (!double.TryParse(numberA1, out numA1))
            {
                throw new Exception("数字A输入有误");
            }
            if (!double.TryParse(numberB1, out numB1))
            {
                throw new Exception("数字B输入有误");
            }

            Operation operation = Factory.CreatOperation(operate1);
            operation.numA = numA1;
            operation.numB = numB1;
            double result = operation.GetResult();
            Console.WriteLine($"{numA1}{operate1}{numB1}={result}");
            #endregion



            Console.ReadKey();

        }
    }
    /// <summary>
    /// 父类
    /// </summary>
    class Operation
    {
        public double numA { get; set; }
        public double numB { get; set; }

        public virtual double GetResult()
        {
            double result = 0;
            return result;
        }
    }

    /// <summary>
    /// 加法运算
    /// </summary>
    class AddOperation : Operation
    {
        public override double GetResult()
        {

            return numA + numB;
        }
    }

    /// <summary>
    /// 减法运算
    /// </summary>
    class SubOperation : Operation
    {
        public override double GetResult()
        {
            return numA - numB;
        }
    }
    /// <summary>
    /// 乘法运算
    /// </summary>
    class MulOperation : Operation
    {
        public override double GetResult()
        {
            return numB * numA;
        }
    }

    /// <summary>
    /// 除法运算
    /// </summary>
    class DivOperation : Operation
    {
        public override double GetResult()
        {
            if (numB == 0)
            {
                throw new Exception("除数不能为0");
            }
            return numA / numB;
        }
    }

    /// <summary>
    /// 工厂类
    /// </summary>
    class Factory
    {
        public static Operation CreatOperation(string operation)
        {
            Operation operation1 = null;
            switch (operation)
            {
                case "+":
                    operation1 = new AddOperation();
                    break;
                case "-":
                    operation1 = new SubOperation();
                    break;
                case "*":
                    operation1 = new MulOperation();
                    break;
                case "/":
                    operation1 = new DivOperation();
                    break;
                default:
                    throw new Exception("不存在得操作符!");
            }
            return operation1;
        }
    }

}
