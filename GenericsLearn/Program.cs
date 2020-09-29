using System;
using System.Collections.Generic;

namespace GenericsLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            //var gWorker = new GenericWorker();
            //gWorker.CreateDifferentDataList();
            //int f = 1;
            //int s = 2;
            //gWorker.Swap<int>(ref f, ref s);

            //Console.WriteLine("Fron main: ");
            //Console.WriteLine(f);
            //Console.WriteLine(s);

            //Console.WriteLine("Running perf tests:");
            //gWorker.RunPerfTests();

            
            List<Animal> a1 = new List<Animal>();
            IMammal<Animal> mammals = new AnimalsList<Bear>() { new Bear() };
            
            a1.Add(new Bear());
            a1.Add(new Lion());
            a1.Add(new Wolf());
            foreach (var item in a1)
            {
                item.Run();
                Console.WriteLine(item.GetType());
                Console.WriteLine($"is result { item is Animal}");
                Console.WriteLine($"typeof result { item.GetType() == typeof(Animal)}");
            }
            
        }
    }
}
