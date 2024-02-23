using System.IO;
using SQLite;
using WGUAPP.Models;

namespace WGUAPP
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection _db;

        static async Task Init()
        {
            if (_db != null)
            {
                return;
            }

            // get absolute path to the database file
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "DegreePlan.db");

            _db = new SQLiteAsyncConnection(dbPath);
            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Assessment>();
        }

        public static async Task CreateEvaluationData()
        {
            await Init();

            // Create a term
            Term term = new()
            {
                Title = "Spring 2024",
                StartDate = new DateTime(2024, 3, 1),
                EndDate = new DateTime(2024, 6, 30)
            };
            await _db.InsertAsync(term);

            // Create a course associated with the term
            Course course = new()
            {
                TermId = term.Id,
                Name = "Computer Science 101",
                StartDate = new DateTime(2024, 3, 15),
                EndDate = new DateTime(2024, 6, 15),
                Status = "In progress", // Set the course status
                InstructorName = "Anika Patel",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "anika.patel@strimeuniversity.edu",
                Notes = "This is an introductory course to computer science.",
                HasPA = true,
                HasOA = true,
            };
            await _db.InsertAsync(course);

            // Create assessments for the course
            Assessment performanceAssessment = new()
            {
                CourseId = course.Id,
                Name = "CSC 101 Coding test",
                Type = "Performance",
                StartDate = new DateTime(2024, 5, 1),
                EndDate = new DateTime(2024, 5, 15)
            };
            await _db.InsertAsync(performanceAssessment);

            Assessment objectiveAssessment = new()
            {
                CourseId = course.Id,
                Name = "CSC 101 Objective Assessment",
                Type = "Objective",
                StartDate = new DateTime(2024, 6, 1),
                EndDate = new DateTime(2024, 6, 10)
            };
            await _db.InsertAsync(objectiveAssessment);
        }

        #region Term Methods

        public static async Task<IEnumerable<Term>> GetTerms()
        {
            await Init();
            var terms = await _db.Table<Term>().ToListAsync();
            return terms;
        }

        public static async Task AddTerm(Term term)
        {
            await Init();
            await _db.InsertAsync(term);
        }

        public static async Task RemoveTerm(int Id)
        {
            await Init();
            await _db.DeleteAsync<Term>(Id);
        }

        public static async Task UpdateTerm(int Id, Term updatedTerm)
        {
            await Init();
            var currentTerm = await _db.Table<Term>().Where(t => t.Id == Id).FirstOrDefaultAsync();

            if (currentTerm != null)
            {
                currentTerm.Title = updatedTerm.Title;
                currentTerm.StartDate = updatedTerm.StartDate;
                currentTerm.EndDate = updatedTerm.EndDate;

                await _db.UpdateAsync(currentTerm);
            }
        }

        #endregion

        #region Course Methods

        public static async Task<IEnumerable<Course>> GetCourses(int termId)
        {
            await Init();
            var courses = await _db.Table<Course>().Where(c => c.TermId == termId).ToListAsync();
            return courses;
        }

        public static async Task<Course> RefreshCourse(int Id)
        {
            await Init();
            var course = await _db.Table<Course>().Where(c => c.Id == Id).FirstOrDefaultAsync();
            if (course == null)
            {
                return new Course();
            }
            return course;
        }

        public static async Task AddCourse(Course course)
        {
            await Init();
            await _db.InsertAsync(course);
        }

        public static async Task RemoveCourse(int Id)
        {
            await Init();
            await _db.DeleteAsync<Course>(Id);
        }

        public static async Task UpdateCourse(int Id, Course updatedCourse)
        {
            await Init();
            var currentCourse = await _db.Table<Course>().Where(c => c.Id == Id).FirstOrDefaultAsync();

            if (currentCourse != null)
            {
                currentCourse.Name = updatedCourse.Name;
                currentCourse.StartDate = updatedCourse.StartDate;
                currentCourse.EndDate = updatedCourse.EndDate;
                currentCourse.Status = updatedCourse.Status;
                currentCourse.InstructorName = updatedCourse.InstructorName;
                currentCourse.InstructorPhone = updatedCourse.InstructorPhone;
                currentCourse.InstructorEmail = updatedCourse.InstructorEmail;
                currentCourse.NotificationsEnabled = updatedCourse.NotificationsEnabled;

                await _db.UpdateAsync(currentCourse);
            }
        }

        public static async Task UpdateCourseNotes(int Id, string courseNotes)
        {
            await Init();
            var course = await _db.Table<Course>().Where(c => c.Id == Id).FirstOrDefaultAsync();

            if (course != null)
            {
                course.Notes = courseNotes;
                await _db.UpdateAsync(course);
            }
        }

        public static async Task UpdateCourseHasAssessment(int Id, bool hasPA, bool hasOA)
        {
            await Init();
            var course = await _db.Table<Course>().Where(c => c.Id == Id).FirstOrDefaultAsync();

            if (course != null)
            {
                course.HasPA = hasPA;
                course.HasOA = hasOA;
                await _db.UpdateAsync(course);
            }
        }

        #endregion

        #region Assessment methods

        public static async Task<Assessment> GetAssessment(int courseId, string assessmentType)
        {
            await Init();
            var assessment = await _db.Table<Assessment>().Where(a => a.CourseId == courseId && a.Type == assessmentType).FirstOrDefaultAsync();
            if (assessment == null)
            {
                return new Assessment();
            }
            return assessment;
        }

        public static async Task AddAssessment(Assessment assessment)
        {
            await Init();
            await _db.InsertAsync(assessment);
        }

        public static async Task UpdateAssessment(int Id, Assessment updatedAssessment)
        {
            await Init();
            var currentAssessment = await _db.Table<Assessment>().Where(a => a.Id == Id).FirstOrDefaultAsync();

            if (currentAssessment != null)
            {
                currentAssessment.Name = updatedAssessment.Name;
                currentAssessment.StartDate = updatedAssessment.StartDate;
                currentAssessment.EndDate = updatedAssessment.EndDate;
                currentAssessment.NotificationsEnabled = updatedAssessment.NotificationsEnabled;

                await _db.UpdateAsync(currentAssessment);
            }
        }

        public static async Task RemoveAssessment(int Id)
        {
            await Init();
            await _db.DeleteAsync<Assessment>(Id);
        }

        #endregion
    }
}
