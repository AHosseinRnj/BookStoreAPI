using Application.Query.GetUser;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<GetUserQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public GetUsersQueryHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = LogManager.GetLogger(typeof(GetUsersQueryHandler));
        }

        public async Task<IEnumerable<GetUserQueryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetUserQueryResponse> listOfUsers;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all Users");
                listOfUsers = await _userRepository.GetUsersAsync();
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
            return listOfUsers;
        }
    }
}