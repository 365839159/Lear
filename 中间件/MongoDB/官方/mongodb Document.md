[TOC]

# 入门

## 1、查看数据库

在shell中，db指的是您当前的数据库，键入db显示当前的数据库。操作返回test 这是默认的数据库。

```sql
> db

test
```

## 2、 切换数据库

切换数据库键入 use [db] 。例如需要切换到 examples 数据库：

```
> use examples

switched to db examples
```

键入db

```
> db

examples
```

无需再切换前创建数据库，当你第一次再该数据库中存入数据时，mongodb会创建该数据库。

## 3、添加数据

MongoDB 将文档存储在[集合中](https://docs.mongodb.com/manual/core/databases-and-collections/)。集合类似于关系数据库中的表。如果集合不存在，MongoDB 会在您第一次存储该集合的数据时创建该集合。

再shell 中键入 db.[collection].insertMany() 将文档添加到集合中，这里示例使用 movies 集合。

```json
>db.movies.insertMany([
   {
      title: 'Titanic',
      year: 1997,
      genres: [ 'Drama', 'Romance' ],
      rated: 'PG-13',
      languages: [ 'English', 'French', 'German', 'Swedish', 'Italian', 'Russian' ],
      released: ISODate("1997-12-19T00:00:00.000Z"),
      awards: {
         wins: 127,
         nominations: 63,
         text: 'Won 11 Oscars. Another 116 wins & 63 nominations.'
      },
      cast: [ 'Leonardo DiCaprio', 'Kate Winslet', 'Billy Zane', 'Kathy Bates' ],
      directors: [ 'James Cameron' ]
   },
   {
      title: 'The Dark Knight',
      year: 2008,
      genres: [ 'Action', 'Crime', 'Drama' ],
      rated: 'PG-13',
      languages: [ 'English', 'Mandarin' ],
      released: ISODate("2008-07-18T00:00:00.000Z"),
      awards: {
         wins: 144,
         nominations: 106,
         text: 'Won 2 Oscars. Another 142 wins & 106 nominations.'
      },
      cast: [ 'Christian Bale', 'Heath Ledger', 'Aaron Eckhart', 'Michael Caine' ],
      directors: [ 'Christopher Nolan' ]
   },
   {
      title: 'Spirited Away',
      year: 2001,
      genres: [ 'Animation', 'Adventure', 'Family' ],
      rated: 'PG',
      languages: [ 'Japanese' ],
      released: ISODate("2003-03-28T00:00:00.000Z"),
      awards: {
         wins: 52,
         nominations: 22,
         text: 'Won 1 Oscar. Another 51 wins & 22 nominations.'
      },
      cast: [ 'Rumi Hiiragi', 'Miyu Irino', 'Mari Natsuki', 'Takashi Naitè' ],
      directors: [ 'Hayao Miyazaki' ]
   },
   {
      title: 'Casablanca',
      genres: [ 'Drama', 'Romance', 'War' ],
      rated: 'PG',
      cast: [ 'Humphrey Bogart', 'Ingrid Bergman', 'Paul Henreid', 'Claude Rains' ],
      languages: [ 'English', 'French', 'German', 'Italian' ],
      released: ISODate("1943-01-23T00:00:00.000Z"),
      directors: [ 'Michael Curtiz' ],
      awards: {
         wins: 9,
         nominations: 6,
         text: 'Won 3 Oscars. Another 6 wins & 6 nominations.'
      },
      lastupdated: '2015-09-04 00:22:54.600000000',
      year: 1942
   }
])
```

键入回车，该操作返回一个包含确认指示符的文档和一个包含`_id`每个成功插入文档的数组 ：

```json
{
        "acknowledged" : true,
        "insertedIds" : [
                ObjectId("6146e76987f8346ec8e1ac9d"),
                ObjectId("6146e76987f8346ec8e1ac9e"),
                ObjectId("6146e76987f8346ec8e1ac9f"),
                ObjectId("6146e76987f8346ec8e1aca0")
        ]
}
```

## 4、查找所有数据

使用 db.[collection].find() 方法进行查找。

要选择集合中的所有文档，请将一个空文档作为[查询过滤器文档](https://docs.mongodb.com/manual/core/document/#std-label-document-query-filter)传递给该方法。

```json
> db.movies.find({})

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9d"), "title" : "Titanic", "year" : 1997, "genres" : [ "Drama", "Romance" ], "rated" : "PG-13", "languages" : [ "English", "French", "German", "Swedish", "Italian", "Russian" ], "released" : ISODate("1997-12-19T00:00:00Z"), "awards" : { "wins" : 127, "nominations" : 63, "text" : "Won 11 Oscars. Another 116 wins & 63 nominations." }, "cast" : [ "Leonardo DiCaprio", "Kate Winslet", "Billy Zane", "Kathy Bates" ], "directors" : [ "James Cameron" ] }
{ "_id" : ObjectId("6146e76987f8346ec8e1ac9e"), "title" : "The Dark Knight", "year" : 2008, "genres" : [ "Action", "Crime", "Drama" ], "rated" : "PG-13", "languages" : [ "English", "Mandarin" ], "released" : ISODate("2008-07-18T00:00:00Z"), "awards" : { "wins" : 144, "nominations" : 106, "text" : "Won 2 Oscars. Another 142 wins & 106 nominations." }, "cast" : [ "Christian Bale", "Heath Ledger", "Aaron Eckhart", "Michael Caine" ], "directors" : [ "Christopher Nolan" ] }
{ "_id" : ObjectId("6146e76987f8346ec8e1ac9f"), "title" : "Spirited Away", "year" : 2001, "genres" : [ "Animation", "Adventure", "Family" ], "rated" : "PG", "languages" : [ "Japanese" ], "released" : ISODate("2003-03-28T00:00:00Z"), "awards" : { "wins" : 52, "nominations" : 22, "text" : "Won 1 Oscar. Another 51 wins & 22 nominations." }, "cast" : [ "Rumi Hiiragi", "Miyu Irino", "Mari Natsuki", "Takashi Naitè" ], "directors" : [ "Hayao Miyazaki" ] }
{ "_id" : ObjectId("6146e76987f8346ec8e1aca0"), "title" : "Casablanca", "genres" : [ "Drama", "Romance", "War" ], "rated" : "PG", "cast" : [ "Humphrey Bogart", "Ingrid Bergman", "Paul Henreid", "Claude Rains" ], "languages" : [ "English", "French", "German", "Italian" ], "released" : ISODate("1943-01-23T00:00:00Z"), "directors" : [ "Michael Curtiz" ], "awards" : { "wins" : 9, "nominations" : 6, "text" : "Won 3 Oscars. Another 6 wins & 6 nominations." }, "lastupdated" : "2015-09-04 00:22:54.600000000", "year" : 1942 }
```

## 5、过滤数据

对于相等匹配 ( `<field>`equals `<value>`)，`<field>: <value>`在[查询过滤器文档中](https://docs.mongodb.com/manual/core/document/#std-label-document-query-filter)指定并传递给 [`db.collection.find()`](https://docs.mongodb.com/manual/reference/method/db.collection.find/#mongodb-method-db.collection.find)方法

查找 title 为 Titanic 的电影

```json
> db.movies.find({"title":"Titanic"})

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9d"), "title" : "Titanic", "year" : 1997, "genres" : [ "Drama", "Romance" ], "rated" : "PG-13", "languages" : [ "English", "French", "German", "Swedish", "Italian", "Russian" ], "released" : ISODate("1997-12-19T00:00:00Z"), "awards" : { "wins" : 127, "nominations" : 63, "text" : "Won 11 Oscars. Another 116 wins & 63 nominations." }, "cast" : [ "Leonardo DiCaprio", "Kate Winslet", "Billy Zane", "Kathy Bates" ], "directors" : [ "James Cameron" ] }
```

可以使用[比较运算符](https://docs.mongodb.com/manual/reference/operator/query-comparison/#std-label-query-selectors-comparison) 来执行更高级的查询：

- 查找2000年之前的电影

```json
> db.movies.find({"released":{$lt:ISODate("2000-01-01")}})

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9d"), "title" : "Titanic", "year" : 1997, "genres" : [ "Drama", "Romance" ], "rated" : "PG-13", "languages" : [ "English", "French", "German", "Swedish", "Italian", "Russian" ], "released" : ISODate("1997-12-19T00:00:00Z"), "awards" : { "wins" : 127, "nominations" : 63, "text" : "Won 11 Oscars. Another 116 wins & 63 nominations." }, "cast" : [ "Leonardo DiCaprio", "Kate Winslet", "Billy Zane", "Kathy Bates" ], "directors" : [ "James Cameron" ] }
{ "_id" : ObjectId("6146e76987f8346ec8e1aca0"), "title" : "Casablanca", "genres" : [ "Drama", "Romance", "War" ], "rated" : "PG", "cast" : [ "Humphrey Bogart", "Ingrid Bergman", "Paul Henreid", "Claude Rains" ], "languages" : [ "English", "French", "German", "Italian" ], "released" : ISODate("1943-01-23T00:00:00Z"), "directors" : [ "Michael Curtiz" ], "awards" : { "wins" : 9, "nominations" : 6, "text" : "Won 3 Oscars. Another 6 wins & 6 nominations." }, "lastupdated" : "2015-09-04 00:22:54.600000000", "year" : 1942 }
```

- 查找获得奖项超过100的电影

```json
> db.movies.find({"awards.wins":{$gt:100}})

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9d"), "title" : "Titanic", "year" : 1997, "genres" : [ "Drama", "Romance" ], "rated" : "PG-13", "languages" : [ "English", "French", "German", "Swedish", "Italian", "Russian" ], "released" : ISODate("1997-12-19T00:00:00Z"), "awards" : { "wins" : 127, "nominations" : 63, "text" : "Won 11 Oscars. Another 116 wins & 63 nominations." }, "cast" : [ "Leonardo DiCaprio", "Kate Winslet", "Billy Zane", "Kathy Bates" ], "directors" : [ "James Cameron" ] }
{ "_id" : ObjectId("6146e76987f8346ec8e1ac9e"), "title" : "The Dark Knight", "year" : 2008, "genres" : [ "Action", "Crime", "Drama" ], "rated" : "PG-13", "languages" : [ "English", "Mandarin" ], "released" : ISODate("2008-07-18T00:00:00Z"), "awards" : { "wins" : 144, "nominations" : 106, "text" : "Won 2 Oscars. Another 142 wins & 106 nominations." }, "cast" : [ "Christian Bale", "Heath Ledger", "Aaron Eckhart", "Michael Caine" ], "directors" : [ "Christopher Nolan" ] }
```

- 查询有 japanese, Mandarin 两种语言的电影

```json
> db.movies.find({"languages":{$in:["japanese","Mandarin"]}})

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9e"), "title" : "The Dark Knight", "year" : 2008, "genres" : [ "Action", "Crime", "Drama" ], "rated" : "PG-13", "languages" : [ "English", "Mandarin" ], "released" : ISODate("2008-07-18T00:00:00Z"), "awards" : { "wins" : 144, "nominations" : 106, "text" : "Won 2 Oscars. Another 142 wins & 106 nominations." }, "cast" : [ "Christian Bale", "Heath Ledger", "Aaron Eckhart", "Michael Caine" ], "directors" : [ "Christopher Nolan" ] }
```

## 5、投影

使用db.[collection].find(<query doc>,<projection doc> ) 返回指定的字段

`<field>: 1` 返回的文档中包含这个字段

`<field>: 0` 返回文档中的排除这个字段



- 返回 title ，directors ，year （_id 默认是返回的）

```json
> db.movies.find( { }, { "title": 1, "directors": 1, "year": 1 } );

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9d"), "title" : "Titanic", "year" : 1997, "directors" : [ "James Cameron" ] }
{ "_id" : ObjectId("6146e76987f8346ec8e1ac9e"), "title" : "The Dark Knight", "year" : 2008, "directors" : [ "Christopher Nolan" ] }
{ "_id" : ObjectId("6146e76987f8346ec8e1ac9f"), "title" : "Spirited Away", "year" : 2001, "directors" : [ "Hayao Miyazaki" ] }
{ "_id" : ObjectId("6146e76987f8346ec8e1aca0"), "title" : "Casablanca", "directors" : [ "Michael Curtiz" ], "year" : 1942 }
```

- 返回 title ，year 排除_id

```json
>db.movies.find( { }, { "_id":0,"title": 1, "year": 1 } );

{ "title" : "Titanic", "year" : 1997 }
{ "title" : "The Dark Knight", "year" : 2008 }
{ "title" : "Spirited Away", "year" : 2001 }
{ "title" : "Casablanca", "year" : 1942 }
```

## 7、 聚合

计算每个`genre`中的值出现次数。

```json
>db.movies.aggregate( [
   { $unwind: "$genres" },
   {
     $group: {
       _id: "$genres",
       genreCount: { $count: { } }
     }
   },
   { $sort: { "genreCount": -1 } }
] )
```

```json
{ "_id" : "Drama", "genreCount" : 3 }
{ "_id" : "Romance", "genreCount" : 2 }
{ "_id" : "War", "genreCount" : 1 }
{ "_id" : "Action", "genreCount" : 1 }
{ "_id" : "Crime", "genreCount" : 1 }
{ "_id" : "Animation", "genreCount" : 1 }
{ "_id" : "Family", "genreCount" : 1 }
{ "_id" : "Adventure", "genreCount" : 1 }
```

- [`$unwind`](https://docs.mongodb.com/manual/reference/operator/aggregation/unwind/#mongodb-pipeline-pipe.-unwind)为`genres`数组中的每个元素输出一个文档。

```json
> db.movies.aggregate([
... {$unwind:"$genres"}])

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9d"), "title" : "Titanic", "year" : 1997, "genres" : "Drama", "rated" : "PG-13", "languages" : [ "English", "French", "German", "Swedish", "Italian", "Russian" ], "released" : ISODate("1997-12-19T00:00:00Z"), "awards" : { "wins" : 127, "nominations" : 63, "text" : "Won 11 Oscars. Another 116 wins & 63 nominations." }, "cast" : [ "Leonardo DiCaprio", "Kate Winslet", "Billy Zane", "Kathy Bates" ], "directors" : [ "James Cameron" ] }

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9d"), "title" : "Titanic", "year" : 1997, "genres" : "Romance", "rated" : "PG-13", "languages" : [ "English", "French", "German", "Swedish", "Italian", "Russian" ], "released" : ISODate("1997-12-19T00:00:00Z"), "awards" : { "wins" : 127, "nominations" : 63, "text" : "Won 11 Oscars. Another 116 wins & 63 nominations." }, "cast" : [ "Leonardo DiCaprio", "Kate Winslet", "Billy Zane", "Kathy Bates" ], "directors" : [ "James Cameron" ] }

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9e"), "title" : "The Dark Knight", "year" : 2008, "genres" : "Action", "rated" : "PG-13", "languages" : [ "English", "Mandarin" ], "released" : ISODate("2008-07-18T00:00:00Z"), "awards" : { "wins" : 144, "nominations" : 106, "text" : "Won 2 Oscars. Another 142 wins & 106 nominations." }, "cast" : [ "Christian Bale", "Heath Ledger", "Aaron Eckhart", "Michael Caine" ], "directors" : [ "Christopher Nolan" ] }

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9e"), "title" : "The Dark Knight", "year" : 2008, "genres" : "Crime", "rated" : "PG-13", "languages" : [ "English", "Mandarin" ], "released" : ISODate("2008-07-18T00:00:00Z"), "awards" : { "wins" : 144, "nominations" : 106, "text" : "Won 2 Oscars. Another 142 wins & 106 nominations." }, "cast" : [ "Christian Bale", "Heath Ledger", "Aaron Eckhart", "Michael Caine" ], "directors" : [ "Christopher Nolan" ] }

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9e"), "title" : "The Dark Knight", "year" : 2008, "genres" : "Drama", "rated" : "PG-13", "languages" : [ "English", "Mandarin" ], "released" : ISODate("2008-07-18T00:00:00Z"), "awards" : { "wins" : 144, "nominations" : 106, "text" : "Won 2 Oscars. Another 142 wins & 106 nominations." }, "cast" : [ "Christian Bale", "Heath Ledger", "Aaron Eckhart", "Michael Caine" ], "directors" : [ "Christopher Nolan" ] }

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9f"), "title" : "Spirited Away", "year" : 2001, "genres" : "Animation", "rated" : "PG", "languages" : [ "Japanese" ], "released" : ISODate("2003-03-28T00:00:00Z"), "awards" : { "wins" : 52, "nominations" : 22, "text" : "Won 1 Oscar. Another 51 wins & 22 nominations." }, "cast" : [ "Rumi Hiiragi", "Miyu Irino", "Mari Natsuki", "Takashi Naitè" ], "directors" : [ "Hayao Miyazaki" ] }

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9f"), "title" : "Spirited Away", "year" : 2001, "genres" : "Adventure", "rated" : "PG", "languages" : [ "Japanese" ], "released" : ISODate("2003-03-28T00:00:00Z"), "awards" : { "wins" : 52, "nominations" : 22, "text" : "Won 1 Oscar. Another 51 wins & 22 nominations." }, "cast" : [ "Rumi Hiiragi", "Miyu Irino", "Mari Natsuki", "Takashi Naitè" ], "directors" : [ "Hayao Miyazaki" ] }

{ "_id" : ObjectId("6146e76987f8346ec8e1ac9f"), "title" : "Spirited Away", "year" : 2001, "genres" : "Family", "rated" : "PG", "languages" : [ "Japanese" ], "released" : ISODate("2003-03-28T00:00:00Z"), "awards" : { "wins" : 52, "nominations" : 22, "text" : "Won 1 Oscar. Another 51 wins & 22 nominations." }, "cast" : [ "Rumi Hiiragi", "Miyu Irino", "Mari Natsuki", "Takashi Naitè" ], "directors" : [ "Hayao Miyazaki" ] }

{ "_id" : ObjectId("6146e76987f8346ec8e1aca0"), "title" : "Casablanca", "genres" : "Drama", "rated" : "PG", "cast" : [ "Humphrey Bogart", "Ingrid Bergman", "Paul Henreid", "Claude Rains" ], "languages" : [ "English", "French", "German", "Italian" ], "released" : ISODate("1943-01-23T00:00:00Z"), "directors" : [ "Michael Curtiz" ], "awards" : { "wins" : 9, "nominations" : 6, "text" : "Won 3 Oscars. Another 6 wins & 6 nominations." }, "lastupdated" : "2015-09-04 00:22:54.600000000", "year" : 1942 }

{ "_id" : ObjectId("6146e76987f8346ec8e1aca0"), "title" : "Casablanca", "genres" : "Romance", "rated" : "PG", "cast" : [ "Humphrey Bogart", "Ingrid Bergman", "Paul Henreid", "Claude Rains" ], "languages" : [ "English", "French", "German", "Italian" ], "released" : ISODate("1943-01-23T00:00:00Z"), "directors" : [ "Michael Curtiz" ], "awards" : { "wins" : 9, "nominations" : 6, "text" : "Won 3 Oscars. Another 6 wins & 6 nominations." }, "lastupdated" : "2015-09-04 00:22:54.600000000", "year" : 1942 }

{ "_id" : ObjectId("6146e76987f8346ec8e1aca0"), "title" : "Casablanca", "genres" : "War", "rated" : "PG", "cast" : [ "Humphrey Bogart", "Ingrid Bergman", "Paul Henreid", "Claude Rains" ], "languages" : [ "English", "French", "German", "Italian" ], "released" : ISODate("1943-01-23T00:00:00Z"), "directors" : [ "Michael Curtiz" ], "awards" : { "wins" : 9, "nominations" : 6, "text" : "Won 3 Oscars. Another 6 wins & 6 nominations." }, "lastupdated" : "2015-09-04 00:22:54.600000000", "year" : 1942 }
```



- [`$group`](https://docs.mongodb.com/manual/reference/operator/aggregation/group/#mongodb-pipeline-pipe.-group)和[`$count`](https://docs.mongodb.com/manual/reference/operator/aggregation/count-accumulator/#mongodb-group-grp.-count)累加器来计算每个 的出现次数`genre`。该值存储在`genreCount`字段中。

  ```json
  > db.movies.aggregate( [    { $unwind: "$genres" },{$group{_id: "$genres",genreCount: { $count: { }}}} ] )
  
  { "_id" : "Romance", "genreCount" : 2 }
  
  { "_id" : "War", "genreCount" : 1 }
  
  { "_id" : "Drama", "genreCount" : 3 }
  
  { "_id" : "Action", "genreCount" : 1 }
  
  { "_id" : "Crime", "genreCount" : 1 }
  
  { "_id" : "Animation", "genreCount" : 1 }
  
  { "_id" : "Family", "genreCount" : 1 }
  
  { "_id" : "Adventure", "genreCount" : 1 }
  ```

  

- [`$sort`](https://docs.mongodb.com/manual/reference/operator/aggregation/sort/#mongodb-pipeline-pipe.-sort)按`genreCount`字段按降序对结果文档进行排序。

```json
> db.movies.aggregate([{ $unwind: "$genres" },{      $group: {_id: "$genres",genreCount: { $count: { }}}},{ $sort: { "genreCount": -1 } } ] )
{ "_id" : "Drama", "genreCount" : 3 }
{ "_id" : "Romance", "genreCount" : 2 }
{ "_id" : "Family", "genreCount" : 1 }
{ "_id" : "Animation", "genreCount" : 1 }
{ "_id" : "Action", "genreCount" : 1 }
{ "_id" : "Crime", "genreCount" : 1 }
{ "_id" : "Adventure", "genreCount" : 1 }
{ "_id" : "War", "genreCount" : 1 }
```

# 数据库和集合

## 概述

MongoDB 将数据记录存储为文档，这些文档存储在集合（数据库中的表）中，

## 数据库

在MongoDB 中 数据库保存一个或多个集合，要选择使用的数据库，在shell中键入：use [db],如下：

```
> use mydb

switched to db mydb
```

## 集合

MongoDB 将文档存储在集合中。集合类似于关系数据库中的表。

![image-20210919200520323](../../image/image-20210919200520323.png)

### 创建集合

如果集合不存在，MongoDB 会在您第一次存储该集合的数据时创建该集合。

```json
> db.myNewCollection2.insertOne( { x: 1 } )

{
        "acknowledged" : true,
        "insertedId" : ObjectId("614727bc87f8346ec8e1aca1")
}
```

```json
> db.myNewCollection3.createIndex( { y: 1 } )

{
        "numIndexesBefore" : 1,
        "numIndexesAfter" : 2,
        "createdCollectionAutomatically" : true,
        "ok" : 1
}
```

## 显示创建

MongoDB 提供了[`db.createCollection()`](https://docs.mongodb.com/manual/reference/method/db.createCollection/#mongodb-method-db.createCollection)使用各种选项显式创建集合的方法，例如设置最大大小或文档验证规则。如果您未指定这些选项，则无需显式创建集合，因为 MongoDB 在您首次存储集合数据时会创建新集合。

## 文件验证

您可以在更新和插入操作期间为集合强制执行[文档验证规则](https://docs.mongodb.com/manual/core/schema-validation/)。有关详细信息，请参阅[架构验证](https://docs.mongodb.com/manual/core/schema-validation/)。

## 修改文档结构

要更改集合中文档的结构，例如添加新字段、删除现有字段或将字段值更改为新类型，请将文档更新为新结构。

## 唯一标识符

集合被分配了一个不可变的UUID。集合 UUID 在分片集群中的副本集和分片的所有成员中保持不变。

要检索集合的 UUID，请运行

```json
> db.getCollectionInfos()

[
        {
                "name" : "myNewCollection2",
                "type" : "collection",
                "options" : {

                },
                "info" : {
                        "readOnly" : false,
                        "uuid" : UUID("0884000d-1411-490c-898a-0b32bbc458ad")
                },
                "idIndex" : {
                        "v" : 2,
                        "key" : {
                                "_id" : 1
                        },
                        "name" : "_id_"
                }
        },
        {
                "name" : "myNewCollection3",
                "type" : "collection",
                "options" : {

                },
                "info" : {
                        "readOnly" : false,
                        "uuid" : UUID("21fe5b6c-2bd8-40e6-94bb-2bcb19a4afae")
                },
                "idIndex" : {
                        "v" : 2,
                        "key" : {
                                "_id" : 1
                        },
                        "name" : "_id_"
                }
        }
]
```

# 视图

MongoDB 视图是一个可查询对象，其内容由其他集合或视图上的[聚合管道](https://docs.mongodb.com/manual/core/aggregation-pipeline/#std-label-aggregation-pipeline)定义 。MongoDB 不会将视图内容持久化到磁盘。当客户端[查询](https://docs.mongodb.com/manual/core/views/#std-label-views-supported-operations)视图时，视图的内容是按需计算的。MongoDB 可以要求客户端 [具有](https://docs.mongodb.com/manual/core/authorization/#std-label-authorization)查询视图的[权限](https://docs.mongodb.com/manual/core/authorization/#std-label-authorization)。MongoDB 不支持针对视图的写操作。

## 创建视图



# 上限集合

## 概述

[上限集合](https://docs.mongodb.com/manual/reference/glossary/#std-term-capped-collection)是固定大小的集合，支持基于插入顺序插入和检索文档的高吞吐量操作。封顶集合的工作方式类似于循环缓冲区：一旦集合填满其分配的空间，它就会通过覆盖集合中最旧的文档来为新文档腾出空间。

## 潜在用例

- 存储大容量系统生成的日志信息。在没有索引的情况下在有上限的集合中插入文档接近将日志信息直接写入文件系统的速度。此外，内置的*先进先出*属性维护事件的顺序，同时管理存储使用。
- 在有上限的集合中缓存少量数据。由于高速缓存是读取而不是写入，您需要确保此集合*始终*保留在工作集中（即在 RAM 中）*或*接受对所需索引或索引的一些写入惩罚。

## 文档删除

您不能从有上限的集合中删除文档。要从集合中删除所有文档，请使用[`drop()`](https://docs.mongodb.com/manual/reference/method/db.collection.drop/#mongodb-method-db.collection.drop) 方法删除集合并重新创建有上限的集合。

## 分片

您不能对有上限的集合进行分片

## 查询效率

使用自然排序有效的从集合中检索最近插入的集合。

## 聚合 $out

聚合管道阶段[`$out`](https://docs.mongodb.com/manual/reference/operator/aggregation/out/#mongodb-pipeline-pipe.-out) 无法将结果写入有上限的集合。

## 创建一个上限集合

您必须使用该[`db.createCollection()`](https://docs.mongodb.com/manual/reference/method/db.createCollection/#mongodb-method-db.createCollection)方法显式创建上限集合 ，创建上限集合时，您必须以字节为单位指定集合的最大大小，MongoDB 将为该集合预先分配该大小。上限集合的大小包括用于内部开销的少量空间。

```json
> db.createCollection( "log", { capped: true, size: 100000 } )

{ "ok" : 1 }
```

如果该`size`字段小于或等于 4096，则集合的上限为 4096 字节。否则，MongoDB 将提高提供的大小，使其成为 256 的整数倍。

您还可以使用`max`以下文档中的字段为集合指定最大文档数：

```json
> db.createCollection("log1", { capped : true, size : 5242880, max : 5000 } )

{ "ok" : 1 }
```

注：该`size`参数*始终*是必需的，即使您指定`max`了文档数量。如果集合在达到最大文档数之前达到最大大小限制，MongoDB 将删除旧文档。

## 查询上限集合

在没有指定排序的有上限的集合上执行  ，MongoDB 保证结果的排序与插入顺序相同。要以反向插入顺序检索文档，请与参数设置为[`find()`](https://docs.mongodb.com/manual/reference/method/db.collection.find/#mongodb-method-db.collection.find)的[`sort()`](https://docs.mongodb.com/manual/reference/method/cursor.sort/#mongodb-method-cursor.sort)方法一起[`$natural`](https://docs.mongodb.com/manual/reference/operator/meta/natural/#mongodb-operator-metaOp.-natural)发出`-1`，如以下示例所示：

```
> db.cappedCollection.find().sort( { $natural: -1 } )
```

## 检查集合是否有上限

```json
> db.log1.isCapped()

true
```

## 将集合转换为Capped

```json
> db.mycoll.insertOne({"age":18})

{
        "acknowledged" : true,
        "insertedId" : ObjectId("6147300187f8346ec8e1aca2")
}

> db.runCommand({"convertToCapped": "mycoll", size: 100000});

{ "ok" : 1 }
```

# 时间序列集合

[时间序列集合](https://docs.mongodb.com/manual/reference/glossary/#std-term-time-series-collection)有效地存储了一段时间内的测量序列。时间序列数据是随时间收集并由一个或多个不变参数唯一标识的任何数据。标识时间序列数据的不变参数通常是数据源的元数据。

## 创建时间序列集合

```json
> db.createCollection("weather", { timeseries: { timeField: "timestamp" } } )

{ "ok" : 1 }	
```



```json
>db.createCollection(
    "weather24h",
    {
       timeseries: {
          timeField: "timestamp",
          metaField: "metadata",
          granularity: "hours"
       },
       expireAfterSeconds: 86400
    }
)

{ "ok" : 1 }
```

创建时间序列集合时，请指定以下选项：

| 场地                     | 类型 | 描述                                                         |
| :----------------------- | :--- | :----------------------------------------------------------- |
| `timeseries.timeField`   | 细绳 | 必需的。每个时间序列文档中包含日期的字段的名称。时间序列集合中的文档必须具有有效的 BSON 日期作为`timeField`. |
| `timeseries.metaField`   | 细绳 | 可选的。每个时间序列文档中包含元数据的字段的名称。指定字段中的元数据应该是用于标记唯一系列文档的数据。元数据应该很少（如果有的话）更改。指定字段的名称可能`_id`与`timeseries.timeField`. 该字段可以是任何类型。 |
| `timeseries.granularity` | 细绳 | 可选的。可能的值是`"seconds"`，`"minutes"`和 `"hours"`。默认情况下，MongoDB 将 设置`granularity`为 `"seconds"`用于高频摄取。`granularity`通过优化时间序列集合中数据的内部存储方式，手动设置参数以提高性能。要为 选择一个值`granularity`，请选择与连续传入测量值之间的时间跨度最接近的匹配项。如果指定`timeseries.metaField`，请考虑具有相同`metaField`字段唯一值的连续传入测量之间的时间跨度。如果测量值`metaField`来自相同的来源，则它们通常具有相同的字段唯一值。如果未指定`timeseries.metaField`，请考虑插入集合中的所有测量值之间的时间跨度。 |
| `expireAfterSeconds`     | 数字 | 可选的。通过指定文档过期的秒数来启用[时间序列集合](https://docs.mongodb.com/manual/reference/glossary/#std-term-time-series-collection)中文档的自动删除 。MongoDB 会自动删除过期的文档。有关详细信息，请参阅[为时间序列集合 (TTL) 设置自动删除](https://docs.mongodb.com/manual/core/timeseries/timeseries-automatic-removal/#std-label-manual-timeseries-automatic-removal)。 |

## 将测量值插入时间序列集合

```json
db.weather.insertMany([{
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-18T00:00:00.000Z"),
   "temp": 12
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-18T04:00:00.000Z"),
   "temp": 11
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-18T08:00:00.000Z"),
   "temp": 11
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-18T12:00:00.000Z"),
   "temp": 12
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-18T16:00:00.000Z"),
   "temp": 16
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-18T20:00:00.000Z"),
   "temp": 15
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-19T00:00:00.000Z"),
   "temp": 13
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-19T04:00:00.000Z"),
   "temp": 12
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-19T08:00:00.000Z"),
   "temp": 11
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-19T12:00:00.000Z"),
   "temp": 12
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-19T16:00:00.000Z"),
   "temp": 17
}, {
   "metadata": {"sensorId": 5578, "type": "temperature"},
   "timestamp": ISODate("2021-05-19T20:00:00.000Z"),
   "temp": 12
}])
```

```json
{
        "acknowledged" : true,
        "insertedIds" : [
                ObjectId("6147316487f8346ec8e1aca3"),
                ObjectId("6147316487f8346ec8e1aca4"),
                ObjectId("6147316487f8346ec8e1aca5"),
                ObjectId("6147316487f8346ec8e1aca6"),
                ObjectId("6147316487f8346ec8e1aca7"),
                ObjectId("6147316487f8346ec8e1aca8"),
                ObjectId("6147316487f8346ec8e1aca9"),
                ObjectId("6147316487f8346ec8e1acaa"),
                ObjectId("6147316487f8346ec8e1acab"),
                ObjectId("6147316487f8346ec8e1acac"),
                ObjectId("6147316487f8346ec8e1acad"),
                ObjectId("6147316487f8346ec8e1acae")
        ]
}
```

## 查询

```json
>db.weather.findOne({
   "timestamp": ISODate("2021-05-18T00:00:00.000Z")
})

{
        "timestamp" : ISODate("2021-05-18T00:00:00Z"),
        "temp" : 12,
        "metadata" : {
                "sensorId" : 5578,
                "type" : "temperature"
        },
        "_id" : ObjectId("6147316487f8346ec8e1aca3")
}
```

## 聚合

```json
>db.weather.aggregate([
   {
      $project: {
         date: {
            $dateToParts: { date: "$timestamp" }
         },
         temp: 1
      }
   },
   {
      $group: {
         _id: {
            date: {
               year: "$date.year",
               month: "$date.month",
               day: "$date.day"
            }
         },
         avgTmp: { $avg: "$temp" }
      }
   }
])

{ "_id" : { "date" : { "year" : 2021, "month" : 5, "day" : 18 } }, "avgTmp" : 12.833333333333334 }
{ "_id" : { "date" : { "year" : 2021, "month" : 5, "day" : 19 } }, "avgTmp" : 12.833333333333334 }
```

# 安装

## 在Red Hat 或CentOS上安装MongoDB社区版

# MongoDB CRUD操作

