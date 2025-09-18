using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF
{
    internal class ExamAttempt
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalScore { get; set; }
        public bool IsSubmitted { get; set; } = false;
        public bool IsGraded { get; set; } = false;
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        [ForeignKey("ExamId")]
        public int ExamId { get; set; }

        public Student Student { get; set; }
        public Exam exam { get; set; }
        public List<StudentAnswer> Answers { get; set; }
    }
}
