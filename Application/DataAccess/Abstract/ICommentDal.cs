﻿using Application.Core.DataAccess;
using Application.Entities.Dtos.Comment;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Abstract
{
   public interface ICommentDal:IEntityRepository<Comment>
    {

        Task CommentUpdate(CommentUpdateDto commentUpdateDto);
    }
}
