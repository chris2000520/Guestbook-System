using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestBookSystem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        // GET: User

       
        public ActionResult AllWords()
        {

            var gb = from u in db.Guestbooks
                     where u.IsPass == true
                     orderby u.CreatedOn descending
                     select u;

            return View("AllWords", gb.ToList());
        }

        
        public ActionResult MyWords()
        {
            
            int UserId = (int)Session["UserId"];
            var gb = from u in db.Guestbooks
                     where u.IsPass == true && u.UserId == UserId
                     orderby u.CreatedOn descending
                     select u;

            return View("MyWords", gb.ToList());
        }

        
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
      
        public ActionResult Create(Guestbook gb)
        {
            if (ModelState.IsValid)
            {
                //gb.CreatedOn = System.DateTime.Now;
                gb.UserId = (int)Session["UserId"];
                gb.IsPass = false;
                db.Guestbooks.Add(gb);
                db.SaveChanges();
                return RedirectToAction("AllWords");
            }
            return View();
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
            return RedirectToAction("AllWords", "User");

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}