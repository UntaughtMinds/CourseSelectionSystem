using System;
using System.Collections.Generic;

namespace CourseSelectionSystem.Entitys
{
    public partial class CourseDb
    {
        public CourseDb()
        {
            StudentCourseMaping = new HashSet<StudentCourseMaping>();
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public int? IsElective { get; set; }
        public int? Credit { get; set; }

        public virtual ICollection<StudentCourseMaping> StudentCourseMaping { get; set; }
    }
}
