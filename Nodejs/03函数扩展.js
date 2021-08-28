
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
    let num1 = [1, 2, 3];
    let num2 = [4, 5, 6]
    let num3 = [...num1, ...num2];
    console.log(num3);

}

//#endregion

//#region   箭头函数语法
{

    //无参数无返回值
    console.log("无参数无返回值")

    let fun1 = function () {
        console.log("null");
    }
    fun1();

    console.log("------------------------------------------");

    let fun11 = () => console.log("null");
    fun11();
    //带参数
    console.log("带参数")

    let fun2 = function (x = 0) {
        console.log(x);
    }
    fun2(2);

    console.log("------------------------------------------");

    let fun22 = (x = 1) => console.log(x);
    fun22(22)

    //带返回值
    console.log("带返回值")

    let fun3 = function () {
        return "fun3";
    }
    console.log(fun3())

    console.log("------------------------------------------");

    let fun33 = () => "fun33";
    console.log(fun33());
    //带参数、带返回值
    console.log("带参数、带返回值")

    let fun4 = function (a, b) {
        return a + b;
    }
    console.log(fun4(1, 2));

    console.log("------------------------------------------");

    let fun44 = (a, b) => a + b;
    console.log(fun44(3, 4));

    //简化匿名函数
    console.log("简化匿名函数")
    let arr = [1, 2, 3];
    arr.forEach(function (value, index, array) {

        console.log(value);
    });
    
    console.log("------------------------------------------");

    arr.forEach((value, index, array) => console.log(value));
    
    console.log("使用 rest（...values）代替arguments")

    let temp = (...values) =>console.log(values);
    temp(1,2,3);
}
//注：（1）箭头函数没有自己的作用域、箭头函数使用的this 都是使用外部的函数的this
//    （2）箭头函数不能new =》let fun=()=>{this.a=123}; new fun() Error :fun is not a constructor
//     （3）箭头函数不能使用 arguments 获取参数列表  可以使用 rest（...values）代替 
//#endregion


