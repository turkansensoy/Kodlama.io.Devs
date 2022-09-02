using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity:Entity
        where TContext: DbContext
    {
        protected TContext Context { get; }
        public EfRepositoryBase(TContext context)
        {
            Context= context;
        }

        public TEntity Add(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges();
            return entity;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
           return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }
    }
}
