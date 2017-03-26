using System;

namespace IrrationalNumberType
{
    class Program
    {
        static void Main(string[] args)
        {
            IArethmetic
            Console.WriteLine("Hello World!");
        }
    }

    public interface IIrrationalNumber
    {
        IIrrationalNumber Add(IIrrationalNumber a, IIrrationalNumber b);
    }

    public struct Pi : IIrrationalNumber
    {
        
    }
}