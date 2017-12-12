using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShBazmool.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
