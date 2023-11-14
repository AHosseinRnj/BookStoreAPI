using Application;

namespace Infrastructure.Persistance
{
    public class DapperUnitOfWork : IDapperUnitOfWork
    {
        private readonly DapperContext DapperContext;
        public DapperUnitOfWork(DapperContext dapperContext)
        {
            DapperContext = dapperContext;
        }

        public void BeginTransaction()
        {
            DapperContext.Transaction = DapperContext.Connection.BeginTransaction();
        }

        public void Commit()
        {
            DapperContext.Transaction.Commit();
        }

        public void Rollback()
        {
            DapperContext.Transaction.Rollback();
        }
    }
}