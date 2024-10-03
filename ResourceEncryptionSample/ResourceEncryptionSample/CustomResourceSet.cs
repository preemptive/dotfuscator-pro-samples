using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ResourceEncryptionSample
{
#pragma warning disable CA1010 // Collections should implement generic interface
    public class CRSet : ResourceSet
    {
        // Constructor: Pass the base name and assembly to read resources
        private List<string> toEncrypt;
        private string key = "123";
        private string resourceToEncrypt = "xyz";

        public CRSet() : base() { toEncrypt = getList(); }
        public CRSet(Stream stream) : base(stream) { toEncrypt = getList(); }
        public CRSet(IResourceReader reader) : base(reader) { toEncrypt = getList(); }
        public CRSet(string filename)
        {

            toEncrypt = getList();
        }
        private List<String> getList()
        {
            char delimiter = '+'; // Specify the delimiter
            string[] stringArray = resourceToEncrypt.Split(delimiter);
            List<String> list = new List<String>();
            for (int i = 0; i < stringArray.Length; i++)
            {
                list.Add(stringArray[i]);
            }
            return list;
        }
        public override IDictionaryEnumerator GetEnumerator()
        {
            //Console.WriteLine("from getenumerator with single argument");
            IDictionaryEnumerator enumerator = base.GetEnumerator();

            var decryptedResources = new Dictionary<object, object>();

            while (enumerator.MoveNext())
            {
                DictionaryEntry entry = (DictionaryEntry)enumerator.Current;
                object resource = entry.Value;
                byte[] resourceBytes;
                resourceBytes = ObjectToByteArray(resource);
                if (!toEncrypt.Contains((string)entry.Key))
                {
                    //decryptedResources.Add((string)entry.Key, resource);
                    object exactResource = removeTypeinfo(resourceBytes);
                    if (resourceBytes[0] == 1)
                    {
                        string x = Encoding.UTF8.GetString(exactResource as byte[]);
                        object final = x;
                        decryptedResources.Add(entry.Key, final);

                    }
                    else
                    {
                        decryptedResources.Add(entry.Key, exactResource);
                    }

                    continue;
                }

                object decryptedResource = Decrypt(resourceBytes, key);
                if (resourceBytes[0] == 1)
                {
                    string x = Encoding.UTF8.GetString(decryptedResource as byte[]);
                    object final = x;
                    decryptedResources.Add(entry.Key, final);

                }
                else
                {
                    decryptedResources.Add(entry.Key, decryptedResource);
                }
            }

            return new Hashtable(decryptedResources).GetEnumerator();
        }
        public override string GetString(string name)
        {
            //Console.WriteLine("from getstring with single argument");

            if (toEncrypt.Contains(name))
            {
                object x = base.GetObject(name);
                byte[] resourceBytes = x as byte[];

                return Encoding.UTF8.GetString(Decrypt(resourceBytes, key));
            }
            else
            {
                object resource = base.GetObject(name);
                object exact = removeTypeinfo(resource as byte[]);
                string x = Encoding.UTF8.GetString(exact as byte[]);
                return x;
            }

        }

        public override string GetString(string name, bool ignoreCase)
        {

            if (toEncrypt.Contains(name))
            {
                object x = base.GetObject(name, ignoreCase);
                byte[] resourceBytes = x as byte[];

                return Encoding.UTF8.GetString(Decrypt(resourceBytes, key));
            }
            else
            {
                object resource = base.GetObject(name, ignoreCase);
                object exact = removeTypeinfo(resource as byte[]);
                string x = Encoding.UTF8.GetString(exact as byte[]);
                return x;
            }
        }
        public override object GetObject(string name)
        {
            if (toEncrypt.Contains(name))
            {
                object resource = base.GetObject(name);
                byte[] resourceBytes;
                resourceBytes = resource as byte[];
                object decryptedResource = Decrypt(resourceBytes, key);
                if (resourceBytes[0] == 1)
                {
                    string x = Encoding.UTF8.GetString(decryptedResource as byte[]);
                    object final = x;
                    return final;
                }
                else
                {
                    return decryptedResource;
                }
            }
            else
            {
                object resource = base.GetObject(name);
                byte[] resourceBytes;
                resourceBytes = resource as byte[];
                object exactResource = removeTypeinfo(resourceBytes);
                if (resourceBytes[0] == 1)
                {
                    string x = Encoding.UTF8.GetString(exactResource as byte[]);
                    object final = x;
                    return final;
                }
                else
                {
                    return exactResource;
                }
            }
        }

        public override object GetObject(string name, bool ignoreCase)
        {
            if (toEncrypt.Contains(name))
            {
                object resource = base.GetObject(name, ignoreCase);
                byte[] resourceBytes;
                resourceBytes = resource as byte[];
                object decryptedResource = Decrypt(resourceBytes, key);
                if (resourceBytes[0] == 1)
                {
                    string x = Encoding.UTF8.GetString(decryptedResource as byte[]);
                    object final = x;
                    return final;
                }
                else
                {
                    return decryptedResource;
                }
            }
            else
            {
                object resource = base.GetObject(name, ignoreCase);
                byte[] resourceBytes;
                resourceBytes = resource as byte[];
                object exactResource = removeTypeinfo(resourceBytes);
                if (resourceBytes[0] == 1)
                {
                    string x = Encoding.UTF8.GetString(exactResource as byte[]);
                    object final = x;
                    return final;


                }
                else
                {
                    return exactResource;

                }
            }

        }
        private byte[] Decrypt(byte[] bytes, string key)
        {
            long iv = long.Parse(key);
            int len = bytes.Length;

            for (int i = 1; i < len; i++)
            {
                bytes[i] = (byte)(bytes[i] ^ iv++);
            }

            int j = 1;
            while (j + 1 < len)
            {
                byte b1 = bytes[j];
                byte b2 = bytes[j + 1];
                bytes[j] = b2;
                bytes[j + 1] = b1;
                j += 2;
            }
            //return bytes;
            int newLength = len / 2;
            byte[] result = new byte[newLength];

            //// Copy even-indexed bytes (including zero index) to the result array
            j = 1;
            for (int i = 0; i < newLength; i++)
            {

                result[i] = bytes[j];
                j += 2;

            }

            return result;


        }
        private byte[] removeTypeinfo(byte[] resourceByte)
        {
            byte[] withoutTypeinfo = new byte[resourceByte.Length - 1];
            for (int i = 0; i < withoutTypeinfo.Length; i++)
            {
                withoutTypeinfo[i] = resourceByte[i + 1];
            }
            return withoutTypeinfo;
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }



    }
#pragma warning restore CA1010 // Collections should implement generic interface
}
