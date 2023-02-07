using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.Interfaces
{
    public interface IDate<T> where T : class 
    {
        Task<IEnumerable<T>> GetByDate(DateTime date);
    }
}
