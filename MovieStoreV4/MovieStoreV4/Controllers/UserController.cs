using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStoreV4.Models;
using MovieStoreV4.ViewModel;

namespace MovieStoreV4.Controllers
{
    public class UserController : Controller
    {
        MovieStoreDBEntities db = new MovieStoreDBEntities();
        // GET: User
        public ActionResult Index()
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    int loginId = Convert.ToInt32(Session["UserID"]);
                    var user = db.tbl_User.Where(u => u.ID == loginId).SingleOrDefault();
                 
                    if (loginId != user.ID)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
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
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_User user)
        {
            if (ModelState.IsValid)
            {
                user.AuthorizationID = 2;
                user.Passive = false;
                db.tbl_User.Add(user);
                db.SaveChanges();
                
                return RedirectToAction("Index","Home");
            }
            return View(user);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_User modelUser)
        {
            var user = db.tbl_User.FirstOrDefault(u => u.Username == modelUser.Username && u.Password == modelUser.Password);
            if (user != null)
            {

                if (user.Passive == false)
                {

                    Session["UserID"] = user.ID;
                    Session["AuthorizationID"] = user.AuthorizationID;
                    Session["UserName"] = user.Username;
                    return RedirectToAction("Index","Home");
                }
                ViewBag.HATA = "Üyeliğiniz pasif lütfen adminle iletişime geçiniz.";
                return View();
            }
            ViewBag.HATA = "Kullanıcı Adı veya Şifre Yanlış";
            return View();
        }
        
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["AuthorizationID"] = null;
            return RedirectToAction("Index", "Home");
             
        }
        public ActionResult Edit(int id)
        {
            var user = db.tbl_User.Where(u => u.ID == id).SingleOrDefault();
            if (Convert.ToInt32(Session["UserID"]) != user.ID)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(tbl_User user,int id)
        {
            if (ModelState.IsValid)
            {
                var users = db.tbl_User.Where(u => u.ID == id).SingleOrDefault();
                users.Username = user.Username;
                users.Password = user.Password;
                users.Phone = user.Phone;
                users.Email = user.Email;
                db.SaveChanges();
                Session["UserName"] = user.Username;
                return RedirectToAction("Index", "Home", new { userId = user.ID });

            }

            return View();
        }


    }
}