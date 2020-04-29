using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CcsDemo.Utils
{
    internal class JSON
    {
        public static string ToJson<T>(T data)
        {
            //  IStringSerialized s = data as IStringSerialized;


            DataContractJsonSerializer serializer
                        = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }


        public static string StreamAsJson(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
              //  serializer.WriteObject(ms, data);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static T AsObj<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                ms.Position = 0;
                var ser = new DataContractJsonSerializer(typeof(T));
                var obj = (T)ser.ReadObject(ms);
                return obj;
            }

        }


        public static T AsObj<T>(Stream stream)
        {
            stream.Position = 0;
            var ser = new DataContractJsonSerializer(typeof(T));
            var obj = (T)ser.ReadObject(stream);
            return obj;

        }
    }
}
