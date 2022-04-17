# mysql使用

使用第三方开源的mysql库: 

github.com/go-sql-driver/mysql （mysql驱动）

github.com/jmoiron/sqlx （基于mysql驱动的封装）

# 链接

```go
database, err := sqlx.Open("mysql", "root:password@tcp(127.0.0.1:3306)/database")
    //database, err := sqlx.Open("数据库类型", "用户名:密码@tcp(地址:端口)/数据库名")
```

> 注意： 导入相关驱动和package
>
>    _ "github.com/go-sql-driver/mysql"
>    "github.com/jmoiron/sqlx"

# Insert

```go
package main

import (
   "fmt"
   _ "github.com/go-sql-driver/mysql"
   "github.com/jmoiron/sqlx"
)

type Person struct {
   UserId   int    `db:"user_id"`
   Username string `db:"username"`
   Sex      string `db:"sex"`
   Email    string `db:"email"`
}


var Db *sqlx.DB

func init() {
   database, err := sqlx.Open("mysql", "root:root@tcp(127.0.0.1:3306)/zxc")
   if err != nil {
      fmt.Println("open mysql failed,", err)
      return
   }
   Db = database
   //defer Db.Close() // 注意这行代码要写在上面err判断的下面
}

func main() {
   r, err := Db.Exec("insert into person(username, sex, email)values(?, ?, ?)", "stu001", "man", "stu01@qq.com")
   if err != nil {
      fmt.Println("exec failed, ", err)
      return
   }
   id, err := r.LastInsertId()
   if err != nil {
      fmt.Println("exec failed, ", err)
      return
   }

   fmt.Println("insert succ:", id)
}
```

web

```go
package main

import (
   "fmt"
   "github.com/gin-gonic/gin"
   _ "github.com/go-sql-driver/mysql"
   "github.com/jmoiron/sqlx"
   "net/http"
)

type Person struct {
   UserId   int    `db:"user_id"`
   Username string `db:"username"`
   Sex      string `db:"sex"`
   Email    string `db:"email"`
}

func main() {
   // 1.创建路由
   r := gin.Default()
   // 2.绑定路由规则，执行的函数
   // gin.Context，封装了request和response
   r.GET("/", func(c *gin.Context) {

      //database, err := sqlx.Open("数据库类型", "用户名:密码@tcp(地址:端口)/数据库名")
      database, err := sqlx.Open("mysql", "root:root@tcp(127.0.0.1:3306)/zxc")
      if err != nil {
         fmt.Println("open mysql failed,", err)
         return
      }
      Db := database
      r, err := Db.Exec("insert into person(username, sex, email)values(?, ?, ?)", "stu001", "man", "stu01@qq.com")
      if err != nil {
         fmt.Println("exec failed, ", err)
         return
      }
      id, err := r.LastInsertId()
      if err != nil {
         fmt.Println("exec failed, ", err)
         return
      }
      fmt.Println("insert succ:", id)
      c.String(http.StatusOK, "hello World!")
   })
   // 3.监听端口，默认在8080
   // Run("里面不指定端口号默认为8080")
   r.Run(":8000")
}
```
