using Application.Commands.CreateAuthor;
using Application.Commands.UpdateAuthor;
using Application.Query.Author.GetAuthor;
using Application.Query.GetBook;
using Application.Repositpries;
using Dapper;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DapperContext _dapperContext;
        public AuthorRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(CreateAuthorCommand author)
        {
            var query = "INSERT INTO Author (Id, FirstName, LastName, Description) VALUES (@Id, @FirstName, @LastName, @Description)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", author.Id, DbType.Int32);
            parameters.Add("FirstName", author.FirstName, DbType.String);
            parameters.Add("LastName", author.LastName, DbType.String);
            parameters.Add("Description", author.Description, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Author WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task<IEnumerable<GetAuthorQueryResponse>> GetAuthorsAsync()
        {
            var query = "SELECT * FROM Author";
            var listOfAuthors = await _dapperContext.Connection.QueryAsync<GetAuthorQueryResponse>(query, null, _dapperContext.Transaction);

            return listOfAuthors;
        }

        public async Task<GetAuthorQueryResponse> GetAuthorById(int id)
        {
            var query = "SELECT * FROM Author WHERE id = @Id";
            var author = await _dapperContext.Connection.QueryFirstAsync<GetAuthorQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return author;
        }
        public async Task<IEnumerable<GetBookQueryResponse>> GetAuthorBooksAsync(int id)
        {
            var query = "SELECT Book.Title, Book.ISBN, Book.Price FROM Book JOIN Author ON Book.AuthorId = Author.Id WHERE Author.Id = @AutId";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<GetBookQueryResponse>(query, new { AutId = id }, _dapperContext.Transaction);

            return listOfBooks;
        }

        public async Task UpdateAsync(UpdateAuthorCommand author)
        {
            var query = "UPDATE Author SET firstname = @FirstName, lastname = @LastName, description = @Description WHERE Id=@Id";

            var parameters = new DynamicParameters();
            parameters.Add("id", author.id, DbType.Int32);
            parameters.Add("FirstName", author.FirstName, DbType.String);
            parameters.Add("LastName", author.LastName, DbType.String);
            parameters.Add("Description", author.Description, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}