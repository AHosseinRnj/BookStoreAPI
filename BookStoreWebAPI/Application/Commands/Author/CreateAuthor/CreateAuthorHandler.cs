using Application.Services;
using MediatR;

namespace Application.Commands.CreateAuthor
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Unit>
    {
        IAuthorWriteService _authorService;
        public CreateAuthorHandler(IAuthorWriteService authorService)
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