using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Dtos.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IAuthService _authService;
        private readonly IWebHostEnvironment _environment;
        public UserController(IUserService userService,IAuthService authService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _authService = authService;
            _environment = environment;
            
    }

        [HttpGet("getbyuserId")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            //Idye göre user getirme
            var result =await _userService.GetById(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return Ok(result.Message);
        }

        [HttpGet("getalluser")]
        public async Task<IActionResult> GetAllUser()
        {
            //tüm userları getirir.
            var result = await _userService.UserGetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }





        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto.Email != null)
            {
                var userExists =await _authService.UserExists(userUpdateDto.Email);

                if (!userExists.Success)
                {
                    return BadRequest(userExists.Message);
                }
            }
            var newImageName = $"{ Guid.NewGuid()}.jpg";
            userUpdateDto.ImageName = newImageName;

            var update =await _userService.Update(userUpdateDto);
            if (update.Success)
            {
                if (userUpdateDto.Image != null)
                {
                    var resimler = Path.Combine(_environment.WebRootPath, "userImage");//dizin bilgisi
                    System.IO.File.Delete(resimler + "\\" + userUpdateDto.ImageName);//eski resim silinir.

                    using (var fileStream = new FileStream(Path.Combine(resimler, newImageName), FileMode.Create))
                    { await userUpdateDto.Image.CopyToAsync(fileStream); }

                }
                return Ok(update.Message);
            }
            return BadRequest(update.Message);
        }






    }
}