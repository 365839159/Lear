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













