using Application.Repositpries;
using MediatR;

namespace Application.Commands.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookRepository.DeleteByIdAsync(request.id);
            _unitOfWork.Commit();
            return Unit.Value;
        }
    }
}