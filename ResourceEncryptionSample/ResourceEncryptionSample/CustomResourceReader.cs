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
#pragma warning disable CA1010
    public class CRReader : IResourceReader
    {
        private ResourceReader _resourceReader;
        private bool disposed = false;
        private List<string> toEncrypt;
        private string key = "123";
        private string resourceToEncrypt = "xyz";

        public CRReader(string fileName)
        {
            _resourceReader = new ResourceReader(fileName);
            toEncrypt = getList();
        }

        public CRReader(Stream stream)
        {
            _resourceReader = new ResourceReader(stream);
            toEncrypt = getList();
        }

        public void Close()
        {
            _resourceReader.Close();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
        public IDictionaryEnumerator GetEnumerator()
        {
            if (_resourceReader == null)
            {
                throw new InvalidOperationException("ResourceReader is not initialized.");
            }

            var decryptedResources = new Dictionary<object, object>();
            foreach (DictionaryEntry entry in _resourceReader)
            {
                object resource = entry.Value;
                byte[] resourceBytes;
                resourceBytes = ObjectToByteArray(resource);
                if (!toEncrypt.Contains((string)entry.Key))
                {
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

        public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
        {

            _resourceReader.GetResourceData(resourceName, out resourceType, out resourceData);
            resourceData = removeMetadataBytes(resourceData);
            if (resourceData[0] == 1)
            {
                resourceType = "ResourceTypeCode.String";
            }
            if (toEncrypt.Contains(resourceName))
            {
                resourceData = Decrypt(resourceData, key);
            }
            else
            {
                resourceData = removeTypeinfo(resourceData);

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
        private byte[] removeMetadataBytes(byte[] withMetadata)
        {
            byte[] withoutMetadata = new byte[withMetadata.Length - 4];
            for (int i = 0; i < withoutMetadata.Length; i++)
            {
                withoutMetadata[i] = withMetadata[i + 4];
            }
            return withoutMetadata;
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
