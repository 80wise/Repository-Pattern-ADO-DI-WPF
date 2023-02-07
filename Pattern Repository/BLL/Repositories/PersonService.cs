using Pattern_Repository.BLL.Interfaces;
using Pattern_Repository.Interfaces;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.BLL.Repositories
{
    public class PersonService: IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Response<bool>> Create(Person entity)
        {
            try
            {
                bool created = await _personRepository.Create(entity);
                if (created)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "person created",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<bool>()
                {
                    Data = false,
                    Description = "Something went wrong from your side",
                    StatusCode = StatusCode.ClientError
                };
            }
            catch
            {
                return new Response<bool>()
                {
                    Data = false,
                    Description = "something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<bool>> Delete(int id)
        {
            try
            {
                bool deleted = await _personRepository.Delete(id);
                if (deleted)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "person deleted",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<bool>()
                {
                    Data = false,
                    Description = "Something went wrong from your side",
                    StatusCode = StatusCode.ClientError
                };
            }
            catch
            {
                return new Response<bool>()
                {
                    Data = false,
                    Description = "something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<IEnumerable<Person>>> FindWithCriterea(Func<Person, bool> predicate)
        {
            try
            {
                IEnumerable<Person> people = await _personRepository.FindWithCriterea(predicate);
                if (people.Count() != 0)
                {
                    return new Response<IEnumerable<Person>>()
                    {
                        Data = people,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Person>>()
                {
                    Data = people,
                    Description = "The link list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Person>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<IEnumerable<Person>>> GetAll()
        {
            try
            {
                IEnumerable<Person> people = await _personRepository.GetAll();
                if (people.Count() != 0)
                {
                    return new Response<IEnumerable<Person>>()
                    {
                        Data = people,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Person>>()
                {
                    Data = people,
                    Description = "The message list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Person>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<Person>> GetById(long id)
        {
            try
            {
                Person person = await _personRepository.GetById(id);
                if (person != null)
                {
                    return new Response<Person>()
                    {
                        Data = person,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<Person>()
                {
                    Data = person,
                    Description = "This link doesn't exist",
                    StatusCode = StatusCode.DataNotFound
                };
            }
            catch
            {
                return new Response<Person>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<IEnumerable<Person>>> GetByName(string name)
        {
            try
            {
                IEnumerable<Person> people = await _personRepository.GetByName(name);
                if (people.Count() != 0)
                {
                    return new Response<IEnumerable<Person>>()
                    {
                        Data = people,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Person>>()
                {
                    Data = people,
                    Description = "The person list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Person>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<bool>> Update(Person entity)
        {
            try
            {
                bool updated = await _personRepository.Update(entity);
                if (updated)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "person updated",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<bool>()
                {
                    Data = false,
                    Description = "Something went wrong from your side",
                    StatusCode = StatusCode.ClientError
                };
            }
            catch
            {
                return new Response<bool>()
                {
                    Data = false,
                    Description = "something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
