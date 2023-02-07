using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<Response<IEnumerable<T>>> GetAll();
        Task<Response<T>> GetById(long id);
        Task<Response<bool>> Create(T entity);
        Task<Response<bool>> Update(T entity);
        Task<Response<bool>> Delete(int id);
        Task<Response<IEnumerable<T>>> FindWithCriterea(Func<T, bool> predicate);
    }
}
