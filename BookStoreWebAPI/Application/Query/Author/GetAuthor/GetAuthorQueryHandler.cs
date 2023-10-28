using Application.Repositpries;
using MediatR;

namespace Application.Query.Author.GetAuthor
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, GetAuthorQueryResponse>
    {
        private IUnitOfWork _unitOfWork;
        public GetAuthorQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;    
        }

        public async Task<GetAuthorQueryResponse> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.AuthorRepository.GetAuthorById(request.id);
            _unitOfWork.Commit();
            return result;
        }
    }
}
