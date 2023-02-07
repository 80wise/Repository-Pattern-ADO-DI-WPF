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
    public class PersonChatService: IPersonChatService
    {
        private readonly IPersonChatRepository _personChatRepository;
        public PersonChatService(IPersonChatRepository personChatRepository)
        {
            _personChatRepository = personChatRepository;
        }

        public async Task<Response<bool>> Create(PersonChat entity)
        {
            try
            {
                bool created = await _personChatRepository.Create(entity);
                if (created)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "link created",
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
                bool deleted = await _personChatRepository.Delete(id);
                if (deleted)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "Link deleted",
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

        public async Task<Response<IEnumerable<PersonChat>>> FindWithCriterea(Func<PersonChat, bool> predicate)
        {
            try
            {
                IEnumerable<PersonChat> peopleChats = await _personChatRepository.FindWithCriterea(predicate);
                if (peopleChats.Count() != 0)
                {
                    return new Response<IEnumerable<PersonChat>>()
                    {
                        Data = peopleChats,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<PersonChat>>()
                {
                    Data = peopleChats,
                    Description = "The link list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<PersonChat>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<IEnumerable<PersonChat>>> GetAll()
        {
            try
            {
                IEnumerable<PersonChat> peopleChats = await _personChatRepository.GetAll();
                if (peopleChats.Count() != 0)
                {
                    return new Response<IEnumerable<PersonChat>>()
                    {
                        Data = peopleChats,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<PersonChat>>()
                {
                    Data = peopleChats,
                    Description = "The message list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<PersonChat>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<PersonChat>> GetById(long id)
        {
            try
            {
                PersonChat pc = await _personChatRepository.GetById(id);
                if (pc != null)
                {
                    return new Response<PersonChat>()
                    {
                        Data = pc,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<PersonChat>()
                {
                    Data = pc,
                    Description = "This link doesn't exist",
                    StatusCode = StatusCode.DataNotFound
                };
            }
            catch
            {
                return new Response<PersonChat>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<bool>> Update(PersonChat entity)
        {
            try
            {
                bool updated = await _personChatRepository.Update(entity);
                if (updated)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "link updated",
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
