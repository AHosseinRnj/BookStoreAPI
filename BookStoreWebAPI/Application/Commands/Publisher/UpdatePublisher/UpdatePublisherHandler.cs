using Application.Repositpries;
using MediatR;

namespace Application.Commands.UpdatePublisher
{
    public class UpdatePublisherHandler : IRequestHandler<UpdatePublisherCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public UpdatePublisherHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.PublisherRepository.UpdateAsync(request);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}