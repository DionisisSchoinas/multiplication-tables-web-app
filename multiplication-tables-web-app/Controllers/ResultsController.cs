using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using multiplication_tables_web_app.Models;

namespace multiplication_tables_web_app.Controllers
{
    public class ResultsController : Controller
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        // GET: Results
        public ActionResult Index()
        {
            try
            {
                Test testAnswers = TempData["test"] as Test;
                var correct_results = new bool[testAnswers.questions.Count];
                for (int i=0; i<testAnswers.questions.Count; i++)
                {
                    correct_results[i] = testAnswers.questions[i].given_answer.Trim().Equals(testAnswers.questions[i].answer.Trim());
                }
                SaveMistakes(correct_results, testAnswers);
                ViewBag.correct_results = correct_results;
                return View(testAnswers);
            }
            catch
            {
                return Redirect("/");
            }
        }

        private void SaveMistakes(bool[] correct_answers, Test test)
        {
            // Not logged in, return
            if (Session["is_teacher"] == null)
                return;

            // Not a student, return
            if ((bool)Session["is_teacher"] == true)
                return;

            int id = (int)Session["student_id"];
            int total = correct_answers.Count();
            int mistakes = 0;
            // For each correct answer add 0 else add 1
            foreach (bool m in correct_answers)
                mistakes += (m) ? 0 : 1;

            /*
                Format of grades:
                mistakes1__total1|_mistakes2__total2|_mistakes3__mistakes3
            */
            Student student = db.Students.Where(m => m.StudentID == id).First();
            string current_grades = student.Grades;

            // Fiil grades if needed
            if (current_grades == null)
            {
                current_grades = "0__0";
                for (int i=1; i<11; i++)
                {
                    current_grades += "|_0__0";
                }
            }
            // Split grades per subject
            string[] grades = current_grades.Split(new string[] { "|_" }, StringSplitOptions.None);

            // Split mistakes and total
            string[] prev_test_grades = grades[test.test_id].Split(new string[] { "__" }, StringSplitOptions.None);
            
            // Add new test results 
            prev_test_grades[0] = (mistakes + int.Parse(prev_test_grades[0])).ToString();
            prev_test_grades[1] = (total + int.Parse(prev_test_grades[1])).ToString();

            // Set new grades
            grades[test.test_id] = prev_test_grades[0] + "__" + prev_test_grades[1];

            // Join all subjects again into single string
            string new_grades = string.Join("|_", grades);

            // Set new grades string into Student object
            student.Grades = new_grades;

            // Update Student object in DB
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
