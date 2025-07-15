using Digital_Core_Layer.Services.Abstract;
using Digital_Infrastructure_Layer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digital_API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainCategoryController : ControllerBase
    {

        private readonly IMainCategoryService mainCategoryService;

        public MainCategoryController(IMainCategoryService mainCategoryService)
        {
            this.mainCategoryService = mainCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MainCategoryDTO mainCategoryDTO)
        {
            var result = await mainCategoryService.CreateMainCategory(mainCategoryDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mainCategoryService.GetMainCategoryAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
