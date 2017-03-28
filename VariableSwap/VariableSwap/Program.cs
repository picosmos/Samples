using System;

namespace VariableSwap
{
    internal class Program
    {
        private static void Main()
        {
            var a = new TestClass(1);
            var b = new TestClass(2);

            Console.WriteLine("Initial (a=1, b=2):");
            Console.WriteLine($"a={a}, b={b}");

            // C# 6.0 and prior
            GenericSwap(ref a, ref b);

            Console.WriteLine("C# 6.0 and prior swap:");
            Console.WriteLine($"a={a}, b={b}");

            a.Value = 3;
            b.Value = 4;

            Console.WriteLine("After asignment of new values (a=3, b=4):");
            Console.WriteLine($"a={a}, b={b}");

            // C# 7.0 and later
            (a, b) = (b, a);

            Console.WriteLine("C# 7.0 and later swap:");
            Console.WriteLine($"a={a}, b={b}");

            a.Value = 5;
            b.Value = 6;

            Console.WriteLine("After asignment of new values (a=5, b=6):");
            Console.WriteLine($"a={a}, b={b}");

            Console.ReadKey();
        }

        private static void GenericSwap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }

    internal class TestClass
    {
        public TestClass(int value)
        {
            this.Value = value;
        }

        public Int32 Value { get; set; }

        public override String ToString()
        {
            return this.Value.ToString();
        }
    }
}