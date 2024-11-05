using LearnIT.Core.Models;
using LearnIT.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIT.Service.Impl
{
    public class CourseService(ICourseRepository courseRepository) : ICourseService
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        public Task<List<CourseModel>> GetAllCoursesAsync(int? CategoryId = null) => _courseRepository.GetAllCoursesAsync(CategoryId);

        public Task<CourseDetailModel> GetCourseDetailAsync(int CourseId) => _courseRepository.GetCourseDetailAsync(CourseId);
    }
}
