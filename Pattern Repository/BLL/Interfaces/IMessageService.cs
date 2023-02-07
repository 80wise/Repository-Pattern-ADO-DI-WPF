using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.BLL.Interfaces
{
    public interface IMessageService:IService<Message>, IDateService<Message>
    {

    }
}
