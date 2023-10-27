using Application.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private IGenreRepository _genreRepository;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IBookRepository BookRepository
        {
            get { return _bookRepository ?? (_bookRepository = new BookRepository(_transaction)); }
        }

        public IAuthorRepository AuthorRepository
        {
            get { return _authorRepository ?? (_authorRepository = new AuthorRepository(_transaction)); }
        }

        public IGenreRepository GenreRepository
        {
            get { return _genreRepository ?? (_genreRepository = new GenreRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _bookRepository = null;
            _authorRepository = null;
            _genreRepository = null;
        }
    }
}