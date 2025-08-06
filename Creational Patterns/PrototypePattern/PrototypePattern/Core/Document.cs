using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern.Core
{
    internal class Document : IPrototype<Document>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Images { get; set; }

        public Document()
        {
            Images = new List<string>();
        }

        public Document DeepClone()
        {
            return new Document
            {
                Title = this.Title,
                Content = this.Content,
                Images = new List<string>(this.Images)
            };
        }

        public Document ShallowClone()
        {
            return (Document)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return "Title: " + Title + "\n" +
                   "Content: " + Content + "\n" +
                   "Images: " + string.Join(", ", Images);
        }
    }
}
