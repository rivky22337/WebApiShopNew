using AutoMapper;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;
        IMapper _mapper;
        private ILogger<CategoryController> _logger;
        private readonly IMemoryCache _memoryCache;
        
        private readonly string cacheKey = "categoriesCache";
        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _logger = logger;
            _memoryCache = memoryCache;
        }
        //GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<CategoryDTO> categoryDTOs)) // הוספנו את התנאי הזה
            {
                _logger.LogInformation("Cache miss for categories. Fetching from service."); // הוספנו את השורה הזו
                IEnumerable<Category> categories = await _categoryService.GetCategoriesAsync();
                categoryDTOs = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);

                var cacheEntryOptions = new MemoryCacheEntryOptions // הוספנו את החלק הזה
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Cache for 30 minutes
                };

                _memoryCache.Set(cacheKey, categoryDTOs, cacheEntryOptions); // הוספנו את השורה הזו
            }
            else
            {
                _logger.LogInformation("Cache hit for categories."); // הוספנו את השורה הזו
            }

            if (categoryDTOs != null)
            {
                _logger.LogInformation("Category controller: get");
                return Ok(categoryDTOs);
            }
            _logger.LogError("Error: Category controller: get");
            return BadRequest();
        }



    }
}
