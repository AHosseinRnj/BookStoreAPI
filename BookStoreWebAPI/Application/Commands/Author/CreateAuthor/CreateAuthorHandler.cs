using Application.Services;
using MediatR;

namespace Application.Commands.CreateAuthor
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Unit>
    {
        IAuthorService _authorService;
        public CreateAuthorHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            await _authorService.AddAsync(request);
            return Unit.Value;
        }
    }
}