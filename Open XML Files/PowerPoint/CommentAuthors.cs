using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Koopakiller.ForumSamples.OpenXml.PowerPoint
{
    public class CommentAuthors
    {
        public CommentAuthors(string root)
        {
            this.ReadCommentAuthors(root);
        }

        private void ReadCommentAuthors(string root)
        {
            var path = Path.Combine(root, "ppt", "commentAuthors.xml");
            if (!File.Exists(path)) { return; }

            var doc = XDocument.Load(path);
            var ns = XNamespace.Get(@"http://schemas.openxmlformats.org/presentationml/2006/main");

            var authors = doc.Root?.Elements(ns + "cmAuthor");
            if (authors == null) { return; }

            foreach (var el in authors)
            {
                this.Authors.Add(new CommentAuthor()
                {
                    Name = el.Attribute("name").Value,
                    Id = int.Parse(el.Attribute("id").Value),
                    Initials = el.Attribute("initials").Value,
                });
            }
        }

        public IList<CommentAuthor> Authors { get; } = new List<CommentAuthor>();
    }
}