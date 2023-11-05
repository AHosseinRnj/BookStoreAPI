using Application.Commands.CreateCategory;
using Application.Commands.UpdateCategory;
using Application.Query.GetBook;
using Application.Query.GetCategory;
using Application.Repositpries;
using Dapper;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _dapperContext;
        public CategoryRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(CreateCategoryCommand category)
        {
            var query = "INSERT INTO Category (id, name) VALUES (@id, @Name)";

            var parameters = new DynamicParameters();
            parameters.Add("id", category.id, DbType.Int32);
            parameters.Add("name", category.name, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Category WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task<IEnumerable<GetCategoryQueryResponse>> GetCategoriesAsync()
        {
            var query = "SELECT Name FROM Category";
            var listOfCategories = await _dapperContext.Connection.QueryAsync<GetCategoryQueryResponse>(query, null, _dapperContext.Transaction);

            return listOfCategories;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetCategoryBooksAsync(int id)
        {
            var query = "SELECT Book.Title, Book.ISBN, Book.Price FROM Book JOIN Category ON Book.CategoryId = Category.Id WHERE Category.Id = @id";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<GetBookQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return listOfBooks;
        }

        public async Task<GetCategoryQueryResponse> GetCategoryByIdAsync(int id)
        {
            var query = "SELECT Name FROM Category WHERE id = @Id";
            var category = await _dapperContext.Connection.QueryFirstAsync<GetCategoryQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return category;
        }

        public async Task UpdateAsync(UpdateCategoryCommand category)
        {
            var query = "UPDATE Category SET name = @Name WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", category.id, DbType.Int32);
            parameters.Add("name", category.name, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}