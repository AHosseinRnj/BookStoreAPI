using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResponse>
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILog _logger;
        private readonly IUserRepository _userRepository;
        public GetUserQueryHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = LogManager.GetLogger(typeof(GetUserQueryHandler));
        }

        public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            GetUserQueryResponse user;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a User by ID: " + request.id);
                user = await _userRepository.GetUserByIdAsync(request.id);
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
            return user;
        }
    }
}