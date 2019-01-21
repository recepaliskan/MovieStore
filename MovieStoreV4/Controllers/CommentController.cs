using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStoreV4.Models;


namespace MovieStoreV4.Controllers
{
    public class CommentController : Controller
    {
        MovieStoreDBEntities db = new MovieStoreDBEntities();

        // GET: Comment
        public ActionResult Delete(int id)
        {
            if (Session["UserID"] != null)
            {
                var loginId = Session["UserID"];
                var comment = db.tbl_Comment.Where(c => c.ID == id).SingleOrDefault();
                var movie = db.tbl_Movie.Where(m => m.ID == comment.MovieID).SingleOrDefault();
                if (comment.UserID == Convert.ToInt32(loginId))
                {
                    db.tbl_Comment.Remove(comment);
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Movies", new { id = movie.ID });
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
           
        }
        public ActionResult AdminDeleteComment(int id)
        {
            try
            { 
                var comment = db.tbl_Comment.Where(c => c.ID == id).SingleOrDefault();
                var movie = db.tbl_Movie.Where(m => m.ID == comment.MovieID).SingleOrDefault();
                db.tbl_Comment.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("Index", "Movie", new { Area = "admin" });
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}