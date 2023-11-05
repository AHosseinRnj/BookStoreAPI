using Application.Commands.CreateUser;
using Application.Commands.UpdateUser;
using Application.Query.GetUser;
using Application.Query.GetUserOrders;
using Application.Repositpries;
using Dapper;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;
        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(CreateUserCommand user)
        {
            var query = "INSERT INTO [dbo].[User] (Id, FirstName, LastName, Address, Phone) VALUES (@Id, @FirstName, @LastName, @Address, @Phone)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", user.id, DbType.Int32);
            parameters.Add("FirstName", user.firstName, DbType.String);
            parameters.Add("LastName", user.lastName, DbType.String);
            parameters.Add("Address", user.Address, DbType.String);
            parameters.Add("Phone", user.Phone, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM [dbo].[User] WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task<GetUserQueryResponse> GetUserByIdAsync(int id)
        {
            var query = "SELECT * FROM [dbo].[User] WHERE id = @Id";
            var user = await _dapperContext.Connection.QueryFirstAsync<GetUserQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return user;
        }

        public async Task<IEnumerable<GetUserOrdersQueryResponse>> GetUserOrdersById(int id)
        {
            var query = "SELECT B.Title, OB.Quantity, OB.Price, (OB.Quantity * OB.Price) AS TotalPrice " +
                        "FROM [Order] O " +
                        "JOIN OrderBook OB ON O.Id = OB.OrderId " +
                        "JOIN Book B ON OB.BookId = B.Id " +
                        "WHERE O.UserId = @id " +
                        "ORDER BY B.Title";
            var listOfOrders = await _dapperContext.Connection.QueryAsync<GetUserOrdersQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return listOfOrders;
        }

        public async Task<IEnumerable<GetUserQueryResponse>> GetUsersAsync()
        {
            var query = "SELECT * FROM [dbo].[User]";
            var listOfUsers = await _dapperContext.Connection.QueryAsync<GetUserQueryResponse>(query, null, _dapperContext.Transaction);

            return listOfUsers;
        }

        public async Task UpdateAsync(UpdateUserCommand request)
        {
            var query = "UPDATE [dbo].[User] SET FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", request.id, DbType.Int32);
            parameters.Add("FirstName", request.user.FirstName, DbType.String);
            parameters.Add("LastName", request.user.LastName, DbType.String);
            parameters.Add("Address", request.user.Address, DbType.String);
            parameters.Add("Phone", request.user.Phone, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}