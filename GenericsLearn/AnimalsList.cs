using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsLearn
{
    public class AnimalsList<T> : List<T>, IMammal<T>
    {
        public T GetMammalType()
        {
            throw new NotImplementedException();
        }

        public T Voice()
        {
            throw new NotImplementedException();
        }
    }
}
