using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Samples
{
    class Serialization
    {
        [STAThread]
        static void Main(string[] args)
        {
            Serialization test = new Serialization();
            if((args.Length == 0) || (!args[0].Equals("-s") && !args[0].Equals("-d")))
            {
                Console.WriteLine("Usage : Serialization -option\n" +
                    "where option can be \n" +
                    "s - serialize\n" +
                    "d - deserialize");
                return;
            }
            test.DoIt(args[0]);
        }

        public void DoIt(string option)
        {
            if(option.Equals("-s"))
            {
                Console.Write("Serializing...");
                Tester testernew = new Tester(1, 10);

                testernew.Compute();
                testernew.Display();
                testernew.Serialize();
            }
            else
            {
                Console.Write("DeSerializing...");
                Tester tester = Tester.DeSerialize();
                tester.Display();
            }
        }
    }

    [Serializable]
    class Tester
    {
        public Tester(int start, int end)
        {
            firstNumber = start;
            secondNumber = end;
        }

        public void Compute()
        {
            sum = firstNumber + secondNumber;
        }

        public void Display()
        {
            Console.WriteLine("\n{0}", sum);
        }

        public void Serialize()
        {
            // create a file stream to write the file
            FileStream stream = new FileStream("Sum.out", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            // serialize to disk
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public static Tester DeSerialize()
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream("Sum.out", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                return (Tester) formatter.Deserialize(stream);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
        private int firstNumber = 0;
        private int secondNumber = 0;
        private int sum;
    }
}
