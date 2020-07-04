using System;
using System.Collections.Generic;

namespace CourseSelectionSystem.Entitys
{
    public partial class StudentCourseMaping
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }

        public virtual CourseDb Course { get; set; }
        public virtual StudentDb Student { get; set; }
    }
}
