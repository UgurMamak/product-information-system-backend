﻿using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {

        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<IList<T>> GetList(Expression<Func<T, bool>> filter = null);
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task DeleteById(Expression<Func<T, bool>> filter = null);
    }
}
