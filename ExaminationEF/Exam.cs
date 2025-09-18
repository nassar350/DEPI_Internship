using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF
{
    internal class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalMarks { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        [ForeignKey("InstructorId")]
        public int InstructorId { get; set; }

        public Course course { get; set; }
        public Instructor instructor { get; set; }
        public List<ExamAttempt> attempt { get; set; }
    }
}
