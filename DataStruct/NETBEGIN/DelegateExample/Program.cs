using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------1、走自定义委托------------------------------------");
            {
                Example.RunDelegate run = Example.Test1;
                var result = run.Invoke("这是参数a", "这是参数b");
                Console.WriteLine("这是接受的返回值{0}", result);
            }

            Console.WriteLine();
            Console.WriteLine("--------------------------2、系统内置委托Action，无返回值------------------------------------");
            {
                Action<string, string> action = (a, b) =>
                 {
                     Console.WriteLine("执行内置委托Action，接受的输入参数，a:{0},b:{1}", a, b);
                 };
                action.Invoke("这是a参数", "这是b参数");
            }

            Console.WriteLine();
            Console.WriteLine("--------------------------3、系统内置委托Func，有返回值------------------------------------");
            {
                Func<int, int, int> func = (a, b) =>
                  {
                      Console.WriteLine("接受到的输入参数，a:{0},b:{1}",a,b);
                      return a * b;
                  };
                var result = func.Invoke(10, 25);
                Console.WriteLine("Func返回的参数a与b的乘积为:" + result);
            }

            Console.WriteLine();
            Console.WriteLine("--------------------------4、系统内置委托Action,多播匿名委托------------------------------------");
            {
                Action<string, string> action = Example.one;
                action += Example.two;
                action += Example.three;
                action += Example.four;
                action += (a, b) => Console.WriteLine("这是5号委托");//匿名方法，无法取消委托
                action -= Example.two;
                action -= Example.four;

                action.Invoke("a", "b");
            }

            Console.WriteLine();
            Console.WriteLine("--------------------------5、泛型委托-----------------------------------");
            {
                Example.ReflectionDelegate<int, string> example = Example.ReflectionMethod;
                example.Invoke(50, "string参数");
            }

            Console.WriteLine();
            Console.WriteLine("--------------------------6、事件-----------------------------------");
            {
                Bridegroom bridegroom = new Bridegroom();
                Friend friend1 = new Friend("张三");
                Friend friend2 = new Friend("李四");
                Friend friend3 = new Friend("王五");

                //使用 “+=” 来订阅事件
                bridegroom.MarryEvent += friend1.SendMessage;
                bridegroom.MarryEvent += friend2.SendMessage;

                //发出通知，此时只有订阅了事件的对象才能收到通知
                bridegroom.OnMarriageComing("朋友门，我要结婚了，到时候准时参加婚礼！");

                Console.WriteLine("---------------------------------------------");

                //使用 “-=” 来取消事件订阅，此时李四将收不到通知
                bridegroom.MarryEvent -= friend2.SendMessage;

                //使用 “+=” 来订阅事件，此时王五可以收到通知
                bridegroom.MarryEvent += friend3.SendMessage;
                //发出通知
                bridegroom.OnMarriageComing("朋友门，我要结婚了，到时候准时参加婚礼！");
            }

            Console.ReadKey();
        }
    }
}
