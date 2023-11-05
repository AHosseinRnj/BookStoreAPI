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
        public GetUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetUsersQueryHandler));
        }

        public async Task<IEnumerable<GetUserQueryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetUserQueryResponse> listOfUsers;

            try
            {
                _logger.Info("Received a request to get all Users");
                listOfUsers = await _unitOfWork.UserRepository.GetUsersAsync();
            }
            catch (Exception ex)
            {
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