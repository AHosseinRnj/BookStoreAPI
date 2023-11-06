using Application.Services;
using MediatR;

namespace Application.Commands.DeleteAuthor
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorService _authorService;
        public DeleteAuthorHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            await _authorService.DeleteByIdAsync(request.id);
            return Unit.Value;
        }
    }
}