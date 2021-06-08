
//#region  ES6之前设置函数默认值
{

    function fun(x) {
        x = x || 1;
        console.log(x);
    }
    fun(null);
    fun(2);

    //传递什么值 才会让默认值生效

    fun(null);
    fun('');
    fun(0);
    fun(false);
    fun(undefined);

    function func1(x) {
        x && console.log(x)  //若x是真值 则打印
        //相当于
        if (x) {
            console.log(x);
        }
    }

    func1(1);

}
//#endregion

//#region  Es6 函数默认值

{
    function func(x = 1, s = 2) {
        console.log(x, s);
    }
    func(2);//没有传值 或者传值是undefined 默认值才生效
}

//#endregion


//#region   函数的解构赋值(解决参数位置的问题)  var{name,age} ={name:"zxc",age:18}

{
    function fun({ name, age } = { name: "default", age: 0 }) {//{ name, age }={name:"zxc",age:18}\{ name='xx', age=20 } {name,age}={} 定义参数  设置默认值 的几种方法
        console.log(`name:${name}, age:${age}`)
    }

    fun({ name: "zxc", age: 18 });
    fun({ age: 19, name: 'qwe' });
    fun();//默认值
    fun(undefined);//默认值

}
//#endregion


//#region  rest 解决多余参数 相当于C#中的pams 

{
    function func(...values) {
        console.log(values)
    }

    func(1, 2, 3);

    //传递数组
    function func1(a, b) {
        console.log(`${a},${b}`);
    }
    let arr = [1, 2];
    //调用函数时如果在实参前使用...运算符数组中的值取出来赋值给函数中的参数
    func1(...arr);
    //方法被obj 调用
    func1.apply({ obj: 1 }, [11, 22]);

    //合并数组
    let num1 =[1,2,3];
    let num2 =[4,5,6]
    let num3=[...num1,...num2];
    console.log(num3);

}

//#endregion