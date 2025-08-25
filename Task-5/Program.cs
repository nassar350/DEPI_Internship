namespace Task_5
{
    internal class Program
    {
        static List<Course> courses = new List<Course>();
        static List<Student> students = new List<Student>();
        static List<Instructor> instructors = new List<Instructor>();
        static List<Exam> exams = new List<Exam>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Examination System ===");
                Console.WriteLine("1. Manage Courses");
                Console.WriteLine("2. Manage Students");
                Console.WriteLine("3. Manage Instructors");
                Console.WriteLine("4. Manage Exams");
                Console.WriteLine("5. Take Exam (Student)");
                Console.WriteLine("6. Reports and Compare Students");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("--- Add Course ---");
                        Console.Write("Enter course title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter Course Code: ");
                        string code = Console.ReadLine();

                        Console.Write("Enter course description: ");
                        string desc = Console.ReadLine();

                        Console.Write("Enter maximum degree: ");
                        int maxDegree = int.Parse(Console.ReadLine());

                        courses.Add(new Course(title, code, desc, maxDegree));
                        Console.WriteLine("Course added Successfully!");
                        Console.WriteLine("Press Enter....");
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("--- Add Student ---");

                        Console.Write("Enter Student Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Student Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Enter Student Phone Number: ");
                        string Phonenumber = Console.ReadLine();

                        if (Phonenumber.Length != 11)
                        {
                            Console.WriteLine("Phone Number is not Valid!");
                            Console.WriteLine("Press Enter...");
                            Console.ReadLine();
                            break;
                        }
                        else if (!email.Contains('@'))
                        {
                            Console.WriteLine("Email is not Valid!");
                            Console.WriteLine("Press Enter...");
                            Console.ReadLine();
                            break;
                        }

                        Student student = new Student(name, email, Phonenumber);
                        students.Add(student);

                        Console.WriteLine("Enroll in how many courses ?");
                        Console.Write("Answer: ");
                        int count = int.Parse(Console.ReadLine());

                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine($"Available Courses: ");
                            ListCourses();

                            Console.Write("Enter the Course number: ");
                            int courseIndex = int.Parse(Console.ReadLine()) - 1;
                            student.AddCourse(courses[courseIndex]);
                            courses[courseIndex].AddStudent(student);
                        }

                        Console.WriteLine("Student Has Been Registered Successfully!");
                        Console.WriteLine("Press Enter....");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("--- Add Instructor ---");

                        Console.Write("Enter Instructor Name: ");
                        string Iname = Console.ReadLine();

                        Console.Write("Enter Instructor Specialization: ");
                        string spec = Console.ReadLine();

                        Console.Write("Enter Instructor Phone Number: ");
                        string phonenumber = Console.ReadLine();

                        if (phonenumber.Length != 11)
                        {
                            Console.WriteLine("Phone Number is not Valid!");
                            Console.WriteLine("Press Enter...");
                            Console.ReadLine();
                            break;
                        }

                        Instructor instructor = new Instructor(Iname, spec, phonenumber);
                        instructors.Add(instructor);

                        Console.WriteLine("Assign to how many courses ?");
                        int count2 = int.Parse(Console.ReadLine());

                        for (int i = 0; i < count2; i++)
                        {
                            Console.WriteLine($"Available Courses: ");
                            ListCourses();

                            Console.Write("Enter the Course number: ");
                            int courseIndex = int.Parse(Console.ReadLine()) - 1;
                            instructor.AddCourse(courses[courseIndex]);
                            courses[courseIndex].setInstructorName(Iname);
                        }

                        Console.WriteLine("Instructor Has Been Registered Successfully!\n" +
                            "Press Enter...");
                        Console.ReadLine();
                        break;
                    case "4":
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("--- Exam Management ---");
                            Console.WriteLine("1. Create Exam");
                            Console.WriteLine("2. Add Question to Exam");
                            Console.WriteLine("3. Edit Question");
                            Console.WriteLine("4. Remove Question");
                            Console.WriteLine("5. Start Exam");
                            Console.WriteLine("6. Duplicate Exam to Another Course");
                            Console.WriteLine("7. View Exam Details");
                            Console.WriteLine("0. Back to Main Menu");

                            Console.Write("Choose an option: ");
                            var ch2 = Console.ReadLine();

                            if (ch2 == "0") break;

                            switch (ch2)
                            {
                                case "1":

                                    Console.Clear();
                                    Console.WriteLine("--- Create Exam ---");
                                    Console.Write("Enter exam type: ");
                                    string type = Console.ReadLine();

                                    Console.WriteLine("Select course:");
                                    ListCourses();
                                    Console.Write("Enter course number: ");
                                    int courseIndex = int.Parse(Console.ReadLine()) - 1;
                                    Course course = courses[courseIndex];

                                    Exam exam = new Exam(course.Title, type);
                                    exams.Add(exam);
                                    course.AddExam(exam);


                                    Console.WriteLine("Exam created Successfully! You can now add questions");
                                    Console.WriteLine("Press Enter...");
                                    Console.ReadLine();
                                    break;
                                case "2":

                                    Console.Clear();
                                    Console.WriteLine("--- Add Question ---");
                                    Console.WriteLine("Select course:");
                                    ListCourses();
                                    Console.Write("Enter course number: ");
                                    int courseIndex2 = int.Parse(Console.ReadLine()) - 1;
                                    Course course2 = courses[courseIndex2];

                                    Console.WriteLine("Available Exams: ");
                                    for (int i = 0; i < course2.exams.Count && course2.exams[i].Started == false; i++)
                                        Console.WriteLine($"{i + 1}. {course2.exams[i].ExamType} - {course2.exams[i].Year}");

                                    Console.Write("Enter Exam Number: ");
                                    int Examindex = int.Parse(Console.ReadLine()) - 1;

                                    Console.WriteLine("Choose question type:\n1. Multiple Choice\n2. True/False\n3. Text");
                                    string qtype = Console.ReadLine();

                                    Console.Write("Question text: ");
                                    string qText = Console.ReadLine();

                                    Console.Write("Marks: ");
                                    decimal marks = decimal.Parse(Console.ReadLine());

                                    decimal currentMarks = exams[Examindex].CurrentMark;
                                    if ((currentMarks + marks) > course2.MaxDegree)
                                    {
                                        Console.WriteLine("Cannot add question. Total marks will exceed course maximum.");
                                        Console.WriteLine("Press Enter ...");
                                        Console.ReadLine();
                                        break;
                                    }

                                    Question q = null;
                                    switch (qtype)
                                    {
                                        case "1":
                                            Console.WriteLine("Enter 4 options:");
                                            List<string> options = new List<string>();

                                            for (int i = 0; i < 4; i++)
                                            {
                                                Console.Write($"Option {i + 1}: ");
                                                options.Add(Console.ReadLine());
                                            }

                                            Console.Write("Correct option Number (1 - 4) is: ");
                                            int correct = int.Parse(Console.ReadLine()) - 1;

                                            q = new MultipleChoice(qText, marks, options[correct], options);
                                            
                                            break;
                                        case "2":
                                            Console.Write("Correct answer (true or false): ");
                                            string tf = Console.ReadLine();

                                            q = new TrueOrFalse(qText, marks, tf);
                                            break;
                                        case "3":
                                            Console.Write("Enter Question Answer: ");
                                            string ans = Console.ReadLine();

                                            q = new TextQuestion(qText, marks, ans);
                                            break;
                                        default:
                                            Console.WriteLine("Invalid Option. Enter A valid One!");
                                            break;
                                    }

                                    exams[Examindex].CurrentMark += marks;
                                    course2.exams[Examindex].AddQuestion(q);

                                    Console.WriteLine("Question added successfully!\nPress Enter...");
                                    Console.ReadLine();
                                    break;
                                case "3":

                                    Console.Clear();
                                    Console.WriteLine("--- Edit Question ---");
                                    Console.WriteLine("Select course:");
                                    ListCourses();
                                    Console.Write("Enter course number: ");
                                    int courseIndex3 = int.Parse(Console.ReadLine()) - 1;
                                    Course course3 = courses[courseIndex3];

                                    Console.WriteLine("Available Exams: ");
                                    if (course3.exams.Count == 0)
                                    {
                                        Console.WriteLine("there is no exams");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        break;
                                    }
                                    for (int i = 0; i < course3.exams.Count && course3.exams[i].Started == false; i++)
                                        Console.WriteLine($"{i + 1}. {course3.exams[i].ExamType} - {course3.exams[i].Year}");

                                    Console.Write("Enter Exam Number: ");
                                    int Examindex2 = int.Parse(Console.ReadLine()) - 1;

                                    Exam exam2 = course3.exams[Examindex2];

                                    if (exam2.questions.Count == 0)
                                    {
                                        Console.WriteLine("there is no questions");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        break;
                                    }
                                    for (int i = 0; i < exam2.questions.Count; i++)
                                        Console.WriteLine($"{i + 1}. {exam2.questions[i].QuestionTitle}\n" +
                                            $"({exam2.questions[i].Marks} marks)");

                                    Console.Write("Select question to edit: ");
                                    int qIndex = int.Parse(Console.ReadLine()) - 1;

                                    Question q2 = exam2.questions[qIndex];

                                    Console.Write("New question text: ");
                                    q2.QuestionTitle = Console.ReadLine();

                                    Console.Write("New marks: ");
                                    decimal newMarks = decimal.Parse(Console.ReadLine());

                                    decimal curMarks = exam2.CurrentMark;
                                    if ((curMarks - q2.Marks + newMarks) > course3.MaxDegree)
                                    {
                                        Console.WriteLine("Total marks would exceed Maximum Course Degree. Edit cancelled.");
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine();
                                        break;
                                    }

                                    exam2.CurrentMark -= q2.Marks;
                                    exam2.CurrentMark += newMarks;
                                    q2.Marks = newMarks;

                                    Console.WriteLine("Question updated successfully!" +
                                        "\nPress Enter...");
                                    Console.ReadLine();
                                    break;
                                case "4":

                                    Console.Clear();
                                    Console.WriteLine("--- Remove Question ---");

                                    Console.WriteLine("Select course:");
                                    ListCourses();
                                    Console.Write("Enter course number: ");
                                    int courseIndex4 = int.Parse(Console.ReadLine()) - 1;
                                    Course course4 = courses[courseIndex4];

                                    Console.WriteLine("Available Exams: ");
                                    for (int i = 0; i < course4.exams.Count && course4.exams[i].Started == false; i++)
                                        Console.WriteLine($"{i + 1}. {course4.exams[i].ExamType} - {course4.exams[i].Year}");

                                    Console.Write("Enter Exam Number: ");
                                    int Examindex3 = int.Parse(Console.ReadLine()) - 1;

                                    Exam exam3 = course4.exams[Examindex3];

                                    for (int i = 0; i < exam3.questions.Count; i++)
                                        Console.WriteLine($"{i + 1}. {exam3.questions[i].QuestionTitle} ({exam3.questions[i].Marks} marks)");

                                    Console.Write("Select question to remove: ");
                                    int qindex = int.Parse(Console.ReadLine()) - 1;

                                    exam3.questions.RemoveAt(qindex);

                                    Console.WriteLine("Question removed successfully! " +
                                        "\nPress Enter...");
                                    Console.ReadLine();
                                    break;
                                case "5":

                                    Console.Clear();
                                    Console.WriteLine("--- Start Exam ---");

                                    Console.WriteLine("Select course:");
                                    ListCourses();
                                    Console.Write("Enter course number: ");
                                    int courseIndex5 = int.Parse(Console.ReadLine()) - 1;
                                    Course course5 = courses[courseIndex5];

                                    Console.WriteLine("Available Exams: ");
                                    for (int i = 0; i < course5.exams.Count && course5.exams[i].Started == false; i++)
                                        Console.WriteLine($"{i + 1}. {course5.exams[i].ExamType} - {course5.exams[i].Year}");

                                    Console.Write("Enter Exam Number: ");
                                    int Examindex4 = int.Parse(Console.ReadLine()) - 1;

                                    course5.exams[Examindex4].Started = true;

                                    Console.WriteLine("Exam Has Started. No further edits allowed.");
                                    Console.WriteLine("Press Enter...");
                                    Console.ReadLine();
                                    break;
                                case "6":

                                    Console.Clear();
                                    Console.WriteLine("--- Duplicate Exam ---");

                                    Console.WriteLine("Select Exam: ");
                                    for (int i = 0; i < exams.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1} - {exams[i].CourseName} - {exams[i].ExamType} - {exams[i].Year}");
                                    }

                                    Console.Write("Enter exam number: ");
                                    int examidx = int.Parse(Console.ReadLine()) - 1;
                                    Exam examm = exams[examidx];

                                    Console.WriteLine("Select target course to copy to: ");
                                    ListCourses();
                                    Console.Write("Enter course number: ");
                                    int newCourseIndex = int.Parse(Console.ReadLine()) - 1;
                                    Course newCourse = courses[newCourseIndex];

                                    Exam newExam = new Exam(newCourse.Title, examm.ExamType);

                                    newExam.CurrentMark = examm.CurrentMark;

                                    foreach (var question in examm.questions)
                                    {
                                        newExam.AddQuestion(CloneQuestion(question));
                                    }

                                    //newExam.questions = examm.questions.Select(CloneQuestion).ToList();

                                    exams.Add(newExam);
                                    newCourse.AddExam(newExam);
                                    Console.WriteLine("Exam duplicated Successfully!" +
                                        "\nPress Enter...");
                                    Console.ReadLine();
                                    break;
                                case "7":

                                    Console.Clear();
                                    Console.WriteLine("--- View Exams ---");

                                    Console.WriteLine("Select course: ");
                                    ListCourses();
                                    int courseIndex6 = int.Parse(Console.ReadLine()) - 1;
                                    Course course6 = courses[courseIndex6];

                                    for (int i = 0; i < course6.exams.Count; i++)
                                    {
                                        Exam e = course6.exams[i];
                                        Console.WriteLine($"{i + 1}. (Course: {e.CourseName})\n" +
                                            $"Type: {e.ExamType}\n" +
                                            $"Questions: {e.questions.Count}\n" +
                                            $"Total Marks: {e.CurrentMark}\n" +
                                            $"Started: {e.Started}");
                                        Console.WriteLine("-------------------------------------");
                                    }
                                    Console.WriteLine("Press Enter...");
                                    Console.ReadLine();
                                    break;
                                case "0":
                                    break;
                                default:
                                    Console.WriteLine("Invalid Option, Please Enter A Valid One!\n" +
                                    "Press Enter...");
                                    Console.ReadLine();
                                    break;
                            }
                        }
                        break;
                    case "5":

                        Console.Clear();
                        Console.WriteLine("--- Take Exam ---");
                        Console.Write("Enter Student ID: ");
                        int sid = int.Parse(Console.ReadLine());

                        foreach (var s in students)
                        {
                            if (s.ID == sid)
                            {
                                Console.WriteLine("Select Course: ");
                                for (int i = 0; i < s.courses.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1} - {s.courses[i].Title}");
                                }

                                Console.Write("Enter course number: ");
                                int courseidx = int.Parse(Console.ReadLine()) - 1;
                                Course coursee = s.courses[courseidx];

                                Console.WriteLine("Select Exam: ");
                                for (int i = 0; i < coursee.exams.Count && coursee.exams[i].Started == false; i++)
                                {
                                    Console.WriteLine($"{i + 1} - {coursee.exams[i].ExamType} - {coursee.exams[i].Year}");
                                }

                                Console.Write("Enter exam number: ");
                                int examidx = int.Parse(Console.ReadLine()) - 1;
                                Exam examm = coursee.exams[examidx];

                                if (s.GetExamScores(examm) > 0)
                                {
                                    Console.WriteLine("Student has Entered Exam Before");
                                    Console.WriteLine("Press Enter...");
                                    Console.ReadLine();
                                    break;
                                }

                                decimal score = 0;
                                foreach (var question in examm.questions)
                                {
                                    Console.WriteLine($"\nQ: {question.QuestionTitle} (Marks: {question.Marks})");

                                    if (question is MultipleChoice mcq)
                                    {
                                        for (int i = 0; i < mcq.Options.Count; i++)
                                        {
                                            Console.WriteLine($"{i + 1}: {mcq.Options[i]}");
                                        }
                                    }

                                    Console.Write("Your answer in text format: ");
                                    string ans = Console.ReadLine();

                                    if (question.CheckAnswer(ans))
                                        score += question.Marks;
                                }

                                s.editExamScore(examm, score);

                                Console.WriteLine($"\nExam Completed! Your score is: {score}");
                                Console.WriteLine("Press Enter...");
                                Console.ReadLine();

                                break;
                            }
                            else
                            {
                                Console.WriteLine("Student Not Found!");
                                break;
                            }
                        }
                        break;
                    case "6":
                        bool work = true;
                        while (work)
                        {
                            Console.Clear();
                            Console.WriteLine("Reports and Compare Students");
                            Console.WriteLine("1. Reports");
                            Console.WriteLine("2. Compare Students");
                            Console.WriteLine("0. Back To Main Menu");
                            Console.Write("Enter your choice: ");
                            string ch = Console.ReadLine();

                            if (ch == "0") break;

                            switch (ch)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("--- Report ---");
                                    Console.Write("Enter Student ID: ");
                                    int id = Convert.ToInt32(Console.ReadLine());

                                    foreach (Student st in students)
                                    {
                                        if (st.ID == id)
                                        {
                                            Console.WriteLine("Student Found!");
                                            st.StudentReport();
                                            break;
                                        }
                                    }

                                    Console.WriteLine("Student Not Found!");
                                    Console.WriteLine("Press Enter...");
                                    Console.ReadLine();
                                    break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("--- Comparison ---");
                                    Console.Write("Enter Exam ID: ");
                                    int examID = Convert.ToInt32(Console.ReadLine());

                                    Console.Write("Enter first student ID: ");
                                    int studentid1 = Convert.ToInt32(Console.ReadLine());

                                    Console.Write("Enter second student ID: ");
                                    int studentid2 = Convert.ToInt32(Console.ReadLine());

                                    Exam exam = null;
                                    Student FirstStudent = null;
                                    Student SecondStudent = null;

                                    foreach (Exam ex in exams)
                                    {
                                        if (ex.ID == examID)
                                        {
                                            exam = ex;
                                            break;
                                        }
                                    }

                                    foreach (Student stu in students)
                                    {
                                        if (stu.ID == studentid1)
                                        {
                                            FirstStudent = stu;
                                        }
                                        if (stu.ID == studentid2)
                                        {
                                            SecondStudent = stu;
                                        }
                                    }

                                    if (exam == null)
                                    {
                                        Console.WriteLine("Exam Not Found!");
                                        break;
                                    }
                                    else if (FirstStudent == null)
                                    {
                                        Console.WriteLine("First Student is Not Found!");
                                        break;
                                    }
                                    else if (SecondStudent == null)
                                    {
                                        Console.WriteLine("Second Student is not Found!");
                                        break;
                                    }

                                    decimal FirstStudentscore = FirstStudent.GetExamScores(exam);
                                    decimal SecondStudentscore = SecondStudent.GetExamScores(exam);

                                    Console.WriteLine($"{FirstStudent.Name}: {FirstStudentscore}\n" +
                                        $"{SecondStudent.Name}: {SecondStudentscore}");
                                    Console.WriteLine(FirstStudentscore == SecondStudentscore ? "Scores are Equal" : $"{(FirstStudentscore > SecondStudentscore ? FirstStudent.Name : SecondStudent.Name)} Performed Better");
                                    Console.WriteLine("Press Enter...");
                                    Console.ReadLine();
                                    break;
                                case "0":
                                    break;
                                default:
                                    Console.WriteLine("Invalid Option, Please Enter A Valid One!\n" +
                                        "Press Enter...");
                                    Console.ReadLine();
                                    break;
                            }
                        }
                        break;
                    case "0":
                        work = false;
                        break;
                    default: 
                        Console.WriteLine("Invalid Option, Please Enter A Valid One!\n" +
                            "Press Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        
        static void ListCourses()
        {
            for (int i = 0; i < courses.Count; i++)
                Console.WriteLine($"{i + 1}. {courses[i].Title}");
        }

        static Question CloneQuestion(Question q)
        {
            if (q is MultipleChoice mcq)
                return new MultipleChoice(mcq.QuestionTitle, mcq.Marks, mcq.Answer, mcq.Options);


            if (q is TrueOrFalse tf)
                return new TrueOrFalse(tf.QuestionTitle, tf.Marks, tf.Answer);
                
            if (q is TextQuestion text)
                return new TextQuestion(text.QuestionTitle, text.Marks, text.Answer);

            return null;
        }

    }
}
