using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getbyuserId")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var result =await _userService.GetById(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return Ok(result.Message);
        }

    }
}