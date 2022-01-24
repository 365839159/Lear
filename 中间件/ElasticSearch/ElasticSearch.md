# 索引

## 创建索引

```shell
PUT /{index_name}
{
	"settings":{
		....	
	},
	"mappings":{
		"properties":{
			...
		}
	}
	
}
```

### setting

设置

```shell
# 指定主分片个数
number_of_shards
# 指定副分片个数
number_of_replicas
```

### mappings

映射

### properties

属性

> 创建索引testindex

```shell
PUT /testindex
{
  "settings": {
    "number_of_shards": 5,
    "number_of_replicas": 10
  },
  "mappings": {
    "properties": {
      "name": {
        "type": "keyword"
      },
      "age": {
        "type": "integer"
      }
    }
  }
}
```

## 删除索引

~~~shell
DELETE /{index_name}
~~~

> 删除testindex

```shell
DELETE /testindex
```

## 关闭索引

~~~shell
POST /{index_name}/_close
~~~

> 关闭testindex

~~~shell
POST /testindex/_close
~~~

## 打开索引

~~~shell
POST /{index_name}/_open
~~~

> 打开索引

~~~shell
POST /testindex/_open
~~~

## 别名

~~~shell
POST /_aliases
{
  "actions": [
    {
      "add": {
        "index": "testindex",			# 索引名
        "alias": "testindexalias",		# 别名
        "is_write_index":true			# 设置写入
      }
    },
    {
      "add": {
        "index": "testindex1",
        "alias": "testindexalias"
      }
    },
    {
      "add": {
        "index": "testindex2",
        "alias": "testindexalias"
      }
    }
  ]
}
~~~

# 映射

## 查看映射

```shell
GET /{index_name}/_mapping
```

查看 index_test 映射

~~~shell
GET /index_test/_mapping
~~~

## 扩展映射

```shell
POST /index_test/_mapping
{
  "properties":{
    "title":{
      "type":"text"
    }
  }
}
```

## 基础数据类型

### keyword

keyword类型的数据不进行切分，直接构建倒排索引；在搜索时，对该类型的查询字符串不进行切分后的部分匹配。keyword类型数据一般用于对文档的过滤、排序和聚合

### text

text类型是可进行切分的字符串类型。在索引时，可按照相应的切词算法对文本内容进行切分，然后构建倒排索引；在搜索时，对该类型的查询字符串按照用户的切词算法进行切分，然后对切分后的部分匹配打分

### 数值类型

ES支持的数值类型有long、integer、short、byte、double、float、half_float、scaled_float和unsigned_long等。

### boolean

写入或者查询该类型的数据时，其值可以使用true和false，或者使用字符串形式的"true"和"false"

### date

在ES中，日期类型的名称为date。ES中存储的日期是标准的UTC格式。



