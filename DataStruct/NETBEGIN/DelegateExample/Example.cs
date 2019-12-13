using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateExample
{
    class Example
    {
        //1、自定义委托
        public delegate string RunDelegate(string a, string b);

        public delegate void ReflectionDelegate<T,U>(T t,U u);

        public static string Test1(string a, string b)
        {
            Console.WriteLine("自定义委托RunDelegate有参数，有返回值。{0}--{1}", a, b);
            return a + "--" + b;
        }

        public static void one(string a,string b)
        {
            Console.WriteLine("这是1号委托");
        }
        public static void two(string a, string b)
        {
            Console.WriteLine("这是2号委托");
        }
        public static void three(string a, string b)
        {
            Console.WriteLine("这是3号委托");
        }
        public static void four(string a, string b)
        {
            Console.WriteLine("这是4号委托");
        }

        public static void ReflectionMethod<T,U>(T t,U u)
        {
            Console.WriteLine("泛型参数为t:{0},u:{1}", t, u);
        }
    }

    //新郎官，充当事件发布者角色
    public class Bridegroom
    {
        //自定义委托
        public delegate void MarryHandler(string msg);
        //定义事件
        public event MarryHandler MarryEvent;

        //发布事件
        public void OnMarriageComing(string msg)
        {
            if (MarryEvent != null)
            {
                MarryEvent(msg);
            }
        }
    }

    public class Friend
    {
        //属性
        public string Name { get; set; }
        //构造函数
        public Friend(string name)
        {
            Name = name;
        }
        //事件处理函数，该函数需要符合 MarryHandler委托的定义
        public void SendMessage(string message)
        {
            //输出通知信息
            Console.WriteLine(message);
            //对事件做出处理
            Console.WriteLine(this.Name + "收到了，到时候准时参加");
        }
    }

}
