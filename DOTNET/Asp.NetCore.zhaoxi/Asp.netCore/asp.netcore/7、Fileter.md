[TOC]

# Asp.net Core 的Aop 支持

作用：动态的增加功能；

## ActionFilter

围绕方法，可以在方法之前，方法之后扩展功能；

### 使用

```
定义一个类继承 ActionFilterAttribute / IActionFilter
```

```
重写父类方法
```

```
标记需要的控制器或方法上
```

### 执行顺序

OnActionExecutionAsync   =>  OnActionExecting    =>

​		Action的执行

 =>   OnActionExecuted   =>  OnResultExecuting    

=>   OnResultExecutionAsync  =>  OnResultExecuting  =>  OnResultExecuted

### 依赖注入

```
1、TypeFilter  ====》直接使用
```

```
2、ServiceFilter   ====》需要注册服务
```

```
//3.全局注册，对项目中的所有Action都生效----且全局注册也是支持依赖注入的；
services.AddControllersWithViews(option=> {
        option.Filters.Add<CustomActionFilterNewAttribute>();
});
```

### 使用场景

日志等



----



## ResultFilter

