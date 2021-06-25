using System;
using System.Collections.Generic;
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

        private string[] testToId = new string[]
        {
            "Προπαίδεια του 1",
            "Προπαίδεια του 2",
            "Προπαίδεια του 3",
            "Προπαίδεια του 4",
            "Προπαίδεια του 5",
            "Προπαίδεια του 6",
            "Προπαίδεια του 7",
            "Προπαίδεια του 8",
            "Προπαίδεια του 9",
            "Προπαίδεια του 10",
            "Τελικό τέστ προπαίδειας",
        };

        // GET: Teacher/Students/
        public ActionResult Students()
        {
            if (NotTeacher())
                return RedirectToAction("Index", "Authorization");

            //var new_students = db.Users.Join(db.Students, users => users.UserID, students => students.UserID, (users, students) => new { StudentID = students.StudentID, Name = users.Name, Grades = students.Grades });
            //ViewBag.students = new SelectList(new_students, "StudentID", "Name");
            var students = db.Students.ToList();

            var studentsData = new StudentData[students.Count()];
            for (int i=0; i < studentsData.Count(); i++)
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
                string[] cur_grade;
                for (int j=0; j<grades.Count(); j++)
                {
                    cur_grade = grades[j].Split(new string[] { "__" }, StringSplitOptions.None);
                    curScore = float.Parse(cur_grade[0]) / float.Parse(cur_grade[1]);
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
                    studentsData[i].best_score = testToId[minTest] + " : " + (1-minScore) * 100 + "%";

                if (maxTest == -1)
                    studentsData[i].worst_score = "Δεν έχουν γίνει τεστ";
                else
                    studentsData[i].worst_score = testToId[maxTest] + " : " + (1-maxScore) * 100 + "%";
            }


            ViewBag.students = studentsData;
            return View();
        }

        // GET: Teacher/StudentDetails/1
        public ActionResult StudentDetails(int id)
        {
            if (NotTeacher())
                return RedirectToAction("Index", "Authorization");

            ViewBag.StudentID = id;
            return View();
        }

        private bool NotTeacher()
        {
            return false;

            if (Session["is_teacher"] == null)
                return true;

            return false;
        }
    }
}
