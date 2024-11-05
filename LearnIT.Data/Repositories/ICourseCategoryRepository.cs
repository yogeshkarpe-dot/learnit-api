using LearnIT.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIT.Data.Repositories
{
    public interface ICourseCategoryRepository
    {
        Task<CourseCategory?> GetByIdAsync(int id);
        Task<List<CourseCategory>> GetCourseCategoriesAsync();
    }
}
