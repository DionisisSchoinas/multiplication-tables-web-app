using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace multiplication_tables_web_app.Models
{
    public class StudentMetadata
    {
        [Key]
        [Display(Name = "Student Id")]
        [Required]
        public string StudentID;
        
        [Display(Name = "User Id")]
        [Required]
        public string UserID;
    }

    public class TeacherMetadata
    {
        [Key]
        [Display(Name = "Teacher Id")]
        [Required]
        public string TeacherID;

        [Display(Name = "User Id")]
        [Required]
        public string UserID;
    }

    public class UserMetadata
    {
        [Key]
        [Display(Name = "User Id")]
        [Required]
        public string UserID;

        [Display(Name = "Όνομα Χρήστη")]
        [Required]
        [MaxLength(40, ErrorMessage = "Όνομα χρήστη έως 40 χαρακτήρες")]
        public string Username;

        [Display(Name = "Κωδικός Χρήστη")]
        [Required]
        [MaxLength(20, ErrorMessage = "Κωδικός χρήστη έως 20 χαρακτήρες")]
        public string Password;

        [Display(Name = "Ονοματεπώνυμο")]
        [Required]
        [MaxLength(80, ErrorMessage = "Ονοματεπώνυμο έως 80 χαρακτήρες")]
        public string Name;
    }
}