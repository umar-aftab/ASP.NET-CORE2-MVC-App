using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShBazmool.Models
{
    public class Explanation
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string BookName { get; set; }
        public string Location { get; set; }
        public byte[] Material { get; set; }
        public string Description { get; set; }
    }
}
