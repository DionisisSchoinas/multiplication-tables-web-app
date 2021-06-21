using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace multiplication_tables_web_app.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/StudentDetails/5
        public ActionResult StudentDetails(int id)
        {
            ViewBag.student_id = id;
            return View();
        }
    }
}
