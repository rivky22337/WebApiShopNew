﻿using AutoMapper;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using AutoMapper;
using DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;
        IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }
        //GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            IEnumerable<Category> categories = await _categoryService.GetCategoriesAsync();
            IEnumerable<CategoryDTO> categoryDTOs = _mapper.Map<IEnumerable<Category>,IEnumerable<CategoryDTO>>(categories);
            if (categoryDTOs != null)
            {
                return Ok(categoryDTOs);
            }
            return BadRequest();
        }
    }
}
