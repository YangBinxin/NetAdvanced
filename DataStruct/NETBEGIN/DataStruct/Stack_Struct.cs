using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    /// <summary>
    /// Stack(堆栈)类主要实现了一个LIFO（Last In First Out，后进先出）的机制。元素从栈的顶部插入（入栈操作），也从堆的顶部移除（出栈操作）。在Stack中主要使用Push，Pop，Peek三个方法对栈进行操作。
    ///     Push方法用于将对象插入Stack的顶部；
    ///     Pop方法用于移除并返回位于Stack顶部的对象；
    ///     Peek方法用于返回位于Stack顶部的对象但不将其移除。
    /// </summary>
    public class Stack_Struct
    {
        public static void Test()
        {
            //实例化Stack类的对象
            Stack stack = new Stack();
            //入栈,使用Pust方法向Stack对向中添加元素
            for (int i = 1; i < 6; i++)
            {
                stack.Push(i);
                Console.WriteLine("{0}入栈", i);
            }
            //返回栈顶元素
            Console.WriteLine("当前栈顶元素为：{0}", stack.Peek().ToString());
            //出栈
            Console.WriteLine("移除栈顶元素：{0}", stack.Pop().ToString());
            //返回栈顶元素
            Console.WriteLine("当前栈顶元素为：{0}", stack.Peek().ToString());
            //遍历栈
            Console.WriteLine("遍历栈");
            foreach (int i in stack)
            {
                Console.WriteLine(i);
            }
            //清空栈
            while (stack.Count != 0)
            {
                int s = (int)stack.Pop();
                Console.WriteLine("{0}出栈", s);
            }
        }
    }
}
