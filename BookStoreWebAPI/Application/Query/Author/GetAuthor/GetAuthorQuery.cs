using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.Author.GetAuthor
{
    public record GetAuthorQuery(int id) : IRequest<GetAuthorQueryResponse>;
}