using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        public ActionResult Index()
        {
            ViewData.Model = "Hello World!";
            return View();
        }

        public ActionResult ViewBagDemo()
        {
            ViewBag.Text = "ViewBag";
            ViewData["Text"] = "ViewBag";
            return View();
        }

        public ActionResult ViewDataDemo()
        {
            ViewData["data"] = db.Client.Take(5);

            ViewData["Text"] = "ViewData";
            ViewBag.Text = "ViewData";
            return View();
        }

        public ActionResult TempDataSave()
        {
            TempData["Text"] = "TempData";
            return RedirectToAction("TempDataDemo");
        }

        public ActionResult TempDataDemo()
        {
            
            return View();
        }
    }
}