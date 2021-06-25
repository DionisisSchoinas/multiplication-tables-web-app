using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace multiplication_tables_web_app.Models
{
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student { }

    [MetadataType(typeof(TeacherMetadata))]
    public partial class Teacher { }

    [MetadataType(typeof(UserMetadata))]
    public partial class User { }

    [MetadataType(typeof(TestNameMetadata))]
    public partial class TestName { }
}