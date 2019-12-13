using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDelegate
{
    class MyDelegateTest
    {
        //1. 委托的声明
        public delegate void NoReturnNoPara();
        public delegate int WithReturnNoPara();
        public delegate void NoReturnWithPara(int id, string name);
        public delegate MyDelegateTest WithReturnWithPara(DateTime time);

        public void show()
        {
            //以“有参无返回值委托”为例,介绍委托的各种用法
            //2.1 用法一
            {
                NoReturnWithPara methord = new NoReturnWithPara(this.Test1);
                methord.Invoke(1, "唐马儒1");
            }

            //2.2 用法二
            {
                NoReturnWithPara methord = this.Test1;
                methord.Invoke(2, "唐马儒2");
            }

            //2.3 用法三 DotNet 2.0 时代
            {
                NoReturnWithPara methord = new NoReturnWithPara
                (
                    delegate (int id, string name)
                    {
                        Console.WriteLine("{0} {1}", id, name);
                    }
                );
                methord.Invoke(3, "唐马儒3");
            }

            //2.4 用法四 DotNet 3.0 时代
            {
                NoReturnWithPara methord = new NoReturnWithPara
                (
                    (int id, string name) =>
                    {
                        Console.WriteLine("{0} {1}", id, name);
                    }
                );
                methord.Invoke(4, "唐马儒4");
            }

            //2.5 用法五 委托约束
            {
                NoReturnWithPara methord = new NoReturnWithPara
                (
                    (id, name) =>
                    {
                        Console.WriteLine("{0} {1}", id, name);
                    }
                );
                methord.Invoke(5, "唐马儒5");
            }

            //2.6 用法六 （如果方法体只有一行，可以去掉大括号和分号）
            {
                NoReturnWithPara methord = new NoReturnWithPara((id, name) => Console.WriteLine("{0} {1}", id, name));
                methord.Invoke(6, "唐马儒6");
            }

            //2.7 用法七
            {
                NoReturnWithPara methord = (id, name) => Console.WriteLine("{0} {1}", id, name);
                methord.Invoke(7, "唐马儒7");
                methord(7, "唐马儒7");
            }

            //2.8 用法八
            {
                Func<int, bool> methord = x => x > 6;
                Console.WriteLine(methord.Invoke(8));
            }

            //2.9 用法九
            {
                Func<int, int, string> methord = (x, y) =>
                 {
                     return (x + y).ToString();
                 };
                Console.WriteLine(methord.Invoke(8, 2));
            }

        }

        private void Test1(int id, string name)
        {
            Console.WriteLine("{0} {1}", id, name);
        }

        private void Test2()
        {
            Console.WriteLine("DoNothing");
        }

        private void Test3()
        {
            Console.WriteLine("DoNothing");
        }
    }

    class CarFactory
    {
        /// <summary>
        /// 方案二：通过传递委托来建造不同的发动机
        /// 原理：传递一个逻辑给我,我去执行
        /// 优点：如果增加新的发动机,只需要单独新增对应建造发动机的方法即可,不需要改变BuildEngine2的内部逻辑,符合开闭原则
        /// </summary>
        public void BuildEngine(BuildEngineDel be)
        {
            be.Invoke();
        }

        //声明一个无参数的委托
        public delegate void BuildEngineDel();

        //下面三个是建造不同发动机的方法
        public void BuildNatural()
        {
            Console.WriteLine("建造自然吸气发动机");
        }
        public void BuildTurbo()
        {
            Console.WriteLine("建造涡轮增压发动机");
        }
        public void BulidElectric()
        {
            Console.WriteLine("建造电动发动机");
        }
    }

    class Calculator
    {
        //解决方案三：声明一个万能方法，传递一个委托进来，相当于传递了一个业务逻辑进行，在该方法里只需要执行即可
        public delegate T myDel<T>(T t);
        /// <summary>
        /// 万能方法
        /// </summary>
        /// <param name="arrs">int类型的数组 </param>
        /// <param name="mydel">系统自带的委托（也可以自定义委托），<int,int>代表：参数和返回值均为int类型</param>
        public static void MySpecMethord<T>(T[] arrs, myDel<T> myDel)
        {
            for (int i = 0; i < arrs.Length; i++)
            {
                arrs[i] = myDel(arrs[i]);
                //arrs[i] = mydel.Invoke(arrs[i]);  //等价于上面那句
                Console.WriteLine(arrs[i]);
            }
        }
    }

    class myMultiDelegate
    {
        //传统的解决方案,在一个方法里书写各种业务。
        //缺点：后期需求变更了,当需要插入更多数据的时候,只能修改该方法内部逻辑,不符合开闭原则


        //下面介绍利用多播委托进行解耦插件式开发
        public delegate void myRegisterDelegate(string userName, string userPwd);

        public static void myRegister(myRegisterDelegate mrd, string userName, string userPwd)
        {
            //对密码进行Md5加密后在进行后续操作
            string md5userPwd = userPwd + "MD5";  //模拟Md5
            mrd.Invoke(userName, md5userPwd);
        }
        /// <summary>
        /// 查询方法
        /// </summary>
        public static void checkIsRegister(string userName, string userPwd)
        {
            Console.WriteLine("查询成功");
        }
        /// <summary>
        /// 向表1中插入数据
        /// </summary>
        public static void writeTable1(string userName, string userPwd)
        {
            Console.WriteLine("向表1中插入数据{0}，{1}", userName, userPwd);
        }
        /// <summary>
        /// 向表2中插入数据
        /// </summary>
        public static void writeTable2(string userName, string userPwd)
        {
            Console.WriteLine("向表2中插入数据{0}，{1}", userName, userPwd);
        }
        /// <summary>
        /// 向表3中插入数据
        /// </summary>
        public static void writeTable3(string userName, string userPwd)
        {
            Console.WriteLine("向表3中插入数据{0}，{1}", userName, userPwd);
        }
    }

    public class MyEvent
    {
        //下面的案例用委托和事件实现相同的功能
        public Action myDelegate;
        /// <summary>
        /// 触发委托执行的方法
        /// </summary>
        public void realizeDelegate()
        {
            if (myDelegate != null)
            {
                myDelegate.Invoke();
            }
        }
        public event Action myEvent;
        /// <summary>
        /// 触发事件执行的方法
        /// </summary>
        public void realizeEvent()
        {
            if (myEvent != null)
            {
                myEvent.Invoke();
            }
        }

        #region 供委托和事件测试调用的方法

        public static void T1()
        {
            Console.WriteLine("方法一");
        }

        public static void T2()
        {
            Console.WriteLine("方法二");
        }

        public static void T3()
        {
            Console.WriteLine("方法三");
        }

        #endregion
    }
}
