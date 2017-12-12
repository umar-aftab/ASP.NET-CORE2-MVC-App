using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShBazmool.Models;
using ShBazmool.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShBazmool.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ShBazmoolDbContext dbContext;
        public ArticleViewModel articleView;

        public ArticleController(ShBazmoolDbContext context)
        {
            dbContext = context;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(int? page)
        {
            var article = dbContext.Articles.ToList();
            var pager = new Pager(article.Count(), page);

            articleView = new ArticleViewModel()
            {
                Articles = article.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                PagerView = pager

            };
            return View(articleView);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AdminIndex(int? page)
        {
            var article = dbContext.Articles.ToList();
            var pager = new Pager(article.Count(), page);

            articleView = new ArticleViewModel()
            {
                Articles = article.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                PagerView = pager

            };
            return View(articleView);
        }

        [Authorize]
        public IActionResult Insert()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Insert(Article article)
        {
            dbContext.Articles.Add(article);
            dbContext.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        
        [HttpGet]
        public IActionResult Read(int ID)
        {
            return View(Find(ID));
        }

        [Authorize]
        [HttpGet]
        public IActionResult ReadAdmin(int ID)
        {
            return View(Find(ID));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int ID)
        {
            return View(Find(ID));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Article writing)
        {
            
            Article obj = new Article
            {
                ID = writing.ID,
                Title = writing.Title,
                Author = writing.Author,
                Material = writing.Material,
                Description = writing.Description,
                DatePublished = writing.DatePublished
            };
            dbContext.Articles.Update(obj);
            dbContext.SaveChanges();
            return RedirectToAction("AdminIndex");

        }

        [Authorize]
        public IActionResult Delete(int ID)
        {
            var article = Find(ID);
            dbContext.Articles.Remove(article);
            dbContext.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        protected Article Find(int ID)
        {
            var article = dbContext.Articles.Find(ID);
            return article;
        }

    }
}
