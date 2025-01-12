using Microsoft.AspNetCore.Mvc;
using Services;
using Entities.Models;
using DTO;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper=mapper;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListProductDTO>>> Get([FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds, [FromQuery] string? desc)
        {
            IEnumerable<Product> products =await _productService.GetProductsAsync(minPrice, maxPrice, categoryIds, desc);
            IEnumerable<ListProductDTO> productDTOs = _mapper.Map<IEnumerable<Product>, IEnumerable<ListProductDTO>>(products);
            if (productDTOs != null)
            {
                return Ok(productDTOs);
            }
            return BadRequest();
        }
    }
}
