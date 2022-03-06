## hello world

### 生产者

![image-20220301081221496](image-20220301081221496.png)

> 1、创建控制台项目



> 2、构建连接参数

~~~C#
//创建连接的工厂（指定连接参数）
var factory = new ConnectionFactory()
{
    HostName = "192.168.65.133",
    UserName = "admin",
    Password = "admin",
    Port = 5672,
    AutomaticRecoveryEnabled = true
};
~~~

或者

~~~C#
var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://admin:admin@192.168.65.133:5672"),
    AutomaticRecoveryEnabled = true
};
~~~



> 3、创建服务器连接和传输通道

~~~C#
//通过连接工厂创建连接
using (var connection = factory.CreateConnection())
//创建信道
using (var channel = connection.CreateModel())
{
    //...........
}
~~~



> 4、在通道里定义一个队列；然后就可以向队列中发送消息

~~~C#
//创建连接的工厂（指定连接参数）
var factory = new ConnectionFactory()
{
    HostName = "192.168.65.133",
    UserName = "admin",
    Password = "admin",
    Port = 5672
};

//通过连接工厂创建连接
using (var connection = factory.CreateConnection())
    
//创建信道
using (var channel = connection.CreateModel())
{
    //定义队列
    channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);
    
    Console.WriteLine("生产者准备就绪");
    Console.WriteLine("请输入要发送的内容！");
    
    string content;
    while ((content = Console.ReadLine()) != "q")
    {
        //将消息转换为二进制数据
        string message = content;
        var body = Encoding.UTF8.GetBytes(message);
        //发布
        channel.BasicPublish(exchange: "",
                             routingKey: "hello",
                             basicProperties: null,
                             body: body);
        Console.WriteLine($" {DateTime.Now}:");
        Console.WriteLine($" Send: {message}");
        Console.WriteLine("请输入要发送的内容！");
        
    }
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
~~~



> 启动

![image-20220303132601427](image-20220303132601427.png)



> 可视化界面中 Queues 中已经有一条数据了

![image-20220303132716410](image-20220303132716410.png)



-----



### 消费者

![image-20220303133310622](image-20220303133310622.png)

> 1、大概和生产者雷同，打开连接和通道声明消费的队列。
>
> 注意：与生产者发布消息的队列保持一致。

~~~C#
//创建连接的工厂（指定连接参数）
var factory = new ConnectionFactory()
{
    HostName = "192.168.65.133",
    UserName = "admin",
    Password = "admin",
    Port = 5672
};
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    //定义消费队列
    channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

	//........................
}
~~~



>2、服务器从队列中传递消息给消费者。由于它会异步向我们推送消息，因此我们提供了一个回调
>
>使用  EventingBasicConsumer.Received 事件处理

~~~C#
//创建连接的工厂（指定连接参数）
var factory = new ConnectionFactory()
{
    HostName = "192.168.65.133",
    UserName = "admin",
    Password = "admin",
    Port = 5672
};
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    //回调事件
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] Received {0}", message);
    };
    //消费
    channel.BasicConsume(queue: "hello",
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}
~~~



> 启动

![image-20220303150357556](image-20220303150357556.png)



> 可视化界面的数据已被消费

![image-20220303150429486](image-20220303150429486.png)





























