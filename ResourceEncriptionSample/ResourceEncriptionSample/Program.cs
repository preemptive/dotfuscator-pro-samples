using System.Collections;
using System.Globalization;
using System.Resources;

namespace ResourceEncriptionSample
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello, World!");
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

            Console.WriteLine("---------Resource Manager---------\n");
            ResourceManager rm = new ResourceManager("ResourceEncriptionSample.Resource1", asm);
            string b = rm.GetString("String1");
            Console.WriteLine("String1 " + b + "\n");

            Console.WriteLine("---------Resource Set---------\n");
            ResourceSet resourceSet = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            if (resourceSet != null)
            {
                foreach (DictionaryEntry entry in resourceSet)
                {
                    string key = (string)entry.Key;
                    object value = entry.Value;
                    Console.WriteLine(key + ": " + value + "\n");
                }
                var enu1 = resourceSet.GetEnumerator();
                while (enu1.MoveNext())
                {
                    //Console.WriteLine(enu1.Current);
                    Console.WriteLine(((DictionaryEntry)enu1.Current).Key);
                    Console.WriteLine(((DictionaryEntry)enu1.Current).Value);
                }
            }
            Console.WriteLine("------HERE--------\n");
            using (var stream = asm.GetManifestResourceStream("ResourceEncriptionSample.Resource1.resources"))
            {
                if (stream != null)
                {
                    CRSet rscSet = new CRSet(stream);

                    var enu3 = rscSet.GetEnumerator();
                    while (enu3.MoveNext())
                    {
                        DictionaryEntry entry = (DictionaryEntry)enu3.Current;
                        Console.WriteLine(entry.Key);
                        Console.WriteLine(entry.Value);
                    }
                }
            }
            Console.WriteLine("--------------\n");

            using (var stream = asm.GetManifestResourceStream("ResourceEncriptionSample.Resource1.resources"))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        CRReader rdr = new CRReader(stream);
                        var enu = rdr.GetEnumerator();
                        while (enu.MoveNext())
                        {
                            //Console.WriteLine(enu.Current);
                            Console.WriteLine(((DictionaryEntry)enu.Current).Key);
                            Console.WriteLine(((DictionaryEntry)enu.Current).Value);
                        }
                    }
                }
            }
        }
        private static byte[] Decrypt(byte[] bytes, int iv)
        {
            int len = bytes.Length;
            if (bytes[0] == 14)
            {

                for (int i = 1; i < len; i++)
                {
                    bytes[i] = (byte)(bytes[i] ^ iv++);
                }

                int j = 1;
                while (j < len)
                {
                    byte b1 = bytes[j];
                    byte b2 = bytes[j + 1];
                    bytes[j] = b2;
                    bytes[j + 1] = b1;
                    j += 2;
                }

                int newLength = len / 2 + 1;
                byte[] result = new byte[newLength];

                // Copy even-indexed bytes (including zero index) to the result array
                j = 1;
                for (int i = 1; i < newLength; i++)
                {
                    //1 3 5 7 
                    result[i] = bytes[j];
                    j += 2;
                }
                result[0] = 14;

                return result;

            }
            else
            {


                for (int i = 0; i < len; i++)
                {
                    bytes[i] = (byte)(bytes[i] ^ iv++);
                }

                int j = 0;
                while (j < len)
                {
                    byte b1 = bytes[j];
                    byte b2 = bytes[j + 1];
                    bytes[j] = b2;
                    bytes[j + 1] = b1;
                    j += 2;
                }

                int newLength = len / 2;
                byte[] result = new byte[newLength];

                // Copy even-indexed bytes (including zero index) to the result array

                for (int i = 0; i < newLength; i++)
                {
                    result[i] = bytes[i * 2];
                    Console.WriteLine(result[i]);
                }

                return bytes;

            }
        }

        private static byte[] Encrypt(byte[] bytes, int iv)
        {
            int len = bytes.Length;
            int i = 0;

            // swap bytes in each word first
            while (i < len)
            {
                byte b1 = bytes[i];
                byte b2 = bytes[i + 1];
                bytes[i] = b2;
                bytes[i + 1] = b1;
                i += 2;
            }

            for (i = 0; i < len; i++)
            {
                bytes[i] = (byte)(bytes[i] ^ iv++);
            }


            return bytes;

        }
        private static byte[] AddZeroBytes(byte[] byteArray)
        {
            List<byte> newByteArray = new List<byte>();
            int cnt = 0;
            foreach (byte b in byteArray)
            {


                {
                    newByteArray.Add(b);
                    newByteArray.Add(0);
                }
                cnt++;
            }

            return newByteArray.ToArray();
        }
    }
}
