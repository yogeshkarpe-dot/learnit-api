using LearnIT.Core.Entities;
using LearnIT.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIT.Data.Repositories.Impl
{
    public class CourseCategoryRepository(LearnItDbContext db) : ICourseCategoryRepository
    {
        private readonly LearnItDbContext _db = db;
        public Task<CourseCategory?> GetByIdAsync(int id) => _db.CourseCategories.FindAsync(id).AsTask();

        public Task<List<CourseCategory>> GetCourseCategoriesAsync() => _db.CourseCategories.ToListAsync();
    }
}
