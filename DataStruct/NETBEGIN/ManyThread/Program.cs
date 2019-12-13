using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyThread
{
    class Program
    {
        /// <summary>
        /// 1、线程：当一个程序开始运行时，它就是一个进程，进程包括运行中的程序和程序所使用到的内存和系统资源。而一个进程又是由多个线程所组成的。
        /// 2、进程：线程是程序中的一个执行流，每个线程都有自己的专有寄存器(栈指针、程序计数器等)，但代码区是共享的，即不同的线程可以执行同样的函数。
        /// 3、多线程：多线程是指程序中包含多个执行流，即在一个程序中可以同时运行多个不同的线程来执行不同的任务，也就是说允许单个程序创建多个并行执行的线程来完成各自的任务。
        /// 4、多线程好处：可以提高CPU的利用率。在多线程程序中，一个线程必须等待的时候，CPU可以运行其它的线程而不是等待，这样就大大提高了程序的效率。
        /// 5、多线程不利方面：线程也是程序，所以线程需要占用内存，线程越多占用内存也越多； 多线程需要协调和管理，所以需要CPU时间跟踪线程； 线程之间对共享资源的访问会相互影响，必须解决竞用共享资源的问题；线程太多会导致控制太复杂，最终可能造成很多Bug；
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Thread2 thread2 = new Thread2();
            //thread2.RunThread();

            //Thread3 thread3 = new Thread3();
            //thread3.RunThread();

            //CellProdCons cell = new CellProdCons();
            //cell.RunThread();

            //Console.WriteLine("程序启动："+DateTime.Now.ToString("HH:mm:ss ffff"));
            //NopiExportData npoi = new NopiExportData();
            //npoi.RunThread();
            //Console.WriteLine("程序结束：" + DateTime.Now.ToString("HH:mm:ss ffff"));

            //Thread4 thread4 = new Thread4();
            //thread4.RunThread();

            //Console.WriteLine("程序启动：" + DateTime.Now.ToString("HH:mm:ss ffff"));
            //NopiExportDataThreadPool pool = new NopiExportDataThreadPool();
            //pool.RunThread();

            //NpoiExportDataTask task = new NpoiExportDataTask();
            //task.RunThread();

            //Console.WriteLine("程序结束：" + DateTime.Now.ToString("HH:mm:ss ffff"));

            ParallelOptimize parallelOptimize = new ParallelOptimize();
            DateTime _for = DateTime.Now;
            var result = parallelOptimize.ArraySum();
            Console.WriteLine("Sum \t\t\t" + "结果：" + result + "\t\t" + "运行时间：" + (DateTime.Now.Ticks - _for.Ticks));

            _for = DateTime.Now;
            result = parallelOptimize.ForLocalArr();
            Console.WriteLine("For \t\t\t" + "结果：" + result + "\t\t" + "运行时间：" + (DateTime.Now.Ticks - _for.Ticks));

            _for = DateTime.Now;
            result = parallelOptimize.ForeachLocalArr();
            Console.WriteLine("Foreach \t\t" + "结果：" + result + "\t\t" + "运行时间：" + (DateTime.Now.Ticks - _for.Ticks));

            _for = DateTime.Now;
            result = parallelOptimize.ThreadPoolWithLock();
            Console.WriteLine("ThreadPool \t\t" + "结果：" + result + "\t\t" + "运行时间：" + (DateTime.Now.Ticks - _for.Ticks));

            _for = DateTime.Now;
            result = parallelOptimize.ThreadPoolWithLock2();
            Console.WriteLine("ThreadPool2 \t\t" + "结果：" + result + "\t\t" + "运行时间：" + (DateTime.Now.Ticks - _for.Ticks));

            _for = DateTime.Now;
            result = parallelOptimize.ParallelForWithLock();
            Console.WriteLine("ParallelFor \t\t" + "结果：" + result + "\t\t" + "运行时间：" + (DateTime.Now.Ticks - _for.Ticks));

            _for = DateTime.Now;
            result = parallelOptimize.ParallelForWithLock2();
            Console.WriteLine("ParallelFor2 \t\t" + "结果：" + result + "\t\t" + "运行时间：" + (DateTime.Now.Ticks - _for.Ticks));

            Console.ReadKey();
        }

    }
}
