day01

数据库特点：海量存储 、查找数度快、并发性问题 、安全性、数据完整性

主键：唯一标识表中的一条记录

​	》不能重复

​	》主键不能为空

选择主键的时候根据列存储的信息不同 ，可以分为

​	》业务主键、

​	》逻辑主键

选择什么样的列作为主键？

一个表中只能有一个主键

1、不允许为空的列

2、没有重复的列

3、与实际业务没有关系的列

4、稳定的列（数据不改变的列）

5、选择单键作为主键

6、尽量使用数字作为主键

外键 

sa账号:

1、先启用windows 账号身份验证和sql server身份验证

2、启用windoes 验证方式和sql server身份验证

3、选择“实例”-》右键 属性 -》安全性 -》服务器验证sql server 和windows 验证 -》确定 -》重启sql server服务

4、启用sa账号:

安全性-》登录名-》sa -》右键-》属性-》常规-》改密码（不要勾选“强制实施密码策略”）->确定

卸载sql

1先卸载

2删除目录

3删除注册表

sql数据类型

1、image      用来存储byte

2、字符串类型

char

nchar

varchar

nvarchar

text

ntext

varchar（max）

nvarchar（max）

============================================================================

带n和不带n的区别

char（2）表示可以存储两个字节 ab 、12、赵，不带n的数据类型，存储中文等双字节字符 占用两个字节 存储英文数字等占用1个字节

nchar （2）带n的 都占用2个字节

不带n的数据类型长度最多8000 带n的 最多4000

char（8000）

varchar（8000）

nchar（4000）

nvarchar（4000）

============================================================================

带var 和不带var

带var 的表示的是：可变长度

不带var的表示的是：固定长度

******************************************************************************************************************

--查询数据库中所有的表

select tables .table_name from INFORMATION_SCHEMA.TABLES where TABLE_TYPE='base table'

--查询 表中的名字 是否为空 类型

select COLUMNS .column_name,columns.is_nullable, columns.data_type from INFORMATION_SCHEMA.COLUMNS where COLUMNS.TABLE_NAME='Area'

***************************************************************************************************************************

代码：

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--直接创建数据库（不带任何参数）

Create  database MyDatabaseOne;

​    ![0](https://note.youdao.com/yws/res/7982/5991265E7BA3405AB3C7A3A7403294EB)

--删除数据库

drop database MyDatabaseOne;

​    ![0](https://note.youdao.com/yws/res/7984/36E7897D76ED4E26938AB8CACC4DE368)

--创建数据库的时候设置一些参数

create database MyDatabaseOne

on primary

(

​	--配置主数据文件的选项

​	name ='MyDatabaseOne',--主数据文件的逻辑名称

​	filename='d:\MydatabaseOne.mdf',--保存路径

​	size=5mb,

​	maxsize=10mb,

​	filegrowth=20%

)

log on

(

​	--配置日志文件的选项

​	name ='MyDatabaseOne_log',

​	filename='d:\MydatabaseOne_log.ldf',--保存路径

​	size=5mb,

​	filegrowth=5mb

)

--创建表

use MyDatabaseOne

create table Mytable1

(

​	AutoID int identity(1,1)  primary key,

​	Name nvarchar (50) not null,

)

--删除表

use MyDatabaseOne

drop  table Mytable1

--创键员工表

use MyDatabaseOne

create table Employees

(

​	EmpID int identity (1,1) primary key,--identity 自动增量

​	name nvarchar not null

)

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

增删改查

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

增》》》》》》》》》》》》

--向员工表插入数据

--语法

--insert into 表名 （列1，列2）values （值1，值2）

--insert into 表名 （列1，列2）output insrteed . 列  values （值1，值2）

insert into  [dbo].[Employees] 

(name)

 values('张三')

select name '姓名' 

from [dbo].[Employees]

--如果表中除了自动编号以外的所有列

--都要插入值，那么可以省略列明,

--但值要一一对应

insert into [dbo].[Employees]

values ('李四')

select name '姓名'

from [dbo].[Employees]

--向自动编号插入值

--启用某个表的‘自动编号列’手动插入值功能

set identity_insert [dbo].[Employees] on

insert into [dbo].[Employees] (EmpID,name)

values (500,N'王五')--N防止乱码

set identity_insert  [dbo].[Employees] off

select *

from [dbo].[Employees]

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--更新语句：

--语法

--update 表名 set 列1=值1，列2=值2....where 条件

select * from [dbo].[Employees]

update  [dbo].[Employees] set  [age]=age-1 ,name=name+'女' where age<20 or name='张三'

select * from [dbo].[Employees]

--where  （age>20 and age <30）or(age>50)

--where中可以使用的其他逻辑运算符 or and not > < <= >= <>(!=)等

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--数据删除

--语法

--delete from 表名 where.....

--不加where 条件 直接删除所有数据

delete from [dbo].[Employees] where  name='张三'

select *from [dbo].[Employees]

--全部删除数据

--1、delete from 表

--2、 truncate table 表 （推荐使用 ）

--truncate特点

--不能加where 条件

--自动编号恢复默认值

--比delete 效率高

--删除数据不触发delete 触发器

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

约束：保证数据的完整性

非空 约束

主键 约束 primary key  constraint  唯一且不为空 

唯一约束   unique constraint 唯一 允许为空 但只能出现一次

默认约束  default constraint 默认值

检查约束 check costraint 范围以及格式限制

外键约束 foreign key constraint  表关系

增加外键约束时，设置级联更新，级联删除：

[ on delete {no action | cascade| set null | set default}]

[ on update{no action | cascade| set null | set default}]

len()>10 

代码实现约束

--手动删除一列

--alter table 表名 drop column  列

alter table employees drop column empAddess

--手动增加一列 

--alter table 表  add 列

alter table employess add empAddess int not null

--手动修改一下empEmail的数据类型

--alter table 表 alter column 列 类型

alter table employess alter column empEmail varchar(1000) 

--为empid增加一个主键约束

--alter table 表名 add constraint PK_Empid（键名） primary key(列)

alter table employess add constraint PK_Empid primary key(empid)

--为empName增加一个非空约束

--alter table 表 alter column 列名 varchar(50) not null

alter table employess alter column empName varchar(50) not null

--为empName 增加一个唯一约束

--alter table 表 add constraint UQ_empNName unique (列)

alter table employess add constraint UQ_empNName unique (empName)

--为性别 增加一个默认约束 男

alter table employess add constraint df_empGender default('男') for empGender

--为年龄增加一个检查约束  年龄必须在0-120间

alter table employess add constraint CK_empAge

check (empAge>=0 and empAge<=120)

==========================================================================

--创建一个部门表 Department 然后增加一个DeptID

alter table enployees add DeptID int

--增加外键约束  必须要有主键列

alter table employess add constraint FK_Depid foreign key (DeptID)

references Department (DepID)

--删除约束

--alter  table employees drop constraint 约束名，约束名

alter  table employees drop constraint CK_empAge,FK_Depid

--通过一条代码增加多个约束

alter table employees add

constraint PK_Empid primary key(empid),

constraint UQ_empNName unique (empName)，

constraint df_empGender default('男') for empGender

--创建表的同时创建约束

create table employees

(

​	empid int identity (1,1) primary key,--主键

​	empName varchar(2)  not null unique check(len(empName)>2),--唯一约束 ，检查约束v 

​	empGennder varchar(1) default ('男'),--默认约束

​	empAge int check (empAge>0 and empAge<100),--检查约束

​	empEmail varchar(100) unique ,--唯一约束

​	empAddress nvarchar(200) not null,--非空约束

​	empDepId int foreign key references Department(DepID) on delete cascade

)

create table Department 

(

​	DepID int identity (1,1) primary key,

​	DepName varchar(50) not null unique

)

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

数据查询

create database db_School

use db_School

create table Tb_Student

(

​	stuID int identity (1,1) primary key,

​	stuName nvarchar(10)  unique  check (len(stuName)>2) not null,

​	stuGender nchar(2) default ('男') not null,

​	stuAge int check(stuAge>0 and stuAge<100),

​	stuAddress nvarchar(200) 

)

insert into [dbo].[Tb_Student] 

VALUES ('N张三','男',18,'N湖北襄阳')

insert into [dbo].[Tb_Student] 

VALUES ('N张四','男',18,'N湖北襄阳')

insert into [dbo].[Tb_Student] 

VALUES ('N张五','男',18,'N湖北襄阳')

insert into [dbo].[Tb_Student] 

VALUES ('N张六','男',18,'N湖北襄阳')

insert into [dbo].[Tb_Student] 

VALUES ('N张七','男',18,'N湖北襄阳')

insert into [dbo].[Tb_Student] 

VALUES ('N张八','男',18,'N湖北襄阳')

--数据检索

--检索所有数据

select * 

from[dbo].[Tb_Student]

--检索部分数据

select   stuID ,stuName,stuAge

from [dbo].[Tb_Student]

--条件查询

select * 

from [dbo].[Tb_Student] where stuAge<17

--为查询结果集中起别名

select 

​	stuID as 编号,

​	stuAge as 年龄,

​	stuAddress as 地址 

from [dbo].[Tb_Student]

select 

​	stuID '学生 编号',

​	stuAge  年龄,

​	stuAddress  地址 

from [dbo].[Tb_Student]

select 

​	  编号=stuID,

​	  年龄=stuAge,

​	  地址=stuAddress 

from [dbo].[Tb_Student]

select 

​	当前系统日期=GETDATE()

--去除重复结果 针对已经查询出来的结果然后去除重复

select distinct * from[dbo].[Tb_Student]

select distinct stuName,stuGender from [dbo].[Tb_Student]

--排序   

--order by 列名

--按照年龄，降序排列

select * from [dbo].[Tb_Student]order by stuAge desc

--按照年龄，升序排列

select * from [dbo].[Tb_Student]order by stuAge asc

select * from [dbo].[Tb_Student]order by stuAge --默认就是升序

--取前五名

select top 5 * from [dbo].[Tb_Student] order by stuAge desc

--去最低5名

select top  5 * from [dbo].[Tb_Student] order by stuAge asc

--取前10%

select top 30 percent  *from[dbo].[Tb_Student] order by stuAge asc

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

聚合函数

--聚合函数

--统计出所有年龄的总和

select SUM(stuAge) 年龄总和 from [dbo].[Tb_Student]

--统计一共有多少记录

select COUNT (*) 人数 from [dbo].[Tb_Student]

--求平均值

select 

平均年龄= (select SUM(stuAge) 年龄总和 from [dbo].[Tb_Student])/(select COUNT (*) 人数 from [dbo].[Tb_Student])

--用AVG求平均值

select 平均年龄=avg(stuAge) from [dbo].[Tb_Student]

--求年龄最大的

select 年龄最大=MAX(stuAge) from [dbo].[Tb_Student]

--求年龄最小的

select 年龄最小=Min(stuAge) from [dbo].[Tb_Student]

--聚合函数常见问题

--1、不统计空值

--2、如果使用聚合函数没有手动groud by 分组 默认为1组

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

条件查询 

--条件查询

--select 列

--from 表

--where 条件

--查询没有及格的学生 （英语或数学一门没及格就算没及格）

select *

from [dbo].[Tb_Score]

where English <60 or Math<60

--查询数学成绩在80-90之间的同学

select *from [dbo].[Tb_Score] where [English]>=80 and [English]<=90

select * from [dbo].[Tb_Score] where [English] between 80 and 90

--查询所有班级id 为3，4，5的那些学生

select * from  [dbo].[Tb_Student]  where id in (3,4,5)

--推荐使用 in 或or 效率低

select *from [dbo].[Tb_Student] where id >=3 and id <=5

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--模糊查询

--通配符 _  % [] ^  

--  _表示任意单个字符

--姓张，两个字的

select *from [dbo].[Tb_Student] where [stuName] like 'N张_'

--姓张，三个字的

select *from [dbo].[Tb_Student] where [stuName] like '张__'

--% 匹配任意多个字符

--只要姓张就查询出来

select * from [dbo].[Tb_Student] where [stuName] like '张%'

select * from [dbo].[Tb_Student] where[stuName] like '张%'and LEN([stuName])=3

--替换表中数据 

update [dbo].[Tb_Student] set stuName =REPLACE(stuName,'N','')

--[] 表示筛选，范围

--中间只能是数字

select * from [dbo].[Tb_Student] where [stuName] like '张[1-9]妹'

--中间是字母

select * from [dbo].[Tb_Student] where [stuName] like '张[a-z]妹'

--中间是字母和数字

select * from [dbo].[Tb_Student] where [stuName] like '张[1-9][a-z]妹'

--中间不是数字 '^'

select * from [dbo].[Tb_Student] where [stuName] like '张^[1-9]妹'

--只要不是张[0-9]妹

select * from [dbo].[Tb_Student] where [stuName] not like '张^[1-9]妹'

--中间可以是任意字符

select * from [dbo].[Tb_Student] where [stuName] like '张_妹'

--查询姓名中带%的人

select * from[dbo].[Tb_Student] where [stuName]  like '%[%]%'

--自己定义转义符

select * from[dbo].[Tb_Student] where [stuName]  like '%/[/]%' escape '/'

select * from[dbo].[Tb_Student] where [stuName]  like '%/[%' escape '/'

select * from[dbo].[Tb_Student] where [stuName]  like '%/]%' escape '/'

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--查询 年龄为空的同学

--null 值无法使用=或<> 号比较

--判断空值必须使用 is null 或 is not null

--任何值和null计算都是null

select * from  [dbo].[Tb_Student] where [stuName] is not null

select * from  [dbo].[Tb_Student] where [stuName] is  null

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--通过order by 进行排序

--1、降序order by 列名 desc 

--2、升序order by 列名  或  order 列名asc 

--3、order by 语句必须放在整个sql 的最后

--4、根据多列进行排序

--5、 根据表达式排序

select *from 表

where ...

gourp by....

order by

--先根据英语成绩排序 在根据数学成绩排序 （先按照英语成绩排序，当英语成绩相同的时候用数学成绩排序 ）

select * from [dbo].[Tb_Score] order by [English]desc ,[Math]desc

--表达式

select

 \* ,

 平均分=(English+Math)*1.0/2

 from [dbo].[Tb_Score] order by 平均分 desc

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--分组

--查询每个级地址 和所在地人数

select 

[stuAddress] 地址,

人数=COUNT(*)

 from [dbo].[Tb_Student]

 group by [stuAddress]

 --请统计出班级中的男女人数

 select [stuGender],

 人数=count(*)

 from [dbo].[Tb_Student]

 group by [stuGender]

--查询每个班级地址 和所在地男同学人数

 select [stuAddress] ,COUNT(*)

 from [dbo].[Tb_Student] 

 where [stuGender]='男'

 group by [stuAddress]

 --对分组以后的数据进行筛选 使用having

 --having和where都是对数据进行筛选 where是对分组前的每一行数据进行筛选，

 --而having是对分组后的每一组数据进行筛选  

 select 地址=[stuAddress],

 班级人数=COUNT(*)

 from [dbo].[Tb_Student] 

 group by [stuAddress]

 having COUNT(*)>5

 --select 语句 的处理顺序

 from 

 on

 join

 where

 group by

 with gube  with rollup

 having

 select

 distlnct

order by

top

分组练习

--热销商品排名

select

​	 [商品名称],

​	sum([销售数量]) as [销售数量]

from [dbo].[dingdan]

group by [商品名称]

 

order by 销售数量 desc

--请统计 销售总价超过300元的商品 名称 和销售价格 并按销售总价排序

select 

​	[商品名称],

​	销售总价=SUM([销售数量]*[销售价格])

from [dbo].[dingdan]

group by[商品名称]

having SUM([销售数量]*[销售价格])>300

order by 销售总价 desc

--统计每个客户对可口可乐的喜爱度

select 

[购买人],购买数量=sum([销售数量])

​	

from [dbo].[dingdan]

where [商品名称]='可口可乐'

group by [购买人]

 order by  购买数量 desc

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

插入多条数据

SELECT 

​	[商品名称] ,

​	销售总价 =SUM([销售数量]*[销售价格])

from [dbo].[dingdan]

group by [商品名称]

union all

select '总销售价格', SUM([销售数量]*[销售价格]) from [dbo].[dingdan]

--要查询成绩中的最高分最低分 平均分

select  最高分=MAX([Math]),

​		最低分=MIN([Math]),

​		平均分=AVG([Math])

from [dbo].[Tb_Score]

--使用union 向表中插入多条数据

select * from[dbo].[Tb_Student]

insert into [dbo].[Tb_Student]

select '张小马1','男',15,'北京海淀区'

union all

select '张小马2','男',15,'北京朝阳区'

union all

select '张小马3','女',15,'北京海淀区'

union all

select '张小马4','男',15,'澳门区'

union all

select '张小马5','女',15,'香港'

select * from[dbo].[Tb_Student]

----备份表数据

select * into stunnt from [dbo].[Tb_Student]

select * from [dbo].[Tb_Student]

--只备份 表结构

select top 0 * into stunnt from [dbo].[Tb_Student]

--向现有的表插入数据

insert into [dbo].[stunnt]

select stuName ,stuGender,stuAge,stuAddress

from [dbo].[Tb_Student]

 

 select * from  [dbo].[stunnt]

--常用字符串函数

--l len（）计算字符的个数 

print len ('HI-最近好吗')

--datalength () 返回占用的字节个数，这个不是字符串函数

print datalength('HI-最近好吗')

--转大写

print upper ('hell')

--转小写

print lower ('WHLLL')

--去空格

print '================='+ltrim('       hellow          ')+ '============' --去左边空格

print  '================='+rtrim('       hellow          ')+ '============' --去右边空格

print  '================='+rtrim(ltrim('       hellow          '))+ '============' --去左右边空格

--4字符串截取字符串

print left('中华人名共和国',2)

print right('关闭设备对v表示',2) 

print substring ('v诡异的事故vbus不v比赛吧vi不断思考吧',1,10)

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--日期函数

print getdate()

print sysdatetime()

--增加时间

select  dateadd(day,200,getdate())

select  dateadd(month,200,getdate())

select  dateadd(year,200,getdate())

select  dateadd(MINUTE,200,getdate())

--请查询出入职一年以上的员工信息

select *

from [dbo].[Tb_Student]

where DATEADD (year,1,[joindame])<=GETDATE()

--统计n年的人的个数

select *,工龄=datediff (year ,joindame,GETDATE())

from [dbo].[Tb_Student]

group by datediff (year ,joindame,GETDATE())

select 工龄=datediff (year ,joindame,GETDATE()),人数=COUNT(*)

from [dbo].[Tb_Student]

group by datediff (year ,joindame,GETDATE())

--计算两个日期的差 datediff

select * from [dbo].[Tb_Student]

select '年龄'=datediff (year ,joindame,GETDATE())

from [dbo].[Tb_Student]

--获取日期的部分

print datepart (year,getdate())

print datepart (month,getdate())

print month (getdate())

print datepart (hour,getdate())

print datepart (minute,getdate())

print datepart (second,getdate())

--一年中的第几天 

print datepart (dayofyear,getdate())

--返回日期的字符串形式

print datename (year ,getdate())

--不同年份入职的员工个数

select 入职年份=datepart (YEAR,[joindame]),人数=count (*)

from [dbo].[Tb_Student]

group by datepart (YEAR,[joindame])

--日期函数练习

select * from [dbo].[通话表]

--输出所有数据中通话时间最长的5条记录 order by  datediff

select top 5 *,通话时长= datediff(SECOND,[startDateTime],[EndDateTime])

from [dbo].[通话表]

--group by datediff(SECOND,[startDateTime],[EndDateTime])

order by 通话时长 desc

--输出所有数据中拨打长途号吗？（以0开头的）总时长 like sum 

select 通话总时长=sum(datediff(SECOND,[startDateTime],[EndDateTime]))

from[dbo].[通话表]

where telNum like '0%'

--假设今天是2010-07-31

--输出本月通话总时长最多的前三个呼叫员的编号

select top 3[callerNumber] , 通话总时长=sum(datediff(SECOND,[startDateTime],[EndDateTime]))

from [dbo].[通话表]

where  datepart (year,[startDateTime])=2010 and datepart (month,[startDateTime])=7

group by [callerNumber]

order by 通话总时长 desc

--输出本月拨打次数最多的额前三个呼叫员 

select top 3 [callerNumber], 次数=COUNT(*) 

from [dbo].[通话表]

where datepart (year,[startDateTime])=2010 and datepart (month,[startDateTime])=7

group by[callerNumber]

order by 次数 desc 

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

内连接

select * from [dbo].[PhoneType]

select * from [dbo].[PhoneNum]

--当需要将多个表中的列共同显示在一个结果集中 使用链接查询、

select *

from [dbo].[PhoneNum]

inner join [dbo].[PhoneType] on [dbo].[PhoneNum].pTypeId=[dbo].[PhoneType].pld

--如果比表中有重名的列 可以加     表名.列名

select 

​	ph.pId,

​	ph.pName,

​	ph.pCellPhone,

​	pt.ptName

from [dbo].[PhoneNum]as ph

inner join [dbo].[PhoneType] as pt on ph.pTypeId=pt.pld

---查询时起别名

select 

​	ph.pId,

​	ph.pName,

​	ph.pCellPhone,

​	pt.ptName

from [dbo].[PhoneNum]as ph

inner join [dbo].[PhoneType] as pt on ph.pTypeId=pt.pld

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--case 语句  

--相当于if else

select *,

头衔=case

 when levelo=1 then'菜鸟'

 when levelo=2 then'小白'

 when levelo=3 then'教师'

 when levelo=4 then'教授'

 else '大师'

end

 from[dbo].[case]

 -- switch

 select *,

头衔=case levelo

​	when 1 then '菜鸟'

​	when 2 then'老鸟'

​	when 3 then'大师'

 

end

 from[dbo].[case]

 ----统计每个销售员的总销售金额 列出销售员名 总金额 称号（>1000金牌>300银牌其他铜牌）

 select  销售员 ,

​		总金额= sum(销售数量*销售价格),

​		称号 =case

​		 when sum(销售数量*销售价格)>1000 then '金牌'

​		 when sum(销售数量*销售价格)>300 then '银牌'

​		 else '铜牌'

​	     end

from Myorders

group by 销售员

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--索引-----

--1、索引的目的：提高查询效率

--聚集索引   只能有一个

--非聚集索引  可以有多个

--增加索引后会增加额外的存储空间 同时降低增加、修改 、删除的效率

select  c3,c4 from [dbo].[TestIndex1002]

where c4>300 and c4<1500 order by c4 asc

--创建索引

create clustered index IXc4 on [dbo].[TestIndex1002](c4)

--删除索引

drop index [dbo].[TestIndex1002].IXc4

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

----独立子查询

select * from [dbo].[PhoneNum] where[pTypeId]=

(select [pld] from  PhoneType where [ptName]='朋友')

--相关子查询

select * from [dbo].[PhoneNum] as pn where

 exists

(select * from  PhoneType as pt where pn.pTypeId= pt.pld and pt.ptName=  '朋友')

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--分页查询首先确定按照什么排序，然后才能确定哪些记录应该在第一页，哪些记录在第二页

select * from [dbo].[ContentInfo]

--每页显示7条数据  使用top

--第一页

select top 7* from [dbo].[ContentInfo] order by dId asc

--第2页 思路： 先查询出（2-1）页的数据dId

select top 7 * from [dbo].[ContentInfo] where dId not in(

select top(7*(2-1)) dId from [dbo].[ContentInfo] order by dId asc) 

order by dId asc

--第3页

select top 7 * from [dbo].[ContentInfo] where dId not in(

select top(7*(3-1)) dId from [dbo].[ContentInfo] order by dId asc)

order by dId asc

--使用row_number()实现分页

-- 先排序 然后编号

select * ,rn=ROW_NUMBER() over(order by [dId] asc)from [dbo].[ContentInfo] 

--根据用户查看的每页记录条数 以及查看第几页 确定应该查询第几条到第几条

--每页显示7页  要看第八也

--从（8-1）*7+1...8*7

select *

 from

(

select * ,rn=ROW_NUMBER() over(order by [dId] asc)

from [dbo].[ContentInfo] ) as t 

where t.rn between (3-1)*7+1 and 3*7

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--左外连接

--left outer join

select 

t1.*,

t2.*

from Tblstudent as t1 left join TblScore as t2 on t1.tsid=t2.tsid 

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--视图--只能写查询语句--对查询语句的一个封装简化sql代码的编写只需要一个视图名称就可以了

--如果视图中的查询语句中包含了重名的列，此时必须需要起别名

create view vw_s //创建视图

as

select * from(select * ,rn=ROW_NUMBER() over(order by [dId] asc)from [dbo].[ContentInfo] ) as t where t.rn between (3-1)*7+1 and 3*7 //查询语句

select * from vw_s

--如果需要排序的时候不能再语句中直接写排序语句，可以在创建完视图后再排序

select * from vw-s order by ..

--T_SQL编程

--1、声明变量  --declare @变量名称   数据类型（50） 

declare @name nvarchar (50)

declare @age int 

--2、赋值

①set @name='刘亚龙'

②select @age=18

--3、输出

select '姓名',@name

select '年龄' ,@age 

--声明多个变量

--declare @name nvarchar (50),@age int

​    ![0](https://note.youdao.com/yws/res/965/C17298DEFA254AC39C39ECD8A95AE302)

--where 循环

declare @i  int = 0--声明加赋值

while @i<100	

begin

print 'hellow'

set @i+=1

end

​    ![0](https://note.youdao.com/yws/res/972/B5E41527C7834C2384A0C66F9547B53B)

--计算1-100的和

declare @ii int =1 ,@sum int =0

while @ii<=100

begin

set @sum+=@ii

set @ii+=1

end

print @sum

​    ![0](https://note.youdao.com/yws/res/975/09C09AFD842F49218DD9978A88BDE19A)

--if语句

declare @n int =10;

if(@n>10)

begin

print '@n>10'

end

else	if @n>5

begin

print '@n>5'

end

else

begin

print '@n>0'

end

​    ![0](https://note.youdao.com/yws/res/978/38B90B9AF62743FEAA02710F763F9D07)

--计算1-100之间所有基数的和

--定义变量

declare @sum int =0, @i int =1

--循环累加基数和

while @i<=100

begin

​	if @i % 2 <> 0

​	begin

​		set @sum += @i

​	end

​	set @i = @i+1

end

print @sum

​    ![0](https://note.youdao.com/yws/res/982/EB22B001FF534720B4FCDC53113815DA)

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--转账之前需要用 if else 判断是否大于10 避免报异常

--事物转账

--保证两条sql语句同时执行成功或者同时失败呢

select * from [dbo].[bank]

update bank set balance-=100  where cid='10001'

update bank set balance+=100  where cid='1'

--打开一个事物

begin transaction

declare @sum int=0

update bank set balance-=100  where cid='10001'

set @sum=@sum+@@ERROR

update bank set balance+=100  where cid='1'

set @sum=@sum+@@ERROR --一条sql语句出错 最后@sum就不是0

if @sum<>0

begin

--表示出错 回滚

rollback

end

else

begin

--表示没有出错 提交

commit

end

​    ![0](https://note.youdao.com/yws/res/1085/6601C6D9915049A7AE1A0BD2B79CC7FD)

--自动提交事物

--数据库打开一个事物 执行成功提交 否则失败

--隐士事物 自动打开事物 但要手动提交 回滚

set implicit_transactions {on | off} 隐士事物

​    ![0](https://note.youdao.com/yws/res/1091/AC00F784C03A4963B0FD7B4EB019CC39)

--显示事物

需要手动打开事物 手动提交 回滚事物

begin tran

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--系统存储过程

​    ![0](https://note.youdao.com/yws/res/1098/495E219C6E984EDF8C18E41C4E4BF875)

​    ![0](https://note.youdao.com/yws/res/1102/7534E7BA1031441D80ED54C31B3D8DA3)

--查询服务器上的所有数据库

exec sp_databases 

--报告有关指定数据库或所有数据库的信息

exec sp_help 

--返回当前数据库下的所有表

exec sp_tables

--返回某张表下的所有列

exec sp_columns 'Book'

​    ![0](https://note.youdao.com/yws/res/1100/8EE019CEB71A4463B14BFDBE7B826CCC)

--获取某个存储过程源代码

exec sp_helptext 'sp_databases' 

create procedure sys.sp_databases

as

​    set nocount on

​    select

​        DATABASE_NAME   = db_name(s_mf.database_id),

​        DATABASE_SIZE   = convert(int,

​                                    case -- more than 2TB(maxint) worth of pages (by 8K each) can not fit an int...

​                                    when sum(convert(bigint,s_mf.size)) >= 268435456

​                                    then null

​                                    else sum(convert(bigint,s_mf.size))*8 -- Convert from 8192 byte pages to Kb

​                                    end),

​        REMARKS         = convert(varchar(254),null)

​    from

​        sys.master_files s_mf

​    where

​        s_mf.state = 0 and -- ONLINE

​        has_dbaccess(db_name(s_mf.database_id)) = 1 -- Only look at databases to which we have access

​    group by s_mf.database_id

​    order by 1

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

​	--创建自己的存储过程

​	create proc usp_hello

​	as

​	begin

​	print 'hello'

​	end

​	--修改存储过程

​	alter proc usp_hello

​	as

​	begin

​	print 'yes'

​	end

​	--删除 存储过程

​	drop proc usp_hello

​	exec  usp_hello

​    ![0](https://note.youdao.com/yws/res/1106/AE1FB8F536854285935AB8DFB92FBD9B)

​	--创建一个带两个参数的存储过程

​	create proc usp_add_number

​	@n1 int,@n2 int

​	as

​	begin

​		select @n1+@n2

​	end

​	exec usp_add_number 1 ,2

​    ![0](https://note.youdao.com/yws/res/1104/18C4D12B2A584BDBBE606AC41A3E0537)

​	select * from [dbo].[Student]

​	create proc usp_age_gender

​	@StuAge int ,@StuGennder  nvarchar

​	as

​	begin 

​		select * from [dbo].[Student] where StuAge=@StuAge and StuGender= @StuGennder

​	end

​	exec usp_age_gender @StuAge=20 , @StuGennder='男'

​	--带输出参数的存储过程

​	create proc usp_show_students

​	@gender nvarchar(4),

​	@count int  output --输出参数

​	as

​	begin

​		select * from [dbo].[Student] where StuGender=@gender

​		--吧总条数赋值给 变量count

​		set @count = (select count (*) from [dbo].[Student] where StuGender=@gender)

​	end 

​	declare @cou int

​	exec usp_show_students @gender ='男', @count=@cou output

​	print @cou

​    ![0](https://note.youdao.com/yws/res/1111/143AD59C7CB04F5886A20B46A6E4641A)

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

​	--使用存储过程实现分页查询

​	select* from [dbo].[ContentInfo]

​	create proc usp_getCategory

​	@pagesize int =7,--每页记录条数

​	@pageindex int =1,--当前查看第几页记录

​	@recount int output , --总的记录条数

​	@pagecount int output--总的页数

​	

​	as 

​	begin

​	--编写查询语句

​	  select *

​	  from (select * ,rn = ROW_NUMBER () over ( order by dId asc)  from [dbo].[ContentInfo] )as t

​	  where t.rn between (@pageindex-1)*@pagesize+1 and @pagesize*@pageindex

​	  --计算总的记录条数

​	  set @recount =(select count(*) from [dbo].[ContentInfo])

​	  --计算总页数

​	  set @pagecount= CEILING( @recount*1.0/@pagesize)

​	end  

​	declare @rcount int ,@pagcount int 

​	exec usp_getCategory @pageindex=3 ,@recount=@rcount output , @pagecount=@pagcount output

​	PRINT @rcount

​	print @pagcount

​    ![0](https://note.youdao.com/yws/res/1114/388642F4998E4103BF4A16E55CFC57B1)

​    ![0](https://note.youdao.com/yws/res/1116/67EF17DAD48145CF8BE5BD8053BDF6CD)

​    ![0](https://note.youdao.com/yws/res/1118/0EAA7F9E3CF449C495BAAA36E88B148C)

​    ![0](https://note.youdao.com/yws/res/1120/CEFB13238CCB49478644CE77E7BDE452)

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--创建存储过程实现转账

create proc usp_money

@from char(4),

@to char(4),

@balance money,

@resultnumber int output --转账是否成功(1表示成功2表示失败3表示余额不足)

as 

begin

--判断金额是否足够转账

declare @money money

select @money=  balance from bank where cid =@from

if @money -@balance>=0

begin

​		--打开一个事物

​		begin tran

​		declare @sum int =0

​			--账户1扣钱

​		update bank set balance-=@balance where cid=@from 

​		set @sum+=@@ERROR

​			--账户2加钱

​			update bank set balance+=@balance where cid=@to 

​			set @sum+=@@ERROR

​			--判断是否成功

​			if	@sum<>0

​			begin

​			set @resultnumber=2--转账失败

​			rollback

​			end

​			else

​			begin

​				set @resultnumber=1--转账成功

​				commit 

​			end

end

else

begin

set @resultnumber=3--余额不足

end

end 

select * from bank

declare @result int 

exec usp_money '1001','1002', 100, @result output

print @result

​    ![0](https://note.youdao.com/yws/res/1122/39587DE48CEC4D168FE1656BA03EE23C)

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--触发器

select * from [dbo].[PhoneNum]

select * from deletePhone

--创建一个和PhoneNum一样表结构的表

select  top 0 * into deletePhone from PhoneNum

--创建一个触发器

create trigger tri_delete_Phone on PhoneNum

after delete 

as

begin

​	insert into deletePhone(pTypeId,pName,pCellPhone,pHomePhone)

​	select pTypeId,pName,pCellPhone,pHomePhone from deleted

end

delete from[dbo].[PhoneNum] where pId=8

|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

当通过set 为变量赋值的时候 如果返回语句不止一个值 报错

当通过set 为变量赋值的时候 如果返回语句不止一个值 将最后一个值返回