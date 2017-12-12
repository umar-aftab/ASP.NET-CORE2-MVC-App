using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShBazmool.Models;

namespace ShBazmool.ViewModels
{
    public class WritingViewModel
    {
        public IEnumerable<Writing> Writings { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public Writing SelectedWriting { get; set; }
        public string DisplayMode { get; set; }
        public Pager PagerView { get; set; }
    }

    public class Book
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
    }
}
