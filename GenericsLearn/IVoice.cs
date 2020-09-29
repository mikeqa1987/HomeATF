using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsLearn
{
    interface IVoice<out T>
    {
        T Voice();
    }

    interface IMammal
    { 
    
    }

    interface IMammal<out T>
    {
        T GetMammalType();
    }
}
