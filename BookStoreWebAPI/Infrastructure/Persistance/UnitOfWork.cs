using Application.Repositpries;

namespace Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DapperContext DapperContext;
        public UnitOfWork(DapperContext dapperContext)
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