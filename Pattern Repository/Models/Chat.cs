using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.Models
{
    public class Chat
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime LastActivityTime { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastActivityTime}";
        }
    }
}
