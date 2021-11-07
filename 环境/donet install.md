# Centos 7

将 Microsoft 包签名密钥添加到受信任密钥列表

```
sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
```

安装sdk

```
sudo yum install dotnet-sdk-5.0
```

安装运行时

```
sudo yum install aspnetcore-runtime-5.0
```

查看版本

```
dotnet --version
```

