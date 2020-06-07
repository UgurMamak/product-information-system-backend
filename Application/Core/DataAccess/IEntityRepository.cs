using Application.Core.Entities;
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
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void DeleteById(Expression<Func<T, bool>> filter = null);
    }
}
