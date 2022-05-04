https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-ubuntu



# Centos 7

```shell
# 将 Microsoft 包签名密钥添加到受信任密钥列表
sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
```

```shell
# 安装sdk
sudo yum install dotnet-sdk-5.0
```

```shell
# 安装运行时
sudo yum install aspnetcore-runtime-5.0
```

```shell
# 查看版本
dotnet --version
```

# Ubuntu 20.4

```shell
# 将 Microsoft 包签名密钥添加到受信任密钥列表，并添加包存储库
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

sudo dpkg -i packages-microsoft-prod.deb

rm packages-microsoft-prod.deb
```

```shell
# 安装sdk
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-5.0
```

```shell
# 安装运行时
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-5.0
```

