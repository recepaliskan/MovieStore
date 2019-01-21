using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStoreV4.Models;
using MovieStoreV4.ViewModel;
using PagedList;
using PagedList.Mvc;

namespace MovieStoreV4.Controllers
{
    public class HomeController : Controller
    {
        MovieStoreDBEntities db = new MovieStoreDBEntities();
        // GET: Home
        public ActionResult Index(int page = 1)
        {
            var movie = db.tbl_Movie.OrderByDescending(m => m.ID).ToPagedList(page, 9);
            return View(movie);
        }

        public ActionResult CategoryPartial()
        {
            return View(db.tbl_Category.ToList());
        }
        public ActionResult MovieSearch(string search = null)
        {
            var searched = db.tbl_Movie.Where(m => m.Title.Contains(search)).ToList();
            return View(searched.OrderByDescending(m => m.ID));
        }


    }
}