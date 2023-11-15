using Application.Commands.CreateUser;
using Application.Commands.UpdateUser;
using Domain.Entities;
using log4net;

namespace Application.Services
{
    public class UserWriteService : IUserWriteService
    {
        private readonly ILog _logger;
        private IEFUnitOfWork _unitOfWork;
        public UserWriteService(IEFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(UserWriteService));
        }

        public async Task AddAsync(CreateUserCommand request)
        {
            try
            {
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    Phone = request.Phone
                };

                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a User: " + ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                await _unitOfWork.UserRepository.DeleteByIdAsync(id);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a User: " + ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateAsync(UpdateUserCommand request)
        {
            try
            {
                var user = new User
                {
                    FirstName = request.User.FirstName,
                    LastName = request.User.LastName,
                    Address = request.User.Address,
                    Phone = request.User.Phone
                };

                await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a User: " + ex.Message, ex);
                throw;
            }
        }
    }
}