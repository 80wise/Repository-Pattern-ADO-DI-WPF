using Pattern_Repository.Exceptions;
using Pattern_Repository.Interfaces;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.DBRepositories
{
    public class PersonChatDBRepository : IPersonChatRepository
    {
        readonly string _connectionString;
        public PersonChatDBRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<bool> Create(PersonChat entity)
        {
            string query = $"INSERT INTO PersonChat (IdPerson,IdChat)" +
                $" VALUES (@IdPerson,@IdChat)";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@IdPerson", SqlDbType.Int).Value = entity.IdPerson;
                    sqlCommand.Parameters.Add("@IdChat", SqlDbType.Int).Value = entity.IdChat;
                    await sqlCommand.ExecuteNonQueryAsync();
                }

                return true;
            }
        }

        public async Task<bool> Delete(int id)
        {
            string query = $"DELETE FROM PersonChat WHERE Id = @id";

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

        public async Task<IEnumerable<PersonChat>> FindWithCriterea(Func<PersonChat, bool> predicate)
        {
            List<PersonChat> peopleChats =(List<PersonChat>) await GetAll();
            List<PersonChat> foundPeopleChat = new List<PersonChat>();
            foreach(PersonChat  pc in peopleChats)
            {
                if (predicate(pc))
                    foundPeopleChat.Add(pc);
            }
            return foundPeopleChat;
        }

        public async Task<IEnumerable<PersonChat>> GetAll()
        {
            string sql = $"select Id,IdPerson,IdChat from PersonChat";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    List<PersonChat> peopleChats = new List<PersonChat>();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            peopleChats.Add(new PersonChat()
                            {
                                Id = (int)sqlDataReader["Id"],
                                IdPerson = (int)sqlDataReader["IdPerson"],
                                IdChat = (int)sqlDataReader["IdChat"]

                            });
                        }
                        return peopleChats;
                    }
                }
            }
        }

        public async Task<PersonChat> GetById(long id)
        {
            string sql = $"select Id,IdPerson,IdChat from Message where Id=@id";

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
                            return new PersonChat()
                            {
                                Id = (int)sqlDataReader["Id"],
                                IdPerson = (int)sqlDataReader["IdPerson"],
                                IdChat = (int)sqlDataReader["IdChat"]

                            };
                        }
                    }
                }

            }

            return null;
        }

        public async Task<bool> Update(PersonChat entity)
        {
            string query = $"UPDATE PersonChat SET IdPerson=@IdPerson,IdChat=@IdChat WHERE Id=@id";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    sqlCommand.Parameters.Add("@IdPerson", SqlDbType.Int).Value = entity.IdPerson;
                    sqlCommand.Parameters.Add("@IdChat", SqlDbType.Int).Value = entity.IdChat;

                    await sqlCommand.ExecuteNonQueryAsync();
                }

                return true;
            }
        }
    }
}
