using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    /// <summary>
    /// ArrayList
    /// ArrayList类主要用于对一个数组中的元素进行各种处理。在ArrayList中主要使用Add、Remove、RemoveAt、Insert四个方法对栈进行操作。
    ///     Add方法用于将对象添加到ArrayList的结尾处；
    ///     Remove方法用于从ArrayList中移除特定对象的第一个匹配项；
    ///     RemoveAt方法用于移除ArrayList的指定索引处的元素；
    ///     Insert方法用于将元素插入ArrayList的指定索引处。
    /// </summary>
    public class ArrayList_Struct
    {
        public static void Test()
        {
            ArrayList arrlist = new ArrayList();//实例化一个ArrayList对象
            
            //使用Add方法向ArrayList中添加元素，将元素添加到ArrayList对象的末尾
            arrlist.Add("苹果");
            arrlist.Add("香蕉");
            arrlist.Add("葡萄");
            foreach (int n in new int[3] { 0, 1, 2 })
            {
                arrlist.Add(n);
            }

            //移除值为的第一个元素
            arrlist.Remove(0);
            //移除当前索引为的元素，即第4个元素
            arrlist.RemoveAt(3);
            //在指定索引处添加一个元素
            arrlist.Insert(1, "apple");
            //遍历ArrayList，并输出所有元素
            for (int i = 0; i < arrlist.Count; i++)
            {
                Console.WriteLine(arrlist[i].ToString());
            }
        }
    }
}
