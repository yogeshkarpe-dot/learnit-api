using LearnIT.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIT.Service
{
    public interface ICourseService
    {
        Task<CourseDetailModel> GetCourseDetailAsync(int CourseId);
        Task<List<CourseModel>> GetAllCoursesAsync(int? CategoryId = null);
    }
}
