using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.Interfaces
{
    public interface IMessageRepository:IRepository<Message>, IDate<Message>
    {
        Task<IEnumerable<Message>> GetByContent(string content);
    }
}
