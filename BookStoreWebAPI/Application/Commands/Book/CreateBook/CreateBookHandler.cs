using Application.Repositpries;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookRepository.AddAsync(request);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}