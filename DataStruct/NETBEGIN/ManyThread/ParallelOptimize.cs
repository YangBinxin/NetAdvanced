using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyThread
{
    /// <summary>
    /// 计算500000个随机数相加
    /// </summary>
    class ParallelOptimize
    {
        private const int ITEMS = 500000;
        private int[] arr = null;

        public ParallelOptimize()
        {
            arr = new int[ITEMS];
            var rnd = new Random();
            for (int i = 0; i < ITEMS; i++)
            {
                arr[i] = rnd.Next(1000);
            }
        }

        public long ArraySum()
        {
            return arr.Sum();
        }

        //for循环相加
        public long ForLocalArr()
        {
            long total = 0;
            for (int i = 0; i < ITEMS; i++)
            {
                total += int.Parse(arr[i].ToString());
            }

            return total;
        }

        //foreach相加
        public long ForeachLocalArr()
        {
            long total = 0;
            foreach (var item in arr)
            {
                total += int.Parse(item.ToString());
            }

            return total;
        }

        //初次线程
        public object _lock = new object();
        public long ThreadPoolWithLock()
        {
            long total = 0;
            int threads = 8;
            var partSize = arr.Length / threads;
            Task[] tasks = new Task[threads];
            for (int iThread = 0; iThread < tasks.Length; iThread++)
            {
                var localThread = iThread;
                tasks[localThread] = Task.Run(() =>
                {
                    for (int j = partSize * localThread; j < (localThread + 1) * partSize; j++)
                    {
                        lock (_lock)
                        {
                            total += arr[j];
                        }
                    }
                });
            }
            Task.WaitAll(tasks);
            return total;
        }

        //改进，使用局部变量
        public long ThreadPoolWithLock2()
        {
            long total = 0;
            int threads = 8;
            var partSize = arr.Length / threads;
            Task[] tasks = new Task[threads];
            for (int iThread = 0; iThread < tasks.Length; iThread++)
            {
                var localThread = iThread;
                tasks[localThread] = Task.Run(() =>
                {
                    long temp = 0;
                    for (int j = partSize * localThread; j < (localThread + 1) * partSize; j++)
                    {
                        temp += arr[j];
                    }

                    lock (_lock)
                    {
                        total += temp;
                    }
                });
            }
            Task.WaitAll(tasks);
            return total;
        }

        //引入 ParallelFor
        public long ParallelForWithLock()
        {
            long total = 0;
            int parts = 8;
            int partSize = ITEMS / parts;
            var parallel = Parallel.For(0, parts, new ParallelOptions(), (iter) =>
              {
                  long temp = 0;
                  for (int iThread = iter * partSize; iThread < (iter + 1) * partSize; iThread++)
                  {
                      var localThread = iThread;
                      temp += arr[localThread];
                  }
                  lock (_lock)
                  {
                      total += temp;
                  }
              });
            return total;
        }

        //改进 ParallelFor
        public long ParallelForWithLock2()
        {
            long total = 0;
            int parts = 8;
            int partSize = ITEMS / parts;
            var parallel = Parallel.For(0, parts, localInit: () => 0L, body: (iter, state, localTotal) =>
                 {
                     for (int iThread = iter * partSize; iThread < (iter + 1) * partSize; iThread++)
                     {
                         var localThread = iThread;
                         localTotal += arr[localThread];
                     }
                     return localTotal;
                 }, localFinally: (localTotal) => { total += localTotal; });
            return total;
        }
    }
}
