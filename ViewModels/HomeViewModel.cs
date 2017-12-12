using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShBazmool.ViewModels;

namespace ShBazmool.ViewModels
{
    public class HomeViewModel
    {
        public ExplanationViewModel ExplanationViews { get; set; }
        public LectureViewModel LectureViews { get; set; }
        public WritingViewModel WritingViews { get; set; }
        public ArticleViewModel ArticleViews { get; set; }
    }
}
