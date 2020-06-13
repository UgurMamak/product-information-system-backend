using Application.Core.Utilities.Results;
using Application.Entities.Dtos.CommetLike;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
    public interface ICommentLikeServive
    {
        Task<IResult> Add(CommentLikeCreateDto commentLike);
        Task<string> LikeExists(CommentLikeCreateDto commentLike);  
        Task<IResult> Delete(CommentLikeCreateDto commentLike);

        Task<IDataResult<CommentLikeDto>> GetCommentLike(string commentId);
    }
}
