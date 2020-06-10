using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bussiness.Abstract;
using Application.Entities.Dtos.Comment;
using Application.Entities.Dtos.CommetLike;
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
        private ICommentLikeServive _commentLikeService;
        public CommentController(ICommentService commentService,ICommentLikeServive commentLikeServive)
        {
            _commentService = commentService;
            _commentLikeService = commentLikeServive;
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


        //------------------------------------------------------------COMMENTLİKE PROCESS
        [HttpPost("commentlike")]
        public async Task<IActionResult> CommentLike(CommentLikeCreateDto commentLikeCreateDto)
        {
            var isThere =await _commentLikeService.LikeExists(commentLikeCreateDto);

            if(isThere=="0")
            {
                var entity = await _commentLikeService.Add(commentLikeCreateDto);
                if (entity.Success)
                {
                    return Ok(entity.Message);
                }
            }
            if(isThere=="1")
            {
                var delete = await _commentLikeService.Delete(commentLikeCreateDto);
                if(delete.Success)
                {
                    var entity = await _commentLikeService.Add(commentLikeCreateDto);
                    if (entity.Success)
                    {
                        return Ok(entity.Message);
                    }
                }                          
            }
            
            if(isThere=="2")
            {
                var entity =await _commentLikeService.Delete(commentLikeCreateDto);
                if(entity.Success)
                {
                    return Ok(entity.Message);
                }
            }
            return BadRequest();        
        }


    }
}