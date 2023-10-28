using Application.Repositpries;
using MediatR;

namespace Application.Commands.UpdateBook
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IUnitOfWork _uniteOfWork;

        public UpdateBookHandler(IUnitOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            await _uniteOfWork.BookRepository.UpdateAsync(request);
            _uniteOfWork.Commit();
            return Unit.Value;
        }
    }
}
