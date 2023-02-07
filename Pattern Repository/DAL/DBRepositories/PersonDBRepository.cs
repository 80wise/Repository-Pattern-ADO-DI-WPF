using Pattern_Repository.Interfaces;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.DBRepositories
{
    public class PersonDBRepository : IPersonRepository
    {
        private readonly string _connectionString;
        public PersonDBRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<bool> Create(Person entity)
        {
            string query = $"INSERT INTO Person (Name,Email)" +
                $" VALUES (@Name,@Email)";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Name", SqlDbType.Int).Value = entity.Name;
                    sqlCommand.Parameters.Add("@Email", SqlDbType.Int).Value = entity.Email;
                    await sqlCommand.ExecuteNonQueryAsync();
                }

                return true;
            }
        }

        public async Task<bool> Delete(int id)
        {
            string query = $"DELETE FROM Person WHERE Id = @id";

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

        public async Task<IEnumerable<Person>> FindWithCriterea(Func<Person, bool> predicate)
        {
            List<Person> people =(List<Person>) await GetAll();
            List<Person> foundPeople = new List<Person>();
            foreach (Person p in people)
            {
                if (predicate(p))
                    foundPeople.Add(p);
            }

            return foundPeople;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            string sql = $"select Id,Name,Email from Person";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    List<Person> people = new List<Person>();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            people.Add(new Person()
                            {
                                Id = (int)sqlDataReader["Id"],
                                Name = sqlDataReader["IdPerson"] as string ?? "Undefined",
                                Email = sqlDataReader["IdChat"] as string ?? "Undefined"

                            });
                        }
                        return people;
                    }
                }
            }
        }

        public async Task<Person> GetById(long id)
        {
            string sql = $"select Id,Name,Email from Person where Id=@id";

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
                            return new Person()
                            {
                                Id = (int)sqlDataReader["Id"],
                                Name = sqlDataReader["Name"] as string ?? "Undefined",
                                Email = sqlDataReader["Email"] as string ?? "Undefined"

                            };
                        }
                    }
                }

            }

            return null;
        }

        public async Task<IEnumerable<Person>> GetByName(string name)
        {
            string sql = $"select Id, Name, Email from Person where Name=@name";
            List<Person> people = new List<Person>();

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
                            people.Add(new Person()
                            {
                                Id = (int)sqlDataReader["Id"],
                                Name = sqlDataReader["Name"] as string ?? "Undefined",
                                Email = sqlDataReader["Email"] as string ?? "Undefined"

                            });
                        }
                    }
                }

            }

            return people;
        }

        public async Task<bool> Update(Person entity)
        {
            string query = $"UPDATE Person SET Name=@name,Email=@email WHERE Id=@id";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
                    sqlCommand.Parameters.Add("@name", SqlDbType.Int).Value = entity.Name;
                    sqlCommand.Parameters.Add("@email", SqlDbType.Int).Value = entity.Email;

                    await sqlCommand.ExecuteNonQueryAsync();
                }

                return true;
            }
        }
    }
}
