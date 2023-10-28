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
            Book book = new Book()
            {
                Id = request.Id,
                Title = request.Title,
                ISBN = request.ISBN,
                Price = request.Price,
            };

            await _unitOfWork.BookRepository.AddAsync(book);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}