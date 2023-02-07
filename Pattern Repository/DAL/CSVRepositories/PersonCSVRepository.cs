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
    public class PersonCSVRepository : IPersonRepository
    {
        List<Person> people;
        readonly string path;

        public PersonCSVRepository(string path)
        {
            people = new List<Person>();
            this.path = path;
        }
        public async Task<bool> Create(Person entity)
        {
            if (people.Count() > 0)
                entity.Id = people.Max(p => p.Id) + 1;
            else
                entity.Id = 1;

            people.Add(entity);
            await Write();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            people.Remove(people.Find(p => p.Id == id));
            await Write();
            return true;
        }

        public async Task<IEnumerable<Person>> FindWithCriterea(Func<Person, bool> predicate)
        {
            List<Person> foundpeople = new List<Person>();
            await Read();
            foreach (Person p in people)
            {
                if (predicate(p))
                {
                    foundpeople.Add(p);
                }
            }

            return foundpeople;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            await Read();
            return people;
        }

        public async Task<Person> GetById(long id)
        {
            await Read();
            return people.Find(c => c.Id == id);
        }

        public async Task<IEnumerable<Person>> GetByName(string name)
        {
            await Read();
            return people.FindAll(p => p.Name == name);
        }

        public async Task<bool> Update(Person entity)
        {
            int i = -1;
            i = people.FindIndex(c => c.Id == entity.Id);
            if (i > -1)
            {
                people[i].Name = entity.Name;
                people[i].Email = entity.Email;

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
                foreach (Person person in people)
                {
                    builder.Append(person.Id)
                        .Append(person.Name)
                        .Append(person.Email);
                    await writer.WriteLineAsync(builder.ToString());
                    builder.Clear();
                }
            }
        }

        private async Task Read()
        {
            using (StreamReader reader = new StreamReader(path))
            {
                people.Clear();
                while (!reader.EndOfStream)
                {
                    try
                    {
                        string line = await reader.ReadLineAsync();
                        string[] temp = line.Split(';');
                        people.Add(new Person()
                        {
                            Id = Convert.ToInt32(temp[0]),
                            Name = temp[1],
                            Email = temp[2]
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
