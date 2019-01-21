using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStoreV4.Models;

namespace MovieStoreV4.Areas.Admin.Controllers
{
    public class UsersAdminController : Controller
    {
        MovieStoreDBEntities db = new MovieStoreDBEntities();
        // GET: Admin/UsersAdmin
        public ActionResult Index()
        {
            var user = db.tbl_User.ToList();
            return View(user);
        }


        public ActionResult PassiveChange(int id)
        {
            tbl_User modelUser = db.tbl_User.Where(u => u.ID == id).FirstOrDefault();
            if (modelUser.Passive == true)
            {
                modelUser.Passive = false;
                db.SaveChanges();
                return RedirectToAction("Index","UsersAdmin");
            }
            else
            {
                modelUser.Passive = true;
                db.SaveChanges();
                return RedirectToAction("Index", "UsersAdmin");
            } 
        }

        public ActionResult Detail(int id)
        {

            var user = db.tbl_Comment.Where(u => u.UserID == id).ToList();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
            
        }


    }
    }