using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Dtos.Comment;
using Application.Entities.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CommentCreateDto commentCreateDto)
        {
            var result =await _commentService.Add(commentCreateDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("delete")]
        public async Task<IActionResult> Delete(CommentDeleteDto commentDeleteDto)
        {
            var entity =await _commentService.Delete(commentDeleteDto);
            if(entity.Success)
            {
                return Ok(entity.Success);
            }
            return BadRequest(entity.Message);
        }

        [HttpPost("update")]
        public async  Task<IActionResult> Update(CommentUpdateDto commentUpdateDto)
        {
            var result =await _commentService.Update(commentUpdateDto);
            if (result.Success) { return Ok(result.Message); }
            return BadRequest(result.Message);
        }


    }
}