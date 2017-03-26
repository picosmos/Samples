using System;

namespace VariableSwap
{
    internal class Program
    {
private static void Main()
{
    var a = 1;
    var b = 2;

    Console.WriteLine($"a={a}, b={b}");

    // C# 6.0 and prior
    GenericSwap(ref a, ref b);

    Console.WriteLine($"a={a}, b={b}");

    // C# 7.0 and later
    (a, b) = (b, a);

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
}