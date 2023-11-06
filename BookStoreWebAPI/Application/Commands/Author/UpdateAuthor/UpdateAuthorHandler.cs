using Application.Services;
using MediatR;

namespace Application.Commands.UpdateAuthor
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IAuthorService _authorService;
        public UpdateAuthorHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            await _authorService.UpdateAsync(request);
            return Unit.Value;
        }
    }
}