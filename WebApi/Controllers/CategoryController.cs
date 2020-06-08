using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Dtos.Category;
using Application.Entities.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Category category)
        {
            var categoryExists = await _categoryService.CategoryExists(category.CategoryName);
            if (!categoryExists.Success)
            {
                return BadRequest(categoryExists.Message);
            }

            var result = await _categoryService.Add(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var entity = await _categoryService.GetList();          
            if(entity.Success)
            {
                return Ok(entity.Data);           
            }
            return BadRequest();
        }

    }
}