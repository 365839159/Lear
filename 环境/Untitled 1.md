# 客户端，作用域和用户仓库的缓存

[![img](https://cdn2.jianshu.io/assets/default_avatar/11-4d7c6ca89f439111aff57b23be1c73ba.jpg)](https://www.jianshu.com/u/90adb519eab6)

[灭蒙鸟](https://www.jianshu.com/u/90adb519eab6)关注

2017.02.25 18:00:43字数 468阅读 536

------

## layout: docs-default

# 客户端，作用域和用户仓库的缓存

IdentityServer有几种方法从数据库中读取数据到内存中。由于IdentityServer的解耦设计，同一个HTTP请求，可能会导致好几次装载数据。这会导致好几次数据库读取动作。由于这种可能性，IdentityServer设计了一个缓存接口，我们可以实现自己的缓存机制。IdentityServer也提供了一个默认的缓存实现。

## 默认缓存

IdentityServer提供的缓存是一个内存缓存，缓存的数据有指定的生存期。默认缓存（真的很简单)可以解决大部分性能问题。配置默认缓存，需要使用下面`IdentityServerServiceFactory`的扩展方法:

- `ConfigureClientStoreCache`
- `ConfigureScopeStoreCache`
- `ConfigureUserServiceCache`
  每个方法都有一些过载，可以无参数调用(默认过期时间5分钟),或者`TimeSpan`参数来指定过期时间。具体用法如下:



```csharp
var factory = InMemoryFactory.Create(
    users:   Users.Get(),
    clients: Clients.Get(),
    scopes:  Scopes.Get());

factory.ConfigureClientStoreCache();
factory.ConfigureScopeStoreCache();
factory.ConfigureUserServiceCache(TimeSpan.FromMinutes(1));
```

这些方法需要在设置合适的存储到`IdentityServerServiceFactory`后调用,他们实际上使用decorator模式包装了实际的存储实现.

## 自定义缓存

如果需要自定义实现缓存(比如Redis),那么要缓存的数据需要实现`ICache<T>`，这个缓存接口定义了下面的API:
If a custom cache impelmentation is desired (e.g. using reddis), then you can implement the `ICache<T>` for the data that needs to be cached. The cache interface defines this API:

- ```
  Task SetAsync(string key, T item)
  ```

  - `key` 和 `item` 会被缓存.

- ```
  Task<T> GetAsync(string key)
  ```

  - `key`用来从缓存获取数据，如果缓存中没有，那么返回`null`.

上面描述的`IdentityServerServiceFactory` 扩展方法也有支持`Registration`的过载方法，来注册自定义的缓存服务:

- `ConfigureClientStoreCache(Registration<ICache<Client>> cacheRegistration)`
- `ConfigureScopeStoreCache(Registration<ICache<IEnumerable<Scope>>> cacheRegistration)`
- `ConfigureUserServiceCache(Registration<ICache<IEnumerable<Claim>>> cacheRegistration)`