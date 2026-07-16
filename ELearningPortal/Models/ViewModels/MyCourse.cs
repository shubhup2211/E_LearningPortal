namespace ELearningPortal.Models.ViewModels
{
    public class MyCourse
    {
    }
    // Poora page load karne ke liye top-level ViewModel
    public class MyCourseIndexViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;

        public int SubCourseId { get; set; }
        public string SubCourseName { get; set; } = string.Empty;
        public int TotalLessons { get; set; }


        public int CompletedLessons { get; set; }
        public int ProgressPercentage => TotalLessons > 0
            ? (int)Math.Round((double)CompletedLessons / TotalLessons * 100)
            : 0;



        // Right side "Course Content" list ke liye
        public List<LessonNodeViewModel> Lessons { get; set; } = new();

        // Left side video player ke liye currently selected lesson
        public LessonDetailViewModel? CurrentLesson { get; set; }

        public List<SubCourseNodeViewModel> SubCourses { get; set; } = new(); // ===== NAYA =====
    }

    // Sidebar list mein har lesson ka ek "node" (jaise screenshot mein 3.Operator, 4.Decision Making etc.)
    public class LessonNodeViewModel
    {
        public int LessonId { get; set; }
        public int DisplayOrder { get; set; }
        public string LessonTitle { get; set; } = string.Empty;
        public string? Duration { get; set; }

        public bool IsUnlocked { get; set; }   // click karne layak hai ya nahi
        public bool IsCompleted { get; set; }  // green tick dikhane ke liye
        public bool IsActive { get; set; }     // currently playing hai
    }

    // Video player + niche wala "Lesson: Variables & Data Types" section ke liye
    public class LessonDetailViewModel
    {
        public int LessonId { get; set; }
        public string LessonTitle { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;   // YouTube URL/ID
        public string? Duration { get; set; }

        public bool IsUnlocked { get; set; }
        public bool VideoWatched { get; set; }       // Status == Completed
        public bool McqCompleted { get; set; }
        public bool AssignmentSubmitted { get; set; }

        public bool ShowMcqButton => VideoWatched && !McqCompleted;
        public bool ShowAssignmentSection => McqCompleted;
    }


    // MCQ Modal ke liye ek question + uske options
    public class McqQuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public List<McqOptionViewModel> Options { get; set; } = new();
    }

    public class McqOptionViewModel
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        // IsCorrect yahan NAHI bhejenge frontend ko (security) — sirf backend pe check hoga
    }



    // Jab user MCQ submit karega, AJAX POST body isi shape mein aayega
    public class McqSubmitViewModel
    {
        public int LessonId { get; set; }
        public List<McqAnswerViewModel> Answers { get; set; } = new();
    }

    public class McqAnswerViewModel
    {
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }
    }


    // MCQ submit ke baad AJAX response isi shape mein jayega
    public class McqResultViewModel
    {
        public bool IsPassed { get; set; }
        public int Score { get; set; }
        public int TotalMarks { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool AssignmentUnlocked { get; set; }
    }


    public class SubCourseNodeViewModel
    {
        public int SubCourseId { get; set; }
        public string SubCourseName { get; set; } = string.Empty;
        public int TotalLessons { get; set; }
        public int CompletedLessons { get; set; }
        public int ProgressPercentage => TotalLessons > 0
            ? (int)Math.Round((double)CompletedLessons / TotalLessons * 100)
            : 0;

        public bool IsPurchased { get; set; }  // UserSubCourse table check
        public bool IsActive { get; set; }     // abhi kaunsa SubCourse khula hai
    }
}
