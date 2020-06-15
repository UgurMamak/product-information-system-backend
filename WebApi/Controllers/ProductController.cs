using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Dtos.Product;
using Application.Entities.Dtos.ProductImage;
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
                        return Ok(entity.Message);
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



        [HttpGet("getallproductcart")] //tüm ürünleri listeler
        public async Task<IActionResult> GetProductCart()
        {
            var result = await _productService.GetProductCart();
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
                var sonuc = result.Data.OrderByDescending(x => x.CommentNumber);
                return Ok(sonuc);
            }
            return BadRequest(result.Message);
        }



        [HttpPost("gettypecart")]//ürün tipine göre getirme
        public async Task<IActionResult> GetTypeProductCart([FromForm]FilterDtos filterDtos)
        {
            //Ürün tiplerine göre listeleme işlemi
            var result = await _productService.GetProductCart(p => filterDtos.ProductType.Contains(p.ProductType));
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getusercart")]//Usera göre cart getirme
        public async Task<IActionResult> GetUserCart(string userId)
        {
            //Ürün tiplerine göre listeleme işlemi
            var result = await _productService.GetProductCart(x => userId.Contains(x.UserId));
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


        [HttpPost("update")]//++++
        public async Task<IActionResult> Update([FromForm] ProductUpdateDto product)
        {                     
            var update = await _productService.Update(product);
            if (update.Success)
            {                
                var imageList = await _productImageService.GetByImageId(product.Id);  //ürüne ait resimleri alır.    
                var deletingImage = imageList.Data.Where(w => !product.oldImageName.Contains(w.ImageName)).ToList();
                if(deletingImage.Count!=0)
                {
                    List<string> images = new List<string>();
                    foreach (var item in deletingImage)
                    {
                        images.Add(item.ImageName);
                    }
                    var deleteEntity = await _productImageService.Delete(images);
                    if (images != null)
                    {
                        var resimler = Path.Combine(_environment.WebRootPath, "productImage");//dizin bilgisi
                        foreach (var item in images)
                        {
                            System.IO.File.Delete(resimler + "\\" + item);
                        }
                    }
                }
              
                if (product.NewImages != null)
                {
                    var resimler = Path.Combine(_environment.WebRootPath, "productImage");//dizin bilgisi
                    List<string> newImages = new List<string>();
                    foreach (var item in product.NewImages)
                    {
                        string imageName = $"{Guid.NewGuid().ToString()}.jpg";//Db ye kaydedilecek olan resimlerin ismi
                        using (var fileStream = new FileStream(Path.Combine(resimler, imageName), FileMode.Create))
                        {
                            newImages.Add(imageName);
                            await item.CopyToAsync(fileStream);
                        }
                    }
                    var saveImage = await _productImageService.Add(newImages, product.Id);
                    if (saveImage.Success)
                    {
                        return Ok(update.Message);
                    }
                    return BadRequest("Resimler yüklenirken hata oluştu");
                }
                return Ok(update.Message);
            }
            return BadRequest(update.Message);
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
                    var result = await _productService.GetProductPoint(productPoint.ProductId);
                    result.Data.Message = "Puanınız eklendi";
                    return Ok(result.Data);
                }
            }

            else
            {
                var entity = await _productService.PointUpdate(productPoint);
                if (entity.Success)
                {
                    var result = await _productService.GetProductPoint(productPoint.ProductId);
                    result.Data.Message = "Puanınız Güncellendi";
                    return Ok(result.Data);
                }
            }
            return BadRequest();
        }

        [HttpGet("getproductpoint")]//+++
        public async Task<IActionResult> GetProductPoint(string productId)
        {
            var result = await _productService.GetProductPoint(productId);
            result.Data.Message = "";
            if (result.Success)
            { return Ok(result.Data); }
            return BadRequest(result.Message);
        }

    }
}