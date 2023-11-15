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
        private readonly IEFUnitOfWork _unitOfWork;
        public BookWriteService(IEFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(BookWriteService));
        }

        public async Task AddAsync(CreateBookCommand request)
        {
            try
            {
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

                await _unitOfWork.BookRepository.AddAsync(book);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a book: " + ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                await _unitOfWork.BookRepository.DeleteByIdAsync(id);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a book: " + ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateAsync(UpdateBookCommand request)
        {
            try
            {
                var book = new Book
                {
                    Id = request.Id,
                    AuthorId = request.Book.AuthorId,
                    CategoryId = request.Book.CategoryId,
                    PublisherId = request.Book.PublisherId,
                    Quantity = request.Book.Quantity,
                    ISBN = request.Book.ISBN,
                    Price = request.Book.Price,
                    Title = request.Book.Title
                };

                await _unitOfWork.BookRepository.UpdateAsync(book);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a book: " + ex.Message, ex);
                throw;
            }
        }
    }
}