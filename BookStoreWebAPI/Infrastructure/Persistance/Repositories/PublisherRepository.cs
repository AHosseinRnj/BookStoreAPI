using Application.Commands.CreatePublisher;
using Application.Commands.UpdatePublisher;
using Application.Query.GetBook;
using Application.Query.GetPublisher;
using Application.Repositpries;
using Dapper;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DapperContext _dapperContext;
        public PublisherRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(CreatePublisherCommand publisher)
        {
            var query = "INSERT INTO Publisher (id, name, biography) VALUES (@id, @Name, @Biography)";

            var parameters = new DynamicParameters();
            parameters.Add("id", publisher.Id, DbType.Int32);
            parameters.Add("name", publisher.Name, DbType.String);
            parameters.Add("biography", publisher.Biography, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Publisher WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task<IEnumerable<GetPublisherQueryResponse>> GetPublishersAsync()
        {
            var query = "SELECT Name, Biography FROM Publisher";
            var listOfPublishers = await _dapperContext.Connection.QueryAsync<GetPublisherQueryResponse>(query, null, _dapperContext.Transaction);

            return listOfPublishers;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetPublisherBooksAsync(int id)
        {
            var query = "SELECT Book.Title, Book.ISBN, Book.Price FROM Book JOIN Publisher ON Book.PublisherId = Publisher.Id WHERE Publisher.Id = @id";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<GetBookQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return listOfBooks;
        }

        public async Task<GetPublisherQueryResponse> GetPublisherByIdAsync(int id)
        {
            var query = "SELECT Name, Biography FROM Publisher WHERE id = @Id";
            var publisher = await _dapperContext.Connection.QueryFirstAsync<GetPublisherQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return publisher;
        }

        public async Task UpdateAsync(UpdatePublisherCommand publisher)
        {
            var query = "UPDATE Publisher SET name = @Name, biography = @Biography WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", publisher.Id, DbType.Int32);
            parameters.Add("name", publisher.Name, DbType.String);
            parameters.Add("biography", publisher.Biography, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}