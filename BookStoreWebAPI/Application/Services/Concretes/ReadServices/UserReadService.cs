using Application.Query.GetUser;
using Application.Query.GetUserOrders;
using Application.Repositories;
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
            var user = await _userRepository.GetUserByIdAsync(id);

            var result = new GetUserQueryResponse
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Address = user.Address,
            };

            return result;
        }

        public async Task<IEnumerable<GetUserOrderItemQueryResponse>> GetUserOrdersById(int id)
        {
            var listOfOrders = await _userRepository.GetUserOrderItemsById(id);

            return listOfOrders;
        }

        public async Task<IEnumerable<GetUserQueryResponse>> GetUsersAsync()
        {
            var listOfUsers = await _userRepository.GetUsersAsync();

            var result = listOfUsers.Select(u => new GetUserQueryResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Phone = u.Phone,
                Address = u.Address
            }).ToList();

            return result;
        }
    }
}