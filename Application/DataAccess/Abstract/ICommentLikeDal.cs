using Application.Core.DataAccess;
using Application.Entities.Dtos.CommetLike;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Abstract
{
    public interface ICommentLikeDal:IEntityRepository<CommentLike>
    {
        Task<CommentLikeDto> GetCommentLike(string commentId);
    }
}
