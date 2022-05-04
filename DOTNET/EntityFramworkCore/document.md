# 环境搭建

## 添加nuget包

`Microsoft.EntityFrameworkCore.SqlServer`

## 创建实体类

~~~C#
    public class Book
    {
        public long BookId { get; set; }
        public string? Title { get; set; }
        public DateTime PubTime { get; set; }
        public decimal Price { get; set; }
    }
~~~

## 创建配置类

~~~C#
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book));
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.AuthorName).IsRequired().HasMaxLength(20).IsUnicode(true);
        }
    }
~~~

## 创建DBContext

~~~C#
    public class MyDbContext : DbContext
    {
        //1、配置类
        public DbSet<Person>? Persons { get; set; }
        public DbSet<Book>? Books { get; set; }

        /// <summary>
        ///2、加载连接字符串
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //配置连接字符串
            optionsBuilder.UseSqlServer("server=.;uid=sa;pwd=zxc123...;database=Demo");
        }

        /// <summary>
        /// 3、加载实体配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //加载当前程序集下的所有继承IEntityTypeConfiguration的配置类
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
~~~



# EFCore Migration 

UP 向上迁移

Down 向下迁移

~~~C#
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //创建
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PubTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //回滚
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Person");
        }
~~~

## 命令

> 包管理器
>
> https://docs.microsoft.com/zh-cn/ef/core/cli/powershell#scaffold-dbcontext

~~~C#
//添加
Add-Migraton  <name>  -Context xxdbCntext
//更新
Update-database -Context xxdbContext
//指定更新到那个版本
Update-database <name>
//删除最后一次的迁移脚本
Remove-Migration
//生成迁移的SQL语句-可以生成 version1 ---version2
Script-Migration <name>
    
Bundle-Migration
Drop-Database
Get-DbContext
Get-Migration
Optimize-DbContext
//反向工程
Scaffold-DbContext
Script-DbContext
~~~

> EF CLI
>
> https://docs.microsoft.com/zh-cn/ef/core/cli/dotnet
>
> 注：切换到 xx.csproj 目录

~~~C#
//删除数据库
dotnet ef database drop
//将数据库更新到上一次迁移或指定的迁移
dotnet ef database update
    //dotnet ef database update init -c MyDbContext
dotnet ef dbcontext info
dotnet ef dbcontext list
dotnet ef dbcontext optimize
//反向工程
dotnet ef dbcontext scaffold
dotnet ef dbcontext script
//添加新的迁移
dotnet ef migrations add
    // dotnet ef migrations add addSex -c MyDbontext
dotnet ef migrations bundle
dotnet ef migrations list
//移除最近一次迁移
dotnet ef migrations remove
//从迁移生成 SQL 脚本
dotnet ef migrations script
     //dotnet ef migrations script  20220128130933_addPerson -c MyDbContext

~~~

# 

# CRUD

~~~C#
        //增
        public async Task<Object> InsertBook(Book book)
        {
            using (var db = new MyDbContext())
            {
                var obj = await db.Books.AddAsync(book);
                var count = await db.SaveChangesAsync();
                return new JsonResult(new { obj.Entity, count });
            }
        }

        //查
        public async Task<Object> GetBooks()
        {
            using (var db = new MyDbContext())
            {
                var obj = await db.Books.ToListAsync();
                return new JsonResult(new { obj });
            }
        }

        //删
        public async Task<Object> DeleteBook(int bookId)
        {
            using (var db = new MyDbContext())
            {
                var book = db.Books.Single(s => s.BookId == bookId);
                db.Books.Remove(book);
                var result = await db.SaveChangesAsync();
                return new JsonResult(new { result });
            }
        }

        //改
        public async Task<Object> UpdateBook(Book book)
        {
            using (var db = new MyDbContext())
            {
                var result = db.Books.Single(s => s.BookId == book.BookId);
                result.Title = book.Title;
                result.Price = book.Price;
                result.PubTime = book.PubTime;
                result.AuthorName = book.AuthorName;
                db.Books.Update(result);
                var count = await db.SaveChangesAsync();
                return new JsonResult(new { count });
            }
        }
~~~

# 约定大于配置

> 主要规则
>
> 1、表名采用DbSet<T>  的属性名
>
> 2、列名采用实体属性名
>
> 3、表中的类型取决于实体类型的属性的类型和特性（可空...）
>
> 4、实体中Id 或者 类名Id为主键 ，short int long 自增，如果主键为guid类型采用默认guid生成机制生成主键值



# 配置方式

> 数据标注:简单，耦合

~~~C#
    [Table("T_Person")]
    public class Person
    {
        [Key] 
        public long PersonId { get; set; }
        [Required] 
        [MaxLength(50)] 
        public string? Name { get; set; }
        public int Age { get; set; }
    }
~~~

> dbContext.OnModelCreating 配置：所有配置代码在一起，特乱

~~~C#
           modelBuilder.Entity<Book>().ToTable("t_Books").HasKey(s => s.BookId);
           modelBuilder.Entity<Book>().Property(s => s.Title).HasMaxLength(50);
~~~

> Fluent API：复杂 解耦

~~~C#
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            #region 实体配置

            //表映射
            builder.ToTable(nameof(Person))
                //排除字段映射
                .Ignore(p => p.Age)
                //设置主键
                .HasKey(p => p.PersonId);
            
            //索引
            builder.HasIndex(p => p.PersonId)
                //唯一索引
                .IsUnique();
            
            //复合索引
            builder.HasIndex(p => new {p.PersonId, p.Age})
                //聚集索引
                .IsClustered();
            
            //视图映射
            builder.ToView(nameof(Person));
            

            #endregion

            #region 属性配置

            builder.Property(s => s.Name)
                //配置列名
                .HasColumnName($"{nameof(Person.Name)}s")
                //配置列数据类型
                .HasColumnType("nvarchar(20)")
                //配置长度
                .HasMaxLength(50)
                //必学的
                .IsRequired(true)
                //unicode 字符
                .IsUnicode(true)
                //默认值
                .HasDefaultValue("zxc")
                //生成列的值
                .ValueGeneratedOnAdd();

            #endregion
        }
    }
~~~

# 主键无小事

？？

# 底层原理

~~~mermaid
graph LR
A[服务] 
A --> B[EFCore]
B-->C[Ado.net core]
C-->D[数据库]
~~~

> **EF Core**
>
> 把C#代码转换为SQL语句的框架。

# EF 做不到的事

~~~C#
//这种代码不能被翻译
var books = ctx.Books.Where(b => IsOK(b.Title));
private static bool IsOK(string s)
{
	return s.Contains("张");
}
//这种也不能被翻译
var books = ctx.Books.Where(b =>b.Title.PadLeft(5)=="hello");
~~~

> EF 能不能用 主要看能不能翻译成sql语句

~~~mermaid
graph LR
A[C#] 
A --> B[EFCore 核心]
B-->C[AST 抽象语法树]
C-->D[sqlserver efcore provider]
C-->E[Mysql efcore provider]
D-->H[Sql]
E-->H[sql]
H-->F[SQLServer ADO.NET core Provider]
H-->G[MYSQL ADO.NET core Provider]
F-->I[数据库]
G-->I

~~~

# 查看EF生成的语句

> 1、日志系统

> 2、dbContext.OnConfiguring中配置输出到控制台

~~~C#
optionsBuilder.LogTo(msg =>
            {
                if (!msg.Contains("CommandExecuting"))
                    return;
                Console.WriteLine(msg);
            });
~~~

> 3、IQueryable.ToQueryString()
>
> 注：只能获取查询的语句

# 同样的sql翻译为不同的sql语句



# 关系配置

## 一对一

法律上一个老公对应一个老婆

wife(Id ,Name,HId)

Husband(Id ,Name)

## 一对多

一个父亲有多个孩子

Dad(id ,Name)

Child(id ,Name ,Fid)

## 多对多

一个教师对应多个学生，一个学生有多个教师

Teacher(id,Nmae)

Student(Id,Name)

T1(SId，TId)

# 配置步骤

1、声明实体类中的关系属性

2、FluentAPI 关系配置

3、使用关系操作



## 一对多配置

~~~C#
public class Article
{
    public long Id{get;set;}
    public string Title{get;set;}
    public string Content{get;set;}
    public List<Comment>Comments{get;set;}=new List<Comment>()
}

public class Comment
{
    public long Id{get;set;}
    public Article Article{get;set;}
    public string Message{get;set;}
}

~~~

# 

