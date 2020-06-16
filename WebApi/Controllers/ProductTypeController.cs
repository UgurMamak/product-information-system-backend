using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private IProductTypeService _productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpPost("add")]//++++
        public async Task<IActionResult> Add(ProductType productType)
        {
            var typeExists = await _productTypeService.TypeExists(productType.ProductTypeName);
            if (!typeExists.Success)
            {
                return BadRequest(typeExists.Message);
            }

            var result = await _productTypeService.Add(productType);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var entity = await _productTypeService.GetList();
            
            if (entity.Success)
            {
                return Ok(entity.Data);
            }
            return BadRequest();
        }

         
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(ProductType productType)
        {
            var entity = await _productTypeService.Delete(productType);
            if (entity.Success)
            {
                return Ok(entity.Success);
            }
            return BadRequest();
        }

    }
}