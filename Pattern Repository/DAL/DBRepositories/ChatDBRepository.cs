using Pattern_Repository.Interfaces;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.DBRepositories
{
    public class ChatDBRepository : IChatRepository
    {
        private readonly string _connectionString;
        public ChatDBRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<bool> Create(Chat entity)
        {
            string query = $"INSERT INTO Chat (Name, LastActivityTime) VALUES (@Name, @LastActivityTime)";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
                    sqlCommand.Parameters.Add("@LastActivityTime", SqlDbType.DateTime).Value = entity.LastActivityTime;
                    await sqlCommand.ExecuteNonQueryAsync();
                }

                return true;
            }
        }

        public async Task<bool> Delete(int id)
        {
            string query = $@"DELETE FROM Chat WHERE Id = @id";

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

        public async Task<IEnumerable<Chat>> FindWithCriterea(Func<Chat, bool> predicate)
        {
            List<Chat> chats = (List<Chat>)await GetAll();
            List<Chat> foundChats = new List<Chat>();

            foreach(Chat c in chats)
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
            string sql = $"select Id, Name, LastActivityTime from Chat";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    List<Chat> chats = new List<Chat>();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            chats.Add(new Chat()
                            {
                                Id = (int)sqlDataReader["Id"],
                                Name = sqlDataReader["Name"] as string ?? "Undefined",
                                LastActivityTime = (DateTime)sqlDataReader["LastActivityTime"]

                            });
                        }
                        return chats;
                    }
                }
            }
        }

        public async Task<IEnumerable<Chat>> GetByDate(DateTime date)
        {
            string sql = $"select Id, Name, LastActivityTime from Chat where LastActivityTime=@date";
            List<Chat> chats = new List<Chat>();

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
                            chats.Add(new Chat()
                            {
                                Id = (int)sqlDataReader["Id"],
                                Name = sqlDataReader["Name"] as string ?? "Undefined",
                                LastActivityTime = (DateTime)sqlDataReader["LastActivityTime"]

                            });
                        }
                    }
                }

            }

            return chats;
        }

        public async Task<Chat> GetById(long id)
        {
            string sql = $"select Id, Name, LastActivityTime from Chat where Id=@id";

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
                            return new Chat()
                            {
                                Id = (int)sqlDataReader["Id"],
                                Name = sqlDataReader["Name"] as string ?? "Undefined",
                                LastActivityTime = (DateTime)sqlDataReader["LastActivityTime"]

                            };
                        }
                    }
                }

            }

            return null;
        }

        public async Task<IEnumerable<Chat>> GetByName(string name)
        {
            string sql = $"select Id, Name, LastActivityTime from Chat where Name=@name";
            List<Chat> chats = new List<Chat>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@name",
                        Value = name,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    sqlCommand.Parameters.Add(nameParam);

                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            chats.Add (new Chat()
                            {
                                Id = (int)sqlDataReader["Id"],
                                Name = sqlDataReader["Name"] as string ?? "Undefined",
                                LastActivityTime = (DateTime)sqlDataReader["LastActivityTime"]

                            });
                        }
                    }
                }

            }

            return chats;
        }

        public async Task<bool> Update(Chat entity)
        {
            string query = $"UPDATE Chat SET Name=@name,LastActivityTime=@lastActivityTime WHERE Id=@id";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = entity.Name;
                    sqlCommand.Parameters.Add("@lastActivityTime", SqlDbType.Int).Value = entity.LastActivityTime;

                    await sqlCommand.ExecuteNonQueryAsync();
                }

                return true;
            }
        }
    }
}
