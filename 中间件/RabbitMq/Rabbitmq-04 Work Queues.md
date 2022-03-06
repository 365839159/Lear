# 工作队列（Work Queues）

![image-20220304114431919](image-20220304114431919.png)

> 场景：假设生产者向队列中添加一条数据的时间为1秒，消费者从队列中消费一条数据执行完业务逻辑需要5秒，在这种情况下队列就会不断堆积最终导致服务瘫痪。
>
> 解决方案：运行多个消费者，同时消费队列中的任务

## 生产者

> 定义一个task_Queue队列；1秒向队列中发送一条消息

~~~C#
var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://admin:admin@192.168.65.133:5672"),
    AutomaticRecoveryEnabled = true
};
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "task_queue",
                         durable: true,//可持久化
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);
    int count = 0;
    while (true)
    {
        count++;
        var message = $"Task {count}";
        var body = Encoding.UTF8.GetBytes(message);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(exchange: "",
                             routingKey: "task_queue",
                             basicProperties: properties,
                             body: body);
        Console.WriteLine("Send {0}", message);
        //暂停一秒
        Task.Delay(1000).Wait();
    }
}
~~~



## 消费者

> 定义一个task_Queue队列；5秒消费一条消息

~~~C#
var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://admin:admin@192.168.65.133:5672"),
    AutomaticRecoveryEnabled = true
};
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "task_queue",
                         durable: true,//可持久化
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

    Console.WriteLine("Waiting for messages.");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (sender, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine("Received {0}", message);
        Task.Delay(5000).Wait();  //等待5秒
        Console.WriteLine("Task Done");

        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);//手动确认
    };
    channel.BasicConsume(queue: "task_queue",
                         autoAck: false,//关闭自动确认
                         consumer: consumer);
    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}
~~~

## 测试

> 启动5个消费者

![image-20220304130826599](image-20220304130826599.png)

> 可视化界面中查看连接数为5个

![image-20220304131030524](image-20220304131030524.png)

> 启动1个生产者

![image-20220304131150907](image-20220304131150907.png)

> 可视化界面连接多了1个生产者

![image-20220304131827909](image-20220304131827909.png)

> 测试结果
>
> 5个消费者一起消费队列中的任务。

![image-20220304131559133](image-20220304131559133.png)

> 运行一段时间后 rabbitmq 队列中 也没有过多的堆积任务

![image-20220304131734017](image-20220304131734017.png)

