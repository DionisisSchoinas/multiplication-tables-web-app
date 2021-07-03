using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using multiplication_tables_web_app.Models;
using multiplication_tables_web_app.Models.Singleton;

namespace multiplication_tables_web_app.Controllers
{
    public class TeacherController : Controller
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: Teacher/Students/
        public ActionResult Students()
        {
            if (NotTeacher())
                return RedirectToAction("Index", "Authorization");

            //var new_students = db.Users.Join(db.Students, users => users.UserID, students => students.UserID, (users, students) => new { StudentID = students.StudentID, Name = users.Name, Grades = students.Grades });
            //ViewBag.students = new SelectList(new_students, "StudentID", "Name");
            var students = db.Students.ToList();

            var studentsData = new StudentData[students.Count()];
            for (int i = 0; i < studentsData.Count(); i++)
            {
                studentsData[i] = new StudentData();
                studentsData[i].student = students[i];

                /*
                    Format of grades:
                    mistakes1__total1|_mistakes2__total2|_mistakes3__mistakes3
                */
                string current_grades = students[i].Grades;

                // Fiil grades if needed
                if (current_grades == null)
                {
                    studentsData[i].worst_score = "Δεν έχουν γίνει τεστ";
                    studentsData[i].best_score = "Δεν έχουν γίνει τεστ";
                    continue;
                }

                // Split grades per subject
                string[] grades = current_grades.Split(new string[] { "|_" }, StringSplitOptions.None);

                // Split mistakes and total
                float minScore = 101;
                int minTest = -1;
                float maxScore = -1;
                int maxTest = -1;
                float curScore;
                for (int j = 0; j < grades.Count(); j++)
                {
                    curScore = float.Parse(grades[j]);
                    if (curScore > maxScore)
                    {
                        maxScore = curScore;
                        maxTest = j;
                    }
                    else if (curScore < minScore)
                    {
                        minScore = curScore;
                        minTest = j;
                    }
                }

                // Since the DB saves the mistakes the maxScore shows the test with the most mistakes
                // so we put 1-maxScore as the worstScore
                if (minTest == -1)
                    studentsData[i].best_score = "Δεν έχουν γίνει τεστ";
                else
                    studentsData[i].best_score = db.TestNames.Find(minTest+1).Name + " : " + minScore + "%";

                if (maxTest == -1)
                    studentsData[i].worst_score = "Δεν έχουν γίνει τεστ";
                else
                    studentsData[i].worst_score = db.TestNames.Find(maxTest + 1).Name + " : " + maxScore + "%";
            }

            ViewBag.students = studentsData;
            return View();
        }

        // GET: Teacher/StudentDetails/1
        public ActionResult StudentDetails(int? id)
        {
            if (NotTeacher())
                return RedirectToAction("Index", "Authorization");

            var student = db.Students.Find(id);
            if (student == null)
                return RedirectToAction("Students");
            
            var studentData = new StudentData();
            studentData.student = student;

            /*
                percentage = correct / total * 100

                Format of grades:
                percentage1|_percentage2|_percentage3
            */
            string current_grades = student.Grades;

            // Fiil grades if needed
            if (current_grades == null)
            {
                studentData.grades = null;
                ViewBag.studentData = studentData;
                return View();
            }

            // Split grades per subject
            string[] grades = current_grades.Split(new string[] { "|_" }, StringSplitOptions.None);

            // Split mistakes and total
            studentData.grades = new float[grades.Length];
            for (int i = 0; i < grades.Count(); i++)
            {
                studentData.grades[i] = float.Parse(grades[i]);
            }

            ViewBag.studentData = studentData;
            ViewBag.testNames = db.TestNames.ToList();
            return View();
        }

        private bool NotTeacher()
        {
            if (Session["is_teacher"] == null)
                return true;

            if (!(bool)Session["is_teacher"])
                return true;

            return false;
        }
    }
}
