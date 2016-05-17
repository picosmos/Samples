using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Koopakiller.ForumSamples.OpenXml.PowerPoint
{
    public class Slide
    {
        public Slide(string root, int num, CommentAuthors commentAuthors)
        {
            this.ReadSlideFile(root, num);
            this.ReadSlideCommentsFile(root, num, commentAuthors);
        }

        private void ReadSlideFile(string root, int num)
        {
            var path = Path.Combine(root, "ppt", "slides", $"slide{num}.xml");
            if (!File.Exists(path))
            {
                return;
            }

            // ReSharper disable once UnusedVariable
            var doc = XDocument.Load(path);//Load file contents
        }

        private void ReadSlideCommentsFile(string root, int num, CommentAuthors commentAuthors)
        {
            var path = Path.Combine(root, "ppt", "comments", $"comment{num}.xml");
            if (!File.Exists(path))
            {
                return;
            }

            var doc = XDocument.Load(path);//Load file contents
            var nsp = XNamespace.Get(@"http://schemas.openxmlformats.org/presentationml/2006/main");
            var nsp15 = XNamespace.Get(@"http://schemas.microsoft.com/office/powerpoint/2012/main");
            var commentsList = new List<Comment>();
            if (doc.Root == null) { return; }

            foreach (var xmlComment in doc.Root.Elements(nsp + "cm"))
            {
                var comment = new Comment()
                {
                    Timestamp = DateTime.Parse(xmlComment.Attribute("dt").Value, CultureInfo.InvariantCulture),
                    Idx = int.Parse(xmlComment.Attribute("idx").Value),
                    Text = xmlComment.Element(nsp + "text")?.Value,
                };

                var xmlCommentAuthorId = int.Parse(xmlComment.Attribute("authorId").Value);
                comment.Author = commentAuthors.Authors.Single(x => x.Id == xmlCommentAuthorId);

                var pos = xmlComment.Element(nsp + "pos");
                if (pos != null)
                {
                    comment.Position = new Position
                    {
                        X = int.Parse(pos.Attribute("x").Value),
                        Y = int.Parse(pos.Attribute("y").Value)
                    };
                }

                var parent = xmlComment.Descendants(nsp15 + "parentCm").FirstOrDefault();
                if (parent != null)
                {
                    comment.ParentIdx = int.Parse(parent.Attribute("idx").Value);
                }
                commentsList.Add(comment);
            }

            foreach (var comment in commentsList)
            {
                if (comment.ParentIdx == null) { continue; }

                foreach (var secondComment in commentsList)
                {
                    if (comment.ParentIdx == secondComment.Idx)
                    {
                        secondComment.SubComments.Add(comment);
                    }
                }
            }
            foreach (var comment in commentsList.Where(x => x.ParentIdx == null))
            {
                this.Comments.Add(comment);
            }
        }

        public IList<Comment> Comments { get; } = new List<Comment>();
    }
}