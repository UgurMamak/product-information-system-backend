using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Core.DataAccess
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
     where TEntity : class, IEntity, new()
     where TContext : DbContext, new()
    {
        public async void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
               await context.SaveChangesAsync();
            }
        }
        public async void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
               await context.SaveChangesAsync();
            }
        }
        public async void DeleteById(Expression<Func<TEntity, bool>> filter = null)
        {
            //gelen sorrguya göre silme işlemi
            using (var context = new TContext())
            {
                var entity =await context.Set<TEntity>().Where(filter).ToListAsync();
                foreach (var item in entity)
                {
                    context.Set<TEntity>().Remove(item);
                }
              await  context.SaveChangesAsync();
            }
        }
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            //tek satır veri çekmek için
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }

        public async  Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            //list yapısı ile çok veri çekmek için
            using (var context = new TContext())
            {
                return  filter == null
                    ? await context.Set<TEntity>().ToListAsync()
                    : await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public async void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
              await  context.SaveChangesAsync();
            }
        }
    }
}
