using System;
using System.Collections.Generic;

namespace Koopakiller.ForumSamples.OpenXml.PowerPoint
{
    public class Comment
    {
        public DateTime Timestamp { get; set; }
        public int? ParentIdx { get; set; }
        public int Idx { get; set; }
        public Position Position { get; set; }

        public string Text { get; set; }

        public CommentAuthor Author { get; set; }
        public IList<Comment> SubComments { get; } = new List<Comment>();
    }
}