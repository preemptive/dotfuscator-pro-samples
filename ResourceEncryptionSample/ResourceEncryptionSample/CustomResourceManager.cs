using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ResourceEncryptionSample
{
    public class CRManager : ResourceManager
    {
        private List<string> toEncrypt;
        private string key = "123";
        private string resourceToEncrypt = "xyz";
        public CRManager(string baseName, Assembly assembly) : base(baseName, assembly)
        {
            toEncrypt = getList();
        }

        public CRManager(Type typename) : base(typename)
        {
            toEncrypt = getList();
        }

        public CRManager() : base()
        {
            toEncrypt = getList();
        }

        public CRManager(string baseName, Assembly assembly, Type typename) : base(baseName, assembly, typename)
        {
            toEncrypt = getList();

        }

        public override string GetString(string name)
        {


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

        public override string GetString(string name, CultureInfo culture)
        {
            if (toEncrypt.Contains(name))
            {
                object x = base.GetObject(name, culture);
                byte[] resourceBytes = x as byte[];

                return Encoding.UTF8.GetString(Decrypt(resourceBytes, key));
            }
            else
            {
                object resource = base.GetObject(name, culture);
                object exact = removeTypeinfo(resource as byte[]);
                string x = Encoding.UTF8.GetString(exact as byte[]);
                return x;
            }
        }
        public override object GetObject(string name, CultureInfo culture)
        {
            if (toEncrypt.Contains(name))
            {
                object resource = base.GetObject(name, culture);
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
                object resource = base.GetObject(name, culture);
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

        public override ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            ResourceSet originalResourceSet = base.GetResourceSet(culture, createIfNotExists, tryParents);

            Dictionary<string, object> decryptedResources = new Dictionary<string, object>();
            IDictionaryEnumerator enumerator = originalResourceSet.GetEnumerator();
            while (enumerator.MoveNext())
            {
                DictionaryEntry entry = (DictionaryEntry)enumerator.Current;
                object resource = entry.Value;
                byte[] resourceBytes;
                resourceBytes = resource as byte[];
                if (!toEncrypt.Contains(entry.Key.ToString()))
                {
                    //decryptedResources.Add((string)entry.Key, resource);
                    object exactResource = removeTypeinfo(resourceBytes);
                    if (resourceBytes[0] == 1)
                    {
                        string x = Encoding.UTF8.GetString(exactResource as byte[]);
                        object final = x;
                        decryptedResources.Add((string)entry.Key, final);

                    }
                    else
                    {
                        decryptedResources.Add((string)entry.Key, exactResource);
                    }

                    continue;
                }

                object decryptedResource = Decrypt(resourceBytes, key);
                if (resourceBytes[0] == 1)
                {
                    string x = Encoding.UTF8.GetString(decryptedResource as byte[]);
                    object final = x;
                    decryptedResources.Add((string)entry.Key, final);

                }
                else
                {
                    decryptedResources.Add((string)entry.Key, decryptedResource);
                }
            }

            ResourceSet decryptedResourceSet = new ResourceSet(new DRReader(decryptedResources));

            return decryptedResourceSet;
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


    }
#pragma warning disable CA1010 // Collections should implement generic interface
    public class DRReader : IResourceReader
    {
        private IDictionary<string, object> _resources;
        private bool _disposed = false; // to detect redundant calls

        public DRReader(IDictionary<string, object> resources)
        {
            _resources = resources;
        }

        public void Close()
        {
            Dispose(true);

        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return (IDictionaryEnumerator)_resources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _resources = null;
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
#pragma warning restore CA1010 // Collections should implement generic interface

}
