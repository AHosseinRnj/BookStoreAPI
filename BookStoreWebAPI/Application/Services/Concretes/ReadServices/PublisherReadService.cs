using Application.Query.GetBook;
using Application.Query.GetPublisher;
using Application.Repositories;
using log4net;

namespace Application.Services
{
    public class PublisherReadService : IPublisherReadService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly IPublisherReadRepository _publisherRepository;
        public PublisherReadService(IDapperUnitOfWork unitOfWork, IPublisherReadRepository publisherRepository)
        {
            _unitOfWork = unitOfWork;
            _publisherRepository = publisherRepository;
            _logger = LogManager.GetLogger(typeof(PublisherReadService));
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetPublisherBooksAsync(int id)
        {
            var listOfBooks = await _publisherRepository.GetPublisherBooksAsync(id);

            var result = listOfBooks.Select(b => new GetBookQueryResponse
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                Price = b.Price,
                Quantity = b.Quantity
            }).ToList();

            return result;
        }

        public async Task<GetPublisherQueryResponse> GetPublisherByIdAsync(int id)
        {
            var publisher = await _publisherRepository.GetPublisherByIdAsync(id);

            var result = new GetPublisherQueryResponse
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Description = publisher.Description
            };

            return result;
        }

        public async Task<IEnumerable<GetPublisherQueryResponse>> GetPublishersAsync()
        {
            var listOfPublishers = await _publisherRepository.GetPublishersAsync();

            var result = listOfPublishers.Select(p => new GetPublisherQueryResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }).ToList();

            return result;
        }
    }
}