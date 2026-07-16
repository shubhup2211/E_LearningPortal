using ELearning.Data;
using ELearning.Enums;
using ELearning.Models.Courses;
using ELearning.Models.Learning;
using ELearningPortal.Enums;
using ELearningPortal.Interfaces.IUser;
using ELearningPortal.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ELearningPortal.Services.User
{
    public class MyCourseService : IMyCourseService
    {
        private readonly AppDbContext db;
        private const int PassPercentage = 100; // "Agar All MCQ correct" -> 100% required

        public MyCourseService(AppDbContext db)
        {
            this.db = db;
        }

        // ================= COURSE CONTENT (Sidebar + First Load) =================
        public async Task<MyCourseIndexViewModel> GetCourseContentAsync(int userId, int subCourseId, int? selectedLessonId)
        {
            var subCourse = await db.SubCourses
                .Include(s => s.Course)
                .Include(s => s.Lessons.Where(l => l.Status == Status.Active))
                .FirstOrDefaultAsync(s => s.SubCourseId == subCourseId);

            if (subCourse == null)
                throw new Exception("SubCourse not found");

            // ===== NAYA: Isi Course ke saare SubCourses uthao (tabs ke liye) =====
            var siblingSubCourses = await db.SubCourses
                .Where(sc => sc.CourseId == subCourse.CourseId && sc.Status == Status.Active)
                .OrderBy(sc => sc.DisplayOrder)
                .ToListAsync();

            var siblingIds = siblingSubCourses.Select(sc => sc.SubCourseId).ToList();

            // User ne kaunse SubCourses purchase kiye hain
            var purchasedIds = await db.UserSubCourses
                .Where(us => us.UserId == userId
                             && siblingIds.Contains(us.SubCourseId)
                             && us.Status == PurchaseStatus.Active)
                .Select(us => us.SubCourseId)
                .ToListAsync();

            var subCourseVmList = new List<SubCourseNodeViewModel>();

            foreach (var sc in siblingSubCourses)
            {
                var lessonsOfThisSc = await db.Lessons
                    .Where(l => l.SubCourseId == sc.SubCourseId && l.Status == Status.Active)
                    .Select(l => l.LessonId)
                    .ToListAsync();

                int completedCountForSc = await db.LessonProgresses
                    .Where(lp => lp.UserId == userId
                                 && lessonsOfThisSc.Contains(lp.LessonId)
                                 && lp.Status == LessonStatus.Completed
                                 && lp.MCQCompleted == true
                                 && lp.AssignmentSubmitted == true)
                    .CountAsync();

                subCourseVmList.Add(new SubCourseNodeViewModel
                {
                    SubCourseId = sc.SubCourseId,
                    SubCourseName = sc.SubCourseName,
                    TotalLessons = lessonsOfThisSc.Count,
                    CompletedLessons = completedCountForSc,
                    IsPurchased = purchasedIds.Contains(sc.SubCourseId),
                    IsActive = sc.SubCourseId == subCourseId
                });
            }
            // ===== NAYA END =====

            var orderedLessons = subCourse.Lessons.OrderBy(l => l.DisplayOrder).ToList();

            // Is user ki saari LessonProgress rows ek baar mein utha lo (perf ke liye)
            var lessonIds = orderedLessons.Select(l => l.LessonId).ToList();
            var progressList = await db.LessonProgresses
                .Where(lp => lp.UserId == userId && lessonIds.Contains(lp.LessonId))
                .ToListAsync();

            var vm = new MyCourseIndexViewModel
            {
                CourseId = subCourse.Course.CourseId,
                CourseName = subCourse.Course.CourseName,
                SubCourseId = subCourse.SubCourseId,
                SubCourseName = subCourse.SubCourseName,
                TotalLessons = orderedLessons.Count,
                SubCourses = subCourseVmList
            };

            int? firstUnlockedLessonId = null;
            int completedCount = 0;

            for (int i = 0; i < orderedLessons.Count; i++)
            {
                var lesson = orderedLessons[i];
                var progress = progressList.FirstOrDefault(p => p.LessonId == lesson.LessonId);

                bool isUnlocked = IsLessonUnlocked(i, orderedLessons, progressList);
                bool isCompleted = progress?.Status == LessonStatus.Completed
                                    && progress.MCQCompleted == true
                                    && progress.AssignmentSubmitted == true;

                if (isCompleted) completedCount++;

                if (isUnlocked && firstUnlockedLessonId == null)
                    firstUnlockedLessonId = lesson.LessonId;

                vm.Lessons.Add(new LessonNodeViewModel
                {
                    LessonId = lesson.LessonId,
                    DisplayOrder = lesson.DisplayOrder,
                    LessonTitle = lesson.LessonTitle,
                    Duration = lesson.Duration,
                    IsUnlocked = isUnlocked,
                    IsCompleted = isCompleted
                });
            }

            vm.CompletedLessons = completedCount;

            // Kaunsa lesson khulega: jo select kiya gaya (agar unlocked hai), warna pehla unlocked lesson
            int lessonToOpen = selectedLessonId.HasValue &&
                                vm.Lessons.Any(l => l.LessonId == selectedLessonId && l.IsUnlocked)
                                ? selectedLessonId.Value
                                : (firstUnlockedLessonId ?? orderedLessons.First().LessonId);

            foreach (var node in vm.Lessons)
                node.IsActive = node.LessonId == lessonToOpen;

            vm.CurrentLesson = await GetLessonDetailAsync(userId, lessonToOpen);

            return vm;
        }

        // ================= SINGLE LESSON DETAIL (video player load) =================
        public async Task<LessonDetailViewModel?> GetLessonDetailAsync(int userId, int lessonId)
        {
            var lesson = await db.Lessons
                .Include(l => l.SubCourse)
                .FirstOrDefaultAsync(l => l.LessonId == lessonId);

            if (lesson == null) return null;

            var allLessons = await db.Lessons
                .Where(l => l.SubCourseId == lesson.SubCourseId && l.Status == Status.Active)
                .OrderBy(l => l.DisplayOrder)
                .ToListAsync();

            var lessonIds = allLessons.Select(l => l.LessonId).ToList();
            var progressList = await db.LessonProgresses
                .Where(lp => lp.UserId == userId && lessonIds.Contains(lp.LessonId))
                .ToListAsync();

            int index = allLessons.FindIndex(l => l.LessonId == lessonId);
            bool isUnlocked = IsLessonUnlocked(index, allLessons, progressList);

            var myProgress = progressList.FirstOrDefault(p => p.LessonId == lessonId);

            return new LessonDetailViewModel
            {
                LessonId = lesson.LessonId,
                LessonTitle = lesson.LessonTitle,
                VideoUrl = lesson.VideoUrl,
                Duration = lesson.Duration,
                IsUnlocked = isUnlocked,
                VideoWatched = myProgress?.Status == LessonStatus.Completed || myProgress?.Status == LessonStatus.InProgress && myProgress.MCQCompleted == true,
                McqCompleted = myProgress?.MCQCompleted ?? false,
                AssignmentSubmitted = myProgress?.AssignmentSubmitted ?? false
            };
        }

        // ================= LOCKING LOGIC (core) =================
        // index = orderedLessons list mein current lesson ka position (0-based)
        private bool IsLessonUnlocked(int index, List<Lesson> orderedLessons, List<LessonProgress> progressList)
        {
            if (index == 0) return true; // Pehla lesson hamesha unlocked

            var prevLesson = orderedLessons[index - 1];
            var prevProgress = progressList.FirstOrDefault(p => p.LessonId == prevLesson.LessonId);

            if (prevProgress == null) return false;

            // Next lesson tabhi khulega jab: previous video complete + MCQ pass + Assignment submit
            return prevProgress.Status == LessonStatus.Completed
                   && prevProgress.MCQCompleted == true
                   && prevProgress.AssignmentSubmitted == true;
        }

        // ================= VIDEO COMPLETE =================
        public async Task<bool> MarkVideoCompletedAsync(int userId, int lessonId)
        {
            var progress = await GetOrCreateProgressAsync(userId, lessonId);

            // Video dekh liya -> InProgress rakho (Completed sirf MCQ+Assignment ke baad milega)
            if (progress.Status == LessonStatus.Pending)
                progress.Status = LessonStatus.InProgress;

            await db.SaveChangesAsync();
            return true;
        }

        // ================= MCQ QUESTIONS FETCH =================
        public async Task<List<McqQuestionViewModel>> GetMcqQuestionsAsync(int lessonId)
        {
            var questions = await db.Questions
                .Include(q => q.QuestionOptions)
                .Where(q => q.LessonId == lessonId && q.Status == Status.Active)
                .OrderBy(q => q.QuestionId)
                .Take(5) // "5 MCQ" requirement
                .ToListAsync();

            return questions.Select(q => new McqQuestionViewModel
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                Options = q.QuestionOptions.Select(o => new McqOptionViewModel
                {
                    OptionId = o.OptionId,
                    OptionText = o.OptionText
                    // IsCorrect kabhi frontend ko nahi bhejenge
                }).ToList()
            }).ToList();
        }

        // ================= MCQ SUBMIT =================
        public async Task<McqResultViewModel> SubmitMcqAsync(int userId, McqSubmitViewModel model)
        {
            var questionIds = model.Answers.Select(a => a.QuestionId).ToList();
            var correctOptions = await db.QuestionOptions
                .Where(o => questionIds.Contains(o.QuestionId) && o.IsCorrect)
                .ToListAsync();

            int totalMarks = model.Answers.Count;
            int score = 0;

            foreach (var ans in model.Answers)
            {
                var correct = correctOptions.FirstOrDefault(o => o.QuestionId == ans.QuestionId);
                if (correct != null && correct.OptionId == ans.SelectedOptionId)
                    score++;
            }

            bool isPassed = totalMarks > 0 && (score * 100 / totalMarks) >= PassPercentage;

            // MCQResult table mein entry save karo (history ke liye)
            db.MCQResults.Add(new MCQResult
            {
                UserId = userId,
                LessonId = model.LessonId,
                Score = score,
                TotalMarks = totalMarks,
                IsPassed = isPassed,
                AttemptedAt = DateTime.Now
            });

            // LessonProgress update karo
            var progress = await GetOrCreateProgressAsync(userId, model.LessonId);
            if (isPassed)
            {
                progress.MCQCompleted = true;
            }

            await db.SaveChangesAsync();

            return new McqResultViewModel
            {
                IsPassed = isPassed,
                Score = score,
                TotalMarks = totalMarks,
                AssignmentUnlocked = isPassed,
                Message = isPassed
                    ? "MCQ Test Pass! Submit the Assignment."
                    : $"You Got {score}/{totalMarks} . Please Try Again — All 5 Should Correct."
            };
        }

        // ================= ASSIGNMENT SUBMIT =================
        public async Task<bool> SubmitAssignmentAsync(int userId, int lessonId, IFormFile file)
        {
            var progress = await GetOrCreateProgressAsync(userId, lessonId);

            // MCQ pass kiye bina assignment submit na hone do (backend-side safety check)
            if (progress.MCQCompleted != true)
                return false;

            // File save karna (abhi simple version — Step 6 mein FileUploadHelper se refactor karenge)
            string uploadsFolder = Path.Combine("wwwroot", "Uploads", "Assignments", userId.ToString(), lessonId.ToString());
            Directory.CreateDirectory(uploadsFolder);
            string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var assignment = await db.Assignments.FirstOrDefaultAsync(a => a.LessonId == lessonId);

            db.AssignmentSubmissions.Add(new AssignmentSubmission
            {
                AssignmentId = assignment?.AssignmentId ?? 0,
                UserId = userId,
                UploadedFile = $"/Uploads/Assignments/{userId}/{lessonId}/{uniqueFileName}",
                SubmittedDate = DateTime.Now,
                ReviewStatus = ApprovalStatus.Pending
            });

            // Assignment submit ho gaya + MCQ pass -> lesson officially Complete + next lesson unlock
            progress.AssignmentSubmitted = true;
            progress.Status = LessonStatus.Completed;
            progress.CompletedDate = DateTime.Now;

            await db.SaveChangesAsync();
            return true;
        }

        // ================= HELPER =================
        private async Task<LessonProgress> GetOrCreateProgressAsync(int userId, int lessonId)
        {
            var progress = await db.LessonProgresses
                .FirstOrDefaultAsync(lp => lp.UserId == userId && lp.LessonId == lessonId);

            if (progress == null)
            {
                progress = new LessonProgress
                {
                    UserId = userId,
                    LessonId = lessonId,
                    Status = LessonStatus.Pending,
                    MCQCompleted = false,
                    AssignmentSubmitted = false
                };
                db.LessonProgresses.Add(progress);
                await db.SaveChangesAsync(); // taaki ProgressId mil jaye
            }

            return progress;
        }
    }
}