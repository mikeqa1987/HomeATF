using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsLearn
{
    public class Wolf : Animal, IMammal
    {
        public override void Run()
        { 
            Console.WriteLine("I Am Wolf!!!WOLF!!!");
        }
    }
}
