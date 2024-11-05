using Azure;
using LearnIT.Core.Models;
using LearnIT.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIT.Data.Repositories.Impl
{
    public class CourseRepository(LearnItDbContext db) : ICourseRepository
    {
        private readonly LearnItDbContext _db = db;
        public async Task<List<CourseModel>> GetAllCoursesAsync(int? categoryId = null)
        {
            var query = _db.Courses.Include(c => c.Category).AsQueryable();

            if (categoryId.HasValue )
                query = query.Where(c => c.CategoryId == categoryId.Value);

            var courses = await query
                .Select(c => new CourseModel
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    Duration = c.Duration,
                    CourseType = c.CourseType,
                    Price = c.Price,
                    SeatsAvailable = c.SeatsAvailable,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    InstructorId = c.InstructorId,
                    CategoryId = c.CategoryId,
                    Category = new CourseCategoryModel
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.CategoryName,
                        Description = c.Category.Description,
                    },
                    UserRating = new UserRatingModel
                    {
                        CourseId = c.CourseId,
                        AverageRating = c.Reviews.Any() ? Convert.ToDecimal(c.Reviews.Average(r => r.Rating)) : 0,
                        TotalRating = c.Reviews.Count
                    }
                }).ToListAsync();

            return courses;
        }

        public async Task<CourseDetailModel> GetCourseDetailAsync(int CourseId)
        {
            var course = await _db.Courses
                .Include(c => c.Category)
                .Include(c => c.Reviews)
                .Include(c => c.SessionDetails)
                .Where(c => c.CourseId == CourseId)
                .Select(c => new CourseDetailModel
                {
                    CourseId = c.CourseId,
                    CategoryId = c.CategoryId,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    Duration = c.Duration,
                    CourseType = c.CourseType,
                    SeatsAvailable = c.SeatsAvailable,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    InstructorId = c.InstructorId,
                    Category = new CourseCategoryModel
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.CategoryName,
                        Description = c.Category.Description
                    },
                    Reviews = c.Reviews.Select(r => new UserReviewModel
                    {
                        CourseId = r.CourseId,
                        Comments = r.Comments,
                        Rating = r.Rating,
                        ReviewDate = r.ReviewDate,
                        UserName = r.User.DisplayName
                    }).OrderByDescending(o => o.Rating).Take(10).ToList(),
                    SessionDetails = c.SessionDetails.Select(sd => new SessionDetailModel
                    {
                        CourseId = sd.CourseId,
                        Description = sd.Description,
                        SessionId = sd.SessionId,
                        Title = sd.Title,
                        VideoUrl = sd.VideoUrl,
                        VideoOrder = sd.VideoOrder
                    }).OrderBy(o => o.VideoOrder).ToList(),
                    UserRating = new UserRatingModel
                    {
                        CourseId = c.CourseId,
                        AverageRating = c.Reviews.Any() ? Convert.ToDecimal(c.Reviews.Average(r => r.Rating)) : 0,
                        TotalRating = c.Reviews.Count
                    }
                })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return course;
        }
    }
}
