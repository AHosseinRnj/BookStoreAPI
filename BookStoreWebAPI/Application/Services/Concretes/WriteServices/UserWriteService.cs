using Application.Commands.CreateUser;
using Application.Commands.UpdateUser;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.Services;
using log4net;

namespace Application.Services
{
    public class UserWriteService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly IUserWriteRepository _userRepository;
        public UserWriteService(IDapperUnitOfWork unitOfWork, IUserWriteRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = LogManager.GetLogger(typeof(UserWriteService));
        }

        public async Task AddAsync(CreateUserCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add a User.");

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    Phone = request.Phone
                };

                await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error adding a User: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("User added successfully.");
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to delete a User by ID: " + id);

                await _userRepository.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error deleting a User: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("User deleted successfully.");
        }

        public async Task UpdateAsync(UpdateUserCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to update a User.");

                var user = new User
                {
                    FirstName = request.User.FirstName,
                    LastName = request.User.LastName,
                    Address = request.User.Address,
                    Phone = request.User.Phone
                };

                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error updating a User: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("User updated successfully.");
        }
    }
}