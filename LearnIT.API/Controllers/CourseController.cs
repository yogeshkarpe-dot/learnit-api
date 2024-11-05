using LearnIT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseService courseService) : ControllerBase
    {
        private readonly ICourseService _courseService = courseService;

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync().ConfigureAwait(false);
            return Ok(courses);
        }

        [HttpGet("Category/{CategoryId}")]
        public async Task<IActionResult> GetAllCoursesByCategoryId([FromRoute] int CategoryId)
        {
            var courses = await _courseService.GetAllCoursesAsync(CategoryId).ConfigureAwait(false);
            return Ok(courses);
        }

        [HttpGet("Detail/{CourseId}")]
        public async Task<IActionResult> GetCourseDetailsAsync(int CourseId)
        {
            var courseDetail = await _courseService.GetCourseDetailAsync(CourseId).ConfigureAwait(false);

            if(courseDetail == null) 
                return NotFound();

            return Ok(courseDetail);
        }
    }
}
