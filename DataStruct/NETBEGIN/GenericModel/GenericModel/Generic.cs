using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericModel
{
    /// <summary>
    /// 泛型
    ///  1、泛型是C# 2.0和CLR的一个特性，在1.0时代，声明一个可以给多个类型参数共同使用的方法很繁杂，需要编写多个方法而参数不同，当然可以使用object，
    ///     但通过object会发生装箱拆箱，降低性能。而泛型为.Net引入了类型参数的概念，使得声明类和方法时不必指定具体的类型参数，其具体类型可以延迟到客户代码当中实现。
    ///  
    ///  2、声明泛型的时候，不指定具体类型，可以用 T 或其他非关键字，而在调用的时候进行制定，其采用的思想是延迟思想。 泛型只能声明 泛型类、泛型方法、泛型接口、泛型委托四种
    ///  
    ///  3、为了满足不同类型，相同代码的重用
    ///  
    ///  4、声明泛型时，可以对泛型T进行相应的约束。泛型约束可以有基类约束，
    ///     接口约束（where T : IInterface），引用类型约束（where T : class ），值类型约束 （where T : struct），无参构造函数约束（where T : new()）
    ///     
    ///  5、泛型的协变和逆变只对修饰泛型接口和泛型委托有作用，泛型方法和泛型类没有作用，只针对引用类型有效。 
    ///     协变（covariant）：在泛型类型前面加上 out 表示该类型只能作为返回值，不能作为传入参数，使得子类集合可以赋值给父类集合 
    ///     逆变 （contravariant）：在泛型类型前面加上 in ，表示该类型只能作为传入参数使用，不能作为返回值，使得可以将父类赋值给子类
    ///     
    ///  6、泛型在JIT即时编译器进行编译的时候，同一个泛型类，不同的参数类型，会生成不同的类型。 
    ///     静态字段/静态构造函数在调用的时候只初始化一次，常驻内存，因此，可以利用这两个特性，结合使用，当在泛型类中声明静态字段和静态构造方法时，
    ///     不同的类型会生成独立的类，作为缓存使用，性能很高
    /// </summary>
    public class Generic
    {
        //泛型方法
        public void GenericMethod<T>(T t) { }

        //泛型接口
        public interface IGenericInterface<T> { }

        //泛型类
        public class IGenericClass<T> { public T Property { get; set; } }

        //泛型委托
        public delegate void GenericDelegate<T>(T tParameter);



        //斜边与逆变
        public void Test()
        {
            {
                Bird bird1 = new Bird();
                Bird bird2 = new Sparrow();
                Sparrow sparrow1 = new Sparrow();
                //Sparrow sparrow2 = new Bird();//错误，不是所有的鸟，都是麻雀
            }

            //引入问题，子类可以赋值给父类，那子类的集合是否可以赋值给父类的集合呢？
            {
                List<Bird> birdList1 = new List<Bird>();
                //List<Bird> birdList2 = new List<Sparrow>(); //错误，因为List<Sparrow>不是List<Bird>的子类，没有父子关系

                List<Bird> birdList3 = new List<Sparrow>().Select(c => (Bird)c).ToList();
                //只能把元素都转一遍，正确
            }

            {
                //协变：接口泛型参数加了个out，就是为了解决上述的问题
                IEnumerable<Bird> birdList4 = new List<Bird>();
                IEnumerable<Bird> birdList5 = new List<Sparrow>();

                Func<Bird> func = new Func<Sparrow>(() => null);

                ICustomerListOut<Bird> customerList1 = new CustomerListOut<Bird>();
                ICustomerListOut<Bird> customerList2 = new CustomerListOut<Sparrow>();
            }

            {
                //逆变
                ICustomerListIn<Sparrow> customerList2 = new CustomerListIn<Sparrow>();
                ICustomerListIn<Sparrow> customerList1 = new CustomerListIn<Bird>();

                ICustomerListIn<Bird> birdList1 = new CustomerListIn<Bird>();
                birdList1.Show(new Sparrow());
                birdList1.Show(new Bird());

                Action<Sparrow> act = new Action<Bird>((Bird i) => { });
            }

            {
                IMyList<Sparrow, Bird> myList1 = new MyList<Sparrow, Bird>();
                IMyList<Sparrow, Bird> myList2 = new MyList<Sparrow, Sparrow>();//协变
                IMyList<Sparrow, Bird> myList3 = new MyList<Bird, Bird>();//逆变
                IMyList<Sparrow, Bird> myList4 = new MyList<Bird, Sparrow>();//协变+逆变
                
            }

            

        }

    }


    /* 指定类型之后的泛型，就已经不再是泛型的了 */
    public class ChildClass : Generic.IGenericClass<int>, Generic.IGenericInterface<string>
    {
    }

    public class ChildClassGeneric<T, S> : Generic.IGenericClass<T>, Generic.IGenericInterface<S>
    {

    }


    #region 协变与逆变

    public class Bird
    {
        public int Id { get; set; }
    }
    public class Sparrow : Bird
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// 逆变：只能修饰传入参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListIn<in T>
    {
        void Show(T t);
    }

    public class CustomerListIn<T> : ICustomerListIn<T>
    {
        public void Show(T t)
        {
        }
    }

    /// <summary>
    /// out 协变 只能是返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListOut<out T>
    {
        T Get();
    }

    public class CustomerListOut<T> : ICustomerListOut<T>
    {
        public T Get()
        {
            return default(T);
        }
    }

    public interface IMyList<in inT, out outT>
    {
        void Show(inT t);
        outT Get();
        outT Do(inT t);
    }

    public class MyList<T1, T2> : IMyList<T1, T2>
    {
        public void Show(T1 t)
        {
            Console.WriteLine(t.GetType().Name);
        }

        public T2 Get()
        {
            Console.WriteLine(typeof(T2).Name);
            return default(T2);
        }

        public T2 Do(T1 t)
        {
            Console.WriteLine(t.GetType().Name);
            Console.WriteLine(typeof(T2).Name);
            return default(T2);
        }
    }


    #endregion

    #region 泛型缓存

    /// <summary>
    /// 字典缓存：静态属性常驻内存
    /// </summary>
    public class DictionaryCache
    {
        private static Dictionary<Type, string> _TypeTimeDictionary = null;
        static DictionaryCache()
        {
            Console.WriteLine("This is DictionaryCache 静态构造函数");
            _TypeTimeDictionary = new Dictionary<Type, string>();
        }

        public static string GetCache<T>()
        {
            Type type = typeof(Type);
            if (!_TypeTimeDictionary.ContainsKey(type))
            {
                _TypeTimeDictionary[type] = string.Format("{0}_{1}", typeof(T).FullName, DateTime.Now.ToString("yyyyMMddHHmmss.fff"));
            }
            return _TypeTimeDictionary[type];
        }
    }

    /// <summary>
    /// 泛型缓存：每个不同的T，都会生成一份不同的副本
    /// 适合不同类型，需要缓存一份数据的场景，效率高
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericCache<T>
    {
        static GenericCache()
        {
            Console.WriteLine("This is GenericCache 静态构造函数");
            _TypeTime = string.Format("{0}_{1}", typeof(T).FullName, DateTime.Now.ToString("yyyyMMddHHmmss.fff"));
        }

        private static string _TypeTime = "";

        public static string GetCache()
        {
            return _TypeTime;
        }
    }

    #endregion
}
