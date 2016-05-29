using System;

namespace WIAIIOIAC
{
    internal class Program
    {
        private static void Main()
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            IBase a;

            a = new InheritedSubClass();
            a.Output();

            a = new ImplementingdSubClass();
            a.Output();

            Console.ReadKey();
        }
    }

    public interface IBase
    {
        void Output();
    }

    public class BaseClass : IBase
    {
        public void Output()
        {
            Console.WriteLine("BaseClass");
        }
    }

    public class InheritedSubClass : BaseClass
    {
        public new void Output()
        {
            Console.WriteLine("SubClass");
        }
    }

    public class ImplementingdSubClass : IBase
    {
        public void Output()
        {
            Console.WriteLine("SubClass");
        }
    }
}