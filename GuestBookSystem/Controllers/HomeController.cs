using GuestBookSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestBookSystem.Controllers
{
    
    public class HomeController : Controller
    {
        GBSDBContext db = new GBSDBContext();

        public ActionResult Index()
        {
            var gb = from u in db.Guestbooks
                     where u.IsPass == true
                     orderby u.CreatedOn descending
                     select u;

            return View("Index", gb.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}