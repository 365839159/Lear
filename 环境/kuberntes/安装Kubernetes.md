

# [一、 环境准备](#一-环境准备)

## 1. 1 机器环境

> 非常重要必须看咯
>
> 1、节点CPU核数必须是 ：>= 2核 /内存要求必须是：>=2G ，否则k8s无法启动
>
> 2、DNS网络： 最好设置为 本地网络连通的DNS,否则网络不通，无法下载一些镜像
>
> 3、兼容问题
>
> docker 19 kubernetes1.19.x
>
> docker 20 kubernetes 1.20.x
>
> 在k8s1.21.1之后 k8s的默认容器不是Docker是Containerd
>
> **注意：**使用docker版本v20.10.8、kubernetes v1.20.5、Go版本v1.13.15

| 节点hostname | 作用     | IP           |
| ------------ | -------- | ------------ |
| kmaster      | kmaster  | 192.168.3.10 |
| knode1       | kworker1 | 192.168.3.11 |
| knode2       | kworker2 | 192.168.3.12 |

## 1.2 服务器静态IP配置

```
[root@localhost ~]# vi /etc/sysconfig/network-scripts/ifcfg-enp0s3
BOOTPROTO="static" #dhcp改为static 
ONBOOT="yes" #开机启用本配置
IPADDR=192.168.3.10 #静态IP 192.168.8.11/192.168.8.12
GATEWAY=192.168.3.1 #默认网关
NETMASK=255.255.255.0 #子网掩码
DNS1=114.114.114.114 #DNS 配置
DNS2=8.8.8.8 #DNS 配置【必须配置，否则SDK镜像下载很慢】
## 重启所有服务器
[root@localhost ~] reboot 
```

## 1.3 设置主机别名

```
[root@localhost ~]# hostnamectl set-hostname kmaster --static
[root@localhost ~]# hostnamectl set-hostname kworker1 --static
[root@localhost ~]# hostnamectl set-hostname kworker2 --static
```

## 1.4 查看主机名

```
## 看看别名是否生效
hostname
```

## 1.5 配置IP host映射关系

```
## 编辑/etc/hosts文件，配置映射关系
vi /etc/hosts
192.168.3.10 kmaster
192.168.3.11 kworker1
192.168.3.12 kworker2
```

## 1.6 安装依赖环境

> 注意：每一台机器都需要安装此依赖环境

```
yum install -y conntrack ntpdate ntp ipvsadm ipset jq iptables curl sysstatlibseccomp wget vim net-tools git iproute lrzsz bash-completion tree bridge-utils unzip bind-utils gcc
```

## 1.7 防火墙配置

> 安装iptables，启动iptables，设置开机自启，清空iptables规则，保存当前规则到默认规则

```
# 关闭防火墙(生产环境建议使用放行端口)
systemctl stop firewalld && systemctl disable firewalld
# 置空iptables
yum -y install iptables-services && systemctl start iptables && systemctl enable iptables && iptables -F && service iptables save
```

## 1.8 关闭selinux[必须操作]

> 因为在K8S集群安装的时候需要执行脚本，如果Selinux没有关闭它会阻止执行。

```
# 关闭swap分区【虚拟内存】并且永久关闭虚拟内存
swapoff -a && sed -i '/ swap / s/^\(.*\)$/#\1/g' /etc/fstab
# 关闭selinux
setenforce 0 && sed -i 's/^SELINUX=.*/SELINUX=disabled/' /etc/selinux/config
```

## 1.9 查看 enforce是否关闭

~~~shell
#getenforce
Disabled
~~~

# [二、系统设置调整](#二-系统设置调整)

## 2.1 调整内核参数

```
cat > kubernetes.conf <<EOF
net.bridge.bridge-nf-call-iptables=1
net.bridge.bridge-nf-call-ip6tables=1
net.ipv4.ip_forward=1
net.ipv4.tcp_tw_recycle=0
vm.swappiness=0
vm.overcommit_memory=1
vm.panic_on_oom=0
fs.inotify.max_user_instances=8192
fs.inotify.max_user_watches=1048576
fs.file-max=52706963
fs.nr_open=52706963
net.ipv6.conf.all.disable_ipv6=1
net.netfilter.nf_conntrack_max=2310720
EOF

#将优化内核文件拷贝到/etc/sysctl.d/文件夹下，这样优化文件开机的时候能够被调用
cp kubernetes.conf /etc/sysctl.d/kubernetes.conf
#手动刷新，让优化文件立即生效
sysctl -p /etc/sysctl.d/kubernetes.conf
```

#### 执行 sysctl -p /etc/sysctl.d/kubernetes.conf 错误 解决方法

~~~shell
[root@kmaster ~]# sysctl -p /etc/sysctl.d/kubernetes.conf
sysctl: cannot stat /proc/sys/net/bridge/bridge-nf-call-iptables: No such file or directory
sysctl: cannot stat /proc/sys/net/bridge/bridge-nf-call-ip6tables: No such file or directory
## 出现以上错误
[root@kmaster ~]# modprobe  br_netfilter
[root@kmaster ~]# sysctl -p /etc/sysctl.d/kubernetes.conf
## 网址 ：https://blog.csdn.net/weibo1230123/article/details/121680887
~~~

## 2.2 调整系统临时区

```
#设置系统时区为中国/上海
timedatectl set-timezone "Asia/Shanghai"
#将当前的UTC 时间写入硬件时钟
timedatectl set-local-rtc 0
#重启依赖于系统时间的服务
systemctl restart rsyslog
systemctl restart crond
```

## 2.3 关闭系统不需要的服务

```
systemctl stop postfix && systemctl disable postfix
```

## 2.4 设置日志保存方式

### 2.4.1 创建保存日志的目录

```
mkdir /var/log/journal
```

### 2.4.2 创建配置文件存放目录

```
mkdir /etc/systemd/journald.conf.d
```

### 2.4.3 创建配置文件

```
cat > /etc/systemd/journald.conf.d/99-prophet.conf <<EOF
[Journal]
Storage=persistent
Compress=yes
SyncIntervalSec=5m
RateLimitInterval=30s
RateLimitBurst=1000
SystemMaxUse=10G
SystemMaxFileSize=200M
MaxRetentionSec=2week
ForwardToSyslog=no
EOF
```

### 2.4.4 重启systemd journald 的配置

```
systemctl restart systemd-journald
```

### 2.4.5 打开文件数调整(可忽略，不执行)

```
echo "* soft nofile 65536" >> /etc/security/limits.conf
echo "* hard nofile 65536" >> /etc/security/limits.conf
```

### 2.4.6 kube-proxy 开启 ipvs 前置条件

```
modprobe br_netfilter
cat > /etc/sysconfig/modules/ipvs.modules <<EOF
#!/bin/bash
modprobe -- ip_vs
modprobe -- ip_vs_rr
modprobe -- ip_vs_wrr
modprobe -- ip_vs_sh
modprobe -- nf_conntrack_ipv4
EOF
#使用lsmod命令查看这些文件是否被引导
    chmod 755 /etc/sysconfig/modules/ipvs.modules && bash /etc/sysconfig/modules/ipvs.modules && lsmod | grep -e ip_vs -e nf_conntrack_ipv4
=========================执行结果================================
ip_vs_sh               16384  0
ip_vs_wrr              16384  0
ip_vs_rr               16384  0
ip_vs                 147456  6 ip_vs_rr,ip_vs_sh,ip_vs_wrr
nf_conntrack_ipv4      20480  0
nf_defrag_ipv4         16384  1 nf_conntrack_ipv4
nf_conntrack          114688  2 ip_vs,nf_conntrack_ipv4
libcrc32c              16384  2 xfs,ip_vs
```

# [三、Docker部署](#三-docker部署)

## 3.1 安装docker

```
# 安装依赖
yum install -y yum-utils device-mapper-persistent-data lvm2
#紧接着配置一个稳定的仓库、仓库配置会保存到/etc/yum.repos.d/docker-ce.repo文件中
yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
#更新Yum安装的相关Docker软件包&安装Docker CE(这里安装Docker最新版本)
yum update -y && yum install docker-ce
```

## 3.2 设置docker daemon文件

```
#创建/etc/docker目录
mkdir /etc/docker
#更新daemon.json文件
cat > /etc/docker/daemon.json <<EOF
{
  "registry-mirrors": [
        "https://ebkn7ykm.mirror.aliyuncs.com",
        "https://docker.mirrors.ustc.edu.cn",
        "http://f1361db2.m.daocloud.io",
        "https://registry.docker-cn.com"
    ],
  "exec-opts": ["native.cgroupdriver=systemd"],
  "log-driver": "json-file",
  "log-opts": {
    "max-size": "100m"
  },
  "storage-driver": "overlay2"
}
EOF
#注意：一定注意编码问题，出现错误---查看命令：journalctl -amu docker 即可发现错误
#创建，存储docker配置文件
# mkdir -p /etc/systemd/system/docker.service.d
```

## 3.3 重启docker服务

```
systemctl daemon-reload && systemctl restart docker && systemctl enable docker
```

# [四、kubeadm安装K8S](#四-kubeadm安装k8s)

## 4.1 yum仓库镜像

> 国内镜像配置(国内建议配置)

```
cat <<EOF > /etc/yum.repos.d/kubernetes.repo
[kubernetes]
name=Kubernetes
baseurl=http://mirrors.aliyun.com/kubernetes/yum/repos/kubernetes-el7-x86_64
enabled=1
gpgcheck=0
repo_gpgcheck=0
gpgkey=http://mirrors.aliyun.com/kubernetes/yum/doc/yum-key.gpg
       http://mirrors.aliyun.com/kubernetes/yum/doc/rpm-package-key.gpg
EOF
```

> 官网镜像配置

```
cat <<EOF > /etc/yum.repos.d/kubernetes.repo
[kubernetes]
name=Kubernetes
baseurl=https://packages.cloud.google.com/yum/repos/kubernetes-el7-x86_64
enabled=1
gpgcheck=1
repo_gpgcheck=1
gpgkey=https://packages.cloud.google.com/yum/doc/yum-key.gpg https://packages.cloud.google.com/yum/doc/rpm-package-key.gpg
EOF
```

## 4.2 安装kubeadm 、kubelet、kubectl(1.20.5)

```
# 指定版本
yum install -y kubelet-1.20.5 kubeadm-1.20.5 kubectl-1.20.5 --disableexcludes=kubernetes
systemctl enable kubelet && systemctl start kubelet
```

## 查看 状态 

~~~shell
systemctl status kubelet
~~~

# [五、准备k8s镜像](#五-准备k8s镜像)

## 5.1 修改配置文件

```
[root@master ~]$ kubeadm config print init-defaults > kubeadm-init.yaml
```

该文件有两处需要修改:

- 将`advertiseAddress: 1.2.3.4`修改为本机地址,比如使用192.168.3.191作为master，就修改`advertiseAddress: 192.168.3.191`
- 将`imageRepository: k8s.gcr.io`修改为`imageRepository: registry.cn-hangzhou.aliyuncs.com/google_containers`

修改完毕后文件如下:

```
apiVersion: kubeadm.k8s.io/v1beta2
bootstrapTokens:
- groups:
  - system:bootstrappers:kubeadm:default-node-token
  token: abcdef.0123456789abcdef
  ttl: 24h0m0s
  usages:
  - signing
  - authentication
kind: InitConfiguration
localAPIEndpoint:
  advertiseAddress: 192.168.3.191 # 本机IP
  bindPort: 6443
nodeRegistration:
  criSocket: /var/run/dockershim.sock
  name: k8s-master
  taints:
  - effect: NoSchedule
    key: node-role.kubernetes.io/master
---
apiServer:
  timeoutForControlPlane: 4m0s
apiVersion: kubeadm.k8s.io/v1beta2
certificatesDir: /etc/kubernetes/pki
clusterName: kubernetes
controllerManager: {}
dns:
  type: CoreDNS
etcd:
  local:
    dataDir: /var/lib/etcd
imageRepository: registry.cn-hangzhou.aliyuncs.com/google_containers #镜像仓库
kind: ClusterConfiguration
kubernetesVersion: v1.20.1
networking:
  dnsDomain: cluster.local
  serviceSubnet: 10.96.0.0/12
  podSubnet: 10.244.0.0/16 # 新增Pod子网络
scheduler: {}
```

## 5.2 根据配置文件拉取镜像

```
[root@kmaster ~]$ kubeadm config images pull --config kubeadm-init.yaml
```

# [六、K8S的Master部署](#六-k8s的master部署)

## 6.1 执行初始化

```
[root@master ~]$ kubeadm init --config kubeadm-init.yaml
```

## 6.2 验证是否成功

```
## 如果在执行完成后出现下面的语句 代表成功 并记录下加入worker节点的命令
kubeadm join 192.168.3.10:6443 --token abcdef.0123456789abcdef \
    --discovery-token-ca-cert-hash sha256:13aff92657d0f3451ac68e3200ebc3c1c6ea6980b1de700ba257ad1538e0ce3
```

## 6.3 查看Master节点网络状态

```
## 配置kubectl执行命令环境 （The connection to the server localhost:8080 was refused - did you specify the right host or port?
）
mkdir -p $HOME/.kube
cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
chown $(id -u):$(id -g) $HOME/.kube/config

## 执行kubectl命令查看机器节点
kubectl get node
-----------------------------------------
NAME     STATUS     ROLES    AGE   VERSION
master   NotReady   master   48m   v1.20.1

## 发现节点STATUS是NotReady的，是因为没有配置网络
```

## 6.4 配置网络

使用以下命令安装Calico网络

```
curl https://docs.projectcalico.org/manifests/calico.yaml -O

## 编辑calico.yaml
## 修改calico.yaml文件设置指定的网卡
# Cluster type to identify the deployment type
- name: CLUSTER_TYPE
value: "k8s,bgp"
# IP automatic detection
- name: IP_AUTODETECTION_METHOD
value: "interface=en.*"
# Auto-detect the BGP IP address.
- name: IP
value: "autodetect"
# Enable IPIP
- name: CALICO_IPV4POOL_IPIP
value: "Never"
## 构建calico网络
kubectl apply -f 
```

此时查看node信息, master的状态已经是`Ready`了.

```
[root@master ~]$ kubectl get node
NAME     STATUS     ROLES    AGE   VERSION
master   Ready   master   48m      v1.20.5
```

> 看到STATUS是Ready的，说明网络已经通了。

# [七、追加Node节点](#七-追加node节点)

```
## 到其他几个node节点进行执行即可
kubeadm join 192.168.3.10:6443 --token abcdef.0123456789abcdef \
    --discovery-token-ca-cert-hash sha256:37eb59b3459a1651222a98e35d057cfd102e8ae311c5fc9bb4be22cd46a59c29
```

# [八、验证状态](#八-验证状态)

```
[root@kmaster ~]# kubectl get node
NAME      STATUS   ROLES                  AGE     VERSION
kmaster   Ready    control-plane,master   26m     v1.20.5
kworker1    Ready    <none>                 5m37s   v1.20.5
kworker2    Ready    <none>                 5m28s   v1.20.5

[root@kmaster ~]# kubectl get pod -n kube-system -o wide
## 如果看到下面的pod状态都是Running状态，说明K8S集群环境就构建完成
```

# [附录](#附录)

```
kubectl delete node --all # 删除所有的节点
kubeadm reset -f # 重置kubeadm
modprobe -r ipip
lsmod
rm -rf ~/.kube/
rm -rf /etc/kubernetes/
rm -rf /etc/systemd/system/kubelet.service.d
rm -rf /etc/systemd/system/kubelet.service
rm -rf /usr/bin/kube*
rm -rf /etc/cni
rm -rf /opt/cni
rm -rf /var/lib/etcd
rm -rf /var/etcd
yum remove kube*
```
