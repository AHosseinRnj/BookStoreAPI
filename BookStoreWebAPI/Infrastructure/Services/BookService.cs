using Application;
using Application.Commands.CreateBook;
using Application.Commands.UpdateBook;
using Application.Query.GetBook;
using Application.Repositpries;
using Application.Services;
using Azure.Core;
using Domain.Entities;
using log4net;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _logger = LogManager.GetLogger(typeof(BookService));
        }

        public async Task AddAsync(CreateBookCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add a book.");

                var book = new Book
                {
                    Title = request.Title,
                    AuthorId = request.AuthorId,
                    CategoryId = request.CategoryId,
                    Quantity = request.Quantity,
                    ISBN = request.ISBN,
                    Price = request.Price,
                    PublisherId = request.PublisherId,
                };

                await _bookRepository.AddAsync(book);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error adding a book: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Book added successfully.");
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to delete a book by ID: " + id);

                await _bookRepository.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error deleting a book: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Book deleted successfully.");
        }

        public async Task<GetBookQueryResponse> GetBookByIdAsync(int id)
        {
            GetBookQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a book by ID: " + id);

                var book = await _bookRepository.GetBookByIdAsync(id);

                result = new GetBookQueryResponse
                {
                    Title = book.Title,
                    Price = book.Price,
                    ISBN = book.ISBN
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting a book: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Book retrieved successfully.");
            return result;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetBooksAsync()
        {
            List<GetBookQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all books");

                var listOfBooks = await _bookRepository.GetBooksAsync();

                result = listOfBooks.Select(b => new GetBookQueryResponse
                {
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Price = b.Price
                }).ToList();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting Books: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Books retrieved successfully.");
            return result;
        }

        public async Task UpdateAsync(UpdateBookCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to update a book.");

                var book = new Book
                {
                    Id = request.Id,
                    AuthorId = request.Book.AuthorId,
                    CategoryId = request.Book.CategoryId,
                    PublisherId = request.Book.PublisherId,
                    ISBN = request.Book.ISBN,
                    Price = request.Book.Price,
                    Title = request.Book.Title
                };

                await _bookRepository.UpdateAsync(book);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error updating a book: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Book updated successfully.");
        }
    }
}