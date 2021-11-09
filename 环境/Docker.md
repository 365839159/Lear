[TOC]



# 环境查看

```shell
# 查看内核
[root@iZ2zebquvlfb5cndmb95u5Z ~]#  uname -r
3.10.0-957.21.3.el7.x86_64
```



```shell
# 查看配置
[root@iZ2zebquvlfb5cndmb95u5Z /]# cat etc/os-release 
NAME="CentOS Linux"
VERSION="7 (Core)"
ID="centos"
ID_LIKE="rhel fedora"
VERSION_ID="7"
PRETTY_NAME="CentOS Linux 7 (Core)"
ANSI_COLOR="0;31"
CPE_NAME="cpe:/o:centos:centos:7"
HOME_URL="https://www.centos.org/"
BUG_REPORT_URL="https://bugs.centos.org/"

CENTOS_MANTISBT_PROJECT="CentOS-7"
CENTOS_MANTISBT_PROJECT_VERSION="7"
REDHAT_SUPPORT_PRODUCT="centos"
REDHAT_SUPPORT_PRODUCT_VERSION="7"

```



# 安装

## Centos 7安装

```shell
# 卸载旧版本
sudo yum remove docker \
                  docker-client \
                  docker-client-latest \
                  docker-common \
                  docker-latest \
                  docker-latest-logrotate \
                  docker-logrotate \
                  docker-engine
```

```shell
# 需要的安装包
sudo yum install -y yum-utils
```

```shell
# 设置镜像地址

#国外地址
sudo yum-config-manager \
    --add-repo \
    https://download.docker.com/linux/centos/docker-ce.repo
    
#国内地址（推荐使用阿里）
sudo yum-config-manager \
    --add-repo \
	http://mirrors.aliyun.com/docker-ce/linux/centos/docker-ce.repo
```

```shell
# 更新yum软件包索引
sudo yum makecache fast	
```

```shell
# 安装docker引擎
sudo yum install docker-ce docker-ce-cli containerd.io

# 解释
docker-ce  #社区版
```

```shell
# 启动docker服务
sudo systemctl start docker
```

```shell
# 查看版本
[root@iZ2zebquvlfb5cndmb95u5Z /]# docker version

Client: Docker Engine - Community
 Version:           20.10.10
 API version:       1.41
 Go version:        go1.16.9
 Git commit:        b485636
 Built:             Mon Oct 25 07:44:50 2021
 OS/Arch:           linux/amd64
 Context:           default
 Experimental:      true

Server: Docker Engine - Community
 Engine:
  Version:          20.10.10
  API version:      1.41 (minimum version 1.12)
  Go version:       go1.16.9
  Git commit:       e2f740d
  Built:            Mon Oct 25 07:43:13 2021
  OS/Arch:          linux/amd64
  Experimental:     false
 containerd:
  Version:          1.4.11
  GitCommit:        5b46e404f6b9f661a205e28d59c982d3634148f8
 runc:
  Version:          1.0.2
  GitCommit:        v1.0.2-0-g52b36a2
 docker-init:
  Version:          0.19.0
  GitCommit:        de40ad0

```

```shell
# 测试 hello-world
[root@iZ2zebquvlfb5cndmb95u5Z /]# sudo docker run hello-world
Unable to find image 'hello-world:latest' locally									# 本地找不到 hello-world:latest
latest: Pulling from library/hello-world											# 仓库拉取  hello-world
2db29710123e: Pull complete 
Digest: sha256:37a0b92b08d4919615c3ee023f7ddb068d12b8387475d64c622ac30f45c29c51
Status: Downloaded newer image for hello-world:latest

Hello from Docker!																	#运行
This message shows that your installation appears to be working correctly.

To generate this message, Docker took the following steps:
 1. The Docker client contacted the Docker daemon.
 2. The Docker daemon pulled the "hello-world" image from the Docker Hub.
    (amd64)
 3. The Docker daemon created a new container from that image which runs the
    executable that produces the output you are currently reading.
 4. The Docker daemon streamed that output to the Docker client, which sent it
    to your terminal.

To try something more ambitious, you can run an Ubuntu container with:
 $ docker run -it ubuntu bash

Share images, automate workflows, and more with a free Docker ID:
 https://hub.docker.com/

For more examples and ideas, visit:
 https://docs.docker.com/get-started/
```



# 卸载

```shell
# 卸载依赖
sudo yum remove docker-ce docker-ce-cli containerd.io

# 删除资源
sudo rm -rf /var/lib/docker

sudo rm -rf /var/lib/containerd
```



# 配置镜像加速

登录阿里云 找到容器镜像服务

![image-20211107104304362](../image/image-20211107104304362.png)

```shell
#创建目录
sudo mkdir -p /etc/docker

#配置
sudo tee /etc/docker/daemon.json <<-'EOF'
{
  "registry-mirrors": ["https://r35ls29f.mirror.aliyuncs.com"]
}
EOF

#重新加载
sudo systemctl daemon-reload

#重启docker
sudo systemctl restart docker
```



# docker run 的运行流程

![image-20211107113256326](../image/image-20211107113256326.png)



# 底层原理



## docker是怎么工作的 

docker是一个cs 结构的系统 ，docker 的守护进程运行在主机上。通过socket 从客户端访问！

docker server 接受到Dcoker client 的指令，就行执行命令

![image-20211107121358265](../image/image-20211107121358265.png)



## docker 为什么比VM快

1、有更少的抽象层

2、docker 利用宿主机的内核 Vm 需要Guest OS

![image-20211107121528994](../image/image-20211107121528994.png)

新建容器的时候，docker 不需要docker不需要重新加载操作系统内核       

# docker 常用命令



## 帮助命令

```shell
# 版本信息
docker version

# 详细信息
docker info

# 帮助
docker [cmd] --help


```



## 帮助文档

https://docs.docker.com/reference/

----



## 镜像命令



### docker images  查看本地所有镜像

帮助文档： https://docs.docker.com/engine/reference/commandline/images/

```shell
# docker images [可选参数]

# 可选项
  -a, --all             # 列出所有镜像
      --digests         Show digests
  -f, --filter filter   # 过滤
      --format string   Pretty-print images using a Go template
      --no-trunc        Don't truncate output
  -q, --quiet           # 只显示ID

# 查看镜像
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker images
REPOSITORY    TAG       IMAGE ID       CREATED       SIZE
hello-world   latest    feb5d9fea6a5   6 weeks ago   13.3kB

#解释
REPOSITORY      镜像仓库
TAG 			版本
IMAGE ID        镜像ID
CREATED			镜像创建时间
SIZE			大小

# 列出全部镜像
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker images -a
REPOSITORY    TAG       IMAGE ID       CREATED       SIZE
hello-world   latest    feb5d9fea6a5   6 weeks ago   13.3kB

# 显示镜像id
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker images -q
feb5d9fea6a5

```



### docker search 搜索

帮助文档：https://docs.docker.com/engine/reference/commandline/search/

```shell
# docker search [可选参数]

# 可选项
  -f, --filter filter   # 过滤
      --format string   Pretty-print search using a Go template
      --limit int       Max number of search results (default 25)
      --no-trunc        Don't truncate output

# 搜索镜像
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker search mysql
NAME                              DESCRIPTION                                     STARS     OFFICIAL   AUTOMATED
mysql                             MySQL is a widely used, open-source relation…   11647     [OK]       
mariadb                           MariaDB Server is a high performing open sou…   4433      [OK]       
mysql/mysql-server                Optimized MySQL Server Docker images. Create…   865                  [OK]

# 搜索 STARS 大于4000 的 mysql
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker search  mysql -f=STARS=4000
NAME      DESCRIPTION                                     STARS     OFFICIAL   AUTOMATED
mysql     MySQL is a widely used, open-source relation…   11647     [OK]       
mariadb   MariaDB Server is a high performing open sou…   4433      [OK]       

```



### docker pull 拉取镜像

帮助文档：https://docs.docker.com/engine/reference/commandline/pull/

```shell
# docker pull imageName [:tag]

[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker pull mysql
Using default tag: latest								# 不写 tag 默认 latest
latest: Pulling from library/mysql						
b380bbd43752: Pull complete 							# docker image 核心 分层下载 、联合文件系统
f23cbf2ecc5d: Pull complete 
30cfc6c29c0a: Pull complete 
b38609286cbe: Pull complete 
8211d9e66cd6: Pull complete 
2313f9eeca4a: Pull complete 
7eb487d00da0: Pull complete 
4d7421c8152e: Pull complete 
77f3d8811a28: Pull complete 
cce755338cba: Pull complete 
69b753046b9f: Pull complete 
b2e64b0ab53c: Pull complete 
Digest: sha256:6d7d4524463fe6e2b893ffc2b89543c81dec7ef82fb2020a1b27606666464d87 		# image 签名
Status: Downloaded newer image for mysql:latest											
docker.io/library/mysql:latest															#真实地址

#docker pull mysql 等价于 docker pull  docker.io/library/mysql:latest					

#下载指定mysql版本镜像
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker pull mysql:5.7
5.7: Pulling from library/mysql
b380bbd43752: Already exists 								# 存在的无需下载
f23cbf2ecc5d: Already exists 
30cfc6c29c0a: Already exists 
b38609286cbe: Already exists 
8211d9e66cd6: Already exists 
2313f9eeca4a: Already exists 
7eb487d00da0: Already exists 
a71aacf913e7: Pull complete 								# 下载不存在的
393153c555df: Pull complete 
06628e2290d7: Pull complete 
ff2ab8dac9ac: Pull complete 
Digest: sha256:2db8bfd2656b51ded5d938abcded8d32ec6181a9eae8dfc7ddf87a656ef97e97
Status: Downloaded newer image for mysql:5.7
docker.io/library/mysql:5.7

# 查看本地镜像
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker images -a
REPOSITORY    TAG       IMAGE ID       CREATED       SIZE
mysql         5.7       938b57d64674   2 weeks ago   448MB
mysql         latest    ecac195d15af   2 weeks ago   516MB
hello-world   latest    feb5d9fea6a5   6 weeks ago   13.3kB

```



### docker rmi 删除镜像

帮助文档：https://docs.docker.com/engine/reference/commandline/rmi/

```shell
# docker rmi [可选参数]

# 可选参数
  -f, --force      # 强制移除
      --no-prune   Do not delete untagged parents


# 查看本地镜像
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker images
REPOSITORY    TAG       IMAGE ID       CREATED       SIZE
mysql         5.7       938b57d64674   2 weeks ago   448MB
mysql         latest    ecac195d15af   2 weeks ago   516MB
hello-world   latest    feb5d9fea6a5   6 weeks ago   13.3kB

# 删除 mysql（镜像ID：938b57d64674）
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker rmi -f 938b57d64674   						# 删除id 938b57d64674 的 image
Untagged: mysql:5.7
Untagged: mysql@sha256:2db8bfd2656b51ded5d938abcded8d32ec6181a9eae8dfc7ddf87a656ef97e97
Deleted: sha256:938b57d64674c4a123bf8bed384e5e057be77db934303b3023d9be331398b761	# 删除不共用的层
Deleted: sha256:d81fc74bcfc422d67d8507aa0688160bc4ca6515e0a1c8edcdb54f89a0376ff1
Deleted: sha256:a6a530ba6d8591630a1325b53ef2404b8ab593a0775441b716ac4175c14463e6
Deleted: sha256:2a503984330e2cec317bc2ef793f5d4d7b3fd8d50009a4f673026c3195460200
Deleted: sha256:e2a4585c625da1cf4909cdf89b8433dd89ed5c90ebdb3a979d068b161513de90

# 删除多个镜像
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker rmi -f 镜像id   镜像id   镜像id  镜像id

# 全部删除
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker rmi -f $(docker images -aq)				# 删除全部容器
Untagged: mysql:latest
Untagged: mysql@sha256:6d7d4524463fe6e2b893ffc2b89543c81dec7ef82fb2020a1b27606666464d87
Deleted: sha256:ecac195d15afac2335de52fd7a0e34202fe582731963d31830f1b97700bf9509
Deleted: sha256:451fe04d80b84c0b7aca0f0bbdaa5de7c7ac85a65389ed5d3ed492f63ac092e2
Deleted: sha256:814cbf8bc7f6bb85685e5b803e16a76406c30d1960c566eee76303ffac600600
Deleted: sha256:735f72e1d1b936bb641b6a1283e4e60bf10a0c36f8244a5e3f8c7d58fa95b98a
Deleted: sha256:f2d209a30c3950fadffb2d82e1faa434da0753bee7aacad9cdec7d8a7a83df37
Deleted: sha256:03b9f8c5331d9534d2372a144bcffc8402e5f7972c9e4b85c634bef203ec6d20
Deleted: sha256:80f5487a88b8061855e99782979ed6069a8dd1c7dfbb1eb63fe42a4a9d119436
Deleted: sha256:f791a6c727931d41c51f8bf24ee32a4dbf0169f372b174f1ff89b4836b97c48e
Deleted: sha256:4c88df098412e11a98936509f3cede57f87154b350b0f75d96713f6e1dd56101
Deleted: sha256:fdba3a2cd286d9a5f65fc00f5254048855ae7dc00f3b3fa3356981eb9a7fe6d0
Deleted: sha256:8b3a69042e0da82429d28be0c474e73290ba4908730de22b2200a7aac9b245bd
Deleted: sha256:90afe56a0643f5bf1b1e8ee147b40a8e12b3fdd7e26bc2d2c50180d68dd524d0
Deleted: sha256:e81bff2725dbc0bf2003db10272fef362e882eb96353055778a66cda430cf81b
Untagged: hello-world:latest
Untagged: hello-world@sha256:37a0b92b08d4919615c3ee023f7ddb068d12b8387475d64c622ac30f45c29c51
Deleted: sha256:feb5d9fea6a5e9606aa995e879d862b825965ba48de054caab5ef356dc6b3412

# 查看镜像
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker images -a
REPOSITORY   TAG       IMAGE ID   CREATED   SIZE									# 已经没有了



```

----



## 容器命令



### docker run   启动容器

 帮助文档： https://docs.docker.com/engine/reference/commandline/run/

```shell
# docker run [可选参数] image [bin/bash]

# 可选参数  

--name 			# 容器名称
-d 				# 后台运行
-it 			# 使用交互方式运行
-p				# 指定容器端口  -p 8080：8080
				# -p ip：主机端口:容器端口
				# -p 主机端口:容器端口
				# -p 容器端口
				# 容器端口
-P				#随机指定端口

# 拉取一个centos 用于测试
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker pull centos
Using default tag: latest
latest: Pulling from library/centos
a1d0c7532777: Pull complete 
Digest: sha256:a27fd8080b517143cbbbab9dfb7c8571c40d67d534bbdee55bd6c473f432b177
Status: Downloaded newer image for centos:latest
docker.io/library/centos:latest

# 启动并进入容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker run -it --name=centos1  centos /bin/bash
[root@7c41a13e0425 /]# ls
bin  dev  etc  home  lib  lib64  lost+found  media  mnt  opt  proc  root  run  sbin  srv  sys  tmp  usr  var

# 解释
docker run			# 运行
-it					# 交互模式
-- name=centos1		# 容器的名字
centos				# 镜像的名字
/bin/bash			# 使用bash 操作

# 退出容器
[root@7c41a13e0425 /]# exit  
exit

# 后台方式启动容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker run --name centos2 -d  centos
aeacf21532c77f0e6a5a624d2466aa24319fdaf1be9f21d1adf9958e479b5b4a

# 问题：docker ps 发现centos2 停止了

# 常见的坑：docker 容器使用后台运行，就必须要有一个前台进程，docker 发现没有应用，就会自动停止。

```



### docker ps 查看容器

帮助文档：https://docs.docker.com/engine/reference/commandline/ps/

```shell
# docker ps [可选参数]

#可选参数
			# 默认显示正在运行的容器
-a 			# 查看所有的容器
-n=1		# 显示最近创建的一个容器
-q			# 只显示容器编号

# 查看本地所有容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps -a
CONTAINER ID   IMAGE          COMMAND       CREATED         STATUS                      PORTS     NAMES
7c41a13e0425   centos         "/bin/bash"   3 minutes ago   Exited (0) 16 seconds ago             centos1

# 查看最近创建的一个容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps -n=1
CONTAINER ID   IMAGE     COMMAND       CREATED          STATUS                     PORTS     NAMES
7c41a13e0425   centos    "/bin/bash"   11 minutes ago   Exited (0) 7 minutes ago             centos1

# 查看容器的编号
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps -aq
7c41a13e0425
09c7ca86b0d2


```



### exit / ctrl+p+q退出容器

~~~shell
# 退出并关闭容器
[root@7c41a13e0425 /]# exit  
exit
# 退出不关闭（ ctrl + p + q）
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker run -it --name=centos2  centos /bin/bash				# ctrl + p + q
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps
CONTAINER ID   IMAGE     COMMAND       CREATED          STATUS         PORTS     NAMES			# 查看容器没有退出
bd7c90e8b239   centos    "/bin/bash"   10 seconds ago   Up 9 seconds             centos2

~~~



### docker rm 移除容器

帮助文档：https://docs.docker.com/engine/reference/commandline/rm/

~~~shell
# docker rm [可选参数] 容器id

# 可选参数
  -f, --force     强制移除
  -l, --link      Remove the specified link
  -v, --volumes   Remove anonymous volumes associated with the container

# 查看本地所有容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps -a
CONTAINER ID   IMAGE          COMMAND       CREATED          STATUS                      PORTS     NAMES
bd7c90e8b239   centos         "/bin/bash"   3 minutes ago    Up 3 minutes                          centos2
7c41a13e0425   centos         "/bin/bash"   21 minutes ago   Exited (0) 18 minutes ago             centos1
09c7ca86b0d2   feb5d9fea6a5   "/hello"      3 hours ago      Exited (0) 3 hours ago                inspiring_johnson

# 移除一个编号 7c41a13e0425 容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker rm  7c41a13e0425
7c41a13e0425

# 移除所有容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]#  docker rm -f $(docker ps -aq)
bd7c90e8b239
09c7ca86b0d2

# 利用管道移除所有容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps -aq|xargs docker rm -f
bd7c90e8b239
09c7ca86b0d2

# 查看所有容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps -a
CONTAINER ID   IMAGE     COMMAND   CREATED   STATUS    PORTS     NAMES					# 已经没有了



~~~



### 启动、停止、杀死

帮助文档：https://docs.docker.com/engine/reference/commandline/start/

​				   https://docs.docker.com/engine/reference/commandline/stop/

​				   https://docs.docker.com/engine/reference/commandline/kill/

~~~shell
# 查看所有容器
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps -a
CONTAINER ID   IMAGE     COMMAND       CREATED          STATUS                     PORTS     NAMES
0670d4374aa3   centos    "/bin/bash"   14 seconds ago   Exited (0) 9 seconds ago             centos1

# 启动并查看
# docker start 容器id
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker start 0670d4374aa3
0670d4374aa3

[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps 
CONTAINER ID   IMAGE     COMMAND       CREATED              STATUS         PORTS     NAMES
0670d4374aa3   centos    "/bin/bash"   About a minute ago   Up 2 seconds             centos1

# 关闭并查看
# docker stop 容器id
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker stop 0670d4374aa3
0670d4374aa3

[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps
CONTAINER ID   IMAGE     COMMAND   CREATED   STATUS    PORTS     NAMES

#杀死并查看
# docker kill 容器id
[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps
CONTAINER ID   IMAGE     COMMAND       CREATED         STATUS         PORTS     NAMES
0670d4374aa3   centos    "/bin/bash"   4 minutes ago   Up 6 seconds             centos1

[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker kill 0670d4374aa3
0670d4374aa3

[root@iZ2zebquvlfb5cndmb95u5Z ~]# docker ps
CONTAINER ID   IMAGE     COMMAND   CREATED   STATUS    PORTS     NAMES

~~~



## 其他命令

### 查看日志

帮助文档：https://docs.docker.com/engine/reference/commandline/logs/

~~~shell
# docker logs [可选参数] 容器id

#可选参数
  -f, --follow         # 跟踪日志输出
  -n, --tail string    # 从日志末尾显示的行数（默认为“全部”
  -t, --timestamps     # 显示时间戳
      --until string   # 在时间戳（例如 2013-01-02T13:23:37Z）或相关（例如 42m 为 42 分钟）之前显示日志


# 编写一段 shell 脚本输出信息
[root@iZ2zebquvlfb5cndmb95u5Z /root]
#docker run --name centos3 -d centos /bin/sh -c "while true; do echo zxc; sleep 2;done"
2019c32f0dcde38a30481f6012fc310ea052f6c43b8d5481a6e01426cf7368ef

[root@iZ2zebquvlfb5cndmb95u5Z /root]
#docker ps
CONTAINER ID   IMAGE     COMMAND                  CREATED          STATUS          PORTS     NAMES
2019c32f0dcd   centos    "/bin/sh -c 'while t…"   11 seconds ago   Up 10 seconds             centos3

# 输出日志
[root@iZ2zebquvlfb5cndmb95u5Z /root]
#docker logs -tf --tail 5  2019c32f0dcd
2021-11-09T14:02:34.499444400Z zxc
2021-11-09T14:02:36.501801226Z zxc
2021-11-09T14:02:38.504164649Z zxc
2021-11-09T14:02:40.506568684Z zxc
2021-11-09T14:02:42.508835580Z zxc


~~~

### 查看容器中的进程信息

帮助文档：https://docs.docker.com/engine/reference/commandline/top/

```shell
# docker top  容器id

[root@iZ2zebquvlfb5cndmb95u5Z /root]
#docker top  2019c32f0dcd
UID      PID     PPID      C      STIME   TTY  TIME      CMD
root     10579   10560     0      21:55   ?    00:00:00  /bin/sh -c while true; do echo zxc; sleep 2;done
root     11121   10579     0      22:08   ?    00:00:00  /usr/bin/coreutils --coreutils-prog-shebang=sleep /usr/bin/sleep 2
```

### 查看镜像的元数据

帮助文档：https://docs.docker.com/engine/reference/commandline/inspect/

```shell
# docker inspect  容器id/镜像id

[root@iZ2zebquvlfb5cndmb95u5Z /root]
#docker inspect 2019c32f0dcd
[
    {
        "Id": "2019c32f0dcde38a30481f6012fc310ea052f6c43b8d5481a6e01426cf7368ef",
        "Created": "2021-11-09T13:55:57.701924242Z",
        "Path": "/bin/sh",
        "Args": [
            "-c",
            "while true; do echo zxc; sleep 2;done"
        ],
        "State": {
            "Status": "running",
            "Running": true,
            "Paused": false,
            "Restarting": false,
            "OOMKilled": false,
            "Dead": false,
            "Pid": 10579,
            "ExitCode": 0,
            "Error": "",
            "StartedAt": "2021-11-09T13:55:58.047646322Z",
            "FinishedAt": "0001-01-01T00:00:00Z"
        },
        "Image": "sha256:5d0da3dc976460b72c77d94c8a1ad043720b0416bfc16c52c45d4847e53fadb6",
        "ResolvConfPath": "/var/lib/docker/containers/2019c32f0dcde38a30481f6012fc310ea052f6c43b8d5481a6e01426cf7368ef/resolv.conf",
        "HostnamePath": "/var/lib/docker/containers/2019c32f0dcde38a30481f6012fc310ea052f6c43b8d5481a6e01426cf7368ef/hostname",
        "HostsPath": "/var/lib/docker/containers/2019c32f0dcde38a30481f6012fc310ea052f6c43b8d5481a6e01426cf7368ef/hosts",
        "LogPath": "/var/lib/docker/containers/2019c32f0dcde38a30481f6012fc310ea052f6c43b8d5481a6e01426cf7368ef/2019c32f0dcde38a30481f6012fc310ea052f6c43b8d5481a6e01426cf7368ef-json.log",
        "Name": "/centos3",
        "RestartCount": 0,
        "Driver": "overlay2",
        "Platform": "linux",
        "MountLabel": "",
        "ProcessLabel": "",
        "AppArmorProfile": "",
        "ExecIDs": null,
        "HostConfig": {
            "Binds": null,
            "ContainerIDFile": "",
            "LogConfig": {
                "Type": "json-file",
                "Config": {}
            },
            "NetworkMode": "default",
            "PortBindings": {},
            "RestartPolicy": {
                "Name": "no",
                "MaximumRetryCount": 0
            },
            "AutoRemove": false,
            "VolumeDriver": "",
            "VolumesFrom": null,
            "CapAdd": null,
            "CapDrop": null,
            "CgroupnsMode": "host",
            "Dns": [],
            "DnsOptions": [],
            "DnsSearch": [],
            "ExtraHosts": null,
            "GroupAdd": null,
            "IpcMode": "private",
            "Cgroup": "",
            "Links": null,
            "OomScoreAdj": 0,
            "PidMode": "",
            "Privileged": false,
            "PublishAllPorts": false,
            "ReadonlyRootfs": false,
            "SecurityOpt": null,
            "UTSMode": "",
            "UsernsMode": "",
            "ShmSize": 67108864,
            "Runtime": "runc",
            "ConsoleSize": [
                0,
                0
            ],
            "Isolation": "",
            "CpuShares": 0,
            "Memory": 0,
            "NanoCpus": 0,
            "CgroupParent": "",
            "BlkioWeight": 0,
            "BlkioWeightDevice": [],
            "BlkioDeviceReadBps": null,
            "BlkioDeviceWriteBps": null,
            "BlkioDeviceReadIOps": null,
            "BlkioDeviceWriteIOps": null,
            "CpuPeriod": 0,
            "CpuQuota": 0,
            "CpuRealtimePeriod": 0,
            "CpuRealtimeRuntime": 0,
            "CpusetCpus": "",
            "CpusetMems": "",
            "Devices": [],
            "DeviceCgroupRules": null,
            "DeviceRequests": null,
            "KernelMemory": 0,
            "KernelMemoryTCP": 0,
            "MemoryReservation": 0,
            "MemorySwap": 0,
            "MemorySwappiness": null,
            "OomKillDisable": false,
            "PidsLimit": null,
            "Ulimits": null,
            "CpuCount": 0,
            "CpuPercent": 0,
            "IOMaximumIOps": 0,
            "IOMaximumBandwidth": 0,
            "MaskedPaths": [
                "/proc/asound",
                "/proc/acpi",
                "/proc/kcore",
                "/proc/keys",
                "/proc/latency_stats",
                "/proc/timer_list",
                "/proc/timer_stats",
                "/proc/sched_debug",
                "/proc/scsi",
                "/sys/firmware"
            ],
            "ReadonlyPaths": [
                "/proc/bus",
                "/proc/fs",
                "/proc/irq",
                "/proc/sys",
                "/proc/sysrq-trigger"
            ]
        },
        "GraphDriver": {
            "Data": {
                "LowerDir": "/var/lib/docker/overlay2/4500e9f06fe6c774172facdd4d235da13c4518e6cd5cb8f719cfe56e5bd9493c-init/diff:/var/lib/docker/overlay2/4276982f224539f98d75963f237925c5c7be2e1b853743b33acfc0fd279e1513/diff",
                "MergedDir": "/var/lib/docker/overlay2/4500e9f06fe6c774172facdd4d235da13c4518e6cd5cb8f719cfe56e5bd9493c/merged",
                "UpperDir": "/var/lib/docker/overlay2/4500e9f06fe6c774172facdd4d235da13c4518e6cd5cb8f719cfe56e5bd9493c/diff",
                "WorkDir": "/var/lib/docker/overlay2/4500e9f06fe6c774172facdd4d235da13c4518e6cd5cb8f719cfe56e5bd9493c/work"
            },
            "Name": "overlay2"
        },
        "Mounts": [],
        "Config": {
            "Hostname": "2019c32f0dcd",
            "Domainname": "",
            "User": "",
            "AttachStdin": false,
            "AttachStdout": false,
            "AttachStderr": false,
            "Tty": false,
            "OpenStdin": false,
            "StdinOnce": false,
            "Env": [
                "PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin"
            ],
            "Cmd": [
                "/bin/sh",
                "-c",
                "while true; do echo zxc; sleep 2;done"
            ],
            "Image": "centos",
            "Volumes": null,
            "WorkingDir": "",
            "Entrypoint": null,
            "OnBuild": null,
            "Labels": {
                "org.label-schema.build-date": "20210915",
                "org.label-schema.license": "GPLv2",
                "org.label-schema.name": "CentOS Base Image",
                "org.label-schema.schema-version": "1.0",
                "org.label-schema.vendor": "CentOS"
            }
        },
        "NetworkSettings": {
            "Bridge": "",
            "SandboxID": "da7db216b95f3a84f5376a9ef61631ae35327485d3f184bb07882e5df26c439b",
            "HairpinMode": false,
            "LinkLocalIPv6Address": "",
            "LinkLocalIPv6PrefixLen": 0,
            "Ports": {},
            "SandboxKey": "/var/run/docker/netns/da7db216b95f",
            "SecondaryIPAddresses": null,
            "SecondaryIPv6Addresses": null,
            "EndpointID": "53184d5f466126a498677a746f6fb94fc6dd64a301830b57abc3afed81beb808",
            "Gateway": "172.17.0.1",
            "GlobalIPv6Address": "",
            "GlobalIPv6PrefixLen": 0,
            "IPAddress": "172.17.0.2",
            "IPPrefixLen": 16,
            "IPv6Gateway": "",
            "MacAddress": "02:42:ac:11:00:02",
            "Networks": {
                "bridge": {
                    "IPAMConfig": null,
                    "Links": null,
                    "Aliases": null,
                    "NetworkID": "d1f8ca3a7c6bf94ee9447b0af818fd0aa635b5d5749529bcd30ccac11ed8da14",
                    "EndpointID": "53184d5f466126a498677a746f6fb94fc6dd64a301830b57abc3afed81beb808",
                    "Gateway": "172.17.0.1",
                    "IPAddress": "172.17.0.2",
                    "IPPrefixLen": 16,
                    "IPv6Gateway": "",
                    "GlobalIPv6Address": "",
                    "GlobalIPv6PrefixLen": 0,
                    "MacAddress": "02:42:ac:11:00:02",
                    "DriverOpts": null
                }
            }
        }
    }
]
```







































































