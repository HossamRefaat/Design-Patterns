using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern.Core
{
    /// <summary>
    /// Simple blog post with just title and author
    /// </summary>
    public class BlogPost
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public BlogPost(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return $"'{Title}' by {Author}";
        }
    }
} 