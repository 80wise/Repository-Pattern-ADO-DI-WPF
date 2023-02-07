using Pattern_Repository.Interfaces;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.CSVRepositories
{
    public class ChatCSVRepository : IChatRepository
    {
        List<Chat> chats;
        readonly string path;

        public ChatCSVRepository(string path)
        {
            chats = new List<Chat>();
            this.path = path;
        }
        public async Task<bool> Create(Chat entity)
        {
            if (chats.Count() > 0)
                entity.Id = chats.Max(c => c.Id) + 1;
            else
                entity.Id = 1;

            chats.Add(entity);
            await Write();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            chats.Remove(chats.Find(m => m.Id == id));
            await Write();
            return true;
        }

        public async Task<IEnumerable<Chat>> FindWithCriterea(Func<Chat, bool> predicate)
        {
            List<Chat> foundChats = new List<Chat>();
            await Read();
            foreach (Chat c in chats)
            {
                if (predicate(c))
                {
                    foundChats.Add(c);
                }
            }

            return foundChats;
        }

        public async Task<IEnumerable<Chat>> GetAll()
        {
            await Read();
            return chats;
        }

        public async Task<IEnumerable<Chat>> GetByDate(DateTime date)
        {
            List<Chat> foundChats = new List<Chat>();
            await Read();
            foreach (Chat c in chats)
            {
                if (c.LastActivityTime.ToShortDateString().Equals(date.ToShortDateString()))
                {
                    foundChats.Add(c);
                }
            }

            return foundChats;
        }

        public async Task<Chat> GetById(long id)
        {
            await Read();
            return chats.Find(c => c.Id == id);
        }

        public async Task<IEnumerable<Chat>> GetByName(string name)
        {
            await Read();
            return chats.FindAll(c => c.Name == name);
        }

        public async Task<bool> Update(Chat entity)
        {
            int i = -1;
            i = chats.FindIndex(c => c.Id == entity.Id);
            if (i > -1)
            {
                chats[i].Name = entity.Name;
                chats[i].LastActivityTime = entity.LastActivityTime;

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
                foreach (Chat chat in chats)
                {
                    builder.Append(chat.Id)
                        .Append(chat.Name)
                        .Append(chat.LastActivityTime);
                    await writer.WriteLineAsync(builder.ToString());
                    builder.Clear();
                }
            }
        }

        private async Task Read()
        {
            using (StreamReader reader = new StreamReader(path))
            {
                chats.Clear();
                while (!reader.EndOfStream)
                {
                    try
                    {
                        string line = await reader.ReadLineAsync();
                        string[] temp = line.Split(';');
                        chats.Add(new Chat()
                        {
                            Id = Convert.ToInt32(temp[0]),
                            Name = temp[1],
                            LastActivityTime = Program.DateFormater(temp[2])
                        });
                    }
                    catch
                    {
                        throw new Exception("Could not read csv file in directory " + path);
                    }
                }
            }
        }
    }
}
