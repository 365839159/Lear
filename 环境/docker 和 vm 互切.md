bcdedit /set hypervisorlaunchtype auto
关闭命令为

1
bcdedit /set hypervisorlaunchtype off


sudo tee /etc/docker/daemon.json <<-'EOF'
{
  "registry-mirrors": [
    "https://registry.docker-cn.com",
    "http://hub-mirror.c.163.com",
    "https://3laho3y3.mirror.aliyuncs.com",
    "http://f1361db2.m.daocloud.io",
    "https://registry.docker-cn.com"
  ],
  "insecure-registries": [],
  "debug": true,
  "experimental": true
}

EOF