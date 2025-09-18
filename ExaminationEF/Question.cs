using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF
{
    internal class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public decimal Marks { get; set; }
        public string QuestionType { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("ExamId")]
        public int ExamId { get; set; }
        public Exam exam { get; set; }
        public List<StudentAnswer> StudentAnswers { get; set; }
    }
}
