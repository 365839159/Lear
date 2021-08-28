

//#region  ES6 之前的类于继承
{
    //定义父类类
    function Animal(name) {
        this.name = name;
    }

    //定义实例方法
    Animal.prototype.ShowName = function () { console.log(this.name) };
    //定义静态方法
    Animal.Die = () => console.log("动物会叫");
    //定义老鼠子类
    function Mouse(name, color) {
        //继承name 属性
        Animal.call(this, name);
        this.color = color;
    }
    //继承父类
    Mouse.prototype = new Animal();

    //调用
    var mouse = new Mouse("jack", "黑");
    mouse.ShowName();
    Animal.Die();
    // Mouse.Die();
}
//#endregion


//#region ES6
{
    class Animal {
        //构造函数
        constructor(name) {
            this.name = name;
        }

        ShowName() {
            console.log(this.name);
        }

        static Die() {
            console.log("动物会叫");
        }
    }

    class Mouse extends Animal {
        constructor(name, color) {
            super(name);//继承属性
            this.color = color;
        }

    }

    var m = new Mouse("tom", "蓝色");
    m.ShowName();
    Mouse.Die();
}
//#endregion
