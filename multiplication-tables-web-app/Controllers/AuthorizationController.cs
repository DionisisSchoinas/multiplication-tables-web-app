using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using multiplication_tables_web_app.Models;

namespace multiplication_tables_web_app.Controllers
{
    public class AuthorizationController : Controller
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: Authorization
        public ActionResult Index()
        {
            ViewBag.hint = "Συμπλήρωσε τα στοιχεία σου";
            return View();
        }

        // POST: Authorization
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User user_login)
        {
            var user = db.Users.Where(u => u.Username.Equals(user_login.Username) && u.Password.Equals(user_login.Password));
            if (user.Count() != 0)
            {
                var logged_user = user.First();
                var teacher = db.Teachers.Where(u => u.UserID.Equals(logged_user.UserID));
                if (teacher.Count() != 0)
                {
                    Session["is_teacher"] = true;
                    Session["teacher_id"] = teacher.First().TeacherID;
                    Session["name"] = logged_user.Name;
                    return Redirect("/");
                }

                var student = db.Students.Where(u => u.UserID.Equals(logged_user.UserID));
                if (student.Count() != 0)
                {
                    Session["is_teacher"] = false;
                    Session["student_id"] = student.First().StudentID;
                    Session["name"] = logged_user.Name;
                    return Redirect("/");
                }

                ViewBag.error = "Άγνωστο πρόβλημα";
                return View();
            }
            else
            {
                ViewBag.error = "Λανθασμένο όνομα χρήστη ή/και κωδικός πρόσβασης";
                return View();
            }
        }

        // GET: Authorization/Logout
        public ActionResult Logout()
        {
            Session.Remove("is_teacher");
            Session.Remove("teacher_id");
            Session.Remove("student_id");
            Session.Remove("name");
            return Redirect("/");
        }

        // GET: Authorization/Student
        public ActionResult Student()
        {
            return View();
        }

        // POST: Authorization/Student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Student(User new_student, string Password_Confirmation)
        {
            if (new_student.Password != Password_Confirmation)
            {
                ViewBag.error = "Κωδικός Χρήστη και Επιβεβαίωση Κωδικού Χρήστη πρέπει να είναι ίδια";
                return View();
            }

            if (db.Users.Where(m => m.Username.Equals(new_student.Username)).Count() != 0)
            {
                ViewBag.error = "Υπάρχει ήδη ο χρήστης";
                return View();
            }

            if (ModelState.IsValid)
            {
                var new_user = db.Users.Add(new_student);
                var student = new Student();
                student.UserID = new_user.UserID;
                db.Students.Add(student);
                db.SaveChanges();
            }

            return Redirect("/");
        }

        // GET: Authorization/Teacher
        public ActionResult Teacher()
        {
            return View();
        }

        // POST: Authorization/Teacher
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Teacher(User new_teacher, string Password_Confirmation)
        {
            if (new_teacher.Password != Password_Confirmation)
            {
                ViewBag.error = "Κωδικός Χρήστη και Επιβεβαίωση Κωδικού Χρήστη πρέπει να είναι ίδια";
                return View();
            }

            if (db.Users.Where(m => m.Username.Equals(new_teacher.Username)).Count() != 0)
            {
                ViewBag.error = "Υπάρχει ήδη ο χρήστης";
                return View();
            }

            if (ModelState.IsValid)
            {
                var new_user = db.Users.Add(new_teacher);
                var teacher = new Teacher();
                teacher.UserID = new_user.UserID;
                db.Teachers.Add(teacher);
                db.SaveChanges();
            }

            return Redirect("/");
        }
    }
}
