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
    public class LectureController : Controller
    {
        private readonly ShBazmoolDbContext dbContext;
        public LectureViewModel audioView;

        public LectureController(ShBazmoolDbContext context)
        {
            dbContext = context;

        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(int? page)
        {
            var audio = dbContext.Lectures
                .Select( x=> new Audio { ID = x.ID, Title = x.Title, Date = x.Date})
                .ToList();
            var pager = new Pager(audio.Count(), page);

            audioView = new LectureViewModel()
            {
                Audios = audio.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                PagerView = pager

            };
            return View(audioView);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AdminIndex(int? page)
        {
            var audio = dbContext.Lectures
                .Select(x => new Audio { ID = x.ID, Title = x.Title, Date = x.Date })
                .ToList();
            var pager = new Pager(audio.Count(), page);

            audioView = new LectureViewModel()
            {
                Audios = audio.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                PagerView = pager

            };
            return View(audioView);
        }

        [Authorize]
        public IActionResult Insert()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Insert(Lecture audio, List<IFormFile> Material)
        {
            foreach (var item in Material)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        audio.Material = stream.ToArray();
                    }
                }
            }
            dbContext.Lectures.Add(audio);
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
        public async Task<IActionResult> Edit(Lecture lecture, List<IFormFile> Material)
        {
            foreach (var item in Material)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        lecture.Material = stream.ToArray();
                    }
                }
            }
            Lecture obj = new Lecture
            {
                ID = lecture.ID,
                Title = lecture.Title,
                Author = lecture.Author,
                Material = lecture.Material,
                Description = lecture.Description,
                Location = lecture.Location,
                Date = lecture.Date
            };
            dbContext.Lectures.Update(obj);
            dbContext.SaveChanges();
            return RedirectToAction("AdminIndex");

        }

        [Authorize]
        public IActionResult Delete(int ID)
        {
            var lec = Find(ID);
            dbContext.Lectures.Remove(lec);
            dbContext.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        protected Lecture Find(int ID)
        {
            var lecture= dbContext.Lectures.Find(ID);
            return lecture;
        }

    }
}
