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
            IEnumerable<GetBookQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a Publisher's Books by ID: " + id);

                var listOfBooks = await _publisherRepository.GetPublisherBooksAsync(id);

                result = listOfBooks.Select(b => new GetBookQueryResponse
                {
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Price = b.Price
                }).ToList();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting an Publisher's Books: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher's Books retrieved successfully.");
            return result;
        }

        public async Task<GetPublisherQueryResponse> GetPublisherByIdAsync(int id)
        {
            GetPublisherQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a Publisher by ID: " + id);

                var publisher = await _publisherRepository.GetPublisherByIdAsync(id);

                result = new GetPublisherQueryResponse
                {
                    Name = publisher.Name,
                    Description = publisher.Description
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting a Publisher: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher retrieved successfully.");
            return result;
        }

        public async Task<IEnumerable<GetPublisherQueryResponse>> GetPublishersAsync()
        {
            List<GetPublisherQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get Publishers");

                var listOfPublishers = await _publisherRepository.GetPublishersAsync();

                result = listOfPublishers.Select(p => new GetPublisherQueryResponse
                {
                    Name = p.Name,
                    Description = p.Description
                }).ToList();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting Publishers: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publishers retrieved successfully.");
            return result;
        }
    }
}