using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
internal class Program
{
    static void Main(string[] args)
    {
        var dict = System.IO.File.ReadLines(@"test.txt")       // Alle Zeilen einlesen
            .Where(line => !string.IsNullOrWhiteSpace(line))   // Zeilen die nur Leerzeichen enthalten aussortieren
            .Where(line => !line.TrimStart().StartsWith("//")) // Zeilen die mit // (und ggf. Leerzeichen davor) anfangen aussortieren
            .SelectMany(SplitIntoKeyValuePairEnumeration)      // Die restlichen Zeilen an jedem 2. Komma aufteilen und alles in eine Liste packen
            .ToDictionary();                                   // Die selbst geschriebene Erweiterungsmethode nutzen
    }

    public static IEnumerable<KeyValuePair<string, string>> SplitIntoKeyValuePairEnumeration(string source)
    {
        var arr = source.Split(',');
        if (arr.Length % 2 != 0)
        {
            throw new InvalidOperationException();
        }
        for (var i = 0; i < arr.Length; i += 2)
        {
            yield return new KeyValuePair<string, string>(arr[i], arr[i + 1]);
        }
    }
}

public static class LinqExtensions
{
    //Ist in vielen F'llen gany hilfreich, daher ausgelagert
    public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
        return source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
}
