using Application.Services;
using MediatR;

namespace Application.Commands.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Unit>
    {
        private readonly IBookService _bookService;
        public CreateBookHandler(IDapperUnitOfWork unitOfWork, IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            await _bookService.AddAsync(request);
            return Unit.Value;
        }
    }
}