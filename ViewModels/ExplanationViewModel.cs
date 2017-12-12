using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShBazmool.Models;

namespace ShBazmool.ViewModels
{
    public class ExplanationViewModel
    {
        public IEnumerable<Explanation> Explanations { get; set; }
        public IEnumerable<Explain> Explains { get; set; }
        public Explanation SelectedAudio { get; set; }
        public string DisplayMode { get; set; }
        public Pager PagerView { get; set; }

    }
    public class Explain
    {
        public string BookName { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

    }
}
