using Application.Core.DataAccess;
using Application.DataAccess.Abstract;
using Application.Entities;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Application.Entities.Dtos.Comment;
using System.Linq;

namespace Application.DataAccess.Concrete
{
    public class EfCommentDal : EfEntityRepositoryBase<Comment, ProductInformationContext>, ICommentDal
    {
        public async Task CommentUpdate(CommentUpdateDto commentUpdateDto)
        {
            using (var context = new ProductInformationContext())
            {
                var entity =await context.Comments.Where(w => w.Id == commentUpdateDto.Id).SingleOrDefaultAsync();
                if (entity != null)
                {
                    entity.Content = commentUpdateDto.Content;
                    //entity.Updated = Convert.ToDateTime(comment.Updated);
                    entity.Updated = DateTime.Now;
                   await context.SaveChangesAsync();
                }
            }
        }
    }
}
