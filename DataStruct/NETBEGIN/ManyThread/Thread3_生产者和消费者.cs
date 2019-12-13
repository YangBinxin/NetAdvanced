using ApiCommon;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManyThread
{
    class Thread3
    {
        int balance;
        Random r = new Random();

        internal int Withdraw(int amount)
        {
            if (balance < 0)
            {
                //如果balance小于0则抛出异常
                throw new Exception("Negative Balance");
            }
            //下面的代码保证在当前线程修改balance的值完成之前
            //不会有其他线程也执行这段代码来修改balance的值
            //因此，balance的值是不可能小于0 的
            lock (this)
            {
                Console.WriteLine("Current Thread:" + Thread.CurrentThread.Name);
                //如果没有lock关键字的保护，那么可能在执行完if的条件判断之后
                //另外一个线程却执行了balance=balance-amount修改了balance的值
                //而这个修改对这个线程是不可见的，所以可能导致这时if的条件已经不成立了
                //但是，这个线程却继续执行balance=balance-amount，所以导致balance可能小于0
                if (balance >= amount)
                {
                    Thread.Sleep(5);
                    balance = balance - amount;
                    return amount;
                }
                else
                {
                    return 0; // transaction rejected
                }
            }
        }
        internal void DoTransactions()
        {
            for (int i = 0; i < 10; i++)
                Withdraw(r.Next(-50, 100));
        }

        static internal Thread[] threads = new Thread[10];
        public void RunThread()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(new ThreadStart(DoTransactions));
                threads[i] = t;
            }
            for (int i = 0; i < 10; i++)
                threads[i].Name = i.ToString();
            for (int i = 0; i < 10; i++)
                threads[i].Start();
            Console.ReadLine();
        }

    }
    public class Cell
    {
        int cellContents; // Cell对象里边的内容
        bool readerFlag = false; // 状态标志，为true时可以读取，为false则正在写入
        public int ReadFromCell()
        {
            lock (this) // Lock关键字保证了什么，请大家看前面对lock的介绍
            {
                if (!readerFlag)//如果现在不可读取
                {
                    try
                    {
                        //等待WriteToCell方法中调用Monitor.Pulse()方法
                        Monitor.Wait(this);
                    }
                    catch (SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }

                Console.WriteLine("Consume: {0}", cellContents);
                readerFlag = false;
                //重置readerFlag标志，表示消费行为已经完成
                Monitor.Pulse(this);
                //通知WriteToCell()方法（该方法在另外一个线程中执行，等待中）
            }
            return cellContents;
        }

        public void WriteToCell(int n)
        {
            lock (this)
            {
                if (readerFlag)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (SynchronizationLockException e)
                    {
                        //当同步方法（指Monitor类除Enter之外的方法）在非同步的代码区被调用
                        Console.WriteLine(e);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        //当线程在等待状态的时候中止 
                        Console.WriteLine(e);
                    }
                }
                cellContents = n;
                Console.WriteLine("Produce: {0}", cellContents);
                readerFlag = true;
                Monitor.Pulse(this);
                //通知另外一个线程中正在等待的ReadFromCell()方法
            }
        }

    }
    public class CellProd
    {
        Cell cell; // 被操作的Cell对象
        int quantity = 1; // 生产者生产次数，初始化为1 

        public CellProd(Cell box, int request)
        {
            //构造函数
            cell = box;
            quantity = request;
        }
        public void ThreadRun()
        {
            for (int looper = 1; looper <= quantity; looper++)
            {
                cell.WriteToCell(looper); //生产者向操作对象写入信息
            }
        }
    }
    public class CellCons
    {
        Cell cell;
        int quantity = 1;

        public CellCons(Cell box, int request)
        {
            //构造函数
            cell = box;
            quantity = request;
        }
        public void ThreadRun()
        {
            int valReturned;
            for (int looper = 1; looper <= quantity; looper++)
            {
                valReturned = cell.ReadFromCell();//消费者从操作对象中读取信息
            }
        }


    }
    public class CellProdCons
    {
        public void RunThread()
        {
            int result = 0; //一个标志位，如果是0表示程序没有出错，如果是1表明有错误发生
            Cell cell = new Cell();

            //下面使用cell初始化CellProd和CellCons两个类，生产和消费次数均为20次
            CellProd prod = new CellProd(cell, 20);
            CellCons cons = new CellCons(cell, 20);

            Thread producer = new Thread(new ThreadStart(prod.ThreadRun));
            Thread consumer = new Thread(new ThreadStart(cons.ThreadRun));
            //生产者线程和消费者线程都已经被创建，但是没有开始执行 
            try
            {
                producer.Start();
                consumer.Start();

                producer.Join();
                consumer.Join();
                Console.ReadLine();
            }
            catch (ThreadStateException e)
            {
                //当线程因为所处状态的原因而不能执行被请求的操作
                Console.WriteLine(e);
                result = 1;
            }
            catch (ThreadInterruptedException e)
            {
                //当线程在等待状态的时候中止
                Console.WriteLine(e);
                result = 1;
            }
            //尽管Main()函数没有返回值，但下面这条语句可以向父进程返回执行结果
            Environment.ExitCode = result;
        }
    }
}
