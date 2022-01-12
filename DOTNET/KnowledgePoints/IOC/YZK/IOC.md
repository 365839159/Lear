# 概念

**控制反转**（Inversion of Control，缩写为**IoC**），是[面向对象编程](https://baike.baidu.com/item/面向对象编程/254878)中的一种设计原则，可以用来减低计算机[代码](https://baike.baidu.com/item/代码/86048)之间的[耦合度](https://baike.baidu.com/item/耦合度/2603938)。

最常见IOC思想的实现方式叫做**[依赖注入](https://baike.baidu.com/item/依赖注入/5177233)**（Dependency Injection，简称**DI**），还有一种方式叫“依赖查找”（Dependency Lookup），也叫服务定位器（serviceLocator）。

>怎么创建 xxxx 对象（自己new）-----> 我要 xxxx 对象（服务定位器）---->用（DI）



## 自己创建对象

缺点：自己需要清楚这个对象是干嘛的，比如是一个连接数据库对象，连接的sqlserver ，如果有一天换成mysql就需要在每个创建对象的地方改一遍。

~~~c#
var obj =new SqlServer();
~~~

## 服务定位器

~~~c#
var obj =serviceLocator.GetService<SqlServer>();
~~~

## DI

将实例注册到容器中

~~~C#
class A{

private ISQL _obj;//接口

public A(ISQL obj){ 
​		_obj=obj
​	}

}
~~~

## 服务

服务对象

## 服务容器

负责管理注册的服务

## 查询服务

创建对象及关联对象

 ## 对象生命周期

Transient（）

Scoped

Singleton