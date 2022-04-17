# 安装

url 

https://blog.csdn.net/fangchao3652/article/details/106793577?spm=1001.2101.3001.6661.1&utm_medium=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1.pc_relevant_default&depth_1-utm_source=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1.pc_relevant_default&utm_relevant_index=1

1.在国内直接用官网推荐的下面这个命令大概率是安装不成功的

~~~shell
$ go get -u github.com/gin-gonic/gin
~~~

2、这时可以在你的项目目录下 执行下面几个命令

~~~shell
go env -w GO111MODULE=on 
go env -w GOPROXY=https://goproxy.cn,direct
go mod init YourProjectName
go get -u github.com/gin-gonic/gin
~~~

Github

https://github.com/gin-gonic/gin