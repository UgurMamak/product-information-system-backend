using Application.Core.Utilities.Results;
using Application.Entities.Dtos.Comment;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
    public interface ICommentService
    {
        Task<IResult> Add(CommentCreateDto commentCreateDto);
        Task<IResult> Delete(CommentDeleteDto commentDeleteDto);
        Task<IResult> Update(CommentUpdateDto commentUpdateDto);
    }
}
