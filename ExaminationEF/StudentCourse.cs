using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF
{
    internal class StudentCourse
    {
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        public DateTime EmrollmentDate { get; set; }
        public decimal Grade { get; set; }
        public bool IsCompleted { get; set; } = false;

        public Student student { get; set; }
        public Course course { get; set; }
    }
}
