using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Dtos.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private readonly IWebHostEnvironment _environment;
        public AuthController(IAuthService authService, IWebHostEnvironment environment)
        {
            _authService = authService;
            _environment = environment;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterDto registerDto)
        {
            var isThereUser = await _authService.UserExists(registerDto.Email);
            if (!isThereUser.Success)
            {
                return BadRequest(isThereUser.Message);
            }
            string Id = Guid.NewGuid().ToString();
            var resimler = Path.Combine(_environment.WebRootPath, "userImage");
            string imageName = $"{Id}.jpg";
            if (registerDto.Image == null)
            {
                imageName = "profileImage.jpg";
            }
            var registerResult =await _authService.Register(registerDto, imageName);
            var result =await _authService.CreateAccessToken(registerResult.Data);//registerResult'ın döndüğü Data(User) bilgisini token üretmek için parametre olarak verdim.
            if (registerDto.Image != null)
            {
                if (registerDto.Image.Length > 0)
                {

                    using (var fileStream = new FileStream(Path.Combine(resimler, imageName), FileMode.Create))
                    {
                        await registerDto.Image.CopyToAsync(fileStream);
                    }
                }
            }
            if (registerResult.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto LoginDto)
        {
            var login =await _authService.Login(LoginDto);
            if(!login.Success)
            {
                return BadRequest(login.Message);
            }
            var token =await _authService.CreateAccessToken(login.Data);
            if (token.Success)
            {
                return Ok(token.Data);
            }
            return BadRequest(token.Message);
        }
    }
}