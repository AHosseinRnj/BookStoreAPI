using Application.Repositpries;
using MediatR;

namespace Application.Commands.CreatePublisher
{
    public class CreatePublisherHandler : IRequestHandler<CreatePublisherCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public CreatePublisherHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.PublisherRepository.AddAsync(request);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}