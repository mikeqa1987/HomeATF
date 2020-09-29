using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HomeATF.Appium
{
    public class DebugTools
    {
        public static Serializator  Serialize { get; }

        public static Console Console { get; }

        static DebugTools()
        {
            Serialize = new Serializator();
            Console = new Console();
        }
    }
}
