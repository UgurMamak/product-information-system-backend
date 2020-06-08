using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Dtos.Product;
using Application.Entities.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private IProductService _productService;
        private readonly IWebHostEnvironment _environment;
        private IProductImageService _productImageService;
        public ProductController(IProductService productService, IWebHostEnvironment environment,IProductImageService productImageService)
        {
            _productService = productService;
            _environment = environment;
            _productImageService = productImageService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] ProductCreateDto productCreateDto)
        {


            var entity = await _productService.Add(productCreateDto);
            if (entity.Success)
            {

                var resimler = Path.Combine(_environment.WebRootPath, "productImage");//dizin bilgisi
                List<string> images = new List<string>();
                foreach (var item in productCreateDto.ProductImages)
                {
                    if (item.Image.Length > 0)
                    {
                        string imageName = $"{Guid.NewGuid().ToString()}.jpg";//Db ye kaydedilecek olan resimlerin ismi
                        using (var fileStream = new FileStream(Path.Combine(resimler, imageName), FileMode.Create))
                        {
                            images.Add(imageName);
                            await item.Image.CopyToAsync(fileStream);
                        }
                    }
                }

              var saveImage= await _productImageService.Add(images,entity.Data.Id);
                if(saveImage.Success)
                {
                    return Ok(entity.Data);
                }
                return BadRequest("Resimler yüklenirken hata oluştu");
            }


            return BadRequest();

        }

    }
}