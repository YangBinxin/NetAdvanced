using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion

            /*
            Console.WriteLine("1、ArrayList数据结构展示：========================");
            ArrayList_Struct.Test();
            Console.WriteLine();

            Console.WriteLine("2、Stack数据结构展示：========================");
            Stack_Struct.Test();
            Console.WriteLine();

            Console.WriteLine("3、Queue数据结构展示：========================");
            Queue_Struct.Test();
            Console.WriteLine();

            Console.WriteLine("4、HashTable数据结构展示：========================");
            HashTable_Struct.Test();
            Console.WriteLine();

            Console.WriteLine("5、SortedList数据结构展示：========================");
            SortedList_Struct.Test();
            Console.WriteLine();

            */

            #endregion
            PrintRandom();
            Console.ReadKey();
        }

        public static object objLock = new object();
        public static void PrintRandom()
        {
            int[] intArray = new int[7];
            for (int i = 0; i < 7; i++)
            {
                while (true)
                {
                    lock (objLock)
                    {
                        int rand = new Random().Next(1, 11);
                        if (!intArray.Contains(rand))
                        {
                            intArray[i] = rand;
                            break;
                        }
                    }
                }
            }

            Array.Sort(intArray);
            intArray.Reverse();
            Console.WriteLine(string.Join(",", intArray));
        }
    }
}
