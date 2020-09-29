using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsLearn
{
    public class AnimalFabric
    {
        public static T CreateAnimal<T>(Type type) where T : Animal
        {
            if (type == typeof(Bear))
            {
                return new Bear() as T;
            }
            else if (type == typeof(Lion))
            {
                return new Lion() as T;
            }
            else if (type == typeof(Wolf))
            {
                return new Wolf() as T;
            }
            else
                return default;
        }

        public static void Wash<T>(AnimalsList<T> animals) where T : Animal
        {
            throw new NotImplementedException();
        }
    }
}
