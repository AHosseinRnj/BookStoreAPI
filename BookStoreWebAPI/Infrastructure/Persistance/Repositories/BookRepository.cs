using Application.Commands.UpdateBook;
using Application.Query.GetBook;
using Application.Repositpries;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        public BookRepository(IDbTransaction dbTransaction)
        {
            _transaction = dbTransaction;
            _connection = _transaction.Connection;
        }

        public async Task AddAsync(Book book)
        {
            var query = "INSERT INTO Book (id, title, isbn, price) VALUES (@id, @Title, @ISBN, @Price)";
            var parameters = new DynamicParameters();
            parameters.Add("id", book.Id, DbType.Int32);
            parameters.Add("Title", book.Title, DbType.String);
            parameters.Add("ISBN", book.ISBN, DbType.String);
            parameters.Add("Price", book.Price, DbType.Double);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Book WHERE id=@Id";
            await _connection.ExecuteAsync(query, new { id }, _transaction);
        }

        public async Task<GetBookQueryResponse> GetBookByIdAsync(int id)
        {
            var query = "SELECT * FROM Book WHERE id=@Id";
            var book = await _connection.QueryFirstAsync<Book>(query, new { id }, _transaction);
            GetBookQueryResponse result = new GetBookQueryResponse()
            {
                Title = book.Title,
                Price = book.Price,
                ISBN = book.ISBN
            };
            return result;
        }

        public async Task UpdateAsync(UpdateBookCommand book)
        {
            var query = "UPDATE Book SET title=@Title, isbn = @ISBN, price=@Price WHERE Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", book.id, DbType.Int32);
            parameters.Add("Title", book.Title, DbType.String);
            parameters.Add("ISBN", book.ISBN, DbType.String);
            parameters.Add("Price", book.price, DbType.Double);
            await _connection.ExecuteAsync(query, parameters, _transaction);
        }
    }
}