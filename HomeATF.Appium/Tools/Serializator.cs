using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace HomeATF.Appium
{
    public class Serializator
    {
        private JsonSerializer serializer;

        public Serializator()
        {
            this.serializer = new JsonSerializer();
        }

        public void SerializeObject(Object o)
        {
            using (StreamWriter sw = new StreamWriter($@"c:\Work\json_{Guid.NewGuid()}.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, o);
            }
        }
    }
}
