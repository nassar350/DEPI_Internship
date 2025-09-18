using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF
{
    internal class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MaximumDegree { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Exam> Exams = new List<Exam>();
        public List<StudentCourse> StudentCourses = new List<StudentCourse>();
        public List<InstructorCourse> InstructorCourses = new List<InstructorCourse>();
    }
}
