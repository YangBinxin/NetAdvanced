using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            //MyDelegateTest test = new MyDelegateTest();
            //test.show();

            CarFactory factory = new CarFactory();
            CarFactory.BuildEngineDel Natural = factory.BuildNatural;
            factory.BuildEngine(Natural);

            //委托
            {
                Console.WriteLine("--------------------------3.将委托当做参数,进行解耦,实现插件式编程(案例二)------------------------------------");
                int[] arr = { 1, 2, 3, 4, 5 };
                //调用形式一：
                Console.WriteLine("--------------------------调用形式一------------------------------------");
                Calculator.myDel<int> mydel = t1;
                Calculator.MySpecMethord(arr, mydel);
                //调用形式二：
                Console.WriteLine("--------------------------调用形式二------------------------------------");
                Calculator.MySpecMethord(arr, t2);
                //调用形式三：
                Console.WriteLine("--------------------------调用形式三------------------------------------");
                Calculator.MySpecMethord(arr, x => x * 2);
            }

            //多播委托
            {
                myMultiDelegate.myRegisterDelegate mrd = myMultiDelegate.checkIsRegister;
                mrd += myMultiDelegate.writeTable1;
                mrd += myMultiDelegate.writeTable2;
                mrd += myMultiDelegate.writeTable3;
                mrd -= myMultiDelegate.writeTable1;

                myMultiDelegate.myRegister(mrd, "张三", "123456");
            }

            //事件与委托
            {
                //委托中的订阅者可以直接Invoke()来调用委托，
                //而事件中的订阅者不能直接Invoke()调用委托，只能通过广播者中的方法来实现调用委托，从而保证广播者独享控制权。
                Console.WriteLine("--------------------------七. 事件------------------------------------");
                MyEvent m1 = new MyEvent();
                //1. 委托实现
                Console.WriteLine("--------------------------1. 委托实现------------------------------------");
                //订阅者进行订阅
                m1.myDelegate += MyEvent.T1;
                m1.myDelegate += MyEvent.T2;
                m1.myDelegate += MyEvent.T3;
                m1.myDelegate.Invoke();  //委托中的订阅者可以直接调用委托
                m1.myDelegate -= MyEvent.T2;
                m1.realizeDelegate();
                //2. 事件实现
                Console.WriteLine("--------------------------2. 事件实现------------------------------------");
                m1.myEvent += MyEvent.T1;
                m1.myEvent += MyEvent.T2;
                m1.myEvent += MyEvent.T3;
                // m1.myEvent.Invoke();    //事件中的订阅者不能直接调用委托
                m1.realizeEvent();         //只能通过发布者中方法来实现委托，保证发布者独享控制权
                m1.myEvent -= MyEvent.T2;
                m1.realizeEvent();
            }

            Console.ReadKey();
        }

        /// <summary>
        /// 加倍方法
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int t1(int x)
        {
            return x * 2;
        }
        /// <summary>
        /// 乘方方法
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int t2(int x)
        {
            return x * x;
        }
        /// <summary>
        /// 立方方法
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int t3(int x)
        {
            return x * x * x;
        }
    }
}
