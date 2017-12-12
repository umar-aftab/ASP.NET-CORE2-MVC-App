using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShBazmool.Models;

namespace ShBazmool.ViewModels
{
    public class ArticleViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public Article SelectedArticle { get; set; }
        public string DisplayMode { get; set; }
        public Pager PagerView { get; set; }
    }
}
