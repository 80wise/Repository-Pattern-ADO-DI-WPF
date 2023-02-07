using Pattern_Repository.Interfaces;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.CSVRepositories
{
    public class MessageCSVRepository : IMessageRepository
    {
        List<Message> messages;
        readonly string path;
        public MessageCSVRepository(string path)
        {
            this.path = path;
            messages = new List<Message>();
        }
        public async Task<bool> Create(Message entity)
        {
            if (messages.Count() > 0)
                entity.Id = messages.Max(m => m.Id) + 1;
            else
                entity.Id = 1;

            messages.Add(entity);
            await Write();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            messages.Remove(messages.Find(m => m.Id == id));
            await Write();
            return true;
        }

        public async Task<IEnumerable<Message>> FindWithCriterea(Func<Message, bool> predicate)
        {
            List<Message> foundMessages = new List<Message>();
            await Read();
            foreach(Message m in messages)
            {
                if (predicate(m)) 
                {
                    foundMessages.Add(m);
                }
            }

            return foundMessages;
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            await Read();
            return messages;
        }

        public async Task<IEnumerable<Message>> GetByContent(string content)
        {
            List<Message> foundMessages = new List<Message>();
            await Read();
            foreach (Message m in messages)
            {
                if (m.Content.Contains(content))
                {
                    foundMessages.Add(m);
                }
            }

            return foundMessages;
        }

        public async Task<IEnumerable<Message>> GetByDate(DateTime date)
        {
            List<Message> foundMessages = new List<Message>();
            await Read();
            foreach (Message m in messages)
            {
                if (m.SendingTime.ToShortDateString().Equals(date.ToShortDateString()))
                {
                    foundMessages.Add(m);
                }
            }

            return foundMessages;
        }

        public async Task<Message> GetById(long id)
        {
            await Read();
            return messages.Find(m => m.Id == id);
        }

        public async Task<bool> Update(Message entity)
        {
            int i = -1;
            i = messages.FindIndex(m => m.Id == entity.Id);
            if (i > -1)
            {
                messages[i].IdSender = entity.IdSender;
                messages[i].IdChat = entity.IdChat;
                messages[i].SendingTime = entity.SendingTime;
                messages[i].ReceiptTime = entity.ReceiptTime;

                await Write();

                return true;
            }
            return false;
        }

        private async Task Write()
        {
            StringBuilder builder;
            using (StreamWriter writer = new StreamWriter(path))
            {
                builder = new StringBuilder();
                foreach (Message message in messages)
                {
                    builder.Append(message.Id)
                        .Append(message.IdSender)
                        .Append(message.IdChat)
                        .Append(message.SendingTime)
                        .Append(message.ReceiptTime);
                    await writer.WriteLineAsync(builder.ToString());
                    builder.Clear();
                }
            }
        }

        private async Task Read()
        {
            using(StreamReader reader = new StreamReader(path))
            {
                messages.Clear();
                while (!reader.EndOfStream)
                {
                    try
                    {
                        string line = await reader.ReadLineAsync();
                        string[] temp = line.Split(';');
                        messages.Add(new Message()
                        {
                            Id = Convert.ToInt32(temp[0]),
                            IdSender = Convert.ToInt32(temp[1]),
                            IdChat = Convert.ToInt32(temp[2]),
                            Content = temp[3],
                            SendingTime = Program.DateFormater(temp[4]),
                            ReceiptTime = Program.DateFormater(temp[5])
                        });
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

    }
}
