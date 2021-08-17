[TOC]

# 设置man 中文显示

```
yum 下载中文说明手册，包名称为man-pages-zh-CN

yum install man-pages-zh-CN

下载完成后，安装保存在/ usr/share/man下

为了区别原有的man命令，采用cman进行别名配置

alias cman='man -M /usr/share/man/zh_CN'

完成后，直接输入cman 命名，就可以看中文手册了

cman rm
```

![image-20210816161802709](D:\y创而新\张显成\代码\Lear\Lear\back-end\image\image-20210816161802709.png)
