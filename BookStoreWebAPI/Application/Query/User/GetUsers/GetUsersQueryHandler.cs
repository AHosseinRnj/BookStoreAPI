using Application.Query.GetUser;
using Application.Repositpries;
using MediatR;

namespace Application.Query.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<GetUserQueryResponse>>
    {
        private IUnitOfWork _unitOfWork;
        public GetUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetUserQueryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var listOfUsers = await _unitOfWork.UserRepository.GetUsersAsync();
            _unitOfWork.Commit();

            return listOfUsers;
        }
    }
}