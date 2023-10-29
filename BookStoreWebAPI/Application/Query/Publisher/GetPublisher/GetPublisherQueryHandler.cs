using Application.Repositpries;
using MediatR;

namespace Application.Query.GetPublisher
{
    public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, GetPublisherQueryResponse>
    {
        private IUnitOfWork _unitOfWork;
        public GetPublisherQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetPublisherQueryResponse> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.PublisherRepository.GetPublisherByIdAsync(request.id);
            _unitOfWork.Commit();

            return result;
        }
    }
}