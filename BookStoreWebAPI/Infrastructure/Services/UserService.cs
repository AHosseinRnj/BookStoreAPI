using Application;
using Application.Commands.CreateUser;
using Application.Commands.UpdateUser;
using Application.Query.GetUser;
using Application.Query.GetUserOrders;
using Application.Repositpries;
using Application.Services;
using Domain.Entities;
using log4net;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = LogManager.GetLogger(typeof(UserService));
        }

        public async Task AddAsync(CreateUserCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add a User.");

                var user = new User
                {
                    Id = request.id,
                    FirstName = request.firstName,
                    LastName = request.lastName,
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

        public async Task<GetUserQueryResponse> GetUserByIdAsync(int id)
        {
            GetUserQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a User by ID: " + id);

                var user = await _userRepository.GetUserByIdAsync(id);

                result = new GetUserQueryResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.Phone
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting a User: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("User retrieved successfully.");
            return result;
        }

        public async Task<IEnumerable<GetUserOrderItemQueryResponse>> GetUserOrdersById(int id)
        {
            IEnumerable<GetUserOrderItemQueryResponse> listOfOrders;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get an User's Orders by ID: " + id);

                listOfOrders = await _userRepository.GetUserOrdersById(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting an User's Orders: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("User's Orders retrieved successfully.");
            return listOfOrders;
        }

        public async Task<IEnumerable<GetUserQueryResponse>> GetUsersAsync()
        {
            List<GetUserQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all Users");
                var listOfUsers = await _userRepository.GetUsersAsync();

                result = listOfUsers.Select(u => new GetUserQueryResponse
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Phone = u.Phone
                }).ToList();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting User: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Users retrieved successfully.");
            return result;
        }

        public async Task UpdateAsync(UpdateUserCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to update a User.");

                var user = new User
                {
                    Id = request.id,
                    FirstName = request.user.FirstName,
                    LastName = request.user.LastName,
                    Address = request.user.Address,
                    Phone = request.user.Phone
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