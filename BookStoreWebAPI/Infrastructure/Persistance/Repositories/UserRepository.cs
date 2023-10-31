using Application.Commands.CreateUser;
using Application.Commands.UpdateUser;
using Application.Query.Author.GetAuthor;
using Application.Query.GetUser;
using Application.Repositpries;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        public UserRepository(IDbTransaction dbTransaction)
        {
            _transaction = dbTransaction;
            _connection = _transaction.Connection;
        }

        public async Task AddAsync(CreateUserCommand user)
        {
            var query = "INSERT INTO User (Id, FirstName, LastName, Address, Phone) VALUES (@Id, @FirstName, @LastName, @Address, @Phone)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", user.id, DbType.UInt32);
            parameters.Add("FirstName", user.firstName, DbType.String);
            parameters.Add("LastName", user.lastName, DbType.String);
            parameters.Add("Address", user.Address, DbType.String);
            parameters.Add("Phone", user.Phone, DbType.String);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM User WHERE id = @Id";
            await _connection.ExecuteAsync(query, new { id }, _transaction);
        }

        public async Task<GetUserQueryResponse> GetUserByIdAsync(int id)
        {
            var query = "SELECT * FROM User WHERE id = @Id";
            var user = await _connection.QueryFirstAsync<GetUserQueryResponse>(query, new { id }, _transaction);

            return user;
        }

        public async Task<IEnumerable<GetUserQueryResponse>> GetUsersAsync()
        {
            var query = "SELECT * FROM User";
            var listOfUsers = await _connection.QueryAsync<GetUserQueryResponse>(query, null, _transaction);

            return listOfUsers;
        }

        public async Task UpdateAsync(UpdateUserCommand request)
        {
            var query = "UPDATE User SET FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", request.id, DbType.UInt32);
            parameters.Add("FirstName", request.user.FirstName, DbType.String);
            parameters.Add("LastName", request.user.LastName, DbType.String);
            parameters.Add("Address", request.user.Address, DbType.String);
            parameters.Add("Phone", request.user.Phone, DbType.String);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }
    }
}