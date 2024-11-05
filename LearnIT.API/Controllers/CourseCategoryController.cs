using LearnIT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController (ICourseCategoryService courseCategoryService) : ControllerBase
    {
        private readonly ICourseCategoryService _courseCategoryService = courseCategoryService;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _courseCategoryService.GetByIdAsync(id).ConfigureAwait(false);

            if (category == null) 
                return NotFound();
            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _courseCategoryService.GetCategoriesAsync().ConfigureAwait(false);
            return Ok(categories);
        }
    }
}
