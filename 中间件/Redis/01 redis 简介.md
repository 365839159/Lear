[TOC]

# Redis

## redis 简介

一个高性能的 key-value 数据库

## redis 优势

性能高：读110000/s ；写81000/s

数据类型丰富：string 、list 、hashe 、set 、 order set

原子性

特性多：通知、key 过期等等

## redis  与其他 key-value 存储有什么不同

- Redis支持数据的持久化，可以将内存中的数据保持在磁盘中，重启的时候可以再次加载进行使用。
- Redis不仅仅支持简单的key-value类型的数据，同时还提供list，set，zset，hash等数据结构的存储。
- Redis支持数据的备份，即master-slave模式的数据备份。



# 登录问题

> 内存session 这种 分发的服务不同将使用不了

~~~mermaid
graph LR
A[客户端] 
A --> B[nginx]
B-->C[web api 1]
B-->D[web api 2]
C-->E[Session]
~~~



> 可以使用Redis

~~~mermaid
graph LR
A[客户端] 
A --> B[nginx]
B-->C[web api 1]
B-->D[web api 2]
C-->E[redis]
D-->E
~~~

# 数据类型

> 常用

String



Hash

List

Set

Zset

> 不常用

BitMaps

HyperLogLoss

Streams

# 







