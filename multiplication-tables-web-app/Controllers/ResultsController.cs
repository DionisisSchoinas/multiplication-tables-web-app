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
                if (testAnswers == null)
                    return Redirect("/");

                var correct_results = new bool[testAnswers.questions.Count];
                var correct_results_total = 0;
                for (int i=0; i<testAnswers.questions.Count; i++)
                {
                    correct_results[i] = testAnswers.questions[i].given_answer.Trim().Equals(testAnswers.questions[i].answer.Trim());
                    correct_results_total += correct_results[i] ? 1 : 0;
                }
                SaveMistakes(correct_results, correct_results_total, testAnswers);
                ViewBag.correct_results = correct_results;
                ViewBag.correct_results_total = correct_results_total;
                ViewBag.hint = "Εδώ βλέπεις τα αποτελέσματά σου";
                return View(testAnswers);
            }
            catch
            {
                return Redirect("/");
            }
        }

        private void SaveMistakes(bool[] correct_answers, int correct_answers_total, Test test)
        {
            // Not logged in, return
            if (Session["is_teacher"] == null)
                return;

            // Not a student, return
            if ((bool)Session["is_teacher"] == true)
                return;

            int id = (int)Session["student_id"];
            int total = correct_answers.Count();
            float perc = (float)correct_answers_total / (float)total * 100;

            /*
                percentage = correct / total * 100

                Format of grades:
                percentage1|_percentage2|_percentage3
            */
            Student student = db.Students.Where(m => m.StudentID == id).First();
            string current_grades = student.Grades;

            // Fiil grades if needed
            if (current_grades == null)
            {
                current_grades = "0";
                for (int i=1; i<db.TestNames.ToList().Count(); i++)
                {
                    current_grades += "|_0";
                }
            }
            // Split grades per subject
            string[] grades = current_grades.Split(new string[] { "|_" }, StringSplitOptions.None);

            float old_perc = float.Parse(grades[test.test_id]);
            if (perc > old_perc)
                old_perc = perc;

            // Set new grades
            grades[test.test_id] = old_perc.ToString();

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
