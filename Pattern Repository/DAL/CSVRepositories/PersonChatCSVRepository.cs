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
    public class PersonChatCSVRepository : IPersonChatRepository
    {
        List<PersonChat> peopleChats;
        readonly string path;

        public PersonChatCSVRepository(string path)
        {
            peopleChats = new List<PersonChat>();
            this.path = path;
        }
        public async Task<bool> Create(PersonChat entity)
        {
            if (peopleChats.Count() > 0)
                entity.Id = peopleChats.Max(pc => pc.Id) + 1;
            else
                entity.Id = 1;

            peopleChats.Add(entity);
            await Write();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            peopleChats.Remove(peopleChats.Find(p => p.Id == id));
            await Write();
            return true;
        }

        public async Task<IEnumerable<PersonChat>> FindWithCriterea(Func<PersonChat, bool> predicate)
        {
            List<PersonChat> foundPeopleChats = new List<PersonChat>();
            await Read();
            foreach (PersonChat pc in peopleChats)
            {
                if (predicate(pc))
                {
                    foundPeopleChats.Add(pc);
                }
            }

            return foundPeopleChats;
        }

        public async Task<IEnumerable<PersonChat>> GetAll()
        {
            await Read();
            return peopleChats;
        }

        public async Task<PersonChat> GetById(long id)
        {
            await Read();
            return peopleChats.Find(pc => pc.Id == id);
        }

        public async Task<bool> Update(PersonChat entity)
        {
            int i = -1;
            i = peopleChats.FindIndex(c => c.Id == entity.Id);
            if (i > -1)
            {
                peopleChats[i].IdPerson = entity.IdPerson;
                peopleChats[i].IdChat = entity.IdChat;

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
                foreach (PersonChat pc in peopleChats)
                {
                    builder.Append(pc.Id)
                        .Append(pc.IdPerson)
                        .Append(pc.IdChat);
                    await writer.WriteLineAsync(builder.ToString());
                    builder.Clear();
                }
            }
        }

        private async Task Read()
        {
            using (StreamReader reader = new StreamReader(path))
            {
                peopleChats.Clear();
                while (!reader.EndOfStream)
                {
                    try
                    {
                        string line = await reader.ReadLineAsync();
                        string[] temp = line.Split(';');
                        peopleChats.Add(new PersonChat()
                        {
                            Id = Convert.ToInt32(temp[0]),
                            IdPerson = Convert.ToInt32(temp[1]),
                            IdChat = Convert.ToInt32(temp[2])
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
