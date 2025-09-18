using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF
{
    internal class InstructorCourse
    {
        [ForeignKey("InstructorId")]
        public int InstructorId { get; set; }
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        public DateTime AssignedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public Instructor instructor { get; set; }
        public Course course { get; set; }
    }
}
