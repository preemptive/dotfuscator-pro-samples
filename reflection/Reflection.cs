using System;
using System.Reflection;

namespace Samples
{
    public class Reflection
    {
        public static void Main()
        {
            string typename = "Samples.Greetings";
            string methodname = "SayGreetings";
            string[] methargs = new string[1];
            methargs[0] = "Bob";

            Reflection reflection = new Reflection();
            reflection.TestInvoke(typename, methodname, methargs);
        }


        public void TestInvoke(string typename, string methodname, string[] methodargs)
        {
            Assembly assembly;
            Type type;
            Object instance;

            try
            {
                //get the requested type from current assembly
                assembly = this.GetType().Assembly;
                type = assembly.GetType(typename, true);
                instance = Activator.CreateInstance(type);
            }
            catch (TypeLoadException)
            {
                Console.WriteLine("Could not load Type: {0}", typename);
                return;
            }

            MethodInfo method = type.GetMethod(methodname);
            if (method == null)
            {
                Console.WriteLine("Suitable method not found!");
                return;
            }

            // Wrong number of parameters?
            ParameterInfo[] param = method.GetParameters();
            if (param.Length != methodargs.Length)
            {
                Console.WriteLine(method.DeclaringType + "." + method.Name + ": Method Signatures Don't Match!");
                return;
            }

            // Ok, can we convert the strings to the right types?
            Object[] newArgs = new Object[methodargs.Length];
            for (int index = 0; index < methodargs.Length; index++)
            {
                try
                {
                    newArgs[index] = Convert.ChangeType(methodargs[index], param[index].ParameterType);
                }
                catch (Exception e)
                {
                    Console.WriteLine(method.DeclaringType + "." + method.Name + ": Argument Conversion Failed", e);
                    return;
                }
            }

            if (!method.IsStatic)
                method.Invoke(instance, methodargs);
            else
                Console.WriteLine("Suitable method not found!");
        }
    }


    class Greetings
    {
        public void SayGreetings(string name)
        {
            SayHello(name);
            SayGoodbye(name);
        }

        public void SayHello(string name)
        {
            Console.WriteLine("Hello {0}!", name);
        }

        public void SayGoodbye(string name)
        {
            Console.WriteLine("Goodbye {0}!", name);
        }
    }
}
