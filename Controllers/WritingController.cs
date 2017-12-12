using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShBazmool.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using ShBazmool.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShBazmool.Controllers
{
    public class WritingController : Controller
    {
        private readonly ShBazmoolDbContext dbContext;
        public WritingViewModel writingView;

        public WritingController(ShBazmoolDbContext context) {
            dbContext = context;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(int? page)
        {
            var written = dbContext.Writings
                .Select(x => new Book { ID = x.ID, Category = x.Category, Title = x.Title })
                .ToList();
            var pager = new Pager(written.Count(), page);

            writingView = new WritingViewModel()
            {
                Books = written.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                PagerView = pager

            };
            return View(writingView);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AdminIndex(int? page)
        {
            var written = dbContext.Writings
                .Select(x => new Book { ID = x.ID, Category = x.Category, Title = x.Title })
                .ToList();
            var pager = new Pager(written.Count(), page);

            writingView = new WritingViewModel()
            {
                Books = written.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                PagerView = pager

            };
            return View(writingView);
        }

        [Authorize]
        public IActionResult Insert()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Insert(Writing writing,List<IFormFile> Material)
        {
            foreach(var item in Material)
            {
                if (item.Length > 0)
                {
                    using(var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        writing.Material = stream.ToArray();
                    }
                }
            }
            dbContext.Writings.Add(writing);
            dbContext.SaveChanges();

            return RedirectToAction("AdminIndex");
        }

        [HttpGet]
        public IActionResult Read(int ID)
        {
            var article = Find(ID);
            return File(article.Material, "application/pdf");
            
        //    return View(book);
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
        public async Task<IActionResult> Edit(Writing writing, List<IFormFile> Material)
        {
            foreach (var item in Material)
            {
                if(item.Length > 0)
                {
                    using(var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        writing.Material = stream.ToArray();
                    }
                }
            }
            Writing obj = new Writing
            {
                ID = writing.ID,
                Title = writing.Title,
                Category = writing.Category,
                Author = writing.Author,
                Material = writing.Material,
                Description = writing.Description,
                DatePublished = writing.DatePublished
            };
            dbContext.Writings.Update(obj);
            dbContext.SaveChanges();
            return RedirectToAction("AdminIndex");

        }

        [Authorize]
        public IActionResult Delete(int ID)
        {
            var written = Find(ID);
            dbContext.Writings.Remove(written);
            dbContext.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        [HttpGet]
        public IActionResult BookType(int ID)
        {
            var book = dbContext.Writings.Find(ID);
            var written = dbContext.Writings
              .Where(c=> c.Category == book.Category)
              .Select(x => new Book { ID = x.ID,Title = x.Title, Category = x.Category })
              .ToList();

            writingView = new WritingViewModel()
            {
                Books = written
            };

            return View(writingView);
        }

        protected Writing Find(int ID)
        {
            var book = dbContext.Writings.Find(ID);
            return book;
        }

    }
}
