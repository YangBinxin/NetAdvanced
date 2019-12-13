using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    /// <summary>
    /// Queue（队列）类主要实现了一个FIFO（First In First Out，先进先出）的机制。元素在队列的尾部插入（入队操作），并从队列的头部移出（出队操作）。在Queue中主要使用Enqueue、Dequeue、Peek三个方法对队进行操作。
    ///     Enqueue方法用于将对象添加到Queue的结尾处；
    ///     Dequeue方法移除并返回位于Queue开始处的对象；
    ///     Peek方法用于返回位于Queue开始处的对象但不将其移除。
    /// </summary>
    public class Queue_Struct
    {
        public static void Test()
        {
            //实例化Queue类的对象
            Queue queue = new Queue();
            //入栈,使用Pust方法向Stack对向中添加元素
            for (int i = 1; i < 6; i++)
            {
                queue.Enqueue(i);
                Console.WriteLine("{0}入队", i);
            }
            //返回队开始处的元素
            Console.WriteLine("当前队开始处元素为：{0}", queue.Peek().ToString());
            //遍历队
            Console.WriteLine("遍历队");
            foreach (int i in queue)
            {
                Console.WriteLine(i);
            }
            //清空栈
            while (queue.Count != 0)
            {
                int q = (int)queue.Dequeue();
                Console.WriteLine("{0}出队", q);
            }
        }
    }
}
