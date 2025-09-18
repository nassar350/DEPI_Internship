using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExaminationEF
{
    internal class ExaminationDbContext : DbContext
    {
        public ExaminationDbContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExaminationDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<ExamAttempt> ExamAttempts { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<InstructorCourse> InstructorCourses { get; set; }
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public DbSet<TrueFalseQuestion> TrueFalseQuestions { get; set; }
        public DbSet<EssayQuestion> EssayQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Exams)
                .WithOne(c => c.course)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .Property(c => c.Title)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .Property(c => c.MaximumDegree)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .Property(c => c.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Student>()
                .HasMany(p => p.ExamAttempts)
                .WithOne(p => p.Student)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(p => p.Email)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(p => p.StudentNumber)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(p => p.EnrollmentDate)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(p => p.StudentNumber)
                .IsUnique();

            modelBuilder.Entity<Instructor>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Instructor>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Instructor>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Instructor>()
                .Property(p => p.Email)
                .IsRequired();

            modelBuilder.Entity<Instructor>()
                .Property(p => p.Specialization)
                .IsRequired();

            modelBuilder.Entity<Instructor>()
                .Property(p => p.HireDate)
                .IsRequired();

            modelBuilder.Entity<Exam>()
                .HasKey(p => p.Id);


            modelBuilder.Entity<Exam>()
                .Property(p => p.Title)
                .IsRequired();

            modelBuilder.Entity<Exam>()
                .HasIndex(p => p.StartDate);

            modelBuilder.Entity<Exam>()
                .Property(p => p.Description)
                .IsRequired();

            modelBuilder.Entity<Exam>()
                .Property(p => p.TotalMarks)
                .IsRequired();

            modelBuilder.Entity<Exam>()
                .Property(p => p.Duration)
                .IsRequired();

            modelBuilder.Entity<Exam>()
                .Property(p => p.StartDate)
                .IsRequired();

            modelBuilder.Entity<Exam>()
                .Property(p => p.EndDate)
                .IsRequired();

            modelBuilder.Entity<Question>()
                .HasKey (p => p.Id);

            modelBuilder.Entity<Question>()
                .Property(p => p.QuestionText)
                .IsRequired();

            modelBuilder.Entity<Question>()
                .Property(p => p.Marks)
                .IsRequired();

            modelBuilder.Entity<Question>()
                .Property(p => p.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<Question>()
                .Property(p => p.QuestionType)
                .IsRequired();

            modelBuilder.Entity<MultipleChoiceQuestion>()
                .Property(p => p.OptionA)
                .IsRequired();

            modelBuilder.Entity<MultipleChoiceQuestion>()
                .Property(p => p.OptionB)
                .IsRequired();

            modelBuilder.Entity<MultipleChoiceQuestion>()
                .Property(p => p.OptionC)
                .IsRequired();

            modelBuilder.Entity<MultipleChoiceQuestion>()
                .Property(p => p.OptionD)
                .IsRequired();

            modelBuilder.Entity<MultipleChoiceQuestion>()
                .Property(p => p.CorrectOption)
                .IsRequired();

            modelBuilder.Entity<TrueFalseQuestion>()
                .Property(p => p.CorrectAnswer)
                .IsRequired();

            modelBuilder.Entity<EssayQuestion>()
                .Property(p => p.GradingCriteria)
                .IsRequired();

            modelBuilder.Entity<StudentCourse>()
                .HasKey(p => new {p.StudentId, p.CourseId});

            modelBuilder.Entity<StudentCourse>()
                .Property(p => p.EmrollmentDate)
                .IsRequired();

            modelBuilder.Entity<InstructorCourse>()
                .HasKey(p => new {p.InstructorId, p.CourseId});

            modelBuilder.Entity<InstructorCourse>()
                .Property(p => p.AssignedDate)
                .IsRequired();

            modelBuilder.Entity<ExamAttempt>()
                .HasKey(p => p.Id);


            modelBuilder.Entity<ExamAttempt>()
                .HasIndex(p => p.StartTime);

            modelBuilder.Entity<ExamAttempt>()
                .Property(p => p.StartTime)
                .IsRequired();

            modelBuilder.Entity<ExamAttempt>()
                .Property(p => p.StartTime)
                .IsRequired();

            modelBuilder.Entity<StudentAnswer>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<StudentAnswer>()
                .Property(p => p.AnswerText)
                .IsRequired();

            modelBuilder.Entity<StudentAnswer>()
                .Property(p => p.SubmittedAt)
                .IsRequired();


            modelBuilder.Entity<Question>()
            .ToTable("Questions")
            .HasDiscriminator<string>("QuestionType")
            .HasValue<MultipleChoiceQuestion>("MultipleChoice")
            .HasValue<TrueFalseQuestion>("TrueFalse")
            .HasValue<EssayQuestion>("Essay");


            modelBuilder.Entity<Student>()
                .HasData(new Student { Id = 1, Name = "Ahmed", Email = "Ahmed@gmail.com", StudentNumber = "S101" });

            modelBuilder.Entity<Instructor>()
                .HasData(new Instructor { Id = 1, Name = "ali", Email = "Ali@gmail.com",Specialization = "development" });

            modelBuilder.Entity<Course>()
                .HasData(new Course { Id = 1, Title = "C#", MaximumDegree = 100, Description = "Programming"}); 
        }
    }
}
