using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        Task Add(string key, object value, int duraction);
        bool IsAdd(string key);
        Task Remove(string key);
        Task RemoveByPattern(string pattern);
    }
}
