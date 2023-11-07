using Application;
using Application.Commands.CreatePublisher;
using Application.Commands.UpdatePublisher;
using Application.Query.GetBook;
using Application.Query.GetPublisher;
using Application.Repositpries;
using Application.Services;
using Domain.Entities;
using log4net;

namespace Infrastructure.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IPublisherRepository _publisherRepository;
        public PublisherService(IUnitOfWork unitOfWork, IPublisherRepository publisherRepository)
        {
            _unitOfWork = unitOfWork;
            _publisherRepository = publisherRepository;
            _logger = LogManager.GetLogger(typeof(PublisherService));
        }

        public async Task AddAsync(CreatePublisherCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add a Publisher.");

                var publisher = new Publisher
                {
                    Id = request.Id,
                    Name = request.Name,
                    Biography = request.Biography
                };

                await _publisherRepository.AddAsync(publisher);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error adding a Publisher: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher added successfully.");
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to delete a Publisher by ID: " + id);

                await _publisherRepository.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error deleting a Publisher: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher deleted successfully.");
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
                    Biography = publisher.Biography
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
                    Biography = p.Biography
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

        public async Task UpdateAsync(UpdatePublisherCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to update a Publisher.");

                var publisher = new Publisher
                {
                    Id = request.Id,
                    Name = request.Name,
                    Biography = request.Biography
                };

                await _publisherRepository.UpdateAsync(publisher);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error updating a Publisher: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher updated successfully.");
        }
    }
}