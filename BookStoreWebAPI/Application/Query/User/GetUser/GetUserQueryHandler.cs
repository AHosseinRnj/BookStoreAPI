using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResponse>
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILog _logger;
        public GetUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetUserQueryHandler));
        }

        public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            GetUserQueryResponse user;

            try
            {
                _logger.Info("Received a request to get a User by ID: " + request.id);
                user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.id);
            }
            catch (Exception ex)
            {
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