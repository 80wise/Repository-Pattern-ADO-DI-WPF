using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.Interfaces
{
    public interface IName<T> where T: class
    {
        Task<IEnumerable<T>> GetByName(string name); 
    }
}
