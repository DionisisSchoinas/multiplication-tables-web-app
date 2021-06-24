using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using multiplication_tables_web_app.Models;

namespace multiplication_tables_web_app.Controllers
{
    public class TeacherController : Controller
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: Teacher/Students/
        public ActionResult Students()
        {
            var new_students = db.Users.Join(db.Students, users => users.UserID, students => students.UserID, (users, students) => new { StudentID = students.StudentID, Name = users.Name });
            ViewBag.students = new SelectList(new_students, "StudentID", "Name");
            return View();
        }

        // POST: Teacher/Students/
        [HttpPost]
        public ActionResult Students(int? StudentID)
        {
            if (StudentID == null)
            {
                ViewBag.error = "Διάλεξε μαθητή";
                return Students();
            }

            return Redirect("/Teacher/StudentDetails/" + StudentID);
        }

        // GET: Teacher/StudentDetails/1
        public ActionResult StudentDetails(int id)
        {
            ViewBag.StudentID = id;
            return View();
        }
    }
}
