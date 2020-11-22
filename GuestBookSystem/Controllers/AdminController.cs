using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestBookSystem.Controllers
{
    [Authorize (Roles ="管理员")]
    public class AdminController : Controller
    {
        // GET: Admin

        GBSDBContext db = new GBSDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckIndex()
        {
            var gb = from u in db.Guestbooks
                     where u.IsPass == false
                     orderby u.CreatedOn descending
                     select u;

            return View("CheckIndex", gb.ToList());
        }

        public ActionResult CheckMessage(int id)
        {
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }
        [HttpPost, ActionName("CheckMessage")]
        public ActionResult CheckMessage1(int id)
        {
            var gb = db.Guestbooks.Find(id);
            gb.IsPass = true;
            db.SaveChanges();
            return RedirectToAction("CheckIndex", new { target = "fc" });
        }


        public ActionResult DeleteIndex()
        {
            var gb = from u in db.Guestbooks
                     where u.IsPass == true
                     orderby u.CreatedOn descending
                     select u;
            return View(gb.ToList());
        }
        public ActionResult Delete(int id)
        {
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfired(int id)
        {
            var gb = db.Guestbooks.Find(id);
            db.Guestbooks.Remove(gb);
            db.SaveChanges();
            return RedirectToAction("DeleteIndex","Admin");

        }

        public ActionResult CommentSummary()
        {
            var gb = from u in db.Guestbooks
                     where u.IsPass == true
                     orderby u.CreatedOn descending
                     select u;
            int count = gb.Count();

            var gb2 = from u in db.Guestbooks
                     where u.IsPass == false
                     orderby u.CreatedOn descending
                     select u;
            int count2 = gb2.Count();

            ViewBag.count = count;
            ViewBag.count2 = count2;
            return View();
          
        }

        public ActionResult UserManage()
        {
            var user = from u in db.Users
                       select u;
            
            return View("UserManage", user.ToList());
        }


        public ActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("UserManage");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}