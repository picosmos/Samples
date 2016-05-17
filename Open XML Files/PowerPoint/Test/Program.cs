using System;
using System.Collections.Generic;

namespace Koopakiller.ForumSamples.OpenXml.PowerPoint.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0) { args = new[] { @"..\..\..\Sample Files\PowerPoint.pptx" }; }
            if (args.Length != 1) { throw new ArgumentOutOfRangeException(nameof(args)); }

            Console.WriteLine($"Filename: {args[0]}");
            var path = ZipFile.Extract(args[0]);
            //Process.Start(path);
            
            var p = new Presentation(path);
            Print(p);

            ZipFile.Zip(path, "Test.pptx");

            Console.ReadKey();
        }

        private static void Print(Presentation p)
        {
            Console.WriteLine($"{p.Slides.Count} slides found");
            Console.WriteLine();
            for (var i = 1; i <= p.Slides.Count; i++)
            {
                Console.WriteLine($"Slide {i}");
                Console.WriteLine(" Comments: ");
                PrintComments(p.Slides[i - 1].Comments, 2);
                Console.WriteLine();
            }
        }

        private static void PrintComments(IList<Comment> comments, int x)
        {
            foreach (var comment in comments)
            {
                Console.WriteLine($"{new string(' ', x)}{comment.Author.Name} ({comment.Author.Initials}) wrote \"{comment.Text}\" at {comment.Timestamp}");
                PrintComments(comment.SubComments, x + 2);
            }
        }
    }
}
