using LearnIT.Core.Models;
using LearnIT.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIT.Service.Impl
{
    public class CourseCategoryService (ICourseCategoryRepository courseCategoryRepository) : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository = courseCategoryRepository;
        public async Task<CourseCategoryModel?> GetByIdAsync(int id)
        {
            var category = await _courseCategoryRepository.GetByIdAsync(id);

            if(category == null)
                return null;

            return new CourseCategoryModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
            };
        }

        public async Task<List<CourseCategoryModel>> GetCategoriesAsync()
        {
            var categories = await _courseCategoryRepository.GetCourseCategoriesAsync();

            return categories.Select(c => new CourseCategoryModel()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description,
            }).ToList();
        }
    }
}
