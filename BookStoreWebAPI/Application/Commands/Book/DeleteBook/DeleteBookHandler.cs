using Application.Services;
using MediatR;

namespace Application.Commands.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookWriteService _bookService;
        public DeleteBookHandler(IBookWriteService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await _bookService.DeleteByIdAsync(request.Id);
            return Unit.Value;
        }
    }
}