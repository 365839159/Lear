
//#region  包含 以什么开头 , 以什么结尾
{
    let a = "hello world !"
    //包含
    console.log(a.includes("l"));

    console.log(a.includes("zxc"));

    //以什么开头
    console.log(a.startsWith("h"));
    //以什么结尾
    console.log(a.endsWith("!"));
}

//#endregion

//#endregion  模板字符串

{
    function func(name, age) {
        return `这是${name},年龄${age}`;
    }
    console.log(func("zxc", 18));


    let muit = `
    zxc,
    abc,
    qwe,
    123
    `;

    console.log(muit);
}

//#endregion