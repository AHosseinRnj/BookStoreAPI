namespace Application
{
    public interface IDapperUnitOfWork
    {
        void BeginTransaction();
        void Rollback();
        void Commit();
    }
}