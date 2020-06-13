using Application.Core.DataAccess;
using Application.DataAccess.Abstract;
using Application.Entities;
using Application.Entities.Dtos.CommetLike;
using Application.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Concrete
{
    public class EfCommentLikeDal:EfEntityRepositoryBase<CommentLike,ProductInformationContext>,ICommentLikeDal
    {
        public async Task<CommentLikeDto> GetCommentLike(string commentId)
        {
            using (var context = new ProductInformationContext())
            {
                var trueNumber =await context.CommentLikes.Where(x => x.CommentId == commentId && x.LikeStatus == true).CountAsync();
                var falseNumber =await context.CommentLikes.Where(x => x.CommentId == commentId && x.LikeStatus == false).CountAsync();
                var entity = new CommentLikeDto { FalseNumber = falseNumber, TrueNumber = trueNumber };
                return entity;

              
            }
        }
    }
}
