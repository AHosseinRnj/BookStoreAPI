using Application.Query.Author.GetAuthor;
using Application.Repositpries;
using MediatR;

namespace Application.Query.GetAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<GetAuthorQueryResponse>>
    {
        private IUnitOfWork _unitOfWork;
        public GetAuthorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetAuthorQueryResponse>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.AuthorRepository.GetAuthorsAsync();
            _unitOfWork.Commit();
            return result;
        }
    }
}