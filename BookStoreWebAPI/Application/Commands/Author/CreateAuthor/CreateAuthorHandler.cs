using Application.Repositpries;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateAuthor
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAuthorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Description = request.Description,
            };

            await _unitOfWork.AuthorRepository.AddAsync(author);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}