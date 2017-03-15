using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    internal class Program
    {
        static void Main()
        {
            var fileName = @"test.txt";
            var dc = DataConfig.Load(fileName);
            Console.WriteLine(@"POSITION4 = " + dc.DataValues[@"POSITION4"]);
            dc.Lines.Add(new CommentLine(@"Sample Comment"));
            var dl = dc.Lines.OfType<DataLine>().FirstOrDefault();
            if (dl != null && !dl.Dictionary.ContainsKey(@"POSXYZ"))
            {
                dl.Dictionary.Add(@"POSXYZ", @"MyVal");
            }
            dc.Save(fileName);
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

    public class DataConfig
    {
        private DataConfig(IEnumerable<String> lines)
        {
            foreach (var line in lines)
            {
                if (line.TrimStart().StartsWith("//"))
                {
                    //Comment
                    //using RawLine instead of CommentLine because we don't know about spaces before the //
                    this.Lines.Add(new RawLine(line));
                }
                else if (String.IsNullOrWhiteSpace(line))
                {
                    this.Lines.Add(new RawLine(line));
                }
                else
                {
                    this.Lines.Add(DataLine.Parse(line));
                }
            }
        }

        public static DataConfig Load(String fileName)
        {
            return new DataConfig(File.ReadLines(fileName));
        }

        public IList<ILine> Lines { get; } = new List<ILine>();

        public void Save(String fileName)
        {
            File.WriteAllLines(fileName, this.Lines.Select(line => line.ToString()));
        }

        public IReadOnlyDictionary<String, String> DataValues
            => new Dictionary<String, String>(
                this.Lines.OfType<DataLine>()
                    .SelectMany(line => line.Dictionary)
                    .ToDictionary());
    }

    public interface ILine
    {
        String ToString();
    }

    public class RawLine : ILine
    {
        public RawLine() { }

        public RawLine(String content)
        {
            this.Content = content;
        }

        public String Content { get; set; }

        public override String ToString()
        {
            return this.Content;
        }
    }

    public class CommentLine : RawLine
    {
        public CommentLine() { }

        public CommentLine(String content)
        {
            this.Content = content;
        }

        public new String Content
        {
            get { return base.Content.Substring(2); }
            set { base.Content = "//" + value; }
        }
    }

    public class DataLine : ILine
    {
        public IDictionary<String, String> Dictionary { get; private set; }

        public static DataLine Parse(String line)
        {
            return new DataLine()
            {
                Dictionary = SplitIntoKeyValuePairEnumeration(line).ToDictionary(),
            };
        }

        public static IEnumerable<KeyValuePair<String, String>> SplitIntoKeyValuePairEnumeration(String source)
        {
            var arr = source.Split(',');
            if (arr.Length % 2 != 0)
            {
                throw new InvalidOperationException();
            }
            for (var i = 0; i < arr.Length; i += 2)
            {
                yield return new KeyValuePair<String, String>(arr[i], arr[i + 1]);
            }
        }

        public override String ToString()
        {
            return String.Join(",", this.Dictionary.Select(entry => entry.Key + "," + entry.Value));
        }
    }
}
