using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF
{
    internal class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; } = true;

        public List<InstructorCourse> InstructorCourses = new List<InstructorCourse>();
        public List<Exam> Exams = new List<Exam>();
    }
}

