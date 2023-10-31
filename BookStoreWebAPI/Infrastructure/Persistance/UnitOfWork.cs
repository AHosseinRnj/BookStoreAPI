using Application.Repositpries;
using Infrastructure.Persistance.Repositories;
using System.Data;

namespace Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {

        private IDbTransaction _transaction;
        private readonly IDbConnection _connection;

        private readonly DapperContext _dapperContext;
        public UnitOfWork(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
            _connection = _dapperContext.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        private IBookRepository _BookRepository { get; set; }
        private IAuthorRepository _AuthorRepository { get; set; }
        private IPublisherRepository _PublisherRepository { get; set; }
        private ICategoryRepository _CategoryRepository { get; set; }

        public IBookRepository BookRepository
        {
            get
            {
                return _BookRepository ?? (_BookRepository = new BookRepository(_transaction));
            }
        }

        public IAuthorRepository AuthorRepository
        {
            get
            {
                return _AuthorRepository ?? (_AuthorRepository = new AuthorRepository(_transaction));
            }
        }

        public IPublisherRepository PublisherRepository
        {
            get
            {
                return _PublisherRepository ?? (_PublisherRepository = new PublisherRepository(_transaction));
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _CategoryRepository ?? (_CategoryRepository = new CategoryRepository(_transaction));
            }
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
            _BookRepository = null;
            _AuthorRepository = null;
            _PublisherRepository = null;
        }
    }
}