﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public ProductController(IProductService productService, IWebHostEnvironment environment, IProductImageService productImageService)
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
                if (productCreateDto.ProductImages != null)
                {
                    var resimler = Path.Combine(_environment.WebRootPath, "productImage");//dizin bilgisi
                    List<string> images = new List<string>();
                    foreach (var item in productCreateDto.ProductImages)
                    {
                        //if (item.Image.Length > 0)
                       // {
                            string imageName = $"{Guid.NewGuid().ToString()}.jpg";//Db ye kaydedilecek olan resimlerin ismi
                            using (var fileStream = new FileStream(Path.Combine(resimler, imageName), FileMode.Create))
                            {
                                images.Add(imageName);
                                await item.CopyToAsync(fileStream);
                            }
                       // }
                    }
                    var saveImage = await _productImageService.Add(images, entity.Data.Id);
                    if (saveImage.Success)
                    {
                        return Ok(entity.Data);
                    }
                    return BadRequest("Resimler yüklenirken hata oluştu");
                }
                return Ok(entity.Message);
            }
            return BadRequest();
        }

        [HttpPost("delete")]//+++++
        public async Task<IActionResult> Delete([FromForm]Product product)//sadece Id değeri ile silinecek.
        {
            //ürüne ait resimleri silmek için 
            var imageList = await _productImageService.GetByImageId(product.Id);
            var entity = await _productService.Delete(product);
            if (entity.Success)
            {
                if (imageList != null)
                {
                    var resimler = Path.Combine(_environment.WebRootPath, "productImage");//dizin bilgisi
                    foreach (var item in imageList.Data)
                    {
                        System.IO.File.Delete(resimler + "\\" + item.ImageName);
                    }
                    return Ok(entity.Message);
                }
            }
            return BadRequest();
        }

        

        [HttpGet("getallproductcart")] //tüm postları listeleme +++
        public async Task<IActionResult> GetProductCart()
        {
            var result =await _productService.GetProductCart();
            if (result.Success)
            {
              //  var sonuc = result.Data.Where(w => w.IsActive == true && w.IsDeleted == false);
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("popularproductcart")] //en çok yorum alan postu çeker.
        public async Task<IActionResult> GetPopularProductCart()
        {
            var result = await _productService.GetProductCart();
            if (result.Success)
            {
                var sonuc = result.Data.OrderByDescending(x=>x.CommentNumber);
                return Ok(sonuc);
            }
            return BadRequest(result.Message);
        }

     

        [HttpGet("gettypecart")]//+++
        public async Task<IActionResult> GetTypeProductCart([FromForm]FilterDtos filterDtos)
        {
            //Ürün tiplerine göre listeleme işlemi
            var result = await _productService.GetProductCart(p=>filterDtos.ProductType.Contains(p.ProductType));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbycategoryproductcart")]//Kategoriye göre ürün listeleme
        public async Task<IActionResult> GetByCategoryProductCart(string categoryId)
        {

          //  var result = await _productService.GetProductCart(p => filterDtos.ProductType.Contains(p.ProductType));

            var result = await _productService.GetProductCart();            
            if (result.Success)
            {
                var cart = new List<ProductCartDto>();
                foreach (var item in result.Data)
                {
                    foreach (var item2 in item.productCategoryDtos)
                    {
                        if (item2.CategoryId == categoryId)
                        {
                            cart.Add(item);
                        }
                    }
                }
                return Ok(cart);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getproductdetail")] //product detay listeleme
        public async Task<IActionResult> GetProductDetail(string productId)
        {
            var result = await _productService.GetProductDetail(productId);
            if (result.Success)
            {
                //  var sonuc = result.Data.Where(w => w.IsActive == true && w.IsDeleted == false);
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        //----------------------------------------PRODUCTPOINT PROCESS---------------------------------
        [HttpPost("pointadd")]
        public async Task<IActionResult> PointAdd([FromForm] ProductPoint productPoint)
        {
            var isThere = await _productService.ProductPointExists(productPoint);

            if (isThere == true)
            {
                var entity = await _productService.PointAdd(productPoint);
                if (entity.Success)
                {
                    return Ok(entity.Message);
                }

            }
            else
            {
                var entity = await _productService.PointUpdate(productPoint);
                if (entity.Success)
                {
                    return Ok(entity.Message);
                }
            }
            return BadRequest();
        }

    }
}