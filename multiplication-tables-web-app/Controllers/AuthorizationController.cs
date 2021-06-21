using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace multiplication_tables_web_app.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        public ActionResult Index()
        {
            return View();
        }

        // POST: Authorization
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            return Redirect("/");
        }

        // GET: Authorization/Student
        public ActionResult Student()
        {
            return View();
        }

        // POST: Authorization/Student
        [HttpPost]
        public ActionResult Student(FormCollection collection)
        {
            return Redirect("/");
        }

        // GET: Authorization/Teacher
        public ActionResult Teacher()
        {
            return View();
        }

        // POST: Authorization/Teacher
        [HttpPost]
        public ActionResult Teacher(FormCollection collection)
        {
            return Redirect("/");
        }
    }
}
