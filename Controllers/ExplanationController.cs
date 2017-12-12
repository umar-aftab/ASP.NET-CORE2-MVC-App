using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShBazmool.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using ShBazmool.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShBazmool.Controllers
{
    public class ExplanationController : Controller
    {
        private readonly ShBazmoolDbContext dbContext;
        public ExplanationViewModel explainView;

        public ExplanationController(ShBazmoolDbContext context)
        {
            dbContext = context;

        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(int? page)
        {
            var audio = dbContext.Explanations
                .Select(x => new Explain { Title= x.Title, BookName = x.BookName, ID= x.ID, Date =x.Date })
                .ToList();
            var pager = new Pager(audio.Count(), page);

            explainView = new ExplanationViewModel()
            {
                Explains = audio.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                PagerView = pager

            };
            return View(explainView);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AdminIndex(int? page)
        {
            var audio = dbContext.Explanations
                .Select(x => new Explain { Title = x.Title, BookName = x.BookName, ID = x.ID, Date = x.Date })
                .ToList();
            var pager = new Pager(audio.Count(), page);

            explainView = new ExplanationViewModel()
            {
                Explains = audio.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                PagerView = pager

            };
            return View(explainView);
        }

        [Authorize]
        public IActionResult Insert()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Insert(Explanation explain, List<IFormFile> Material)
        {
            foreach (var item in Material)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        explain.Material = stream.ToArray();
                    }
                }
            }
            dbContext.Explanations.Add(explain);
            dbContext.SaveChanges();

            return RedirectToAction("AdminIndex");
        }

        public IActionResult Listen(int ID)
        {
           return View(Find(ID));
        }


        [Authorize]
        [HttpGet]
        public IActionResult ListenAdmin(int ID)
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
        public async Task<IActionResult> Edit(Explanation explain, List<IFormFile> Material)
        {
            foreach (var item in Material)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        explain.Material = stream.ToArray();
                    }
                }
            }
            Explanation obj = new Explanation
            {
                ID = explain.ID,
                Title = explain.Title,
                Author = explain.Author,
                Material = explain.Material,
                Description = explain.Description,
                BookName = explain.BookName,
                Location = explain.Location,
                Date = explain.Date
            };
            dbContext.Explanations.Update(obj);
            dbContext.SaveChanges();
            return RedirectToAction("AdminIndex");

        }

        [Authorize]
        public IActionResult Delete(int ID)
        {
            var exp = Find(ID);
            dbContext.Explanations.Remove(exp);
            dbContext.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        protected Explanation Find(int ID)
        {
            var explain = dbContext.Explanations.Find(ID);
            return explain;
        }


    }
}
