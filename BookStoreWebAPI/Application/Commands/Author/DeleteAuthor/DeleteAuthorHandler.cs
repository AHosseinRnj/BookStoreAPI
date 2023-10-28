using Application.Repositpries;
using MediatR;

namespace Application.Commands.DeleteAuthor
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAuthorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.AuthorRepository.DeleteByIdAsync(request.id);
            _unitOfWork.Commit();
            return Unit.Value;
        }
    }
}