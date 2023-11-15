using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class CategoryReadRepository : ICategoryReadRepository
    {
        private readonly DapperContext _dapperContext;
        public CategoryReadRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var query = "SELECT * FROM Categories";
            var listOfCategories = await _dapperContext.Connection.QueryAsync<Category>(query, null, _dapperContext.Transaction);

            return listOfCategories;
        }

        public async Task<IEnumerable<Book>> GetCategoryBooksAsync(int id)
        {
            var query = "SELECT * FROM Books WHERE Books.CategoryId = @id";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<Book>(query, new { id }, _dapperContext.Transaction);

            return listOfBooks;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var query = "SELECT * FROM Categories WHERE id = @Id";
            var category = await _dapperContext.Connection.QueryFirstAsync<Category>(query, new { id }, _dapperContext.Transaction);

            return category;
        }
    }
}