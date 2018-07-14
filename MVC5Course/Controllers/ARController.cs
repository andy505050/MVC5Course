using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewTest()
        {
            string model = "My Data";
            return View((Object)model);
        }

        public ActionResult PartialViewTest()
        {
            string model = "My Data";
            return PartialView("ViewTest", (Object)model);
        }

        public ActionResult FileTest(string dl)
        {
            if(dl==null)
                return File("~/App_Data/FileTest.jpg","image/jpeg");
            else
                return File("~/App_Data/FileTest.jpg", "image/jpeg","Yes.jpg");
        }
    }
}