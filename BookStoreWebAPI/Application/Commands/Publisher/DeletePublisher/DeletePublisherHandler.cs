using Application.Repositpries;
using MediatR;

namespace Application.Commands.DeletePublisher
{
    public class DeletePublisherHandler : IRequestHandler<DeletePublisherCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public DeletePublisherHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.PublisherRepository.DeleteByIdAsync(request.id);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}