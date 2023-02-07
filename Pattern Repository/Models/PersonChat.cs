using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.Models
{
    public class PersonChat
    {
        public long Id { get; set; }
        public long IdPerson { get; set; }
        public long IdChat { get; set; }
    }
}
