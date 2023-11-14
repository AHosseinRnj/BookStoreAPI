using Application.Commands.CreateBook;
using Application.Commands.UpdateBook;
using Application.Repositories;
using Domain.Entities;
using log4net;

namespace Application.Services
{
    public class BookWriteService : IBookWriteService
    {
        private readonly ILog _logger;
        private readonly IDapperUnitOfWork _unitOfWork;
        private readonly IBookWriteRepository _bookRepository;
        public BookWriteService(IDapperUnitOfWork unitOfWork, IBookWriteRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _logger = LogManager.GetLogger(typeof(BookWriteService));
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