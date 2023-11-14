using Application.Repositpries;
using Dapper;
using Domain.Entities;
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

        public async Task AddAsync(Category category)
        {
            var query = "INSERT INTO Category (id, name) VALUES (@id, @Name)";

            var parameters = new DynamicParameters();
            parameters.Add("id", category.Id, DbType.Int32);
            parameters.Add("name", category.Name, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Category WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var query = "SELECT Name FROM Category";
            var listOfCategories = await _dapperContext.Connection.QueryAsync<Category>(query, null, _dapperContext.Transaction);

            return listOfCategories;
        }

        public async Task<IEnumerable<Book>> GetCategoryBooksAsync(int id)
        {
            var query = "SELECT Book.Title, Book.ISBN, Book.Price FROM Book JOIN Category ON Book.CategoryId = Category.Id WHERE Category.Id = @id";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<Book>(query, new { id }, _dapperContext.Transaction);

            return listOfBooks;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var query = "SELECT Name FROM Category WHERE id = @Id";
            var category = await _dapperContext.Connection.QueryFirstAsync<Category>(query, new { id }, _dapperContext.Transaction);

            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            var query = "UPDATE Category SET name = @Name WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", category.Id, DbType.Int32);
            parameters.Add("name", category.Name, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}