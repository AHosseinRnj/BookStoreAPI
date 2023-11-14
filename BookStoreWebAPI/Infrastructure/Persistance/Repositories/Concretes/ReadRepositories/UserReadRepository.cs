using Application.Query.GetUserOrders;
using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly DapperContext _dapperContext;
        public UserReadRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var query = "SELECT * FROM [dbo].[Users] WHERE id = @Id";
            var user = await _dapperContext.Connection.QueryFirstAsync<User>(query, new { id }, _dapperContext.Transaction);

            return user;
        }

        public async Task<IEnumerable<GetUserOrderItemQueryResponse>> GetUserOrderItemsById(int id)
        {
            var query = "SELECT B.Title, OI.Quantity, OI.Price, (OI.Quantity * OI.Price) AS TotalPrice " +
                        "FROM [Orders] O " +
                        "JOIN OrderItems OI ON O.Id = OI.OrderId " +
                        "JOIN Books B ON OI.BookId = B.Id " +
                        "WHERE O.UserId = @id " +
                        "ORDER BY B.Title";
            var listOfOrders = await _dapperContext.Connection.QueryAsync<GetUserOrderItemQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return listOfOrders;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var query = "SELECT * FROM [dbo].[Users]";
            var listOfUsers = await _dapperContext.Connection.QueryAsync<User>(query, null, _dapperContext.Transaction);

            return listOfUsers;
        }
    }
}