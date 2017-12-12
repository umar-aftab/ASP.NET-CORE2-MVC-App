using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShBazmool.Models;
using System.Security.Claims;
using ShBazmool.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShBazmool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShBazmoolDbContext dbContext;
        public ExplanationViewModel explanation;
        public WritingViewModel writing;
        public LectureViewModel lecture;
        public ArticleViewModel article;
        public HomeViewModel homeView;

        public HomeController(ShBazmoolDbContext context)
        {
            dbContext = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            
            explanation = new ExplanationViewModel()
            {
                Explains = dbContext.Explanations
                            .Select(x => new Explain { Title = x.Title, BookName = x.BookName, ID = x.ID, Date = x.Date })
                            .Take(5)
                            .ToList()
            };

            writing = new WritingViewModel()
            {
                Books = dbContext.Writings
                            .Select(x=> new Book { ID = x.ID, Category = x.Category, Title = x.Title})
                            .Take(5)
                            .ToList()
            };

            lecture = new LectureViewModel()
            {
                Audios = dbContext.Lectures
                            .Select(x=>new Audio { ID = x.ID, Title = x.Title, Date = x.Date })
                            .Take(5)
                            .ToList()
            };

            article = new ArticleViewModel()
            {
                Articles = dbContext.Articles.Take(5).ToList()
            };

            homeView = new HomeViewModel()
            {
                ExplanationViews = explanation,
                WritingViews = writing,
                LectureViews = lecture,
                ArticleViews = article
            };

            return View(homeView);
        }

        public IActionResult Bio()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

    }
}
