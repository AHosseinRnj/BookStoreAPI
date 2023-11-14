using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class PublisherReadRepository : IPublisherReadRepository
    {
        private readonly DapperContext _dapperContext;
        public PublisherReadRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            var query = "SELECT Name, Biography FROM Publisher";
            var listOfPublishers = await _dapperContext.Connection.QueryAsync<Publisher>(query, null, _dapperContext.Transaction);

            return listOfPublishers;
        }

        public async Task<IEnumerable<Book>> GetPublisherBooksAsync(int id)
        {
            var query = "SELECT Book.Title, Book.ISBN, Book.Price FROM Book JOIN Publisher ON Book.PublisherId = Publisher.Id WHERE Publisher.Id = @id";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<Book>(query, new { id }, _dapperContext.Transaction);

            return listOfBooks;
        }

        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            var query = "SELECT Name, Biography FROM Publisher WHERE id = @Id";
            var publisher = await _dapperContext.Connection.QueryFirstAsync<Publisher>(query, new { id }, _dapperContext.Transaction);

            return publisher;
        }
    }
}