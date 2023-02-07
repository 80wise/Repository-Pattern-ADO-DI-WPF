using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.BLL.Interfaces
{
    public interface IDateService<T> where T : class
    {
        Task<Response<IEnumerable<T>>> GetByDate(DateTime date);
    }
}
