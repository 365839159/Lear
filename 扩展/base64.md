# 网址

https://baike.baidu.com/item/base64/8545775?fr=aladdin#4_6

## 例子

### C#

```C#
///<summary>
///Base64加密
///</summary>
///<paramname="Message"></param>
///<returns></returns>
public string Base64Code(string Message)
{
	byte [] bytes=Encoding.Default.GetBytes(Message);
	return Convert.ToBase64String(bytes);
}
///<summary>
///Base64解密
///</summary>
///<paramname="Message"></param>
///<returns></returns>
public string Base64Decode(string Message)
{
	byte [] bytes=Convert.FromBase64String(Message);
	return Encoding.Default.GetString(bytes);
}
```

