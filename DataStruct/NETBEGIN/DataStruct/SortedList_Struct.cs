using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    /// <summary>
    /// SortedList类也是键/值对的集合，但与哈希表不同的是这些键/值对是按键排序，并可以按照键和索引访问。在SortedList中主要使用Add、Remove、RemoveAt三个方法对SortedList进行操作。
    /// Add方法用于将带有指定键和值的元素添加到SortedList中；
    /// Remove方法用于从SortedList中移除带有指定键的元素；
    /// RemoveAt方法用于移除SortedList的指定索引处的元素。
    /// </summary>
    public class SortedList_Struct
    {
        public static void Test()
        {
            //实例化SortedListTest类的对象
            SortedList student = new SortedList();
            //向SortedList中添加元素
            student.Add("S1001", "Tom");
            student.Add("S1003", "Jim");
            student.Add("S1002", "Lily");
            student.Add("S1004", "Lucy");
            //遍历SortedList
            foreach (DictionaryEntry element in student)
            {
                string id = element.Key.ToString();
                string name = element.Value.ToString();
                Console.WriteLine("学生的ID：{0}   学生姓名：{1}", id, name);
            }
            //移除SortedList中key为“S1003”的元素
            student.Remove("S1003");
            //移除SortedList中索引为“”的元素，即第一个元素
            student.RemoveAt(0);
        }
    }
}
