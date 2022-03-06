# namespace 

System.Linq

# Enumerable 类

https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable?view=net-6.0

# 语言集成查询 (LINQ)

https://docs.microsoft.com/zh-cn/dotnet/csharp/linq/

# 使用语言集成查询 (LINQ)

https://docs.microsoft.com/zh-cn/dotnet/csharp/tutorials/working-with-linq

# LINQ to SQL 可执行的操作

https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/sql/linq/what-you-can-do-with-linq-to-sql

# LINQ to XML

https://docs.microsoft.com/zh-cn/dotnet/standard/linq/linq-xml-overview

# LINQ to Entities

https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/ef/language-reference/linq-to-entities

# LINQ to Objects

https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/linq/linq-to-objects

~~~C#
//官方
int[] nums = new int[] {1, 2, 3, 4, 5};


var result = nums.Where(x => x % 2 == 0);
foreach (var item in result)
{
    Console.WriteLine(item);
}
~~~

~~~C#
//自己封装的

#region 全部返回

var result1 = nums.MyWhere1(s => s % 2 == 0);
foreach (var item in result1)
{
    Console.WriteLine(item);
}

#endregion

#region yeild返回

var result2 = nums.MyWhere2(s => s % 2 == 0);
foreach (var item in result2)
{
    Console.WriteLine(item);
}

#endregion


static class ArrayExtend
{
    //全部返回
    public static IEnumerable<T> MyWhere1<T>(this IEnumerable<T> items, Func<T, bool> func)
    {
        if (items == null)
        {
            throw new NullReferenceException("items");
        }

        if (func == null)
        {
            throw new NullReferenceException("func");
        }

        List<T> result = new List<T>();
        foreach (var item in items)
        {
            if (func.Invoke(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    //yield 返回
    public static IEnumerable<T> MyWhere2<T>(this IEnumerable<T> items, Func<T, bool> func)
    {
        if (items == null)
        {
            throw new NullReferenceException("items");
        }

        if (func == null)
        {
            throw new NullReferenceException("func");
        }

        foreach (var item in items)
        {
            if (func.Invoke(item))
            {
                yield return item;
            }
        }
    }
}
~~~

# 常用方法

~~~c#
int[] nums = new int[] {1, 2, 3, 4, 5};

//满足元素大于1的返回
var result = nums.Where(s => s > 1);

//满足元素大于1的数量
var result1 = nums.Count(s => s > 1);

//判断是否有满足元素大于1的
var result2 = nums.Any(s => s > 1);

//有且只有一个元素等于1 多余或少于1个会抛异常
var result3 = nums.Single(s => s == 1);

//有且只有一个元素等于1或者没有 多余1个会抛异常
var result4 = nums.SingleOrDefault(s => s > 100);

//取符合条件的第一个元素，没有会抛异常
var result5 = nums.First(s => s > 1);

//取符合条件的第一个元素没有返回默认值
var result6 = nums.FirstOrDefault(s => s > 100);

//排序 正序
var result7 = nums.OrderBy(s => s);

//排序 倒序
var result8 = nums.OrderByDescending(s => s);

//排序 参数为表达式
var result9 = nums.OrderByDescending(s => s is > 1 and < 3);

//多排序规则 先根据 s is > 1 and < 3 本身排序 在根据本身+Guid排序
var result10 = nums.OrderByDescending(s => s is > 1 and < 3).ThenBy(s => s.ToString() + Guid.NewGuid().ToString());

//跳过多少条数据取多少条数据
var result11 = nums.Skip(2).Take(1);

//获取最大值
var result12 = nums.Max(s => s is > 1 and < 11);

//获取最小值
var result13 = nums.Min(s => s is > 1 and < 11);

//相加
var result14 = nums.Sum();

//平均
var result15 = nums.Average();

//分组
var result16 = nums.GroupBy(s => s);
foreach (var item in result16)
{
    foreach (var e in item)
    {
        Console.WriteLine(e);
    }
}

//投影
var result17 = nums.Select(s => new {age = s});

~~~

![image-20220218100941258](C:\Users\xian_cheng\Desktop\Learm\Node\Lear\DOTNET\KnowledgePoints\Linq\image-20220218100941258.png)

