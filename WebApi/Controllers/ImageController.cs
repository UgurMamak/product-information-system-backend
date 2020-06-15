using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Dtos.ProductImage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        private readonly IWebHostEnvironment _environment;
        private IProductImageService _productImageService;
        public ImageController(IWebHostEnvironment environment,IProductImageService productImageService)
        {
            _environment = environment;
            _productImageService = productImageService;
        }


        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromForm] ImageDeleteDto imageDeleteDto)
        {
            /*
          var entity= await _productImageService.Delete(imageDeleteDto);
            if(entity.Success)
            {
                var resimler = Path.Combine(_environment.WebRootPath, "productImage");//dizin bilgisi
                foreach (var item in imageDeleteDto.imageLists)
                {
                    System.IO.File.Delete(resimler + "\\" + item.ImageName);
                }
                return Ok(entity.Message);
            }*/
            return BadRequest();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] ImageCreateDto imageCreateDto)
        {
            var resimler = Path.Combine(_environment.WebRootPath, "productImage");//dizin bilgisi
            List<string> images = new List<string>();
            foreach (var item in imageCreateDto.ProductImages)
            {
                string imageName = $"{Guid.NewGuid().ToString()}.jpg";//Db ye kaydedilecek olan resimlerin ismi
                using (var fileStream = new FileStream(Path.Combine(resimler, imageName), FileMode.Create))
                {
                    images.Add(imageName);
                    await item.CopyToAsync(fileStream);
                }         
            }
            var saveImage = await _productImageService.Add(images, imageCreateDto.ProductId);
            if(saveImage.Success)
            {
                return Ok(saveImage.Message);
            }
            return BadRequest();
        }




    }
}