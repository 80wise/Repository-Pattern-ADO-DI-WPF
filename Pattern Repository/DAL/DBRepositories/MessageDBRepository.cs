using Pattern_Repository.Interfaces;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Pattern_Repository.Exceptions;
using System.Xml.Linq;

namespace Pattern_Repository.DBRepositories
{
    public class MessageDBRepository : IMessageRepository
    {
        readonly string _connectionString;
        public MessageDBRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<bool> Create(Message entity)
        {
            string query = $"INSERT INTO Message (IdSender,IdChat,Content,SendingTime,ReiceiptTime)" +
                $" VALUES (@IdSender,@IdChat,@Content,@IsRead,@SendingTime,@ReiceiptTime)";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    if (entity.Content != null)
                    {
                        sqlCommand.Parameters.Add("@IdSender", SqlDbType.Int).Value = entity.IdSender;
                        sqlCommand.Parameters.Add("@IdChat", SqlDbType.Int).Value = entity.IdChat;
                        sqlCommand.Parameters.Add("@Content", SqlDbType.NVarChar).Value = entity.Content;
                        sqlCommand.Parameters.Add("@SendingTime", SqlDbType.DateTime).Value = entity.SendingTime;
                        sqlCommand.Parameters.Add("@ReiceiptTime", SqlDbType.DateTime).Value = entity.ReceiptTime;
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        throw new EmptyMessageException();
                    }
                }

                return true;
            }
        }

        public async Task<bool> Delete(int id)
        {
            string query = $"DELETE FROM Message WHERE Id = @id";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    await sqlCommand.ExecuteNonQueryAsync();
                }

                return true;
            }
        }

        public async Task<IEnumerable<Message>> FindWithCriterea(Func<Message, bool> predicate)
        {
            List<Message> messages =(List<Message>) await GetAll();
            List<Message> foundMessages = new List<Message>();
            foreach(Message m in messages)
            {
                if (predicate(m))
                    foundMessages.Add(m);
            }
            return foundMessages;
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            string sql = $"select Id,IdSender,IdChat,Content,SendingTime,ReiceiptTime from Message";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    List<Message> messages = new List<Message>();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            messages.Add(new Message()
                            {
                                Id = (int)sqlDataReader["Id"],
                                IdSender = (int)sqlDataReader["IdSender"],
                                IdChat = (int)sqlDataReader["IdChat"],
                                Content = sqlDataReader["Content"] as string ?? "Undefined",
                                SendingTime = (DateTime)sqlDataReader["SendingTime"],
                                ReceiptTime = (DateTime)sqlDataReader["ReceiptTime"]

                            });
                        }
                        return messages;
                    }
                }
            }
        }

        public async Task<IEnumerable<Message>> GetByContent(string content)
        {
            string sql = $"select Id,IdSender,IdChat,Content,SendingTime,ReiceiptTime from Message where Name like %@content%";
            List<Message> messages = new List<Message>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlParameter contentParam = new SqlParameter
                    {
                        ParameterName = "@content",
                        Value = content,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    sqlCommand.Parameters.Add(contentParam);

                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            messages.Add(new Message()
                            {
                                Id = (int)sqlDataReader["Id"],
                                IdSender = (int)sqlDataReader["IdSender"],
                                IdChat = (int)sqlDataReader["IdChat"],
                                Content = sqlDataReader["Content"] as string ?? "Undefined",
                                SendingTime = (DateTime)sqlDataReader["LastActivityTime"],
                                ReceiptTime = (DateTime)sqlDataReader["LastActivityTime"]

                            });
                        }
                    }
                }

            }

            return messages;
        }

        public async Task<IEnumerable<Message>> GetByDate(DateTime date)
        {
            string sql = $"select Id,IdSender,IdChat,Content,SendingTime,ReiceiptTime from Message" +
                $" where SendingTime=@date";
            List<Message> messages = new List<Message>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlParameter dateParam = new SqlParameter
                    {
                        ParameterName = "@date",
                        Value = date,
                        SqlDbType = SqlDbType.DateTime,
                        Direction = ParameterDirection.Input
                    };

                    sqlCommand.Parameters.Add(dateParam);

                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            messages.Add(new Message()
                            {
                                Id = (int)sqlDataReader["Id"],
                                IdSender = (int)sqlDataReader["IdSender"],
                                IdChat = (int)sqlDataReader["IdChat"],
                                Content = sqlDataReader["Content"] as string ?? "Undefined",
                                SendingTime = (DateTime)sqlDataReader["LastActivityTime"],
                                ReceiptTime = (DateTime)sqlDataReader["LastActivityTime"]

                            });
                        }
                    }
                }

            }

            return messages;
        }

        public async Task<Message> GetById(long id)
        {
            string sql = $"select Id,IdSender,IdChat,Content,SendingTime,ReiceiptTime from Message where Id=@id";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    sqlCommand.Parameters.Add(idParam);

                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (await sqlDataReader.ReadAsync())
                        {
                            return new Message()
                            {
                                Id = (int)sqlDataReader["Id"],
                                IdSender = (int)sqlDataReader["IdSender"],
                                IdChat = (int)sqlDataReader["IdChat"],
                                Content = sqlDataReader["Content"] as string ?? "Undefined",
                                SendingTime = (DateTime)sqlDataReader["LastActivityTime"],
                                ReceiptTime = (DateTime)sqlDataReader["LastActivityTime"]

                            };
                        }
                    }
                }

            }

            return null;
        }

        public async Task<bool> Update(Message entity)
        {
            string query = $"UPDATE Message SET IdSender=@IdSender,IdChat=@IdChat,Content=@Content," +
                $" SendingTime=@SendingTime,ReiceiptTime=@ReiceiptTime WHERE Id=@id";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    sqlCommand.Parameters.Add("@IdSender", SqlDbType.Int).Value = entity.IdSender;
                    sqlCommand.Parameters.Add("@IdChat", SqlDbType.Int).Value = entity.IdChat;
                    sqlCommand.Parameters.Add("@Content", SqlDbType.NVarChar).Value = entity.Content;
                    sqlCommand.Parameters.Add("@SendingTime", SqlDbType.DateTime).Value = entity.SendingTime;
                    sqlCommand.Parameters.Add("@ReiceiptTime", SqlDbType.DateTime).Value = entity.ReceiptTime;

                    await sqlCommand.ExecuteNonQueryAsync();
                }

                return true;
            }
        }
    }
}
