//#region 全局对象 js windows   node.js global
{
    // console.log(global);

    //描述当前node.js 进程状态的对象,提供一个与操作系统的简单接口
    // console.log(global.process);

    //打印栈信息
    console.trace();
}
//#endregion

//#region 获取执行文件的目录和绝对文件名
{
    console.log(__filename);
    console.log(__dirname);
}
//#endregion

//#region  定时器

{
    //定时器
    let setTime = setTimeout(() => {
        console.log('zxc');
    }, 1000);

    // clearTimeout(setTime);//清除定时器

    let setInt = setInterval(() => {
        console.log('zxc');
    }, 1000);

    clearInterval(setInt);
}
//#endregion

//#region buffer
{
    let buf = Buffer.from('zxc');//根据字符串创建buffer对象 默认 utf-8 
    console.log(buf);

    let arrBuf = Buffer.from([1, 2, 3]);
    console.log(arrBuf);

    let bufLen = Buffer.alloc(10);//创建长度为10 的buffer
    console.log(bufLen);
    console.log(bufLen.length);//10

    //写入数据
    bufLen.write("zxc");
    console.log(bufLen.length);//10
}
//#endregion