using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    /// <summary>
    /// Hashtable（哈希表）是一种键/值对集合，这些键/值对根据键的哈希代码进行组织。在一个Hashtable中插入一对Key/Value时，它自动将Key值映射到Value，并允许获取与一个指定的Key相关联的value。在Hashtable中主要使用Add、Remove两个方法对哈希表进行操作
    ///     Add方法用于将带有指定键和值的元素添加到Hashtable中；
    ///     Remove方法用于从Hashtable中移除带有指定键的元素。
    /// </summary>
    public class HashTable_Struct
    {
        public static void Test()
        {
            //实例化Hashtable类的对象
            Hashtable student = new Hashtable();
            //向Hashtable中添加元素
            student.Add("S1001", "Tom");
            student.Add("S1002", "Jim");
            student.Add("S1003", "Lily");
            student.Add("S1004", "Lucy");
            //遍历Hashtable
            foreach (DictionaryEntry element in student)
            {
                string id = element.Key.ToString();
                string name = element.Value.ToString();
                Console.WriteLine("学生的ID：{0}   学生姓名：{1}", id, name);
            }
            //移除Hashtable中的元素
            student.Remove("S1003");
        }
    }
}
