using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons.Utils
{
    public static class ObjectConversion
    {

        public static T ToObject<T>(this IntPtr data, int size)
        {
            try
            {
                byte[] managedArray = new byte[size];
                Marshal.Copy(data, managedArray, 0, size);
                return ToObject<T>(managedArray);
            }
            catch
            {
                return default;
            }
        }

        public static byte[] ToBytes(this IntPtr data, int size)
        {
            try
            {
                byte[] managedArray = new byte[size];
                Marshal.Copy(data, managedArray, 0, size);
                return managedArray;
            }
            catch
            {
                return default;
            }
        }

        public static T ToObject<T>(this byte[] data)
        {
            try
            {
                if (data == null)
                {
                    return default(T);
                }
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream(data))
                {
                    object obj = bf.Deserialize(ms);
                    return (T) obj;
                }
            }
            catch
            {
                return default;
            }
        }

        public static byte[] ToByteArray<T>(T obj)
        {
            try
            {
                if (obj == null)
                {
                    return default;
                }
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            }
            catch
            {
                return default;
            }
        }

    }
}
