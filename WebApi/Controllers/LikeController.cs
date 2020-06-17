using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Dtos.LikeProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private ILikeProductService _likeProductService;
        public LikeController(ILikeProductService likeProductService)
        {
            _likeProductService = likeProductService;
        }

        [HttpPost("add")]//++++
        public async Task <IActionResult> Add(LikeProductCreateDto likeProductCreateDto)
        {
            var gonder =await _likeProductService.LikeProductExists(likeProductCreateDto);


            if (gonder == "2")
            {
                var delete =await _likeProductService.Delete(likeProductCreateDto);
                if (delete.Success)
                {
                    var updateNumber =await _likeProductService.GetNumberStatus(likeProductCreateDto.ProductId);
                    if (likeProductCreateDto.LikeStatus == true) updateNumber.Data.Message = "Beğeni kaldırıldı.";
                    else updateNumber.Data.Message = "Beğenmeme geri kaldırıldı.";
                    return Ok(updateNumber.Data);
                }
            }

            //Hiç kayıt yok yeni kayıt ekle
            if (gonder == "0")
            {
                var result =await _likeProductService.Add(likeProductCreateDto);
                var updateNumber =await _likeProductService.GetNumberStatus(likeProductCreateDto.ProductId);
                if (result.Success)
                {
                    if (likeProductCreateDto.LikeStatus == true) updateNumber.Data.Message = "Bu postu beğendiniz";
                    else updateNumber.Data.Message = "Bu postu beğenmediniz";
                    return Ok(updateNumber.Data);
                }
            }

            //kayıt var ama güncelleme işlemi yapılacak.
            if (gonder == "1")
            {
                var delete =await _likeProductService.Delete(likeProductCreateDto);
                if (delete.Success)
                {
                    var result =await _likeProductService.Add(likeProductCreateDto);
                    var updateNumber =await _likeProductService.GetNumberStatus(likeProductCreateDto.ProductId);

                    if (likeProductCreateDto.LikeStatus == true) updateNumber.Data.Message = "Bu postu beğendiniz";
                    else updateNumber.Data.Message = "Bu postu beğenmediniz.";

                    if (result.Success)
                    {
                        return Ok(updateNumber.Data);
                    }
                }
            }
            return BadRequest();
        }


        [HttpGet("getnumberstatus")]//+++
        public async Task<IActionResult> GetNumberStatucDto(string productId)
        {
            var result =await _likeProductService.GetNumberStatus(productId);
            result.Data.Message = "";
            if (result.Success)
            { return Ok(result.Data); }
            return BadRequest(result.Message);
        }




    }
}