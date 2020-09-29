using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GenericsLearn
{
    public class GenericWorker
    {
        public void CreateDifferentDataList()
        {
            Node head = new TypedNode<Char>('.');
            var next1 = new TypedNode<DateTime>(DateTime.Now, head);
            var next2 = new TypedNode<string>("Today is ", next1);
            Console.WriteLine(next2.ToString());

            List<Node> list = new List<Node>() { head, next1, next2 };

            Console.WriteLine("Printing list of different nodes: ");
            foreach (var node in list)
            {
                Console.WriteLine(node.mNext);
            }


        }

        public void Swap<T>(ref T first, ref T second)
        {
            Console.WriteLine($"Values before swap -  first: {first} second: {second}");
            T temp = second;
            second = first;
            first = temp;
            Console.WriteLine($"Values after swap -  first: {first} second: {second}");

        }

        public void RunPerfTests()
        {
            ValueTypePerfTest();
            ReferenceTypePerfTest();
        }

        private static void ValueTypePerfTest()
        {
            const Int32 count = 100000000;
            using (new OperationTimer("List<Int32>"))
            {
                List<Int32> l = new List<Int32>();
                for (Int32 n = 0; n < count; n++)
                {
                    l.Add(n); // Без упаковки
                    Int32 x = l[n]; // Без распаковки
                }
                l = null; // Для удаления в процессе уборки мусора
            }
            using (new OperationTimer("ArrayList of Int32"))
            {
                ArrayList a = new ArrayList();
                for (Int32 n = 0; n < count; n++)
                {
                    a.Add(n); // Упаковка
                    Int32 x = (Int32)a[n]; // Распаковка
                }
                a = null; // Для удаления в процессе уборки мусора
            }
        }
        private static void ReferenceTypePerfTest()
        {
            const Int32 count = 100000000;
            using (new OperationTimer("List<String>"))
            {
                List<String> l = new List<String>();
                for (Int32 n = 0; n < count; n++)
                {
                    l.Add("X"); // Копирование ссылки
                    String x = l[n]; // Копирование ссылки
                }
                l = null; // Для удаления в процессе уборки мусора
            }
            using (new OperationTimer("ArrayList of String"))
            {
                ArrayList a = new ArrayList();
                for (Int32 n = 0; n < count; n++)
                {
                    a.Add("X"); // Копирование ссылки
                    String x = (String)a[n]; // Проверка преобразования
                } // и копирование ссылки
                a = null; // Для удаления в процессе уборки мусора
            }
        }
    }
}
