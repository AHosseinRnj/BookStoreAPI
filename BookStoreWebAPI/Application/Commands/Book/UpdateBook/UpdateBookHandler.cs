﻿using Application.Services;
using MediatR;

namespace Application.Commands.UpdateBook
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookService _bookService;
        public UpdateAuthorHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            await _bookService.UpdateAsync(request);
            return Unit.Value;
        }
    }
}