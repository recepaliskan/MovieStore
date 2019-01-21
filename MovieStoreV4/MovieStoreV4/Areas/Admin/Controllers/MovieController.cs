using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MovieStoreV4.Models;

namespace MovieStoreV4.Areas.Admin.Controllers
{
    public class MovieController : Controller
    {
        private MovieStoreDBEntities db = new MovieStoreDBEntities();

        // GET: Admin/Movie
        public ActionResult Index()
        {
            var movie = db.tbl_Movie.ToList();
            return View(movie);
        }

        
        public ActionResult Details(int id)
        {
            var movie = db.tbl_Movie.Where(m => m.ID == id).SingleOrDefault();
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Admin/Movie/Create
        public ActionResult Create()
        {
         
            ViewBag.CategoryID = db.tbl_Category.ToList();
            return View();
        }

     
        [HttpPost]       
        public ActionResult Create(tbl_Movie movie, HttpPostedFileBase Image)
        {

            try
            {
                if(Image != null)
                {
                    WebImage img = new WebImage(Image.InputStream);
                    FileInfo fileInfo = new FileInfo(Image.FileName);

                    string newPhoto = Guid.NewGuid().ToString() + fileInfo.Extension;
                    img.Save("~/Uploads/MoviePhoto/" + newPhoto);
                    movie.Image = "/Uploads/MoviePhoto/" + newPhoto;
                    

                }
                db.tbl_Movie.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(movie);
            }
            
        }

        // GET: Admin/Movie/Edit/5
        public ActionResult Edit(int id)
        {
            var movie = db.tbl_Movie.Where(m => m.ID == id).SingleOrDefault();
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = db.tbl_Category.ToList();

            return View(movie);
        }

       
        [HttpPost]       
        public ActionResult Edit(int id, HttpPostedFileBase Image,tbl_Movie movie)
        {
            try
            { 
             var movies = db.tbl_Movie.Where(m => m.ID == id).SingleOrDefault();
                if (Image != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(movies.Image)))
                    {
                        System.IO.File.Delete(Server.MapPath(movies.Image));
                    }
                    WebImage img = new WebImage(Image.InputStream);
                    FileInfo fileInfo = new FileInfo(Image.FileName);

                    string newPhoto = Guid.NewGuid().ToString() + fileInfo.Extension;
                    img.Save("~/Uploads/MoviePhoto/" + newPhoto);
                    movies.Image = "/Uploads/MoviePhoto/" + newPhoto;
                    movies.Title = movie.Title;
                    movies.Price = movie.Price;
                    movies.CategoryID = movie.CategoryID;
                    movies.Description = movie.Description;
                    movies.Teaser = movie.Teaser;
                    movies.Video = movie.Video;
                   
                    db.SaveChanges();
                }

                
                return RedirectToAction("Index");
            
            }
            catch 
            {
                return View();
            }
         
        }

        //// GET: Admin/Movie/Delete/5
        public ActionResult Delete(int id)
        {
            var movie = db.tbl_Movie.Where(m => m.ID == id).SingleOrDefault();

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteOK(int id)
        {

            try
            {
                var movie = db.tbl_Movie.Where(m => m.ID == id).SingleOrDefault();
                if (movie == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(movie.Image)))
                {
                    System.IO.File.Delete(Server.MapPath(movie.Image));
                }
                foreach(var i in movie.tbl_Comment.ToList())
                {
                    db.tbl_Comment.Remove(i);
                }
                db.tbl_Movie.Remove(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

    }
}
