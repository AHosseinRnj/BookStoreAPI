using Application.Repositpries;
using MediatR;

namespace Application.Commands.UpdateAuthor
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IUnitOfWork _uniteOfWork;

        public UpdateAuthorHandler(IUnitOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            await _uniteOfWork.AuthorRepository.UpdateAsync(request);
            _uniteOfWork.Commit();
            return Unit.Value;
        }
    }
}
