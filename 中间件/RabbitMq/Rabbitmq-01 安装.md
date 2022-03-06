# 介绍

![image-20220228133050731](image-20220228133050731.png)

> RabbitMQ : 消息代理，接受和转发消息
>
> 消息格式：二进制数据块



>行话
>
>生产者：发送消息的程序
>
>队列：存放消息的缓冲区
>
>消费者：接受消息的程序



> 在下图中，“P”是我们的生产者，“C”是我们的消费者。中间的框是一个队列 RabbitMQ 代表消费者保存的消息缓冲区

![image-20220301080855482](image-20220301080855482.png)



----



# Centos7 单机安装 RabbitMQ

> 创建存放 rabbitmq 下载文件的文件夹

~~~shell
# 创建一个新目录
mkdir /usr/rabbitmq

# 进入该目录
cd /usr/rabbitmq
~~~

> 安装 erlang 
>
> 官网：https://www.erlang-solutions.com/downloads/
>
> rabbitmq 是高并发语言 erlang 开发所以运行 rabbitmq 需要先安装 erlang 

~~~shell
# 下载 erlang 安装包
wget https://packages.erlang-solutions.com/erlang-solutions-2.0-1.noarch.rpm

# 解压
rpm -Uvh erlang-solutions-2.0-1.noarch.rpm

# 安装
sudo yum install erlang -y

~~~

> 安装 socat 

~~~shell
yum install -y socat
~~~

> 安装 rabbitmq
>
> 官网：https://www.rabbitmq.com/download.html

~~~shell
# 下载 rabbitmq
wget https://github.com/rabbitmq/rabbitmq-server/releases/download/v3.9.13/rabbitmq-server-3.9.13-1.el7.noarch.rpm

#解压
rpm -Uvh rabbitmq-server-3.9.13-1.el7.noarch.rpm

#安装
yum install -y rabbitmq-server

# 启动
systemctl start rabbitmq-server

# 查看状态（active（running））
systemctl status rabbitmq-server

~~~

> 安装可视化插件

~~~shell
rabbitmq-plugins enable  rabbitmq_management
~~~

> 允许通过防火墙

~~~shell
# 开放可视化界面端口
firewall-cmd --zone=public --add-port=15672/tcp --permanent

# 开放连接端口
firewall-cmd --zone=public --add-port=5672/tcp --permanent

~~~

>重启防火墙

~~~shell
# 重启防火墙
firewall-cmd --reload
~~~

> 注意：guest 账号只能在本地登录，远程需要添加新的管理员账号
>
> 添加新的管理员账号

~~~shell
# 添加账号 第一个 admin 为账号 第二个为密码
rabbitmqctl  add_user admin admin

#设置权限 admin 为账号 administrator 为角色 
rabbitmqctl  set_user_tags admin administrator

# 重启 rabbitmq 
systemctl restart rabbitmq-server
~~~

>访问可视化页面
>
>http://ip:15672

> 远程连接配置

~~~bash
#授权远程访问，否则不能创建连接(admin 为账号)
rabbitmqctl set_permissions -p / admin "." "." ".*"
~~~

> 其他命令

~~~shell
一、防火墙的开启、关闭、禁用命令
（1）设置开机启用防火墙：systemctl enable firewalld.service
（2）设置开机禁用防火墙：systemctl disable firewalld.service
（3）启动防火墙：systemctl start firewalld
（4）关闭防火墙：systemctl stop firewalld
（5）检查防火墙状态：systemctl status firewalld
二、使用firewall-cmd配置端口
（1）查看防火墙状态：firewall-cmd --state
（2）重新加载配置：firewall-cmd -reload
（3）查看开放的端口：firewall-cmd --list-ports
（4）开启防火墙端口：firewall-cmd -zone=public -add-port=9200/tcp permanent
命令含义：
–zone #作用域
–add-port=9200/tcp #添加端口，格式为：端口/通讯协议
–permanent #永久生效，没有此参数重启后失效
注意：添加端口后，必须用命令firewall-cmd -reload重新加载一遍才会生效
（5）关闭防火墙端口：firewall-cmd -zone=public -remove-port=9200/tcp permanent

~~~



