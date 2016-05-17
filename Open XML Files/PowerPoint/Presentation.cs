using System;
using System.Collections.Generic;
using System.IO;

namespace Koopakiller.ForumSamples.OpenXml.PowerPoint
{
    public class Presentation
    {
        public Presentation(string root)
        {
            this.CommentAuthors = new CommentAuthors(root);
            this.ReadSlides(root);
        }

        private void ReadSlides(string root)
        {
            var path = Path.Combine(root, "ppt", "slides");
            foreach (var file in Directory.GetFiles(path))
            {
                var fi = new FileInfo(file);
                if (fi.Extension != ".xml" || !fi.Name.StartsWith("slide"))
                {
                    continue;
                }
                var num = int.Parse(fi.Name.Substring("slide".Length, fi.Name.IndexOf(".", StringComparison.Ordinal) - "slide".Length));
                this.Slides.Add(new Slide(root, num, this.CommentAuthors));
            }
        }

        public CommentAuthors CommentAuthors { get; }

        public IList<Slide> Slides { get; } = new List<Slide>();
    }
}