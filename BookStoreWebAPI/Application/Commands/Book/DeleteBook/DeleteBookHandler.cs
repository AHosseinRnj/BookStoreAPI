using Application.Services;
using MediatR;

namespace Application.Commands.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookService _bookService;
        public DeleteBookHandler(IBookService bookService)
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