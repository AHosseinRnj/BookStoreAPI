using Application.Services;
using MediatR;

namespace Application.Commands.DeleteAuthor
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorWriteService _authorService;
        public DeleteAuthorHandler(IAuthorWriteService authorService)
        {
            _authorService = authorService;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            await _authorService.DeleteByIdAsync(request.Id);
            return Unit.Value;
        }
    }
}