using Application.Commands.CreateAuthor;
using Application.Commands.UpdateAuthor;
using Application.Query.Author.GetAuthor;
using Application.Query.GetBook;
using Application.Repositpries;
using Application.Services;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task AddAsync(CreateAuthorCommand request)
        {
            var author = new Author
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Description = request.Description
            };

            await _authorRepository.AddAsync(author);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _authorRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetAuthorBooksAsync(int id)
        {
            var listOfAuthorBooks = await _authorRepository.GetAuthorBooksAsync(id);

            var result = listOfAuthorBooks.Select(b => new GetBookQueryResponse
            {
                Title = b.Title,
                ISBN = b.ISBN,
                Price = b.Price
            });

            return result;
        }

        public async Task<GetAuthorQueryResponse> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetAuthorById(id);

            var authorReponse = new GetAuthorQueryResponse
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Description = author.Description
            };

            return authorReponse;
        }

        public async Task<IEnumerable<GetAuthorQueryResponse>> GetAuthorsAsync()
        {
            var listOfAuthors = await _authorRepository.GetAuthorsAsync();

            var result = listOfAuthors.Select(a => new GetAuthorQueryResponse
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Description = a.Description
            });

            return result;
        }

        public async Task UpdateAsync(UpdateAuthorCommand request)
        {
            var author = new Author
            {
                Id = request.id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Description = request.Description
            };

            await _authorRepository.UpdateAsync(author);
        }
    }
}