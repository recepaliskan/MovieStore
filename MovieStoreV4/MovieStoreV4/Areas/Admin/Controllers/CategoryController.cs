using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieStoreV4.Models;

namespace MovieStoreV4.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private MovieStoreDBEntities db = new MovieStoreDBEntities();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var category = db.tbl_Category.ToList();
            return View(category);
        }

     
    

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]        
        public ActionResult Create(tbl_Category category)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Category.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        
        public ActionResult Edit(int id)
        {
            var category = db.tbl_Category.Where(c => c.ID == id).SingleOrDefault();
            
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(int id,tbl_Category category)
        {
            var categories = db.tbl_Category.Where(c => c.ID == id).SingleOrDefault();
            categories.Name = category.Name;
            db.SaveChanges();
            return View();
        }

      
        public ActionResult Delete(int id)
        {

            var category = db.tbl_Category.Where(c => c.ID == id ).SingleOrDefault();
            //if (category.tbl_Movie.Where(x => x.tbl_Category.ID ==id ).Count() == 1)
            //{
            //    return HttpNotFound();
            //}
            if (category == null)
            {
                return HttpNotFound();
            }
           
            return View(category);
            
            
            
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteOK(int id)
        {

            try
            {
                var category = db.tbl_Category.Where(c => c.ID == id).SingleOrDefault();
                if (category == null)
                {
                    return HttpNotFound();
                }
                db.tbl_Category.Remove(category);
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
