using Application.Repositpries;
using Infrastructure.Persistance.Repositories;
using System.Data;

namespace Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {

        private IDbTransaction _transaction;
        private readonly DapperContext _dapperContext;
        private readonly IDbConnection _connection;
        private IBookRepository _BookRepository { get; set; }
        public UnitOfWork(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
            _connection = _dapperContext.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();

        }

        public IBookRepository BookRepository { get { return _BookRepository ?? (_BookRepository = new BookRepository(_transaction)); } }

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
        }

    }
}