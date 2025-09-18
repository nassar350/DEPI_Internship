using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationEF
{
    internal class StudentAnswer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public char SelectedOption { get; set; }
        public bool BooleanAnswer { get; set; }
        public decimal MarksObtained { get; set; }
        public DateTime SubmittedAt { get; set; }
        [ForeignKey("ExamAttemptId")]
        public int ExamAttemptId { get; set; }
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }

        public ExamAttempt examAttempt { get; set; }
        public Question question { get; set; }
    }
}
