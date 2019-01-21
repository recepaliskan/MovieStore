using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStoreV4.Models;
using MovieStoreV4.ViewModel;

namespace MovieStoreV4.Controllers
{
    public class MoviesController : Controller
    {
        MovieStoreDBEntities db = new MovieStoreDBEntities();

        public ActionResult Detail(int id)
        {
            var movie = db.tbl_Movie.Where(m => m.ID == id).SingleOrDefault();
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }
        public ActionResult Watch(int id)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    

                    var movie = db.tbl_Movie.Where(m => m.ID == id).SingleOrDefault();
                    int loginId = (int)Session["UserID"];
                    if (movie.tbl_MovieUser.Where(x => x.UserID == loginId).Count() < 1)
                    {
                        return HttpNotFound();
                    }
                    if (movie == null)
                    {
                        return HttpNotFound();
                    }
                    return View(movie);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
                

        }
        public JsonResult Comment(string comment,int movieId)
        {
            var loginId = Session["UserID"];
            if (comment == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);                
            }
            db.tbl_Comment.Add(new tbl_Comment { UserID = Convert.ToInt32(loginId), MovieID = movieId, Comment = comment, Date = DateTime.Now });
            db.SaveChanges();

            return Json(false,JsonRequestBehavior.AllowGet);
        }

        public ActionResult CategorizedMovie(int id)
        {
            List<tbl_Movie> categorizedMovie = db.tbl_Movie.Where(m => m.CategoryID == id).ToList();
            return View(categorizedMovie);

        }

        public ActionResult Buy(tbl_MovieUser buy, int id)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    

                    var loginId = Session["UserID"];
                    buy.MovieID = id;
                    buy.UserID = Convert.ToInt32(loginId);
                    db.tbl_MovieUser.Add(buy);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return HttpNotFound();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }

           
        }
        public ActionResult BuyMovies()
        {

            if (Session["UserID"] != null)
            { 
                int loginId = (int)Session["UserID"];
                List<tbl_MovieUser> buyMovies = db.tbl_MovieUser.Where(m => m.UserID == loginId && m.MovieID==m.tbl_Movie.ID).ToList();
            
                return View(buyMovies);
            }
            else
            {
                return HttpNotFound();
            }
        }



    }
}













//public ActionResult DenemeListele()
//{
//    MovieUserViewModel movieUserViewModel = new MovieUserViewModel();
//    movieUserViewModel.modelMovies = db.tbl_Movie.ToList();
//    movieUserViewModel.modelMovieUsers = db.tbl_MovieUser.ToList();
//    return View(movieUserViewModel);
//}


//public ActionResult NotMovies()
//{
//    int loginId = (int)Session["UserID"];
//    List<tbl_MovieUser> buyMovies = db.tbl_MovieUser.Where(m => m.UserID == loginId && m.MovieID == m.tbl_Movie.ID).ToList();
//    List<tbl_Movie> satinAlinmayan = new List<tbl_Movie>();
//    var movieslist = db.tbl_Movie.ToList();
//    foreach (var item in movieslist)
//    {
//        if (buyMovies.Count() < 1)
//        {
//            satinAlinmayan = movieslist;
//            break;

//        }
//        foreach (var item1 in buyMovies)
//        {
//            if (item.ID != item1.MovieID)
//            {
//                if (buyMovies.Where(i => i.MovieID == item.ID).ToList().Count() != 1)
//                {
//                    if (satinAlinmayan.Where(i => i.ID == item.ID).ToList().Count() == 1)
//                    {
//                        continue;
//                    }
//                    else
//                    {
//                        satinAlinmayan.Add(item);
//                    }
//                }

//            }
//        }
//    }

//    return View(satinAlinmayan);
//}