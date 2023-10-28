using Application.Repositpries;
using MediatR;

namespace Application.Query.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, GetBookQueryResponse>
    {
        private IUnitOfWork _unitOfWork;

        public GetBookQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetBookQueryResponse> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.BookRepository.GetBookByIdAsync(request.id);
            _unitOfWork.Commit();
            return result;
        }
    }
}