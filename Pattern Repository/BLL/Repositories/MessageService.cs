using Pattern_Repository.BLL.Interfaces;
using Pattern_Repository.Interfaces;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.BLL.Repositories
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<Response<bool>> Create(Message entity)
        {
            try
            {
                bool created = await _messageRepository.Create(entity);
                if (created)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "message created",
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
                bool deleted = await _messageRepository.Delete(id);
                if (deleted)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "Message deleted",
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

        public async Task<Response<IEnumerable<Message>>> FindWithCriterea(Func<Message, bool> predicate)
        {
            try
            {
                IEnumerable<Message> chats = await _messageRepository.FindWithCriterea(predicate);
                if (chats.Count() != 0)
                {
                    return new Response<IEnumerable<Message>>()
                    {
                        Data = chats,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Message>>()
                {
                    Data = chats,
                    Description = "The message list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Message>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<IEnumerable<Message>>> GetAll()
        {
            try
            {
                IEnumerable<Message> messages = await _messageRepository.GetAll();
                if (messages.Count() != 0)
                {
                    return new Response<IEnumerable<Message>>()
                    {
                        Data = messages,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Message>>()
                {
                    Data = messages,
                    Description = "The message list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Message>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<IEnumerable<Message>>> GetByDate(DateTime date)
        {
            try
            {
                IEnumerable<Message> messages = await _messageRepository.GetByDate(date);
                if (messages.Count() != 0)
                {
                    return new Response<IEnumerable<Message>>()
                    {
                        Data = messages,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<IEnumerable<Message>>()
                {
                    Data = messages,
                    Description = "The message list is empty",
                    StatusCode = StatusCode.EmptyResource
                };
            }
            catch
            {
                return new Response<IEnumerable<Message>>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<Message>> GetById(long id)
        {
            try
            {
                Message message = await _messageRepository.GetById(id);
                if (message != null)
                {
                    return new Response<Message>()
                    {
                        Data = message,
                        Description = "Ok",
                        StatusCode = StatusCode.OK
                    };
                }
                return new Response<Message>()
                {
                    Data = message,
                    Description = "This message doesn't exist",
                    StatusCode = StatusCode.DataNotFound
                };
            }
            catch
            {
                return new Response<Message>()
                {
                    Data = null,
                    Description = "Something went wrong from the server side",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<bool>> Update(Message entity)
        {
            try
            {
                bool updated = await _messageRepository.Update(entity);
                if (updated)
                {
                    return new Response<bool>()
                    {
                        Data = true,
                        Description = "message updated",
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
