using Application.Commands.CreatePublisher;
using Application.Commands.UpdatePublisher;
using Application.Repositories;
using Domain.Entities;
using log4net;

namespace Application.Services
{
    public class PublisherWriteService : IPublisherWriteService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly IPublisherWriteRepository _publisherRepository;
        public PublisherWriteService(IDapperUnitOfWork unitOfWork, IPublisherWriteRepository publisherRepository)
        {
            _unitOfWork = unitOfWork;
            _publisherRepository = publisherRepository;
            _logger = LogManager.GetLogger(typeof(PublisherWriteService));
        }

        public async Task AddAsync(CreatePublisherCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add a Publisher.");

                var publisher = new Publisher
                {
                    Name = request.Name,
                    Description = request.Description
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

        public async Task UpdateAsync(UpdatePublisherCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to update a Publisher.");

                var publisher = new Publisher
                {
                    Id = request.Id,
                    Name = request.Publisher.Name,
                    Description = request.Publisher.Description
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