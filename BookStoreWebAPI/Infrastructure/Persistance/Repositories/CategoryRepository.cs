using Application.Commands.CreateCategory;
using Application.Commands.UpdateCategory;
using Application.Query.GetBook;
using Application.Query.GetCategory;
using Application.Query.GetPublisher;
using Application.Repositpries;
using Dapper;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        public CategoryRepository(IDbTransaction dbTransaction)
        {
            _transaction = dbTransaction;
            _connection = _transaction.Connection;
        }

        public async Task AddAsync(CreateCategoryCommand category)
        {
            var query = "INSERT INTO Category (id, name) VALUES (@id, @Name)";

            var parameters = new DynamicParameters();
            parameters.Add("id", category.id, DbType.Int32);
            parameters.Add("name", category.name, DbType.String);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Category WHERE id = @Id";
            await _connection.ExecuteAsync(query, new { id }, _transaction);
        }

        public async Task<IEnumerable<GetCategoryQueryResponse>> GetCategoriesAsync()
        {
            var query = "SELECT Name FROM Category";
            var listOfCategories = await _connection.QueryAsync<GetCategoryQueryResponse>(query, null, _transaction);

            return listOfCategories;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetCategoryBooksAsync(int id)
        {
            var query = "SELECT Book.Title, Book.ISBN, Book.Price FROM Book JOIN Category ON Book.CategoryId = Category.Id WHERE Category.Id = @id";
            var listOfBooks = await _connection.QueryAsync<GetBookQueryResponse>(query, new { id }, _transaction);

            return listOfBooks;
        }

        public async Task<GetCategoryQueryResponse> GetCategoryByIdAsync(int id)
        {
            var query = "SELECT Name FROM Category WHERE id = @Id";
            var category = await _connection.QueryFirstAsync<GetCategoryQueryResponse>(query, new { id }, _transaction);

            return category;
        }

        public async Task UpdateAsync(UpdateCategoryCommand category)
        {
            var query = "UPDATE Category SET name = @Name WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", category.id, DbType.Int32);
            parameters.Add("name", category.name, DbType.String);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }
    }
}