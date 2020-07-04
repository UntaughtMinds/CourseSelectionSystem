using System;
using System.Collections.Generic;

namespace CourseSelectionSystem.Entitys
{
    public partial class StudentDb
    {
        public StudentDb()
        {
            StudentCourseMaping = new HashSet<StudentCourseMaping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int? Credit { get; set; }

        public virtual ICollection<StudentCourseMaping> StudentCourseMaping { get; set; }
    }
}
