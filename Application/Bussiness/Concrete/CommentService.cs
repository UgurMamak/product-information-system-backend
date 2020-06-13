using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.Comment;
using Application.Entities.Dtos.CommetLike;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
    public class CommentService : ICommentService
    {
        private ICommentDal _commentDal;
        public CommentService(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public async Task<IResult> Add(CommentCreateDto commentCreateDto)
        {
            var comment = new Comment
            {
                Content = commentCreateDto.Content,
                UserId = commentCreateDto.UserId,
                ProductId = commentCreateDto.ProductId,               
                Created = DateTime.Now
            };
            await _commentDal.Add(comment);
            return new SuccessResult(Messages.CommentAdded);
        }
  
        public async Task<IResult> Delete(CommentDeleteDto commentDeleteDto)
        {
            var comment = new Comment
            {
                Id = commentDeleteDto.Id
            };
            await _commentDal.Delete(comment);
            return new SuccessResult(Messages.CommentDeleted);
        }

        public async Task<IResult> Update(CommentUpdateDto commentUpdateDto)
        {
            await _commentDal.CommentUpdate(commentUpdateDto);
            return new SuccessResult(Messages.CommentUpdated);
        }


      

    }
}
