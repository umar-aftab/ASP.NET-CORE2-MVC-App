using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShBazmool.Models;

namespace ShBazmool.ViewModels
{
    public class LectureViewModel
    {
        public IEnumerable<Lecture> Lectures { get; set; }
        public IEnumerable<Audio> Audios { get; set; }
        public Lecture SelectedAudio { get; set; }
        public string DisplayMode { get; set; }
        public Pager PagerView { get; set; }
    }

    public class Audio
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

    }
}
