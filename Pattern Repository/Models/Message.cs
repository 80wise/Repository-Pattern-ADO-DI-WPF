using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.Models
{
    public class Message: IComparable<Message>
    {
        public long Id { get; set; }
        public long IdSender { get; set; }
        public long IdChat { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime SendingTime { get; set; }
        public DateTime ReceiptTime { get; set; }

        public int CompareTo(Message other)
        {
            return this.SendingTime.CompareTo(other.SendingTime);
        }

        public override string ToString()
        {
            return $"{Id} {IdSender} {IdChat} {Content} {SendingTime} {ReceiptTime}";
        }
    }
}
