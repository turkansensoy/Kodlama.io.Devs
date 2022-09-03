using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{
    public interface IRepository<T>:IQuery<T> where T: Entity
    {
        T Get(Expression<Func<T, bool>> predicate);
        T Delete(T entity);
        T Add(T entity);
        T Update(T entity);
    }
}
