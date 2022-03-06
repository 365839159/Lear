# docker 下安装

https://www.pianshen.com/article/5896320324/

# 创建索引（Ik分词）

```shell
PUT /userinfo
{
  "mappings": {
    "properties": {
      "id": {
        "type": "integer"
      },
      "name": {
        "type": "text",
        "analyzer": "ik_max_word"
      },
      "age": {
        "type": "integer"
      },
      "adress": {
        "type": "text",
        "analyzer": "ik_max_word"
      },
      "phone": {
        "type": "keyword"
      }
    }
  },
  "settings": {
    "number_of_shards": 3,
    "number_of_replicas": 0
  }
}
```

```json
//结果
{
  "acknowledged" : true,
  "shards_acknowledged" : true,
  "index" : "userinfo"
}
```
