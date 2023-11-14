using Application.Query.GetUser;
using Application.Query.GetUserOrders;
using Application.Repositories;
using Infrastructure.Services;
using log4net;

namespace Application.Services
{
    public class UserReadService : IUserReadService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly IUserReadRepository _userRepository;
        public UserReadService(IDapperUnitOfWork unitOfWork, IUserReadRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = LogManager.GetLogger(typeof(UserReadService));
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

                listOfOrders = await _userRepository.GetUserOrderItemsById(id);
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
    }
}