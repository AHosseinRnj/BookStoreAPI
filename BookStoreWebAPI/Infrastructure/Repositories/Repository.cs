using System.Data;

namespace Infrastructure.Repositories
{
    public class Repository
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public Repository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}