using Application.Query.GetPublisher;
using Application.Repositpries;
using MediatR;

namespace Application.Query.GetPublishers
{
    public class GetPublishersQueryHandler : IRequestHandler<GetPublishersQuery, IEnumerable<GetPublisherQueryResponse>>
    {
        private IUnitOfWork _unitOfWork;
        public GetPublishersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetPublisherQueryResponse>> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.PublisherRepository.GetPublishersAsync();
            _unitOfWork.Commit();

            return result;
        }
    }
}