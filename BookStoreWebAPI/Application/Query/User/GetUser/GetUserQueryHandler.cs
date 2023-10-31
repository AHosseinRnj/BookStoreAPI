using Application.Repositpries;
using MediatR;

namespace Application.Query.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResponse>
    {
        private IUnitOfWork _unitOfWork;
        public GetUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.id);
            _unitOfWork.Commit();

            return user;
        }
    }
}