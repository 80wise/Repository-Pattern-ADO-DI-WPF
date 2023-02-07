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
    public class ChatSevice: IChatService
    {
        private readonly IChatRepository _chatRepository;
        public ChatSevice(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Response<bool>> Create(Chat entity)
        {
            try
            {
                bool created = await _chatRepository.Create(entity);
                if (created)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "Chat created",
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
                bool deleted = await _chatRepository.Delete(id);
                if (deleted)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "Chat deleted",
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

        public async Task<Response<IEnumerable<Chat>>> FindWithCriterea(Func<Chat, bool> predicate)
        {
            try
            {
                IEnumerable<Chat> chats = await _chatRepository.FindWithCriterea(predicate);
                if (chats.Count() != 0)
                {
                    return new Response<IEnumerable<Chat>>()
                    {
                        Data = chats,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Chat>>()
                {
                    Data = chats,
                    Description = "The chat list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Chat>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<IEnumerable<Chat>>> GetAll()
        {
            try
            {
                IEnumerable<Chat> chats = await _chatRepository.GetAll();
                if(chats.Count() != 0)
                {
                    return new Response<IEnumerable<Chat>>()
                    {
                        Data = chats,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Chat>>()
                {
                    Data = chats,
                    Description = "The chat list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Chat>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            
        }

        public async Task<Response<IEnumerable<Chat>>> GetByDate(DateTime date)
        {
            try
            {
                IEnumerable<Chat> chats = await _chatRepository.GetByDate(date);
                if (chats.Count() != 0)
                {
                    return new Response<IEnumerable<Chat>>()
                    {
                        Data = chats,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Chat>>()
                {
                    Data = chats,
                    Description = "The chat list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Chat>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<Chat>> GetById(long id)
        {
            try
            {
                Chat chat = await _chatRepository.GetById(id);
                if (chat != null)
                {
                    return new Response<Chat>()
                    {
                        Data = chat,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<Chat>()
                {
                    Data = chat,
                    Description = "This chat doesn't exist",
                    StatusCode = StatusCode.DataNotFound
                };
            }
            catch
            {
                return new Response<Chat>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<IEnumerable<Chat>>> GetByName(string name)
        {
            try
            {
                IEnumerable<Chat> chats = await _chatRepository.GetByName(name);
                if (chats.Count() != 0)
                {
                    return new Response<IEnumerable<Chat>>()
                    {
                        Data = chats,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Chat>>()
                {
                    Data = chats,
                    Description = "The chat list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Chat>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<bool>> Update(Chat entity)
        {
            try
            {
                bool updated = await _chatRepository.Update(entity);
                if (updated)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "Chat updated",
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
